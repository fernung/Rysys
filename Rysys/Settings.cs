using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Rysys
{
    public static class Settings
    {
        public static ContentManager Content { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }

        public static int Width
        {
            get => Graphics.GraphicsDevice.Viewport.Width;
            set => Graphics.PreferredBackBufferWidth = value;
        }
        public static int Height
        {
            get => Graphics.GraphicsDevice.Viewport.Height;
            set => Graphics.PreferredBackBufferHeight = value;
        }
        public static Rectangle Bounds
        {
            get => Graphics.GraphicsDevice.Viewport.Bounds;
        }
        public static bool FullScreen
        {
            get => Graphics.IsFullScreen;
            set => Graphics.IsFullScreen = value;
        }

        public static void Initialize(ContentManager content, GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;
        }
    }
}
