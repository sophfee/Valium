using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Valium.ECS;
using Valium.GLTF;
using Valium.GPU;
using static OpenTK.Mathematics.Matrix4;

namespace Valium;

public static class Application
{
	private static void Main(string[] args)
	{
		StreamReader file = File.OpenText("Data/silver.gltf");
		string data = file.ReadToEnd();
		Data gltf = JsonConvert.DeserializeObject<Data>(data);
		gltf.FilePath = "Data/silver.gltf";
		
		NativeWindowSettings windowSettings = new()
		{
			ClientSize = new Vector2i(1280, 720),
			Profile = ContextProfile.Core,
			API = ContextAPI.OpenGL,
			APIVersion = new Version(4, 6),
			AutoLoadBindings = true,
			IsEventDriven = false,
			//Flags = ContextFlags.ForwardCompatible | ContextFlags.Debug,
			Title = "I <3 VALIUM",
			StartVisible = false
		};
		//NativeWindow nativeWindow = new(windowSettings);
		GameWindowSettings gameWindowSettings = new()
		{
			UpdateFrequency = 120.0
		};

		GameWindow gameWindow = new(gameWindowSettings, windowSettings);
		gameWindow.MakeCurrent();

		Entity rootEntity = Model.LoadFromGLTF(ref gltf);
		StandardShaders.InitializeShaders();
		
		gameWindow.IsVisible = true;
		
		StandardShaders.UseDefaultShaderProgram();
		ulong ticks = 0;

		GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);

		double time = 0.00;

		gameWindow.UpdateFrame += (FrameEventArgs args) =>
		{
			time += args.Time;
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

			gameWindow.ProcessEvents(0.0);

			Matrix4 model = CreateTranslation(Vector3.Zero);

			Matrix4 projection =
				CreatePerspectiveFieldOfView(float.DegreesToRadians(70.0f), 16.0f / 9.0f, 0.015f, 128.0f);

			Matrix4 view =
				LookAt(new Vector3(MathF.Sin((float)time * 4.0f) * 2.0f, 0.0f, MathF.Cos((float)time * 4.0f) * 2.0f),
					Vector3.Zero,
					Vector3.UnitY);

			GL.UniformMatrix4(0, false, ref model);
			GL.UniformMatrix4(1, false, ref view);
			GL.UniformMatrix4(2, false, ref projection);
			rootEntity.Update(deltaTime: 0.0);

			gameWindow.SwapBuffers();
			
			ticks++;
		};

		gameWindow.Run();
		
		rootEntity.RemoveAllChildren();
		StandardShaders.DisposeShaders();
		gameWindow.Dispose();
		
		Thread.Sleep(TimeSpan.FromSeconds(5));
	}
}