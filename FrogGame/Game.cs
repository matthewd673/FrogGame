using Microsoft.Xna.Framework;
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


        public static string debugOutput = "";

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

            GenerateMap();
            EntityManager.AddEntity(new Wall(60, 60, 8, 8));
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

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(53, 31, 52));

            Renderer.Render(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
