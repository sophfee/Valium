using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Scene
{
	[JsonProperty("name")]
	public string Name;
	
	[JsonProperty("nodes")]
	public int[] Nodes;
}