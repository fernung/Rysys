using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Rysys
{
    public static class Settings
    {
        public const int DefaultScreenWidth = 1280;
        public const int DefaultScreenHeight = 720;
        public static ContentManager Content { get; private set; }
        public static GraphicsDeviceManager Graphics { get; private set; }
        public static Viewport Viewport { get => Graphics.GraphicsDevice.Viewport; }

        public static int Width
        {
            get => Graphics.PreferredBackBufferWidth;
            set => Graphics.PreferredBackBufferWidth = value;
        }
        public static int Height
        {
            get => Graphics.PreferredBackBufferHeight;
            set => Graphics.PreferredBackBufferHeight = value;
        }
        public static Vector2 ScreenSize 
        { get => new Vector2(Graphics.GraphicsDevice.Viewport.Width, Graphics.GraphicsDevice.Viewport.Height); }
        public static Vector2 WorldSize { get; set; } = Vector2.Zero;
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
