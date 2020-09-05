using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace FrogGame
{
    public static class InputManager
    {

        public static KeyboardState keyState;
        public static MouseState mouseState;

        public static int mouseX;
        public static int mouseY;

        public static void UpdateInput()
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            mouseX = mouseState.X;
            mouseY = mouseState.Y;

            if(Game.state == Game.GameState.Title)
            {
                if (keyState.IsKeyDown(Keys.Enter))
                    Game.InitializeNewGame();
            }

            if(Game.state == Game.GameState.End)
            {
                if (keyState.IsKeyDown(Keys.Enter))
                    Game.InitializeNewGame();
            }

        }

    }
}
