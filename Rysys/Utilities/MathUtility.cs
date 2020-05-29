using Microsoft.Xna.Framework;
using System;

namespace Rysys.Utilities
{
    public static class MathUtility
    {
        public static Vector2 FromPolar(float angle, float magnitude) => magnitude * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
    }
}
