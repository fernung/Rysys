using Microsoft.Xna.Framework;
using Rysys.ECS;

namespace Rysys.Physics
{
    public interface ITransform : IComponent
    {
        Vector2 Position { get; set; }
        float Orientation { get; set; }
        float Scale { get; set; }

        void Move(float x, float y);
        void Move(Vector2 position);
    }

    public class Transform : Component, ITransform
    {
        public Vector2 Position { get; set; }
        public float Orientation { get; set; }
        public float Scale { get; set; }

        public Transform() : this(Vector2.Zero) { }
        public Transform(float value) : this(new Vector2(value)) { }
        public Transform(float x, float y) : this(new Vector2(x, y)) { }
        public Transform(Vector2 position, float orientation=0.0f, float scale=1.0f) : base() 
        { 
            Position = position;
            Orientation = orientation;
            Scale = scale;
        }

        public void Move(float x, float y) => Move(new Vector2(x, y));
        public void Move(Vector2 position) => Position += position;
    }
}
