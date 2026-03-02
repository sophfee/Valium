using OpenTK.Graphics.OpenGL4;
using static OpenTK.Graphics.OpenGL4.GL;

namespace Valium.GPU;

public static class StandardShaders
{
	struct VertexFragmentShaderProgram
	{
		public int ProgramObject;
		public int VertexShader;
		public int FragmentShader;
	}

	private static VertexFragmentShaderProgram DefaultShader; 

	public static void InitializeShaders()
	{
		DefaultShader = new VertexFragmentShaderProgram
		{
			ProgramObject = CreateProgram(),
			VertexShader = CreateShader(ShaderType.VertexShader),
			FragmentShader = CreateShader(ShaderType.FragmentShader)
		};
		
		ShaderSourceFromFile(DefaultShader.VertexShader, "Shaders/Default.vert");
		CompileAndValidateShader(DefaultShader.VertexShader);
		AttachShader(DefaultShader.ProgramObject, DefaultShader.VertexShader);
		
		ShaderSourceFromFile(DefaultShader.FragmentShader, "Shaders/Default.frag");
		CompileAndValidateShader(DefaultShader.FragmentShader);
		AttachShader(DefaultShader.ProgramObject, DefaultShader.FragmentShader);
		
		LinkProgram(DefaultShader.ProgramObject);
		ValidateProgram(DefaultShader.ProgramObject);
	}

	public static void UseDefaultShaderProgram() => UseProgram(DefaultShader.ProgramObject);

	internal static void ValidateShader(int shader)
	{
		GetShader(shader, ShaderParameter.CompileStatus, out int compileStatus);
		Console.WriteLine($"Shader[{shader}] compile status: {compileStatus}");

		//if (compileStatus == 1) return;
		string shaderInfoLog = GetShaderInfoLog(shader);
		Console.WriteLine("---------------------SHADER COMPILER ERROR---------------------");
		Console.WriteLine(shaderInfoLog);
		Console.WriteLine("---------------------------------------------------------------");
	}

	internal static void ValidateProgram(int program)
	{
		GetProgram(program, GetProgramParameterName.LinkStatus, out int linkStatus);
		Console.WriteLine($"Program[{program}] link status: {linkStatus}");
		//if (linkStatus == 1) return;
		string programInfoLog = GetProgramInfoLog(program);
		Console.WriteLine("---------------------PROGRAM LINKER ERROR---------------------");
		Console.WriteLine(programInfoLog);
		Console.WriteLine("--------------------------------------------------------------");
	}

	internal static void CompileAndValidateShader(int shader)
	{
		CompileShader(shader);
		ValidateShader(shader);
	}
		
	internal static void ShaderSourceFromFile(int shader, string path)
	{
		if (!File.Exists(path))
			throw new FileNotFoundException();

		string source = File.ReadAllText(path);
		Console.WriteLine($"Shader source from {path}:\n{source}");
		ShaderSource(shader, 1, [source], [source.Length]);
	}

	public static void DisposeShaders()
	{
		DeleteShader(DefaultShader.VertexShader);
		DeleteShader(DefaultShader.FragmentShader);
		DeleteProgram(DefaultShader.ProgramObject);
	}
}