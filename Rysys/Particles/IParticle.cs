using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.Physics;

namespace Rysys.Particles
{
    public interface IParticle : ITransform
    {
        Texture2D Texture { get; set; }
        Color Tint { get; set; }
        float Duration { get; set; }
        float LifeTime { get; set; }
        ParticleState State { get; set; }

        void Set(Texture2D texture, Vector2 position, Color tint, float duration, Vector2 scale, ParticleState state, float theta = 0);
    }

    public class Particle : Transform, IParticle
    {
        public Texture2D Texture { get; set; }
        public Color Tint { get; set; }
        public float Duration { get; set; }
        public float LifeTime { get; set; }
        public ParticleState State { get; set; }

        public Particle() : this(null, Vector2.Zero, Color.White, 1.0f, Vector2.One, ParticleState.Empty) { }
        public Particle(Texture2D texture, Vector2 position, Color tint, float duration, Vector2 scale, ParticleState state, float theta=0) : base(position, scale, theta)
        {
            Texture = texture;
            Tint = tint;
            Duration = duration;
            LifeTime = 1.0f;
            State = state;
        }

        public void Set(Texture2D texture, Vector2 position, Color tint, float duration, Vector2 scale, ParticleState state, float theta = 0)
        {
            Texture = texture;
            Position = position;
            Tint = tint;
            Duration = duration;
            Scale = scale;
            LifeTime = 1.0f;
            State = state;
            Orientation = theta;
        }
    }
}
