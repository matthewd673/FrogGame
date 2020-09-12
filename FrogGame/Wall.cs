using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Wall : Entity
    {

        public Wall(float x, float y, int width, int height) : base(Sprites.wall, x, y, width, height)
        {
            col = new Collider(0, 0, width, height);
        }

    }
}
