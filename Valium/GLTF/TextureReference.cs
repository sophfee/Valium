using Newtonsoft.Json;

namespace Valium.GLTF;

public readonly struct TextureReference(int index)
{
	[JsonProperty("index")] public int Index { get; } = index;
}