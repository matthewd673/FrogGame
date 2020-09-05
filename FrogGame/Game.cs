﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FrogGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int score;
        public static int health;

        public enum GameState
        {
            Title,
            Game,
            End,
        }

        public static GameState state = GameState.Title;

        public Game()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //set window size
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

            LoadTitle();

            //InitializeNewGame();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Sprites.LoadTextures(Content);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            if (state == GameState.Game)
            {
                Spawner.Update();
                InputManager.UpdateInput();
                EntityManager.Update();
                Renderer.cam.Update();

                if(health == 0)
                {
                    LoadEndScreen();
                }

            }

            if (state == GameState.Title)
            {
                InputManager.UpdateInput();
            }

            if (state == GameState.Title)
            {
                InputManager.UpdateInput();
            }

            base.Update(gameTime);
        }

        public static void LoadTitle()
        {
            state = GameState.Title;
        }

        public static void LoadEndScreen()
        {
            state = GameState.End;
        }

        public static void InitializeNewGame()
        {
            state = GameState.Game;

            EntityManager.Clear();
            score = 0;
            health = 3;

            EntityManager.AddEntity(new Frog(20, 20));
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(53, 31, 52));

            Renderer.Render(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
