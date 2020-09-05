using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public static class CollisionSolver
    {

        public static List<Entity> GetAllColliding(Entity a)
        {
            List<Entity> colliding = new List<Entity>();

            foreach(Entity e in EntityManager.GetEntities())
            {
                if (IsColliding(a.x, a.y, a.width, a.height, e.x, e.y, e.width, e.height))
                    colliding.Add(e);
            }

            return colliding;
        }

        public static bool IsColliding(float aX, float aY, int aW, int aH, float bX, float bY, int bW, int bH)
        {
            if(aX + aW < bX || aX > bW + bX) return false;
            if (aY + aH < bY || aY > bH + bY) return false;

            return true;
        }

        public static bool IsPointInBounds(float aX, float aY, int aW, int aH, float pX, float pY)
        {
            if (pX > aX && pX < aX + aW && pY > aY && pY < aY + aH) return true;
            
            return false;
        }

    }
}
