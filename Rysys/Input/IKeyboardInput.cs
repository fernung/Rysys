using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rysys.ECS;

namespace Rysys.Input
{
    public interface IKeyboardInput : IComponent
    {
        KeyboardState Current { get; }
        KeyboardState Previous { get; }

        bool Pressed(Keys key);
        bool Released(Keys key);
        bool Held(Keys key);
    }
    public class KeyboardInput : Component, IKeyboardInput
    {
        public KeyboardState Current { get; protected set; }
        public KeyboardState Previous { get; protected set; }

        public override void Update(GameTime gameTime)
        {
            Previous = Current;
            Current = Keyboard.GetState();
            base.Update(gameTime);
        }

        public bool Pressed(Keys key) => Previous.IsKeyUp(key) && Current.IsKeyDown(key);
        public bool Released(Keys key) => Previous.IsKeyDown(key) && Current.IsKeyUp(key);
        public bool Held(Keys key) => Previous.IsKeyDown(key) && Current.IsKeyDown(key);
    }
}
