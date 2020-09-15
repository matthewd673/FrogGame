using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public static class Renderer
    {

        public static Camera cam = new Camera(0, 0, 800, 600, 4);

        public static readonly Color OFFWHITE = new Color(248, 255, 215);

        public static void Render(SpriteBatch spriteBatch)
        {

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if(Game.state == Game.GameState.Title)
            {
                spriteBatch.Draw(Sprites.title, new Rectangle(0, 0, cam.width, cam.height), Color.White);
                spriteBatch.End();
                return;
            }

            if (Game.state == Game.GameState.End)
            {
                spriteBatch.Draw(Sprites.end, new Rectangle(0, 0, cam.width, cam.height), Color.White);
                spriteBatch.End();
                return;
            }

            //draw background
            for (int i = 0; i < 25; i++)
            {
                for(int j = 0; j < 20; j++)
                {
                    spriteBatch.Draw(Sprites.background, new Rectangle((i * (cam.scale * 32)) + (int)cam.x, (j * (cam.scale * 32)) + (int)cam.y, 32 * cam.scale, 32 * cam.scale), Color.White);
                }
            }

            //draw entities
            foreach(Entity e in EntityManager.GetEntities())
            {
                e.Render(spriteBatch);
            }

            //render ui
            //draw cursor
            CursorManager.Render(spriteBatch);

            //draw debug info
            spriteBatch.DrawString(Sprites.font, Game.debugOutput, new Vector2(10, 10), Color.Red);

            spriteBatch.End();
        }

        public static void DrawLine(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 end)
        {
            spriteBatch.Draw(texture, start, null, Color.White,
                             (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                             new Vector2(0f, (float)texture.Height / 2),
                             new Vector2(Vector2.Distance(start, end), 1f),
                             SpriteEffects.None, 0f);
        }

        public static void DrawLineRect(SpriteBatch spriteBatch, int x, int y, int w, int h, Color color)
        {
            spriteBatch.Draw(Sprites.pixel, new Rectangle(x, y, w, 2), color);
            spriteBatch.Draw(Sprites.pixel, new Rectangle(x, y, 2, h), color);
            spriteBatch.Draw(Sprites.pixel, new Rectangle(x, y + h, w, 2), color);
            spriteBatch.Draw(Sprites.pixel, new Rectangle(x + w, y, 2, h), color);
        }

        public static Rectangle GetRenderRect(float x, float y, int w, int h)
        {
            return new Rectangle((int)(x - cam.x) * cam.scale, (int)(y - cam.y) * cam.scale, w * cam.scale, h * cam.scale);
        }

    }
}
