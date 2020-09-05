using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public class Frog : PhysicsEntity
    {

        public float jumpAccel = 1f;

        public int jumpCooldown;
        public int maxJumpCooldown = 100;

        public bool isMoving = false;
        public bool isSquishing = false;

        public int squishCooldown;
        public int maxSquishCooldown = 60;

        public bool nextCooldownShorter = false;

        public Frog(float x, float y) : base(Sprites.frog, x, y, 8, 8)
        {
            jumpCooldown = maxJumpCooldown;
            squishCooldown = maxSquishCooldown;
        }

        public override void Update()
        {

            //check if moving
            if (Math.Abs(v.speed) > 0)
                isMoving = true;
            else
                isMoving = false;

            //check if should be preparing jump
            if((InputManager.MouseHeld() && CollisionSolver.IsPointInBounds(
                x * Renderer.cam.scale,
                y * Renderer.cam.scale,
                width * Renderer.cam.scale,
                height * Renderer.cam.scale,
                InputManager.mouseX,
                InputManager.mouseY
                )) || InputManager.MouseHeld() && isSquishing) //continues the squish if mouse is in bounds and held, or mouse is held and squish is already in progress (so you can drag out)
            {
                isSquishing = true;

                if (squishCooldown > 0)
                    squishCooldown--;

            }
            else
            {
                isSquishing = false;
                squishCooldown = maxSquishCooldown;
            }

            //jump is ready!
            if (squishCooldown <= 0) 
            {
                isSquishing = false;
                squishCooldown = maxSquishCooldown;

                float angleToMouse = GameMath.GetAngleBetweenPoints(x * Renderer.cam.scale, y * Renderer.cam.scale, InputManager.mouseX, InputManager.mouseY);

                Jump(angleToMouse + (float)Math.PI);

                //Jump(CalculateJumpAngle());
            }

            //update sprite accordingly
            if (isSquishing)
            {
                if (squishCooldown > (maxSquishCooldown / 2))
                    sprite = Sprites.frogSquish;
                else
                    sprite = Sprites.frogSquishExtreme;
            }
            else
                sprite = Sprites.frog;

            /*
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
            */

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

            base.Update();
        }

        public void Jump(float jumpAngle)
        {
            /*
            float jumpAX = (float)Math.Sin(jumpAngle) * jumpAccel;
            float jumpAY = (float)Math.Cos(jumpAngle) * jumpAccel;

            aX += jumpAX;
            aY += jumpAY;
            */

            v.SetSpeed(jumpAccel);
            v.angle = jumpAngle;


            //constant decel
            v.Decelerate(0.1f);
        }

        public override void Render(SpriteBatch spriteBatch)
        {

            if(isSquishing)
                Renderer.DrawLine(spriteBatch, Sprites.pixel, new Vector2((x + 4) * Renderer.cam.scale, (y + 4) * Renderer.cam.scale), new Vector2(InputManager.mouseX, InputManager.mouseY));

            base.Render(spriteBatch);
        }

        public float CalculateJumpAngle()
        {
            return GameMath.GetAngleBetweenPoints(x, y, InputManager.mouseX / Renderer.cam.scale, InputManager.mouseY / Renderer.cam.scale);
        }

    }
}
