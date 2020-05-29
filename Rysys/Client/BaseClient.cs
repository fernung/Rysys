using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.Graphics;

namespace Rysys.Client
{
    public class BaseClient : Game
    {
        private IGameState _state { get; set; }

        public GraphicsDeviceManager Graphics { get; protected set; }
        public SpriteBatch SpriteBatch { get; protected set; }

        public BaseClient(IGameState state)
        {
            _state = state;
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Settings.Initialize(Content, Graphics);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            TextureManager.LoadContent();
            GameStateManager.Add(_state);
        }

        protected override void UnloadContent()
        {
            GameStateManager.Clear();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GameStateManager.RequestExit) Exit();
            GameStateManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GameStateManager.Draw(SpriteBatch);

            base.Draw(gameTime);
        }
    }
}
