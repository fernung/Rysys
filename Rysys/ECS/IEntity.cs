using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Rysys.ECS
{
    public interface IEntity
    {
        List<IComponent> Components { get; }
        T Component<T>() where T : IComponent;
        void Component(IComponent component);
        void Remove(IComponent component);

        void Update(GameTime gameTime);
    }

    public class Entity : IEntity
    {
        public List<IComponent> Components { get; protected set; }

        public Entity() : this(new List<IComponent>()) { }
        public Entity(List<IComponent> components)
        {
            Components = components;
        }

        public T Component<T>() where T : IComponent
        {
            foreach (IComponent c in Components)
                if (c.GetType().Equals(typeof(T))) return (T)c;
            return default(T);
        }
        public void Component(IComponent component)
        {
            Components.Add(component);
            component.Entity = this;
            component.Initialize();
        }
        public void Remove(IComponent component)
        {
            component.Entity = null;
            Components.Remove(component);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IComponent c in Components)
                c.Update(gameTime);
        }
    }
}
