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
        PlayerIndex Index { get; }
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
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 input = GetComponent<GamePadInput>().LeftStickDirection() * GetComponent<Kinematics>().Speed;
            GetComponent<Kinematics>().Accelerate(input.X, input.Y);

            GetComponent<Kinematics>().Position = Vector2.Clamp
            (
                GetComponent<Kinematics>().Position,
                GetComponent<Sprite>().Size * 2,
                Settings.WorldSize - GetComponent<Sprite>().Size * 2
            );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
