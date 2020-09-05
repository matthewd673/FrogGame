using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Collider
    {

        float offX;
        float offY;
        int width;
        int height;

        public Collider(float offX, float offY, int width, int height)
        {
            this.offX = offX;
            this.offY = offY;
            this.width = width;
            this.height = height;
        }

    }
}
