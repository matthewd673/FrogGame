using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Wall : Entity
    {

        public Wall(float x, float y) : base(Sprites.wall, x, y, 8, 8)
        {
            col = new Collider(0, 0, 8, 8);
        }

    }
}
