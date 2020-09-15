using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public class Animation
    {

        public Texture2D spriteSheet;
        public List<Texture2D> frames;
        int frame;
        public int maxDelay;
        public int delayCt;

        public bool loop = true;

        bool doneLoop = false;

        public Animation(Texture2D spriteSheet, int spriteCount, int delay)
        {
            this.spriteSheet = spriteSheet;
            maxDelay = delay;

            //create list of frames
            int spriteW = (spriteSheet.Width / spriteCount);
            for (int i = 0; i < spriteCount; i++)
            {
                int x = i * spriteW;
                int y = 0;
                int w = spriteW;
                int h = spriteSheet.Height;

                Texture2D cropped = new Texture2D(Game._graphics.GraphicsDevice, w, h);

                Color[] data = new Color[w * h];
                spriteSheet.GetData(0, new Rectangle(x, y, w, h), data, 0, w * h);

                cropped.SetData(data);

                frames.Add(cropped);
            }
        }

        public Texture2D Animate()
        {

            //if a non-looping animation and its done, don't bother counting
            if (doneLoop)
                return frames[frame];

            delayCt++;

            if (delayCt >= maxDelay)
            {
                delayCt = 0;
                FrameForward();
            }

            return frames[frame];

        }

        public void FrameForward()
        {

            frame++;

            if (loop)
            {
                if (frame >= frames.Count)
                    frame = 0;
            }
            else
            {
                if (frame >= frames.Count)
                {
                    frame = frames.Count - 1;
                    doneLoop = true;
                }
            }

        }

    }
}
