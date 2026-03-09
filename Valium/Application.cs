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
			Profile = ContextProfile.Core,
			API = ContextAPI.OpenGL,
			APIVersion = new Version(4, 6),
			AutoLoadBindings = true
		};
		GameWindow nativeWindow = new(GameWindowSettings.Default, windowSettings);
		nativeWindow.Context.MakeCurrent();
		nativeWindow.Title = "I love valium";
		nativeWindow.IsVisible = false;

		Entity rootEntity = Model.LoadFromGLTF(ref gltf);
		StandardShaders.InitializeShaders();
		Display.ThrowIfErrors();
		
		nativeWindow.IsVisible = true;
		
		StandardShaders.UseDefaultShaderProgram();
		Display.ThrowIfErrors();

		Matrix4 projection =
			CreatePerspectiveFieldOfView(float.DegreesToRadians(5.0f), 16.0f / 9.0f, 0.05f, 1024.0f);
		Matrix4 view = 
			LookAt(new Vector3(0.0f, 1.0f, 2.0f),
				Vector3.Zero, 
				Vector3.UnitY);
		
		GL.UniformMatrix4(1, false, ref view);
		GL.UniformMatrix4(2, false, ref projection);
		Display.ThrowIfErrors();

		while (true)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
			nativeWindow.ProcessEvents(20.0);
			rootEntity.Update(deltaTime: 0.0);
			nativeWindow.Context.SwapBuffers();
		
			
		}
		
		rootEntity.RemoveAllChildren();
		StandardShaders.DisposeShaders();
		nativeWindow.Dispose();
		
		Thread.Sleep(TimeSpan.FromSeconds(5));
	}
}