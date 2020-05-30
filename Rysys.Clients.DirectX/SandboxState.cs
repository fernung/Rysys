using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rysys.Actors;
using Rysys.Client;
using Rysys.Graphics;
using Rysys.Input;
using Rysys.Particles;
using Rysys.Physics;
using Rysys.Extensions;
using System;
using KeyboardInput = Rysys.Input.KeyboardInput;

namespace Rysys.Clients.DirectX
{
    public class SandboxState : GameState
    {
        private const int DefaultMaxPoints = 3600;

        private Viewport _defaultViewport;
        private Viewport _leftViewport;
        private Viewport _rightViewport;

        public IPlayer player1;
        public IPlayer player2;
        public ICamera2D camera1;
        public ICamera2D camera2;
        public IGrid grid;
        public IParticleManager particles;

        public SandboxState() : base() 
        {
            
        }

        public override void Initialize()
        {
            base.Initialize();

            _defaultViewport = Graphics.Viewport;
            _leftViewport = _defaultViewport;
            _rightViewport = _defaultViewport;
            _leftViewport.Width = _defaultViewport.Width / 2;
            _rightViewport.Width = _defaultViewport.Width / 2;
            _rightViewport.X = _defaultViewport.Width / 2;

            Settings.WorldSize = Settings.ScreenSize * 2;
            grid = new Grid(new Rectangle(0, 0, (int)Settings.WorldSize.X, (int)Settings.WorldSize.Y), 
                            new Vector2((float)Math.Sqrt(Settings.WorldSize.X * Settings.WorldSize.Y / DefaultMaxPoints)));
            player1 = new Player(TextureType.Player, PlayerIndex.One);
            player2 = new Player(TextureType.Player, PlayerIndex.Two); 
            camera1 = new Camera2D(_leftViewport);
            camera2 = new Camera2D(_rightViewport);
            particles = new ParticleManager(1024 * 20, ParticleState.Update);

            Components.Add(grid);
            Components.Add(player1);
            Components.Add(player2);
            Components.Add(camera1);
            Components.Add(camera2);
            Components.Add(particles);

            player1.Initialize();
            player1.GetComponent<Kinematics>().Position = Settings.WorldSize / 2 - player1.GetComponent<Sprite>().Size;
            player1.GetComponent<Sprite>().Color = Color.Red;

            player2.Initialize();
            player2.GetComponent<Kinematics>().Position = Settings.WorldSize / 2 + player2.GetComponent<Sprite>().Size;
            player2.GetComponent<Sprite>().Color = Color.Blue;

            camera1.Origin = camera1.Size / 2;
            camera2.Origin = camera2.Size / 2; 
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
                particles.Create(TextureManager.Laser, player1.GetComponent<Kinematics>().Position, 60, 300, ParticleType.None, 0.0f);
                grid.ApplyImplosiveForce(750, player1.GetComponent<Kinematics>().Position, 50);
            }

            if (player2.GetComponent<GamePadInput>().Pressed(Buttons.LeftTrigger) ||
                player2.GetComponent<GamePadInput>().Pressed(Buttons.RightTrigger))
            {
                particles.Create(TextureManager.Laser, player2.GetComponent<Kinematics>().Position, 60, 300, ParticleType.None, 0.0f);
                grid.ApplyExplosiveForce(750, player2.GetComponent<Kinematics>().Position, 50);
            }

            camera1.Position = player1.GetComponent<Kinematics>().Position;
            camera2.Position = player2.GetComponent<Kinematics>().Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            /*
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            grid.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            particles.Draw(spriteBatch);
            spriteBatch.End();
            */

            Graphics.Viewport = _leftViewport;
            Draw(spriteBatch, camera1);
            Graphics.Viewport = _rightViewport;
            Draw(spriteBatch, camera2);
            Graphics.Viewport = _defaultViewport;
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            spriteBatch.DrawLine(new Vector2(Settings.Width / 2, 0), new Vector2(Settings.Width / 2, Settings.Height), Color.White, 2.0f);
            spriteBatch.End();
        }

        private void Draw(SpriteBatch spriteBatch, ICamera2D camera)
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive, transformMatrix: camera.TransformationMatrix);
            grid.Draw(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            particles.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
