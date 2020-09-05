using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class BadFrog : Frog
    {

        float minAttackDist = 80;

        Random rng = new Random();

        public BadFrog(float x, float y) : base(x, y)
        {
            sprite = Sprites.badFrog;

            maxJumpCooldown = 350;
            jumpCooldown = maxJumpCooldown;

        }

        public override void UpdateLogic()
        {
            if (jumpCooldown > 0)
                jumpCooldown--;
            else
            {
                jumpCooldown = maxJumpCooldown;

                //calculate jump direction
                //get distance toward friendly frog
                Frog friendlyFrog = (Frog)EntityManager.FindFirstEntityOfType(typeof(Frog));

                if (friendlyFrog == null)
                    return;

                float distToFrog = GameMath.GetDistanceBetweenPoints(x, y, friendlyFrog.x, friendlyFrog.y);

                //target random point by default
                float tX = rng.Next(0, Renderer.cam.width / Renderer.cam.scale);
                float tY = rng.Next(0, Renderer.cam.height / Renderer.cam.scale);

                if (distToFrog <= minAttackDist)
                {
                    //target frog
                    tX = friendlyFrog.x;
                    tY = friendlyFrog.y;
                }

                //jump
                Jump(GameMath.GetAngleBetweenPoints(x, y, tX, tY));

            }
        }

        public override void Update()
        {
            base.Update();
        }

    }
}
