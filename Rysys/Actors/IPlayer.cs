using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rysys.Graphics;
using Rysys.Input;
using Rysys.Particles;
using Rysys.Physics;

using KeyboardInput = Rysys.Input.KeyboardInput;

namespace Rysys.Actors
{
    public interface IPlayer : IActor
    {

    }

    public class Player : Actor, IPlayer
    {
        public PlayerIndex Index { get; protected set; }

        public Player(TextureType type, PlayerIndex index) : base(type)
        {
            Index = index;
        }

        public override void Initialize()
        {
            base.Initialize();

            AddComponent(new MouseInput());
            AddComponent(new KeyboardInput());
            AddComponent(new GamePadInput(Index));
            AddComponent(new ParticleManager(1024 * 10, ParticleState.Update));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 input = GetComponent<GamePadInput>().LeftStickDirection() * GetComponent<Kinematics>().Speed;
            GetComponent<Kinematics>().Accelerate(input.X, input.Y);

            if(GetComponent<GamePadInput>().Pressed(Buttons.LeftTrigger) ||
                GetComponent<GamePadInput>().Pressed(Buttons.RightTrigger))
            {
                GetComponent<ParticleManager>().Create(TextureManager.Pixel, GetComponent<Kinematics>().Position, Color.White, 120);
            }

            GetComponent<ParticleManager>().Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
