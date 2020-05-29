using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rysys.Actors;
using Rysys.Client;
using Rysys.Input;
using Rysys.Physics;

using KeyboardInput = Rysys.Input.KeyboardInput;

namespace Rysys.Clients.DirectX
{
    public class SandboxState : GameState
    {
        public IActor player;


        public SandboxState() : base() 
        {
            player = new Actor(ActorType.Player);
            Components.Add(player);
        }

        public override void Initialize()
        {
            player.Initialize();
            player.AddComponent(new MouseInput());
            player.AddComponent(new KeyboardInput());
            player.AddComponent(new GamePadInput(PlayerIndex.One));
            player.GetComponent<Transform>().Position = new Vector2(Settings.Width / 2, Settings.Height / 2);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (player.GetComponent<GamePadInput>().Pressed(Buttons.Back) ||
                player.GetComponent<KeyboardInput>().Pressed(Keys.Escape))
                GameStateManager.RequestExit = true;

            player.GetComponent<Transform>().Position = player.GetComponent<MouseInput>().Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
