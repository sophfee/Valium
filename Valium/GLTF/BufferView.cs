using Newtonsoft.Json;

namespace Valium.GLTF;

public struct BufferView
{
	[JsonProperty("buffer")] public int Buffer;
	[JsonProperty("byteLength")] public int Length;
	[JsonProperty("byteOffset")] public int Offset;
	[JsonProperty("target")] public int? Target;
}