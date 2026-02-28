using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Image
{
	[JsonProperty("mimeType")]
	public string MimeType;
	
	[JsonProperty("name")]
	public string Name;
	
	[JsonProperty("uri")]
	public string Uri;
}