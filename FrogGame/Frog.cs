using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Frog : PhysicsEntity
    {

        public float jumpAccel = 3f;

        public int jumpCooldown;
        public int maxJumpCooldown = 200;

        public bool nextCooldownShorter = false;

        public Frog(float x, float y) : base(Sprites.frog, x, y, 8, 8)
        {
            jumpCooldown = maxJumpCooldown;
        }


        public virtual void UpdateLogic()
        {
            //jump
            if (jumpCooldown > 0)
                jumpCooldown--;
            else
            {
                if (!nextCooldownShorter)
                    jumpCooldown = maxJumpCooldown;
                else
                    jumpCooldown = maxJumpCooldown / 4;

                nextCooldownShorter = false;

                Jump(CalculateJumpAngle());

                //shake screen
                Renderer.cam.SetShake(6f, 0.5f);
            }

            //check collisions
            List<Entity> colliding = CollisionSolver.GetAllColliding(this);
            if (colliding.Count > 0)
            {
                foreach (Entity e in colliding)
                {
                    if (e.GetType() == typeof(Pickup))
                    {
                        //PICKUP!
                        Pickup p = (Pickup)e;

                        switch (p.pType)
                        {
                            case Pickup.PickupType.Coin:
                                Game.score++;
                                break;
                            case Pickup.PickupType.Fly:
                                nextCooldownShorter = true;
                                break;
                        }

                        p.forRemoval = true;

                    }

                    if (e.GetType() == typeof(BadFrog))
                    {
                        //ENEMY :(
                        Game.health--;
                        e.forRemoval = true;
                        Renderer.cam.SetShake(10f, 0.3f);
                    }
                }
            }
        }

        public override void Update()
        {

            UpdateLogic();

            base.Update();
        }

        public void Jump(float jumpAngle)
        {
            float jumpAX = (float)Math.Sin(jumpAngle) * jumpAccel;
            float jumpAY = (float)Math.Cos(jumpAngle) * jumpAccel;

            aX += jumpAX;
            aY += jumpAY;
        }

        public float CalculateJumpAngle()
        {
            return GameMath.GetAngleBetweenPoints(x, y, InputManager.mouseX / Renderer.cam.scale, InputManager.mouseY / Renderer.cam.scale);
        }

    }
}
