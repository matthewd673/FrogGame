using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public class Frog : PhysicsEntity
    {

        public static int teamCount = 0;
        public static int maxTeamCount = 6;

        public bool isMoving = false;
        public bool isSquishing = false;
        public bool isSelected = false;
        public bool wasSelected = false;

        public int squishCooldown;
        public int maxSquishCooldown = 40;

        public float maxPullDistance = 60;
        float pullDistance;
        float pullPercentage;

        public float jumpForce = 4f;
        float currentJumpForce = 4f;

        int movingTime = 0;
        int maxMovingTime = 100;

        public float maxJumpDistance = 60;
        float aimAngle;

        Tounge tounge;
        float toungeRange = 30;

        public bool automatic = false;
        int timeSinceUserControl = 0;
        int maxTimeSinceUserControl = 600;

        public Frog(float x, float y) : base(Sprites.frog, x, y, 8, 8)
        {
            squishCooldown = maxSquishCooldown;
            tounge = new Tounge(this, 0, 0, new Vector2(0, 0));
            teamCount++;
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

            if(timeSinceUserControl < maxTimeSinceUserControl)
                timeSinceUserControl++;

            //check if frog is selected
            wasSelected = isSelected;
            if (
                    (InputManager.MouseHeld() &&
                        GameMath.GetDistanceBetweenPoints(
                            (x + 4) * Renderer.cam.scale,
                            (y + 4) * Renderer.cam.scale,
                            InputManager.mouseX,
                            InputManager.mouseY
                            ) < 48
                        )
                        || InputManager.MouseHeld() && isSquishing && wasSelected)
            {
                isSelected = true;
                timeSinceUserControl = 0;
            }
            else
                isSelected = false;

            if (timeSinceUserControl >= maxTimeSinceUserControl)
                automatic = true;
            else
                automatic = false;

            //check if should be preparing jump
            if (!isMoving)
            {

                if (!automatic)
                    UserControl();
                else
                    AutoControl();

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
            
            CheckCollisions();

            //update sprite based on state
            UpdateSprite();

            base.Update();
        }        

        void UserControl()
        {
            if (isSelected)
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

                currentJumpForce = jumpForce * pullPercentage;

            }
            else
            {
                if (wasSelected == isSelected) //only reset if the mouse wasn't JUST released
                {
                    isSquishing = false;
                    squishCooldown = maxSquishCooldown;
                }
            }

            //jump is ready! (mouse has been released, no longer squishing
            if (isSquishing && wasSelected && !isSelected)
            {
                isSquishing = false;
                squishCooldown = maxSquishCooldown;

                //Jump(aimAngle);

                AddForce(currentJumpForce, aimAngle);

                //Game.debugOutput = currentJumpForce.ToString();

                currentJumpForce = jumpForce; //reset jump force

                //Jump(CalculateJumpAngle());
            }
        }

        void AutoControl()
        {
            isSquishing = true;

            if (squishCooldown > 0)
                squishCooldown--;

            if (squishCooldown <= 0) //cooldown just ran out
            {
                isMoving = true;
                isSquishing = false;
                squishCooldown = maxSquishCooldown;

                Entity nearestGoal = GetNearestGoal();

                if (nearestGoal == null)
                    return;

                //calculate angle
                aimAngle = GameMath.GetAngleBetweenPoints(x, y, nearestGoal.x, nearestGoal.y);
                AddForce(jumpForce, aimAngle);
            }
        }

        Entity GetNearestGoal()
        {
            List<Entity> entityList = EntityManager.GetEntities();
            float nearestDist = 99999;
            Entity nearestGoal = null;

            foreach (Entity e in entityList)
            {
                if (e.GetType() == typeof(BadFrog) || e.GetType() == typeof(Pickup))
                {
                    float distToGoal = GameMath.GetDistanceBetweenPoints(x, y, e.x, e.y);
                    if (distToGoal < nearestDist)
                    {
                        nearestDist = distToGoal;
                        nearestGoal = e;
                    }
                }
            }

            return nearestGoal;

        }

        public void CheckCollisions()
        {
            List<Entity> colliding = CollisionSolver.GetAllColliding(this);

            foreach(Entity e in colliding)
            {
                Type eType = e.GetType();

                if(eType == typeof(Pickup))
                {
                    ToungeGrabbed(e);
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

            if (isSquishing && isSelected)
            {
                //draw mouse angle line
                Renderer.DrawLine(spriteBatch,
                    Sprites.dragLine,
                    new Vector2((x + 4) * Renderer.cam.scale, (y + 4) * Renderer.cam.scale),
                    new Vector2(InputManager.mouseX, InputManager.mouseY));
            }

            if (automatic)
            {
                spriteBatch.Draw(Sprites.autoStatus, new Rectangle((int)(x + 2) * Renderer.cam.scale, (int)(y - 6) * Renderer.cam.scale, 16, 16), Color.White);
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
                {
                    sprite = Sprites.frogSquish;
                    if (isSelected)
                        sprite = Sprites.frogSquishSelected;
                }
                else
                {
                    sprite = Sprites.frogSquishExtreme;
                    if (isSelected)
                        sprite = Sprites.frogSquishExtremeSelected;
                }
            }
            else if (isMoving)
            {
                sprite = Sprites.frogMoving;
            }
            else
            {
                sprite = Sprites.frog;
                if (isSelected)
                    sprite = Sprites.frogSelected;
            }
        }

        public void ToungeGrabbed(Entity e)
        {
            e.forRemoval = true;
            if(e.GetType() == typeof(Pickup))
            {
                Pickup p = (Pickup)e;
                if (p.pType == Pickup.PickupType.HoloFrog)
                {
                    if (teamCount < maxTeamCount)
                    {
                        Frog newFrog = new Frog(16, 16);
                        EntityManager.AddEntity(newFrog);
                    }
                }
                if (p.pType == Pickup.PickupType.Coin)
                    Game.score++;
            }
        }

    }
}
