using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public static class Sprites
    {

        public static Texture2D frog;
        public static Texture2D badFrog;
        public static Texture2D fly;
        public static Texture2D coin;
        public static Texture2D background;
        public static Texture2D powerbarFill;
        public static Texture2D powerbarEmpty;
        public static Texture2D heart;
        public static Texture2D cursor;

        public static Texture2D title;
        public static Texture2D end;

        public static void LoadTextures(ContentManager content)
        {
            frog = content.Load<Texture2D>("img/frog");
            badFrog = content.Load<Texture2D>("img/bad-frog");
            fly = content.Load<Texture2D>("img/fly");
            coin = content.Load<Texture2D>("img/coin");
            background = content.Load<Texture2D>("img/ground");
            powerbarFill = content.Load<Texture2D>("img/powerbar-fill");
            powerbarEmpty = content.Load<Texture2D>("img/powerbar-empty");
            heart = content.Load<Texture2D>("img/heart");
            cursor = content.Load<Texture2D>("img/cursor");

            title = content.Load<Texture2D>("img/title");
            end = content.Load<Texture2D>("img/end");
        }

    }
}
