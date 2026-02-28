using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Texture
{
	[JsonProperty("sampler")]
	public int Sampler;
	
	[JsonProperty("source")]
	public int Source;
}