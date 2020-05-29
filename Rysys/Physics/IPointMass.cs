using Microsoft.Xna.Framework;
using Rysys.ECS;

namespace Rysys.Physics
{
    public interface IPointMass : IComponent
    {
        Vector3 Position { get; set; }
        Vector3 Velocity { get; set; }
        Vector3 Acceleration { get; }
        float InverseMass { get; set; }
        float Damping { get; }

        void ApplyForce(Vector3 force);
        void IncreaseDamping(float factor);
    }

    public class PointMass : Component, IPointMass
    {
        private const float DefaultDamping = 0.98f;
        private const float DefaultMinSpeed = 0.001f;

        public Vector3 Position { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector3 Acceleration { get; protected set; }
        public float InverseMass { get; set; }
        public float Damping { get; protected set; }

        public PointMass(Vector3 position, float inverseMass)
        {

            Position = position;
            Velocity = Vector3.Zero;
            Acceleration = Vector3.Zero;
            InverseMass = inverseMass;
            Damping = DefaultDamping;
        }

        public void ApplyForce(Vector3 force) => Acceleration += force * InverseMass;
        public void IncreaseDamping(float factor) => Damping *= factor;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Velocity += Acceleration;
            Position += Velocity;
            Acceleration = Vector3.Zero;

            if (Velocity.LengthSquared() < DefaultMinSpeed * DefaultMinSpeed) 
                Velocity = Vector3.Zero;

            Velocity *= Damping;
            Damping = DefaultDamping;
        }
    }
}
