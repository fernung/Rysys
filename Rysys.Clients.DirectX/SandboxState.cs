using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rysys.Actors;
using Rysys.Client;
using Rysys.Graphics;
using Rysys.Input;
using Rysys.Physics;

using KeyboardInput = Rysys.Input.KeyboardInput;

namespace Rysys.Clients.DirectX
{
    public class SandboxState : GameState
    {
        public IPlayer player1;
        public IPlayer player2;


        public SandboxState() : base() 
        {
            player1 = new Player(ActorType.Player, PlayerIndex.One);
            player2 = new Player(ActorType.Player, PlayerIndex.Two);

            Components.Add(player1);
            Components.Add(player2);
        }

        public override void Initialize()
        {
            player1.Initialize();
            player1.GetComponent<Kinematics>().Position = new Vector2(Settings.Width / 2, Settings.Height / 2) - player1.GetComponent<Sprite>().Size;
            player1.GetComponent<Sprite>().Color = Color.Red;

            player2.Initialize();
            player2.GetComponent<Kinematics>().Position = new Vector2(Settings.Width / 2, Settings.Height / 2) + player2.GetComponent<Sprite>().Size;
            player2.GetComponent<Sprite>().Color = Color.Blue;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (player1.GetComponent<GamePadInput>().Pressed(Buttons.Back) ||
                player1.GetComponent<KeyboardInput>().Pressed(Keys.Escape))
                GameStateManager.RequestExit = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
