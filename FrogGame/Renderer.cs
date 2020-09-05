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
            //healthbar
            for(int i = 0; i < Game.health; i++)
            {
                spriteBatch.Draw(Sprites.heart, new Rectangle(i * 38, 0, 36, 36), Color.White);
            }

            //draw cursor
            spriteBatch.Draw(Sprites.cursor, new Rectangle(InputManager.mouseX - 4, InputManager.mouseY - 4, 8, 8), Color.White);

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

    }
}
