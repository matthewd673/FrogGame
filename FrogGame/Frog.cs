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
        public float speed = 1f;

        public int jumpCooldown;
        public int maxJumpCooldown = 100;

        public bool isMoving = false;
        public bool isSquishing = false;

        public int squishCooldown;
        public int maxSquishCooldown = 40;

        public float maxPullDistance = 60;
        float pullDistance;
        float pullPercentage;

        public float maxJumpDistance = 60;
        float aimAngle;
        float jumpStrength;
        Vector2 jumpStart;
        Vector2 target;
        float remainingJumpDistance;

        public Frog(float x, float y) : base(Sprites.frog, x, y, 8, 8)
        {
            jumpCooldown = maxJumpCooldown;
            squishCooldown = maxSquishCooldown;
        }

        public override void Update()
        {

            //check if moving
            /*
            if (Math.Abs(v.speed) > 0)
                isMoving = true;
            else
                isMoving = false;
            */

            //check if should be preparing jump
            if (!isMoving)
            {
                if ((InputManager.MouseHeld() && CollisionSolver.IsPointInBounds(
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

                    jumpStrength = (float)((float)(maxSquishCooldown - squishCooldown) / (float)maxSquishCooldown);

                    //calculate aim angle
                    float angleToMouse = GameMath.GetAngleBetweenPoints(x * Renderer.cam.scale, y * Renderer.cam.scale, InputManager.mouseX, InputManager.mouseY);
                    aimAngle = angleToMouse + (float)Math.PI;

                    //calculate mouse pull
                    pullDistance = GameMath.GetDistanceBetweenPoints((x + 4) * Renderer.cam.scale, (y + 4) * Renderer.cam.scale, InputManager.mouseX, InputManager.mouseY);

                    pullPercentage = (float)(pullDistance / maxPullDistance);
                    if (pullPercentage > 1)
                        pullPercentage = 1;
                    if (pullPercentage < 0.3f)
                        pullPercentage = 0.5f;

                    //calculate target positioning
                    float targetDist = maxJumpDistance * jumpStrength * pullPercentage;
                    float targetX = (float)Math.Sin(aimAngle) * targetDist;
                    float targetY = (float)Math.Cos(aimAngle) * targetDist;

                    Game.debugOutput = aimAngle.ToString();

                    jumpStart = new Vector2(x, y);
                    target = new Vector2(targetX + x - 4, targetY + y - 4);

                }
                else
                {
                    if (!InputManager.MouseJustReleased()) //only reset if the mouse wasn't JUST released
                    {
                        isSquishing = false;
                        squishCooldown = maxSquishCooldown;
                    }
                }

                //jump is ready! (mouse has been released, no longer squishing
                if (isSquishing && InputManager.MouseJustReleased())
                {
                    isSquishing = false;
                    squishCooldown = maxSquishCooldown;

                    Jump(aimAngle);

                    //Jump(CalculateJumpAngle());
                }
            }

            //move to target, if necessary
            if(isMoving)
                MoveToTarget();

            CheckCollisions();

            //update sprite based on state
            UpdateSprite();

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

            /*
            v.SetSpeed(jumpAccel);
            v.angle = jumpAngle;


            //constant decel
            v.Decelerate(0.1f);
            */

            isMoving = true;
        }

        public void MoveToTarget()
        {

            float adjSpeed = 0;

            float startingDistance = GameMath.GetDistanceBetweenPoints(jumpStart.X, jumpStart.Y, target.X, target.Y);
            float currentDistance = GameMath.GetDistanceBetweenPoints(x, y, target.X, target.Y);

            adjSpeed = speed * (float)((float)(startingDistance - currentDistance) / (float)startingDistance);

            Game.debugOutput = adjSpeed.ToString();

            //float angle = GameMath.GetAngleBetweenPoints(x, y, target.X, target.Y);
            //x += (float)Math.Sin(angle) * speed;
            //y += (float)Math.Cos(angle) * speed;

            Vector2 lPos = GameMath.LerpVectors(new Vector2(x, y), target, 0.15f);

            x = lPos.X;
            y = lPos.Y;

            remainingJumpDistance = currentDistance;

            //stop if in close proximity
            if (currentDistance < 3)
                isMoving = false;
        }

        public void CheckCollisions()
        {
            List<Entity> colliding = CollisionSolver.GetAllColliding(this);

            foreach(Entity e in colliding)
            {
                Type eType = e.GetType();

                if(eType == typeof(Wall))
                {
                    Wall w = (Wall)e;

                    //THESE BOUNCE PHYSICS ARE WRONG :(

                    bool travellingHorizontally = true;
                    if (Math.Cos(aimAngle) > Math.Sin(aimAngle))
                        travellingHorizontally = false;

                    float inverseAngle = aimAngle;

                    if (travellingHorizontally)
                        inverseAngle -= (float)(Math.PI / 2);
                    else
                        inverseAngle += (float)(Math.PI / 2);

                    //float inverseAngle = aimAngle - (float)(Math.PI / 2);
                    float newTargetX = (float)Math.Sin(inverseAngle) * remainingJumpDistance;
                    float newTargetY = (float)Math.Cos(inverseAngle) * remainingJumpDistance;

                    target = new Vector2(newTargetX + x, newTargetY + y);

                }
            }
        }

        public override void Render(SpriteBatch spriteBatch)
        {

            if (isSquishing)
            {
                //draw mouse angle line
                Renderer.DrawLine(spriteBatch,
                    Sprites.pixel,
                    new Vector2((x + 4) * Renderer.cam.scale, (y + 4) * Renderer.cam.scale),
                    new Vector2(InputManager.mouseX, InputManager.mouseY));
            }

            if (isSquishing || isMoving)
            {
                //draw landing target
                //adjust target sprite based on frog state
                Texture2D targetSprite = Sprites.target;
                if (isMoving)
                    targetSprite = Sprites.targetLanding;

                spriteBatch.Draw(targetSprite,
                    new Rectangle((int)target.X * Renderer.cam.scale, (int)target.Y * Renderer.cam.scale, 8 * Renderer.cam.scale, 8 * Renderer.cam.scale),
                    Color.White);
            }
            base.Render(spriteBatch);
        }

        public void UpdateSprite()
        {
            if (isSquishing)
            {
                if (squishCooldown > (maxSquishCooldown / 2))
                    sprite = Sprites.frogSquish;
                else
                    sprite = Sprites.frogSquishExtreme;
            }
            else if (isMoving)
            {
                sprite = Sprites.frogMoving;
            }
            else
                sprite = Sprites.frog;
        }

    }
}
