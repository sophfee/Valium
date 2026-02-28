using OpenTK.Mathematics;

namespace Valium.ECS;

public class Transform : IComponent
{
	public Entity? Entity { get; set; } = null!;
	public void Awake() { }
	public void Sleep() { }
	public void Update(double deltaTime) { }
	
	public Vector3 Position { get; set; }
	public Quaternion Rotation { get; set; }
	public Vector3 Scale { get; set; }
	
	public Matrix4 Matrix
	{
		get
		{
			Matrix4 translate = Matrix4.CreateTranslation(Position);
			Matrix4 quaternion = Matrix4.CreateFromQuaternion(Rotation);
			Matrix4 scale = Matrix4.CreateScale(Scale);
			
			if (Entity is null) throw new NullReferenceException();
			if (Entity.Parent is null || !Entity.Parent.HasComponent<Transform>()) return translate * quaternion * scale;
			var trsParent = Entity.Parent?.GetComponent<Transform>()?.Matrix;

			if (trsParent != null) 
				return (translate * quaternion * scale) * trsParent.Value;
			else 
				return translate * quaternion * scale;
		}
	}
}