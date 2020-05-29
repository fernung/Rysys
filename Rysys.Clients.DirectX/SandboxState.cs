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
        public IActor player1;
        public IActor player2;


        public SandboxState() : base() 
        {
            player1 = new Actor(ActorType.Player);
            player2 = new Actor(ActorType.Player);

            Components.Add(player1);
            Components.Add(player2);
        }

        public override void Initialize()
        {
            player1.Initialize();
            player1.AddComponent(new MouseInput());
            player1.AddComponent(new KeyboardInput());
            player1.AddComponent(new GamePadInput(PlayerIndex.One));
            player1.GetComponent<Kinematics>().Position = new Vector2(Settings.Width / 2, Settings.Height / 2) - player1.GetComponent<Sprite>().Size;
            player1.GetComponent<Sprite>().Color = Color.Red;

            player2.Initialize();
            player2.AddComponent(new MouseInput());
            player2.AddComponent(new KeyboardInput());
            player2.AddComponent(new GamePadInput(PlayerIndex.Two));
            player2.GetComponent<Kinematics>().Position = new Vector2(Settings.Width / 2, Settings.Height / 2) + player2.GetComponent<Sprite>().Size;
            player2.GetComponent<Kinematics>().Speed = 4.0f;
            player2.GetComponent<Sprite>().Color = Color.Blue;

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (player1.GetComponent<GamePadInput>().Pressed(Buttons.Back) ||
                player1.GetComponent<KeyboardInput>().Pressed(Keys.Escape))
                GameStateManager.RequestExit = true;

            Vector2 input = player1.GetComponent<GamePadInput>().LeftStickDirection() * player1.GetComponent<Kinematics>().Speed;
            player1.GetComponent<Kinematics>().Accelerate(input.X, input.Y);

            input = player2.GetComponent<GamePadInput>().LeftStickDirection() * player2.GetComponent<Kinematics>().Speed;
            player2.GetComponent<Kinematics>().Accelerate(input.X, input.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.Begin();
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
