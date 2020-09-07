using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace FrogGame
{
    public class Tounge
    {

        public float speed = 10f;

        public float x;
        public float y;

        public Vector2 target;
        public Entity targetEntity;

        public Frog parent;

        public bool isOut = false;
        bool returningToParent = false;

        public Tounge(Frog parent, float x, float y, Vector2 target)
        {
            this.parent = parent;
            this.x = x;
            this.y = y;
            this.target = target;
        }

        public void Update()
        {
            MoveToTarget();
        }

        public void SetTarget(Vector2 pos, Entity e)
        {
            target = pos;
            targetEntity = e;
        }

        public void MoveToTarget()
        {

            Vector2 lPos = GameMath.LerpVectors(new Vector2(x, y), target, 0.2f);

            x = lPos.X;
            y = lPos.Y;

            float distToTarget = GameMath.GetDistanceBetweenPoints(x, y, target.X, target.Y);

            //stop if in close proximity
            if (distToTarget < 3)
            {
                if (!returningToParent)
                {
                    //destroy target
                    if (targetEntity != null)
                        targetEntity.forRemoval = true;
                    //report back to frog
                    parent.ToungeGrabbed(targetEntity);

                    //return to parent
                    targetEntity = null;
                    target = new Vector2(parent.x, parent.y);
                    returningToParent = true;
                }
                else
                {
                    isOut = false;
                    returningToParent = false;
                }
            }
        }

    }
}
