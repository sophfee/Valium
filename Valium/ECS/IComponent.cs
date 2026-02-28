namespace Valium.ECS;

public interface IComponent
{
	public Entity? Entity { get; set; }
	public void Awake();
	public void Sleep();
	public void Update(double deltaTime);
}