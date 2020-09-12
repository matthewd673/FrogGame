using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public class BadFrog : PhysicsEntity
    {

        public static int teamCount = 0;

        float minAttackDist = 80;

        Random rng = new Random();

        public bool isSquishing;
        public bool isMoving;

        public int squishCooldown;
        public int maxSquishCooldown = 80;

        int movingTime = 0;
        int maxMovingTime = 100;

        public float jumpForce = 4f;
        float aimAngle;

        Tounge tounge;
        float toungeRange = 30;

        public BadFrog(float x, float y) : base(Sprites.badFrog, x, y, 8, 8)
        {
            squishCooldown = maxSquishCooldown;
            //tounge = new Tounge(this, 0, 0, new Vector2(0, 0));
            teamCount++;
        }

        /*
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
        */

        public override void Update()
        {

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

            //execute jump
            if (!isMoving)
            {

                isSquishing = true;

                if (squishCooldown > 0)
                    squishCooldown--;

                if (squishCooldown <= 0) //if cooldown just ran out
                {
                    isMoving = true;
                    isSquishing = false;
                    squishCooldown = maxSquishCooldown;

                    Entity nearestGoal = GetNearestGoal();

                    if (nearestGoal == null)
                        return;

                    //calculate aim angle
                    aimAngle = GameMath.GetAngleBetweenPoints(x, y, nearestGoal.x, nearestGoal.y);

                    //move towards target
                    AddForce(jumpForce, aimAngle);
                }
            }

            UpdateSprite();

            base.Update();
        }

        public void UpdateSprite()
        {
            if (isSquishing)
            {
                if (squishCooldown > (maxSquishCooldown / 2))
                    sprite = Sprites.badFrogSquish;
                else
                    sprite = Sprites.badFrogSquishExtreme;
            }
            else if (isMoving)
                sprite = Sprites.badFrogMoving;
            else
                sprite = Sprites.badFrog;
        }
        
        Entity GetNearestGoal()
        {
            List<Entity> entityList = EntityManager.GetEntities();
            float nearestDist = 99999;
            Entity nearestGoal = null;

            foreach(Entity e in entityList)
            {
                if(e.GetType() == typeof(Frog) || e.GetType() == typeof(Pickup))
                {
                    float distToGoal = GameMath.GetDistanceBetweenPoints(x, y, e.x, e.y);
                    if(distToGoal < nearestDist)
                    {
                        nearestDist = distToGoal;
                        nearestGoal = e;
                    }
                }
            }

            return nearestGoal;

        }

    }
}
