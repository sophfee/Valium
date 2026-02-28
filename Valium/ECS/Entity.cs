using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Valium.ECS;

public class Entity
{
	protected readonly List<IComponent> 
		components = [];
	
	public ReadOnlyCollection<IComponent>
		Components => components.AsReadOnly();

	protected readonly List<Entity>
		children = [];
	
	public ReadOnlyCollection<Entity>
		Children => children.AsReadOnly(); 
	
#nullable enable
	
	public Entity?
		Parent { get; set; }

	public string
		Name { get; set; } = "Entity"; // Anyone can change because like... why not.

#nullable restore

	public T AddComponent<T>(bool wakeOnCreation = true) where T : IComponent, new()
	{
		T component = new()
		{
			Entity = this
		};
		components.Add(component);
		if (wakeOnCreation)
			component.Awake();
		return component;
	}

	public T? GetComponent<T>() where T : class, IComponent, new()
	{
		if (components.OfType<T>().Any())
			return components
				.OfType<T>()
				.First();
		return null as T;
	}
	
	public bool HasComponent<T>() where T : class, IComponent, new()
		=> components.OfType<T>().Any();

	public void RemoveComponent<T>() where T : class, IComponent, new()
	{
		T? component = GetComponent<T>();
		if (component == null) throw new NullReferenceException();
		IDisposable? disposable = component as IDisposable;
		disposable?.Dispose();
		components.Remove(item: component);
	}
	
	public void AddChild(Entity entity) 
		=> children.Add(entity);
	
	public void RemoveChild(Entity entity) 
		=> children.Remove(entity);
	
	public void RemoveAllChildren() 
		=> children.Clear();

	public void Update(double deltaTime)
	{
		foreach (IComponent component in components) 
			component.Update(deltaTime);
		
		foreach (Entity entity in children) 
			entity.Update(deltaTime);
	}
}