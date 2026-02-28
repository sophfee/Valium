using Newtonsoft.Json;

namespace Valium.GLTF;

[JsonObject]
public struct Data
{
	public string FilePath;
	
	[JsonProperty("scene")]
	public int Scene;
	
	[JsonProperty("scenes")]
	public Scene[] Scenes;
	
	[JsonProperty("nodes")]
	public Node[] Nodes;
	
	[JsonProperty("materials")]
	public Material[] Materials;
	
	[JsonProperty("meshes")]
	public Mesh[] Meshes;
	
	[JsonProperty("textures")]
	public Texture[] Textures;
	
	[JsonProperty("images")]
	public Image[] Images;
	
	[JsonProperty("Skins")]
	public Skin[] Skins;
	
	[JsonProperty("accessors")]
	public Accessor[] Accessors;
	
	[JsonProperty("bufferViews")]
	public BufferView[] BufferViews;
	
	[JsonProperty("samplers")]
	public Sampler[] Samplers;
	
	[JsonProperty("buffers")]
	public Buffer[] Buffers;
}