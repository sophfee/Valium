using Newtonsoft.Json;

namespace Valium.GLTF;

public struct BufferView
{
	[JsonProperty("buffer")] public int Buffer;
	[JsonProperty("bufferLength")] public int Length;
	[JsonProperty("bufferOffset")] public int Offset;
	[JsonProperty("target")] public int? Target;
}