using Microsoft.Xna.Framework;
using Rysys.ECS;
using Rysys.Extensions;
using Rysys.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rysys.Physics
{

    public interface IKinematics : ITransform
    {
        float Friction { get; set; }
        float Speed { get; set; }
        Vector2 Acceleration { get; set; }
        Vector2 Velocity { get; set; }

        void AccelerateX(float x);
        void AccelerateY(float y);
        void Accelerate(float x, float y);

        void DecelerateX(float x);
        void DecelerateY(float y);
        void Decelerate(float x, float y);
    }

    public class Kinematics : Transform, IKinematics
    {
        private const float DefaultFriction = 0.88f;
        private const float DefaultSpeed = 3.0f;
        private const float DefaultMaxSpeed = 8.0f;
        private const float DefaultMinSpeed = 0.0001f;
        
        public float Friction { get; set; }
        public float Speed { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 Velocity { get; set; }


        public Kinematics() : this(Vector2.Zero, DefaultSpeed) { }
        public Kinematics(Vector2 position, float speed) : base(position, Vector2.One)
        {
            Friction = DefaultFriction;
            Speed = speed;
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            Velocity += Acceleration;
            Velocity = Vector2.Clamp(Velocity, new Vector2(-DefaultMaxSpeed), new Vector2(DefaultMaxSpeed));
            Position += Velocity;

            float length = Velocity.LengthSquared();
            if (length > 0) Orientation = Velocity.ToAngle();
            if (length < DefaultMinSpeed * DefaultMinSpeed) Velocity = Vector2.Zero;

            Velocity *= Friction;
            Acceleration = Vector2.Zero;
            base.Update(gameTime);
        }


        public void AccelerateX(float x) => Accelerate(x, 0);
        public void AccelerateY(float y) => Accelerate(0, y);
        public void Accelerate(float x, float y) => Acceleration = new Vector2(x, y);

        public void DecelerateX(float x) => Decelerate(x, 0);
        public void DecelerateY(float y) => Decelerate(0, y);
        public void Decelerate(float x, float y) => Acceleration = new Vector2(-x, -y);

    }
}
