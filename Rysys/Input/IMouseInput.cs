using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rysys.ECS;

namespace Rysys.Input
{
    public interface IMouseInput : IComponent
    {
        MouseState Current { get; }
        MouseState Previous { get; }

        Vector2 Position { get; }
        Vector2 OldPosition { get; }

        bool Pressed(MouseButton button);
        bool Released(MouseButton button);
        bool Held(MouseButton button);
    }

    public class MouseInput : Component, IMouseInput
    {
        public MouseState Current { get; protected set; }
        public MouseState Previous { get; protected set; }

        public Vector2 Position { get => new Vector2(Current.X, Current.Y); }
        public Vector2 OldPosition { get => new Vector2(Previous.X, Previous.Y); }

        public override void Update(GameTime gameTime)
        {
            Previous = Current;
            Current = Mouse.GetState();

            base.Update(gameTime);
        }

        public bool Pressed(MouseButton button) => 
            PreviousState(button) == ButtonState.Released && 
            CurrentState(button) == ButtonState.Pressed;
        public bool Released(MouseButton button) =>
            PreviousState(button) == ButtonState.Pressed &&
            CurrentState(button) == ButtonState.Released;
        public bool Held(MouseButton button) =>
            PreviousState(button) == ButtonState.Pressed &&
            CurrentState(button) == ButtonState.Pressed;

        private ButtonState CurrentState(MouseButton button) => button == MouseButton.Left ? Current.LeftButton :
                                                                button == MouseButton.Right ? Current.RightButton :
                                                                Current.MiddleButton;
        private ButtonState PreviousState(MouseButton button) => button == MouseButton.Left ? Previous.LeftButton :
                                                                 button == MouseButton.Right ? Previous.RightButton :
                                                                 Previous.MiddleButton;
    }
}
