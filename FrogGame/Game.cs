using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FrogGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int score;

        public static int velocityFreezeMaxCooldown = 10;
        public static int velocityFreezeCooldown;
        public static bool velocityFrozen = false;

        public static string debugOutput = "";

        public enum GameState
        {
            Title,
            Game,
            End,
            Victory,
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
        }

        protected override void Update(GameTime gameTime)
        {

            if (state == GameState.Game)
            {

                if (velocityFreezeCooldown > 0)
                {
                    velocityFreezeCooldown--;
                    velocityFrozen = true;
                }

                if (velocityFreezeCooldown <= 0)
                    velocityFrozen = false;

                Spawner.Update();
                InputManager.UpdateInput();
                EntityManager.Update();
                CursorManager.Update();
                Renderer.cam.Update();

                if (Frog.teamCount <= 0)
                    LoadEndScreen();

                if (Frog.points >= 6)
                    LoadWinScreen();
            }

            if (state == GameState.Title)
            {
                InputManager.UpdateInput();
            }

            if (state == GameState.End)
            {
                InputManager.UpdateInput();
            }

            if (state == GameState.Victory)
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

        public static void LoadWinScreen()
        {
            state = GameState.Victory;
        }

        public static void InitializeNewGame()
        {
            state = GameState.Game;

            EntityManager.Clear();
            score = 0;

            EntityManager.AddEntity(new Frog(20, 20));
            Frog.teamCount = 1;

            GenerateMap();
            //EntityManager.AddEntity(new Wall(60, 60, 8, 8));
        }

        static void GenerateMap()
        {
            
            for(int i = 0; i < 25; i++)
            {
                EntityManager.AddEntity(new Wall(i * 8, 0, 8, 8));
                EntityManager.AddEntity(new Wall(i * 8, 142, 8, 8));
            }


            /*
            EntityManager.AddEntity(new Wall(0, 0, 200, 8));
            EntityManager.AddEntity(new Wall(120, 0, 200, 8));

            EntityManager.AddEntity(new Wall(0, 0, 8, 150));
            EntityManager.AddEntity(new Wall(192, 0, 8, 150));
            */


            for (int j = 0; j < 19; j++)
            {
                EntityManager.AddEntity(new Wall(0, j * 8, 8, 8));
                EntityManager.AddEntity(new Wall(192, j * 8, 8, 8));
            }
        }

        public static void FreezeVelocity()
        {
            velocityFreezeCooldown = velocityFreezeMaxCooldown;
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(53, 31, 52));

            Renderer.Render(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
