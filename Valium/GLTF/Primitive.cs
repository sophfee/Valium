using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Primitive
{
	[JsonProperty("name")] public string? Name;
	[JsonProperty("attributes")] public Dictionary<string, int> Attributes;
	[JsonProperty("indices")] public int Indices;
	[JsonProperty("materials")] public int Materials;
}