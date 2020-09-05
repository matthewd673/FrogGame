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
                spriteBatch.Draw(e.sprite, new Rectangle(((int)e.x * cam.scale) + (int)cam.x, ((int)e.y * cam.scale) + (int)cam.y, e.width * cam.scale, e.height * cam.scale), Color.White);

                if(e.GetType() == typeof(Frog) || e.GetType() == typeof(BadFrog))
                {
                    Frog f = (Frog)e;
                    int jumpFill = (int)(((float)(f.maxJumpCooldown - f.jumpCooldown) / (float)f.maxJumpCooldown) * e.width * cam.scale);

                    spriteBatch.Draw(Sprites.powerbarEmpty, new Rectangle(((int)e.x * cam.scale) + (int)cam.x, ((int)e.y * cam.scale) - 6 + (int)cam.y, e.width * cam.scale, 4), Color.White);
                    spriteBatch.Draw(Sprites.powerbarFill, new Rectangle(((int)e.x * cam.scale) + (int)cam.x, ((int)e.y * cam.scale) - 6 + (int)cam.y, jumpFill, 4), Color.White);
                }

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



    }
}
