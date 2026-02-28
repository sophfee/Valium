using Newtonsoft.Json;

namespace Valium.GLTF;

public struct Buffer()
{
	[JsonProperty("byteLength")] public ulong ByteLength = 0;
	[JsonProperty("uri")] public string Uri = string.Empty;

	private bool bufferDataInitialized = false;
	private byte[] bufferData = [];

	private void InitializeBuffer()
	{
		string uri = Uri;
		if (!File.Exists(uri))
			uri = $"Data/{uri}";
		FileStream fileStream = File.Open(uri, FileMode.Open, FileAccess.Read, FileShare.Read);
		bufferData = new byte[fileStream.Length];
		fileStream.ReadExactly(bufferData, 0, (int)fileStream.Length);
		bufferDataInitialized = true;
	}

	public ReadOnlySpan<byte> Data
	{
		get
		{
			if (bufferDataInitialized) return bufferData.AsSpan();
			InitializeBuffer();
			return bufferData.AsSpan();
		}
	}

	public ref byte this[int index]
	{
		get
		{
			if (!bufferDataInitialized) InitializeBuffer();
			return ref this.bufferData[int.Clamp(index, 0, bufferData.Length)];
		}
	}
}