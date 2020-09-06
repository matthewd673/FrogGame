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

        public static KeyboardState lastKeyState;
        public static MouseState lastMouseState;

        public static int mouseX;
        public static int mouseY;

        public static void UpdateInput()
        {

            lastKeyState = keyState;
            lastMouseState = mouseState;

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

        public static bool MouseHeld()
        {
            return mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool MouseJustReleased()
        {
            return mouseState.LeftButton == ButtonState.Released && lastMouseState.LeftButton == ButtonState.Pressed;
        }

        public static void FlingFrog()
        {
            Frog f = (Frog) EntityManager.FindFirstEntityOfType(typeof(Frog));

            if (f == null)
                return;

            bool mouseOnFrog = CollisionSolver.IsPointInBounds(f.x, f.y, f.width, f.height, mouseX, mouseY);

        }

    }
}
