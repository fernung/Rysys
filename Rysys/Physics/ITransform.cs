using Microsoft.Xna.Framework;
using Rysys.ECS;

namespace Rysys.Physics
{
    public interface ITransform : IComponent
    {
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }
        float Orientation { get; set; }

        void Move(float x, float y);
        void Move(Vector2 position);
    }

    public class Transform : Component, ITransform
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public float Orientation { get; set; }

        public Transform() : this(Vector2.Zero, Vector2.One) { }
        public Transform(float value) : this(new Vector2(value), Vector2.One) { }
        public Transform(float x, float y) : this(new Vector2(x, y), Vector2.One) { }
        public Transform(Vector2 position, Vector2 scale, float orientation=0.0f) : base() 
        { 
            Position = position;
            Scale = scale;
            Orientation = orientation;
        }

        public void Move(float x, float y) => Move(new Vector2(x, y));
        public void Move(Vector2 position) => Position += position;
    }
}
