using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public class PhysicsEntity : Entity
    {
        
        public float aX = 0;
        public float aY = 0;
        public float aDecay = 0.1f;
        public Velocity v = new Velocity(0, 0);

        public PhysicsEntity(Texture2D sprite, float x, float y, int width, int height) : base(sprite, x, y, width, height)
        {

        }

        public override void Update()
        {

            /*
            float newAX = Math.Abs(aX) - aDecay;
            float newAY = Math.Abs(aY) - aDecay;

            if (newAX < 0)
            {
                newAX = 0;
                aX = 0;
            }
            else
                aX = newAX * Math.Sign(aX);

            if (newAY < 0)
            {
                newAY = 0;
                aY = 0;
            }
            else
                aY = newAY * Math.Sign(aY);

            velX += aX;
            velY += aY;

            x += aX;
            y += aY;
            */

            x += v.GetSpeedX();
            y += v.GetSpeedY();

            base.Update();
        }

    }
}
