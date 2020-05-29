using Microsoft.Xna.Framework;
using System;

namespace Rysys.Extensions
{
    public static class Vector2Extensions
    {
        public static float ToAngle(this Vector2 vector) => (float)Math.Atan2(vector.Y, vector.X);
        public static Vector2 ScaleTo(this Vector2 vector, float length) => vector * (length / vector.Length());
        public static Point ToPoint(this Vector2 vector) => new Point((int)vector.X, (int)vector.Y);
    }
}
