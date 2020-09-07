using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public class PhysicsEntity : Entity
    {

        public float mass = 1;
        public Acceleration a = new Acceleration(0, 0);
        public Velocity v = new Velocity(0, 0);

        public float frictionCoefficient = 0.15f;

        public PhysicsEntity(Texture2D sprite, float x, float y, int width, int height) : base(sprite, x, y, width, height)
        {

        }

        public void AddForce(float force, float angle)
        {
            float forceX = (float)Math.Sin(angle) * (force / mass);
            float forceY = (float)Math.Cos(angle) * (force / mass);

            a.aX += forceX;
            a.aY += forceY;
        }

        void ApplyFriction()
        {

            float aXMag = Math.Abs(a.aX);
            float aYMag = Math.Abs(a.aY);

            if (GetStrengthOfMotion() > 0.1f)
                AddForce(mass * frictionCoefficient, GetAngleOfMotion() + (float)(Math.PI));

        }

        public float GetAngleOfMotion()
        {
            return GameMath.GetAngleBetweenPoints(x, y, x + v.vX, y + v.vY);
        }

        public float GetStrengthOfMotion()
        {
            return (float) Math.Sqrt(Math.Pow(v.vX, 2) + Math.Pow(v.vY, 2));
        }

        public float GetAngleOfAcceleration()
        {
            return GameMath.GetAngleBetweenPoints(x, y, x + a.aX, y + a.aY);
        }

        public float GetStrengthOfAcceleration()
        {
            return (float)Math.Sqrt(Math.Pow(a.aX, 2) + Math.Pow(a.aY, 2));
        }

        public override void Update()
        {

            ApplyFriction();

            v.vX += a.aX;
            v.vY += a.aY;

            x += v.vX;
            y += v.vY;

            //reset acceleration
            a = new Acceleration(0, 0);

            //prevent drifting
            if (GetStrengthOfMotion() < 0.1f)
                v = new Velocity(0, 0);

            base.Update();
        }

    }
}
