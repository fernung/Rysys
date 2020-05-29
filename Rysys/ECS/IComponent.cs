using Microsoft.Xna.Framework;

namespace Rysys.ECS
{
    public interface IComponent
    {
        IEntity Entity { get; set; }

        void Initialize();
        void Update(GameTime gameTime);

        T GetComponent<T>() where T : IComponent;
        void AddComponent(IComponent component);
        void RemoveComponent(IComponent component);
        bool HasComponent<T>() where T : IComponent;
    }

    public class Component : IComponent
    {
        public IEntity Entity { get; set; }

        public Component() : this(new Entity()) { }
        public Component(IEntity entity) { Entity = entity; }

        public virtual void Initialize() { }
        public virtual void Update(GameTime gameTime) { }

        public T GetComponent<T>() where T : IComponent => Entity.Component<T>();
        public void AddComponent(IComponent component) => Entity.Component(component);
        public void RemoveComponent(IComponent component) => Entity.Remove(component);
        public bool HasComponent<T>() where T : IComponent => GetComponent<T>() != null;
    }
}
