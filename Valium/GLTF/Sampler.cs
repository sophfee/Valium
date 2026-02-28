using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Sampler
{
	[JsonProperty("magFilter")] public int MagFilter;
	[JsonProperty("minFilter")] public int MinFilter;
	[JsonProperty("wrapS")] public int WrapS;
	[JsonProperty("wrapT")] public int WrapT;
}