using System;
using System.Collections.Generic;
using System.Text;

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
            return (float)Math.Sqrt(Math.Pow(bX - aX, 2) + Math.Pow(bY - bX, 2));
        }

        public static float RandomFloat()
        {
            double mantissa = (rng.NextDouble() * 2.0) - 1.0;
            return (float)Math.Abs(mantissa);
        }

    }
}
