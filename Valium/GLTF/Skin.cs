using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Skin
{
	[JsonProperty("name")] public string Name;
	[JsonProperty("joints")] public int[] Joints;
	[JsonProperty("inverseBindMatrices")] public int InverseBindMatrices;
}