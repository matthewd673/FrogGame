using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public class CursorDebug : Entity
    {

        public CursorDebug() : base(Sprites.fly, 0, 0, 4, 4)
        {

        }

        public override void Update()
        {

            x = InputManager.mouseX / Renderer.cam.scale;
            y = InputManager.mouseY / Renderer.cam.scale;

            base.Update();
        }

    }
}
