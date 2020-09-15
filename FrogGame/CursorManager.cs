using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public static class CursorManager
    {

        public enum CursorMode
        {
            Normal,
            BoundingBox,
        }

        public static CursorMode mode;

        public static Vector2 boundingA;
        public static Vector2 boundingB;
        static bool updatingB = true;

        public static void Update()
        {
            List<Entity> entityList = EntityManager.GetEntities();

            //set mode
            if (InputManager.MouseRightDown())
                mode = CursorMode.BoundingBox;
            else
                mode = CursorMode.Normal;

            if (mode == CursorMode.BoundingBox)
            {
                if (InputManager.MouseRightJustPressed())
                {
                    boundingA = new Vector2(InputManager.mouseX, InputManager.mouseY);
                    boundingB = new Vector2(InputManager.mouseX, InputManager.mouseY);
                }

                if (InputManager.MouseRightHeld())
                {

                    if (boundingB.X < boundingA.X || boundingB.Y < boundingA.Y)
                        updatingB = false;
                    else
                        updatingB = true;

                    if (updatingB)
                        boundingB = new Vector2(InputManager.mouseX, InputManager.mouseY);
                    else
                        boundingA = new Vector2(InputManager.mouseX, InputManager.mouseY);

                    
                }

                //select all frogs in range
                foreach (Entity e in entityList)
                {
                    if (e.GetType() == typeof(Frog))
                    {

                        Rectangle frogScreenPos = Renderer.GetRenderRect(e.x, e.y, e.width, e.height);

                        if (CollisionSolver.IsColliding(
                            frogScreenPos.X,
                            frogScreenPos.Y,
                            frogScreenPos.Width,
                            frogScreenPos.Height,
                            boundingA.X,
                            boundingA.Y,
                            (int)(boundingB.X - boundingA.X),
                            (int)(boundingB.Y - boundingA.Y)))
                        {
                            ((Frog)e).isInGroup = true;
                        }
                        else
                            ((Frog)e).isInGroup = false;

                    } 
                }
            }
            else
            {
                if (InputManager.MouseRightJustReleased())
                {
                    //deselect all frogs in range
                    foreach (Entity e in entityList)
                    {
                        if (e.GetType() == typeof(Frog))
                        {

                            Rectangle frogScreenPos = Renderer.GetRenderRect(e.x, e.y, e.width, e.height);

                            if (CollisionSolver.IsColliding(
                                frogScreenPos.X,
                                frogScreenPos.Y,
                                frogScreenPos.Width,
                                frogScreenPos.Height,
                                boundingA.X,
                                boundingA.Y,
                                (int)(boundingB.X - boundingA.X),
                                (int)(boundingB.Y - boundingA.Y)))
                            {
                                ((Frog)e).isInGroup = false;
                            }

                        }
                    }
                }
            }
        }

        public static void Render(SpriteBatch spriteBatch)
        {
            Rectangle cursorRect = new Rectangle(InputManager.mouseX, InputManager.mouseY, 16, 16);
            //render cursor
            switch (mode)
            {
                case CursorMode.Normal:
                    spriteBatch.Draw(Sprites.cursor, cursorRect, Color.White);
                    break;
                case CursorMode.BoundingBox:
                    spriteBatch.Draw(Sprites.cursorBounding, cursorRect, Color.White);
                    break;
            }

            //render bounding box
            if(mode == CursorMode.BoundingBox)
            {
                Renderer.DrawLineRect(spriteBatch, (int)boundingA.X, (int)boundingA.Y, (int)(boundingB.X - boundingA.X), (int)(boundingB.Y - boundingA.Y), Renderer.OFFWHITE);
            }
        }

    }
}
