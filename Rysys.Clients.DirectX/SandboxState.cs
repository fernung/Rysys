using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rysys.Actors;
using Rysys.Client;
using Rysys.Graphics;
using Rysys.Input;
using Rysys.Particles;
using Rysys.Physics;
using System;
using KeyboardInput = Rysys.Input.KeyboardInput;

namespace Rysys.Clients.DirectX
{
    public class SandboxState : GameState
    {
        private const int DefaultMaxPoints = 1800;

        public IPlayer player1;
        public IPlayer player2;
        public IGrid grid;
        public IParticleManager particles;

        public SandboxState() : base() 
        {
            
        }

        public override void Initialize()
        {
            grid = new Grid(Settings.Bounds, new Vector2((float)Math.Sqrt(Settings.Bounds.Width * Settings.Bounds.Height / DefaultMaxPoints)));
            player1 = new Player(TextureType.Player, PlayerIndex.One);
            player2 = new Player(TextureType.Player, PlayerIndex.Two);
            particles = new ParticleManager(1024 * 20, ParticleState.Update);

            Components.Add(grid);
            Components.Add(player1);
            Components.Add(player2);
            Components.Add(particles);

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
            
            if (player1.GetComponent<GamePadInput>().Pressed(Buttons.LeftTrigger) ||
                player1.GetComponent<GamePadInput>().Pressed(Buttons.RightTrigger))
            {
                particles.Create(TextureManager.Laser, player1.GetComponent<Kinematics>().Position, 180, 1200, ParticleType.None, 0.0f);
                grid.ApplyImplosiveForce(500, player1.GetComponent<Kinematics>().Position, 50);
            }

            if (player2.GetComponent<GamePadInput>().Pressed(Buttons.LeftTrigger) ||
                player2.GetComponent<GamePadInput>().Pressed(Buttons.RightTrigger))
            {
                particles.Create(TextureManager.Laser, player2.GetComponent<Kinematics>().Position, 60, 1200, ParticleType.None, 0.0f);
                grid.ApplyExplosiveForce(500, player2.GetComponent<Kinematics>().Position, 50);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            grid.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(spriteBatch);

            spriteBatch.Begin();
            particles.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
