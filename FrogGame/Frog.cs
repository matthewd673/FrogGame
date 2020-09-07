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

        public float jumpForce = 4f;
        float currentJumpForce = 4f;

        float movingTime = 0;
        float maxMovingTime = 100;

        public float maxJumpDistance = 60;
        float aimAngle;
        Vector2 jumpStart;
        Vector2 target;
        float remainingJumpDistance;

        Tounge tounge;
        float toungeRange = 30;

        public Frog(float x, float y) : base(Sprites.frog, x, y, 8, 8)
        {
            jumpCooldown = maxJumpCooldown;
            squishCooldown = maxSquishCooldown;
            tounge = new Tounge(this, 0, 0, new Vector2(0, 0));
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

            if (GetStrengthOfMotion() == 0)
                isMoving = false;
            else
                isMoving = true;

            if (isMoving)
                movingTime++;

            if (movingTime > maxMovingTime)
            {
                v = new Velocity(0, 0);
                movingTime = 0;
            }

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
                    float targetDist = maxJumpDistance * pullPercentage;
                    float targetX = (float)Math.Sin(aimAngle) * targetDist;
                    float targetY = (float)Math.Cos(aimAngle) * targetDist;

                    currentJumpForce = jumpForce * pullPercentage;

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

                    //Jump(aimAngle);

                    AddForce(currentJumpForce, aimAngle);

                    Game.debugOutput = currentJumpForce.ToString();

                    currentJumpForce = jumpForce; //reset jump force

                    //Jump(CalculateJumpAngle());
                }
            }

            //handle tounge
            if (!tounge.isOut)
            {
                //update position
                tounge.x = x;
                tounge.y = y;

                //search for something to grab
                foreach(Entity e in EntityManager.GetEntities())
                {
                    if(e.GetType() == typeof(Pickup) && GameMath.GetDistanceBetweenPoints(x, y, e.x, e.y) < toungeRange)
                    {
                        Pickup p = (Pickup)e;

                        tounge.SetTarget(new Vector2(p.x, p.y), p);
                        tounge.isOut = true;

                    }
                }
            }

            //update tounge if out
            if (tounge.isOut)
                tounge.Update();
            
            //CheckCollisions();

            //update sprite based on state
            UpdateSprite();

            base.Update();
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

            //render tounge behind everything else
            if (tounge.isOut)
            {
                Renderer.DrawLine(spriteBatch,
                    Sprites.tounge,
                    new Vector2((x + 4) * Renderer.cam.scale, (y + 4) * Renderer.cam.scale),
                    new Vector2(tounge.x * Renderer.cam.scale, tounge.y * Renderer.cam.scale));
            }

            if (isSquishing)
            {
                //draw mouse angle line
                Renderer.DrawLine(spriteBatch,
                    Sprites.dragLine,
                    new Vector2((x + 4) * Renderer.cam.scale, (y + 4) * Renderer.cam.scale),
                    new Vector2(InputManager.mouseX, InputManager.mouseY));
            }

            /*
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
            */

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

        public void ToungeGrabbed(Entity e)
        {
            if(e.GetType() == typeof(Pickup))
            {
                Pickup p = (Pickup)e;
                if (p.pType == Pickup.PickupType.Coin)
                    Game.score++;
            }
        }

    }
}
