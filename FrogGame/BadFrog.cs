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
        public static int maxTeamCount = 9;

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
            tounge = new Tounge(this, 0, 0, new Vector2(0, 0));
            teamCount++;
        }

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

            //handle tounge
            if (!tounge.isOut)
            {
                //update position
                tounge.x = x;
                tounge.y = y;

                //search for something to grab
                foreach (Entity e in EntityManager.GetEntities())
                {
                    if (e.GetType() == typeof(Pickup) && GameMath.GetDistanceBetweenPoints(x, y, e.x, e.y) < toungeRange)
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

            UpdateSprite();

            base.Update();
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

            base.Render(spriteBatch);
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

        public void ToungeGrabbed(Entity e)
        {
            e.forRemoval = true;
            if (e.GetType() == typeof(Pickup))
            {
                Pickup p = (Pickup)e;
                if (p.pType == Pickup.PickupType.HoloFrog)
                {
                    if (teamCount < maxTeamCount)
                    {
                        BadFrog newBadFrog = new BadFrog(p.x, p.y);
                        EntityManager.AddEntity(newBadFrog);
                        EntityManager.AddEntity(new Popup(Popup.PopupType.NewBadFrog, x + 2, y - 4));
                    }
                }
                //if (p.pType == Pickup.PickupType.Coin)
                    //Game.score++;
            }
        }

        public void Kill()
        {
            forRemoval = true;
            teamCount--;
            EntityManager.AddEntity(new Popup(Popup.PopupType.PlusOne, x + 2, y - 4));
            Game.FreezeVelocity();
        }

    }
}
