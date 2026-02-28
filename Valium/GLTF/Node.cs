using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Node
{
	[JsonProperty("children")]
	public int[]? Children;
	
	[JsonProperty("name")]
	public string? Name;
	
	[JsonProperty("rotation")]
	public float[]? Rotation;
	
	[JsonProperty("translation")]
	public float[]? Translation;
	
	[JsonProperty("scale")]
	public float[]? Scale;
	
	[JsonProperty("mesh")]
	public int? Mesh;
	
	[JsonProperty("skin")]
	public int? Skin;
}