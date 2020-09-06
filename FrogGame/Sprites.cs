using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public static class Sprites
    {

        public static Texture2D pixel;

        public static Texture2D frog;
        public static Texture2D frogSquish;
        public static Texture2D frogSquishExtreme;
        public static Texture2D frogMoving;

        public static Texture2D badFrog;
        
        public static Texture2D fly;
        public static Texture2D coin;

        public static Texture2D crate;
        public static Texture2D wall;
        public static Texture2D background;

        public static Texture2D target;
        public static Texture2D targetLanding;
        public static Texture2D powerbarFill;
        public static Texture2D powerbarEmpty;
        public static Texture2D heart;
        public static Texture2D cursor;

        public static Texture2D title;
        public static Texture2D end;

        public static SpriteFont font;

        public static void LoadTextures(ContentManager content)
        {
            pixel = content.Load<Texture2D>("img/pixel");

            frog = content.Load<Texture2D>("img/frog");
            frogSquish = content.Load<Texture2D>("img/frog-squish");
            frogSquishExtreme = content.Load<Texture2D>("img/frog-squish-extreme");
            frogMoving = content.Load<Texture2D>("img/frog-moving");

            badFrog = content.Load<Texture2D>("img/bad-frog");
            
            fly = content.Load<Texture2D>("img/fly");
            coin = content.Load<Texture2D>("img/coin");

            crate = content.Load<Texture2D>("img/crate");
            wall = content.Load<Texture2D>("img/wall");
            background = content.Load<Texture2D>("img/ground");

            target = content.Load<Texture2D>("img/target");
            targetLanding = content.Load<Texture2D>("img/target-landing");
            powerbarFill = content.Load<Texture2D>("img/powerbar-fill");
            powerbarEmpty = content.Load<Texture2D>("img/powerbar-empty");
            heart = content.Load<Texture2D>("img/heart");
            cursor = content.Load<Texture2D>("img/cursor");

            title = content.Load<Texture2D>("img/title");
            end = content.Load<Texture2D>("img/end");

            font = content.Load<SpriteFont>("font");
        }

    }
}
