using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Valium.ECS;
using Valium.GLTF;
using static OpenTK.Graphics.OpenGL4.GL;
using static OpenTK.Graphics.OpenGL4.ObjectLabelIdentifier;
using Buffer = Valium.GLTF.Buffer;

namespace Valium.GPU;

public class Model : IComponent, IDisposable
{
	public Entity? 
		Entity { get; set; }

	
	protected uint[]
		vertexArrays = [];
	
	protected int[]
		elementCounts = [];

	protected uint[]
		buffers = [];

	private void ApplyMesh(ref Data data, int meshId)
	{
		ref Mesh meshData = ref data.Meshes[meshId];
		// buffers involved in this vao
		this.vertexArrays = new uint[meshData.Primitives.Length];
		this.elementCounts = new int[meshData.Primitives.Length];
		CreateVertexArrays(vertexArrays.Length,
			vertexArrays);
		int nBuffers = meshData.Primitives.Sum(primitive => primitive.Attributes.Count);
		this.buffers = new uint[nBuffers];
		CreateBuffers(this.buffers.Length,
			this.buffers);
		
		int iVertexArrays = 0, iBuffers = 0;
		
		foreach (Primitive primitive in meshData.Primitives)
		{
			uint vao = this.vertexArrays[iVertexArrays];
			if (primitive.Name != null)
				ObjectLabel(
					identifier: VertexArray,
					name: vao,
					length: primitive.Name.Length,
					label: primitive.Name ?? iVertexArrays.ToString());
			BindVertexArray(array: vao);
			uint iAttrib = 0u;
			foreach (KeyValuePair<string, int> attrib in primitive.Attributes)
			{
				uint attribIndex = iAttrib++;
				ref Accessor accessor = ref data.Accessors[attrib.Value];
				ref BufferView bufferView = ref data.BufferViews[attrib.Value];
				ref Buffer buffer = ref data.Buffers[bufferView.Buffer];
				
				VertexArrayAttribFormat(vaobj: vao,
					attribindex: attribIndex,
					size: accessor.NumberOfComponents,
					type: VertexAttribType.Float,
					normalized: false,
					relativeoffset: 0);

				uint bufferId = (uint)iBuffers++;
				
				NamedBufferStorage(buffers[bufferId], bufferView.Length, ref buffer[bufferView.Offset], BufferStorageFlags.ClientStorageBit);
				NamedBufferData(buffers[bufferId],  bufferView.Length, ref buffer[bufferView.Offset], BufferUsageHint.StaticDraw);

				if (bufferView.Target is not null)
				{
					switch (bufferView.Target)
					{
						case 34962: // Vertex Attribute
							VertexArrayVertexBuffer(vao, attribIndex, buffers[attribIndex], bufferView.Offset, 0);
							VertexArrayAttribBinding(vao, attribIndex, attribIndex);
							EnableVertexArrayAttrib(vao, attribIndex);
							break;
						case 34963:
							VertexArrayElementBuffer(vao, buffers[bufferId]);
							elementCounts[iVertexArrays] = accessor.Count;
							break;
					}
				}
			}

			iVertexArrays++;
		}
		
		BindVertexArray(0);
	}

	private static Entity CreateEntityFromNode(Entity? entity, ref Data data, ref Node node)
	{
		Entity myEntity = new()
		{
			Name = node.Name,
			Parent = entity
		};
		
		if (node.Translation is not null && node.Rotation is not null && node.Scale is not null) {
			if (node.Translation.Length >= 3
			    && node.Rotation.Length >= 4
			    && node.Scale.Length >= 3)
			{
				Transform transform = myEntity.AddComponent<Transform>();
				transform.Position = new Vector3(
					node.Translation[0],
					node.Translation[1],
					node.Translation[2]
				);
				transform.Rotation = new Quaternion(
					node.Rotation[0],
					node.Rotation[1],
					node.Rotation[2],
					node.Rotation[3]
				);
				transform.Scale = new Vector3(
					node.Scale[0],
					node.Scale[1],
					node.Scale[2]
				);
			}
		}

		if (node.Children is not null)
		{
			foreach (int nodeID in node.Children)
			{
				ref Node child = ref data.Nodes[nodeID];
				myEntity.AddChild(CreateEntityFromNode(myEntity, ref data, ref child));
			}
		}

		if (node.Mesh == -1) return myEntity;
		Model mdl = myEntity.AddComponent<Model>();
		mdl.ApplyMesh(ref data, node.Mesh);

		return myEntity;
	}
	
	public static Entity LoadFromGLTF(ref Data data)
	{
		ref Scene scene = ref data.Scenes[data.Scene];

		if (scene.Nodes.Length == 1)
		{
			ref Node node = ref data.Nodes[scene.Nodes[0]];
			return CreateEntityFromNode(
				null,
				ref data,
				ref node
			);
		}
		else
		{
			Entity root = new()
			{
				Name = scene.Name
			};
			foreach (int nodeId in scene.Nodes) 
				root.AddChild(
					CreateEntityFromNode(
						null,
						ref data, 
						ref data.Nodes[nodeId]
					)
				);
			return root;
		}
	}

	public void Awake()
	{
	}

	public void Sleep()
	{
	}

	public void Update(double deltaTime)
	{
		for (int i = 0; i < this.vertexArrays.Length; i++)
		{
			uint vertexArray = this.vertexArrays[i];
			int elementCount = this.elementCounts[i];
			BindVertexArray(vertexArray);

			if (Entity?.GetComponent<Transform>() is Transform transform)
			{
				Matrix4 transformMatrix = transform.Matrix;
				UniformMatrix4(0, false, ref transformMatrix);
			}
			
			DrawElements(PrimitiveType.Triangles, elementCount, DrawElementsType.UnsignedShort, 0);
		}
	}

	public void Dispose()
	{
		DeleteBuffers(buffers.Length, buffers);
		DeleteVertexArrays(vertexArrays.Length, vertexArrays);
		GC.SuppressFinalize(this);
	}
}