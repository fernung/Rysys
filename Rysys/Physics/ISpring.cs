using Microsoft.Xna.Framework;
using Rysys.ECS;

namespace Rysys.Physics
{
    public interface ISpring : IComponent
    {
        IPointMass End1 { get; set; }
        IPointMass End2 { get; set; }
        float TargetLength { get; set; }
        float Stiffness { get; set; }
        float Damping { get; set; }
    }

    public class Spring : Component, ISpring
    {
        public IPointMass End1 { get; set; }
        public IPointMass End2 { get; set; }
        public float TargetLength { get; set; }
        public float Stiffness { get; set; }
        public float Damping { get; set; }

        public Spring(IPointMass end1, IPointMass end2, float stiffness, float damping) : base()
        {
            End1 = end1;
            End2 = end2;
            Stiffness = stiffness;
            Damping = damping;
            TargetLength = Vector3.Distance(end1.Position, end2.Position) * 0.95f;
        }

        public override void Update(GameTime gameTime)
        {
            var x = End1.Position - End2.Position;
            float length = x.Length();
            if (length <= TargetLength) return;

            x = (x / length) * (length - TargetLength);
            var dv = End2.Velocity - End1.Velocity;
            var force = Stiffness * x - dv * Damping;

            End1.ApplyForce(-force);
            End2.ApplyForce(force);
        }
    }
}
