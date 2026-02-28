using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Node(int[] ch, string nm, float[] rt, float[] tr, float[] sc, int ms, int sk)
{
	[JsonProperty("children")]
	public int[]? Children = ch;
	
	[JsonProperty("name")]
	public string Name = nm;
	
	[JsonProperty("rotation")]
	public float[]? Rotation = rt;
	
	[JsonProperty("translation")]
	public float[]? Translation = tr;
	
	[JsonProperty("scale")]
	public float[]? Scale = sc;
	
	[JsonProperty("mesh")]
	public int Mesh = ms;
	
	[JsonProperty("skin")]
	public int Skin = sk;

	public Node() : this([], string.Empty, [], [0.0f, 0.0f, 0.0f], [], -1, -1)
	{
	}
}