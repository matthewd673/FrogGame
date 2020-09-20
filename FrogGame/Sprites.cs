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
        public static Texture2D frogSelected;
        public static Texture2D frogSquishSelected;
        public static Texture2D frogSquishExtremeSelected;
        public static Texture2D tounge;

        public static Texture2D badFrog;
        public static Texture2D badFrogSquish;
        public static Texture2D badFrogSquishExtreme;
        public static Texture2D badFrogMoving;
        
        public static Texture2D fly;
        public static Texture2D coin;
        public static Texture2D holoFrog;

        public static Texture2D crate;
        public static Texture2D wall;
        public static Texture2D background;

        public static Texture2D dragLine;
        public static Texture2D target;
        public static Texture2D targetLanding;
        public static Texture2D powerbarFill;
        public static Texture2D powerbarEmpty;
        public static Texture2D autoStatus;
        public static Texture2D popupPlusOne;
        public static Texture2D popupMinusOne;
        public static Texture2D popupNewFrog;
        public static Texture2D popupNewBadFrog;
        public static Texture2D frogPortrait;
        public static Texture2D frogBar;
        public static Texture2D badFrogPortrait;
        public static Texture2D badFrogBar;
        public static Texture2D cursor;
        public static Texture2D cursorBounding;

        public static Texture2D title;
        public static Texture2D end;
        public static Texture2D victory;

        public static SpriteFont font;

        public static void LoadTextures(ContentManager content)
        {
            pixel = content.Load<Texture2D>("img/pixel");

            frog = content.Load<Texture2D>("img/frog");
            frogSquish = content.Load<Texture2D>("img/frog-squish");
            frogSquishExtreme = content.Load<Texture2D>("img/frog-squish-extreme");
            frogMoving = content.Load<Texture2D>("img/frog-moving");
            frogSelected = content.Load<Texture2D>("img/frog-selected");
            frogSquishSelected = content.Load<Texture2D>("img/frog-squish-selected");
            frogSquishExtremeSelected = content.Load<Texture2D>("img/frog-squish-extreme-selected");
            tounge = content.Load<Texture2D>("img/tounge");

            badFrog = content.Load<Texture2D>("img/bad-frog");
            badFrogSquish = content.Load<Texture2D>("img/bad-frog-squish");
            badFrogSquishExtreme = content.Load<Texture2D>("img/bad-frog-squish-extreme");
            badFrogMoving = content.Load<Texture2D>("img/bad-frog-moving");
            
            fly = content.Load<Texture2D>("img/fly");
            coin = content.Load<Texture2D>("img/coin");
            holoFrog = content.Load<Texture2D>("img/holo-frog");

            crate = content.Load<Texture2D>("img/crate");
            wall = content.Load<Texture2D>("img/wall");
            background = content.Load<Texture2D>("img/ground");

            dragLine = content.Load<Texture2D>("img/drag-line");
            target = content.Load<Texture2D>("img/target");
            targetLanding = content.Load<Texture2D>("img/target-landing");
            powerbarFill = content.Load<Texture2D>("img/powerbar-fill");
            powerbarEmpty = content.Load<Texture2D>("img/powerbar-empty");
            autoStatus = content.Load<Texture2D>("img/auto-status");
            popupPlusOne = content.Load<Texture2D>("img/plus-one-status");
            popupMinusOne = content.Load<Texture2D>("img/minus-one-status");
            popupNewFrog = content.Load<Texture2D>("img/new-frog-status");
            popupNewBadFrog = content.Load<Texture2D>("img/new-badfrog-status");
            frogPortrait = content.Load<Texture2D>("img/frog-portrait");
            frogBar = content.Load<Texture2D>("img/frog-bar");
            badFrogPortrait = content.Load<Texture2D>("img/bad-frog-portrait");
            badFrogBar = content.Load<Texture2D>("img/bad-frog-bar");
            cursor = content.Load<Texture2D>("img/cursor");
            cursorBounding = content.Load<Texture2D>("img/cursor-bounding");

            title = content.Load<Texture2D>("img/title");
            end = content.Load<Texture2D>("img/end");
            victory = content.Load<Texture2D>("img/victory");

            font = content.Load<SpriteFont>("font");
        }

    }
}
