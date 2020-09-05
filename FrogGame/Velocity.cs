using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Velocity
    {

        public float speed;
        public float angle;

        public Velocity(float speed, float angle)
        {
            this.speed = speed;
            this.angle = angle;
        }

        public void Accelerate(float a)
        {
            speed += a;
        }

        public void SetSpeed(float speed)
        {
            this.speed = speed;
        }

        public void Decelerate(float d)
        {

            float newSpeed = Math.Abs(speed);

            newSpeed -= d;

            if (newSpeed > 0)
                speed = newSpeed * Math.Sign(speed);
            else
                speed = 0;

        }

        public float GetSpeedX()
        {
            return (float)Math.Sin(angle) * speed;
        }

        public float GetSpeedY()
        {
            return (float)Math.Cos(angle) * speed;
        }

    }
}
