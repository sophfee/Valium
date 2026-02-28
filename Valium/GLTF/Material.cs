using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Material
{
	[JsonProperty("name")] public string Name;
	[JsonProperty("emissiveTexture")] public TextureReference Emissive;
	[JsonProperty("normalTexture")] public TextureReference Normal;
	[JsonProperty("occlusionTexture")] public TextureReference Occlusion;

	public struct PbrMetallicRoughness
	{
		[JsonProperty("baseColorTexture")] public TextureReference BaseColor;
		[JsonProperty("metallicRoughnessTexture")] public TextureReference MetallicRoughness;
	}
}