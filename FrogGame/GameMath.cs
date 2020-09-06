using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace FrogGame
{
    public static class GameMath
    {

        static Random rng = new Random();

        public static float GetAngleBetweenPoints(float aX, float aY, float bX, float bY)
        {
            return (float)Math.Atan2(bX - aX, bY - aY);
        }

        public static float GetDistanceBetweenPoints(float aX, float aY, float bX, float bY)
        {
            return (float)Math.Sqrt(Math.Pow(bX - aX, 2) + Math.Pow(bY - aY, 2));
        }

        public static float RandomFloat()
        {
            double mantissa = (rng.NextDouble() * 2.0) - 1.0;
            return (float)Math.Abs(mantissa);
        }

        public static float Lerp(float a, float b, float by)
        {
            return a * (1 - by) + b * by;
        }

        public static Vector2 LerpVectors(Vector2 a, Vector2 b, float by)
        {
            float lX = Lerp(a.X, b.X, by);
            float lY = Lerp(a.Y, b.Y, by);

            return new Vector2(lX, lY);
        }

    }
}
