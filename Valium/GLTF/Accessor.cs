using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Valium.GLTF;

[JsonObject]
public struct Accessor
{
	[JsonProperty("bufferView")] public int BufferView;

	public enum AccessorComponentType
	{
		SignedByte = 5120,
		UnsignedByte = 5121,
		SignedShort = 5122,
		UnsignedShort = 5123,
		SignedInt = 5124,
		UnsignedInt = 5125,
		Float = 5126
	}

	[JsonConverter(typeof(StringEnumConverter))]
	[JsonProperty("componentType")] public AccessorComponentType ComponentType;
	[JsonProperty("count")] public int Count;

	public enum AccessorType
	{
		SCALAR,
		VEC2,
		VEC3,
		VEC4,
		MAT2,
		MAT3,
		MAT4,
	}

	[JsonConverter(typeof(StringEnumConverter))]
	[JsonProperty("type")] public AccessorType TypeOf;

	public int NumberOfComponents =>
		this.TypeOf switch
		{
			AccessorType.SCALAR => 1,
			AccessorType.VEC2 => 2,
			AccessorType.VEC3 => 3,
			AccessorType.VEC4 => 4,
			AccessorType.MAT2 => 4,
			AccessorType.MAT3 => 9,
			AccessorType.MAT4 => 16,
			_ => throw new ArgumentOutOfRangeException()
		};

	public int ComponentTypeSize =>
		this.ComponentType switch
		{
			AccessorComponentType.SignedByte => sizeof(sbyte),
			AccessorComponentType.UnsignedByte => sizeof(byte),
			AccessorComponentType.SignedShort => sizeof(short),
			AccessorComponentType.UnsignedShort => sizeof(ushort),
			AccessorComponentType.SignedInt => sizeof(int),
			AccessorComponentType.UnsignedInt => sizeof(uint),
			AccessorComponentType.Float => sizeof(float),
			_ => throw new ArgumentOutOfRangeException()
		};

	public int Stride => ComponentTypeSize * NumberOfComponents;
	public int Size => Stride * Count;
}