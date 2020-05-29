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
        bool Connected { get; }

        bool Pressed(Buttons button);
        bool Released(Buttons button);
        bool Held(Buttons button);

        bool Vibrate(float leftAmount, float rightAmount);
        Vector2 LeftStickDirection();
        Vector2 RightStickDirection();
        Vector2 StickDirection(Vector2 thumbstick);
    }

    public class GamePadInput : Component, IGamePadInput
    {
        public PlayerIndex Index { get; protected set; }
        public GamePadState Current { get; protected set; }
        public GamePadState Previous { get; protected set; }
        public bool Connected { get => Current.IsConnected; }

        public GamePadInput() : this(PlayerIndex.One) { }
        public GamePadInput(PlayerIndex index) : base()
        {
            Index = index;
            Previous = GamePad.GetState(index);
            Current = GamePad.GetState(index);
        }

        public override void Update(GameTime gameTime)
        {
            if(Connected)
            {
                Previous = Current;
                Current = GamePad.GetState(Index);
            }

            base.Update(gameTime);
        }

        public bool Vibrate(float leftAmount, float rightAmount) => Connected ? GamePad.SetVibration(Index, leftAmount, rightAmount) : false;
        public bool Pressed(Buttons button) => Connected ? Previous.IsButtonUp(button) && Current.IsButtonDown(button) : false;
        public bool Released(Buttons button) => Connected ? Previous.IsButtonDown(button) && Current.IsButtonUp(button) : false;
        public bool Held(Buttons button) => Connected ? Previous.IsButtonDown(button) && Current.IsButtonDown(button) : false;

        public Vector2 LeftStickDirection() => Connected ? StickDirection(Current.ThumbSticks.Left) : Vector2.Zero;
        public Vector2 RightStickDirection() => Connected ? StickDirection(Current.ThumbSticks.Right) : Vector2.Zero;
        public Vector2 StickDirection(Vector2 thumbstick)
        {
            Vector2 direction = thumbstick;
            direction.Y *= -1;
            if (direction.LengthSquared() > 1) direction.Normalize();
            return direction;
        }
    }
}
