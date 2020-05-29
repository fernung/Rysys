﻿using Microsoft.Xna.Framework;
using System;

namespace Rysys.Utilities
{
    public static class ColorUtility
    {
        public static Color FromHSV(float hue, float saturation, float value)
        {
            if (hue == 0 && saturation == 0)
                return new Color(value, value, value);

            float c = saturation * value;
            float x = c * (1 - Math.Abs(hue % 2 - 1));
            float m = value - c;

            if (hue < 1) return new Color(c + m, x + m, m);
            else if (hue < 2) return new Color(x + m, c + m, m);
            else if (hue < 3) return new Color(m, c + m, x + m);
            else if (hue < 4) return new Color(m, x + m, c + m);
            else if (hue < 5) return new Color(x + m, m, c + m);
            else return new Color(c + m, m, x + m);
        }
    }
}
