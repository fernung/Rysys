using Microsoft.Xna.Framework;
using Rysys.Extensions;
using System;

namespace Rysys.Particles
{
    public struct ParticleState
    {
        public static ParticleState Empty => new ParticleState(Vector2.Zero, ParticleType.None, 1.0f);
        private const float DefaultMinVelocity = 0.00000000001f;
        private static readonly Random Random = new Random();

        public Vector2 Velocity { get; set; }
        public ParticleType Type { get; set; }
        public float LengthMultiplier { get; set; }

        public ParticleState(Vector2 velocity, ParticleType type, float lengthMultiplier = 1.0f)
        {
            Velocity = velocity;
            Type = type;
            LengthMultiplier = lengthMultiplier;
        }

        public static ParticleState GetRandom(float minVelocity, float maxVelocity) => 
            new ParticleState(Random.NextVector2(minVelocity, maxVelocity), ParticleType.None, 1);

        public static void Update(IParticle p)
        {
            Vector2 velocity = p.State.Velocity;
            Vector2 position, size = Settings.WorldSize;
            float speed, alpha;

            p.Position += velocity;
            p.Orientation = velocity.ToAngle();

            speed = velocity.Length();
            alpha = Math.Min(1.0f, Math.Min(p.LifeTime * 2.0f, speed * 1.0f));
            alpha *= alpha;

            p.Tint = new Color(p.Tint, (byte)(255 * alpha));
            p.Scale = p.State.Type == ParticleType.Bullet ? 
                      new Vector2(p.State.LengthMultiplier * Math.Min(Math.Min(1.0f, (0.1f * speed) + 1.0f), alpha), p.Scale.Y) :
                      new Vector2(p.State.LengthMultiplier * Math.Min(Math.Min(1.0f, (0.2f * speed) + 1.0f), alpha), p.Scale.Y);

            position = p.Position;
            if (position.X < 0) velocity.X = Math.Abs(velocity.X);
            else if (position.X > size.X) velocity.X = -Math.Abs(velocity.X);
            if (position.Y < 0) velocity.Y = Math.Abs(velocity.Y);
            else if (position.Y > size.Y) velocity.Y = -Math.Abs(velocity.Y);

            if (Math.Abs(velocity.X) + Math.Abs(velocity.Y) < DefaultMinVelocity) velocity = Vector2.Zero;
            else if (p.State.Type == ParticleType.Enemy) velocity *= 0.94f;
            else velocity *= 0.96f + Math.Abs(position.X) % 0.04f;

            p.State = new ParticleState(velocity, p.State.Type, p.State.LengthMultiplier);
        }
    }
}
