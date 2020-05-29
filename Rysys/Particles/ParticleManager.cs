using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.ECS;
using Rysys.Extensions;
using Rysys.Graphics;
using Rysys.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rysys.Particles
{
    public interface IParticleManager : IComponent
    {
        Action<Particle> UpdateMethod { get; set; }
        ParticleContainer Particles { get; set; }

        void Create(Texture2D texture, Vector2 position, Color tint, float duration, Vector2 scale, ParticleState state, float theta);
        void Create(Texture2D texture, Vector2 position, Color tint, float duration, int amount, ParticleType type, float theta);
        void Draw(SpriteBatch spriteBatch);
    }
    public class ParticleManager : Component, IParticleManager
    {
        private const int DefaultParticleAmount = 1000;
        private static readonly Random Random = new Random();

        public Action<Particle> UpdateMethod { get; set; }
        public ParticleContainer Particles { get; set; }

        public ParticleManager(int capacity, Action<Particle> updateMethod) 
        {
            UpdateMethod = updateMethod;
            Particles = new ParticleContainer(capacity);

            for (int i = 0; i < capacity; i++)
            {
                Particles[i] = new Particle();
            }
        }


        public void Create(Texture2D texture, Vector2 position, Color tint, float duration, Vector2 scale, ParticleState state, float theta=0.0f)
        {
            Particle p;
            if(Particles.Count == Particles.Capacity)
            {
                p = Particles[0];
                Particles.Start++;
            }
            else
            {
                p = Particles[Particles.Count];
                Particles.Count++;
            }

            p.Set(texture, position, tint, duration, scale, state, theta);
        }
        public void Create(Texture2D texture, Vector2 position, Color tint, float duration, int amount = DefaultParticleAmount, ParticleType type = ParticleType.None, float theta = 0.0f)
        {
            for (int i = 0; i < amount; i++)
            {
                Create(texture, position, tint, duration, Vector2.One, new ParticleState
                {
                    Velocity = Random.NextVector2(0, 10),
                    Type = type,
                    LengthMultiplier = 1.0f
                }, theta);
            }
        }

        public override void Update(GameTime gameTime)
        {
            int removal = 0;
            for (int i = 0; i < Particles.Count; i++) 
            {
                var p = Particles[i];
                UpdateMethod(p);
                p.LifeTime -= 1.0f / p.Duration;

                ParticleContainer.Swap(Particles, i - removal, i);
                if (p.LifeTime < 0) removal++;
            }
            Particles.Count -= removal;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin;
            for (int i = 0; i < Particles.Count; i++)
            {
                var p = Particles[i];
                origin = new Vector2(p.Texture.Width / 2, p.Texture.Height / 2);
                spriteBatch.Draw(p.Texture, p.Position, null, p.Tint, p.Orientation, origin, p.Scale, 0, 0);
            }
        }
    }
}
