using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace Valium.GPU;

/// <summary>
/// Drives display functionality
/// </summary>
public static class Display
{
	public static void ThrowIfErrors()
	{
		ErrorCode errorCode = GL.GetError();
		StringBuilder @string = new();
		while (errorCode != ErrorCode.NoError)
		{
			@string.AppendLine($"GL Error: {Enum.GetName(errorCode)}");
			errorCode = GL.GetError();
		}

		if (@string.Length == 0)
			return;
		
		throw new Exception(@string.ToString());
	}
}