using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Mesh
{
	[JsonProperty("name")]  public string Name;
	[JsonProperty("primitives")] public Primitive[] Primitives;
}