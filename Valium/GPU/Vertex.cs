using System.Runtime.InteropServices;
using OpenTK.Mathematics;

namespace Valium.GPU;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public struct Vertex
{
	public Vector3 Position;
	public Vector3 Normal;
	public Vector2 UV0;
}