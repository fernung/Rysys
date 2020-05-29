using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rysys.ECS;

namespace Rysys.Input
{
    public interface IGamePadInput : IComponent
    {
        PlayerIndex Index { get; }
        GamePadState Current { get; }
        GamePadState Previous { get; }

        bool Pressed(Buttons button);
        bool Released(Buttons button);
        bool Held(Buttons button);

        void Vibrate(float leftAmount, float rightAmount);
        Vector2 LeftStickDirection();
        Vector2 RightStickDirection();
        Vector2 StickDirection(Vector2 thumbstick);
    }

    public class GamePadInput : Component, IGamePadInput
    {
        public PlayerIndex Index { get; protected set; }
        public GamePadState Current { get; protected set; }
        public GamePadState Previous { get; protected set; }

        public GamePadInput() : this(PlayerIndex.One) { }
        public GamePadInput(PlayerIndex index) : base()
        {
            Index = index;
        }

        public override void Update(GameTime gameTime)
        {
            Previous = Current;
            Current = GamePad.GetState(Index);

            base.Update(gameTime);
        }

        public void Vibrate(float leftAmount, float rightAmount) => GamePad.SetVibration(Index, leftAmount, rightAmount);
        public bool Pressed(Buttons button) => Previous.IsButtonUp(button) && Current.IsButtonDown(button);
        public bool Released(Buttons button) => Previous.IsButtonDown(button) && Current.IsButtonUp(button);
        public bool Held(Buttons button) => Previous.IsButtonDown(button) && Current.IsButtonDown(button);

        public Vector2 LeftStickDirection() => StickDirection(Current.ThumbSticks.Left);
        public Vector2 RightStickDirection() => StickDirection(Current.ThumbSticks.Right);
        public Vector2 StickDirection(Vector2 thumbstick)
        {
            Vector2 direction = thumbstick;
            direction.Y *= -1;
            if (direction.LengthSquared() > 1) direction.Normalize();
            return direction;
        }
    }
}
