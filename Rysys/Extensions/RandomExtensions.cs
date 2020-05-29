using Microsoft.Xna.Framework;
using System;
namespace Rysys.Extensions
{
    public static class RandomExtensions
    {
        public static float NextFloat(this Random random, float minValue, float maxValue) => (float)random.NextDouble() * (maxValue - minValue) + minValue;
        public static Vector2 NextVector2(this Random random, float minLength, float maxLength)
        {
            double theta = random.NextDouble() * 2 * Math.PI;
            float length = random.NextFloat(minLength, maxLength);
            return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
        }
    }
}
