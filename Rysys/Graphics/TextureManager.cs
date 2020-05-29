using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.Actors;

namespace Rysys.Graphics
{
    public static class TextureManager
    {
        public static Texture2D Load(TextureType actor) => Load($"Textures\\{actor.ToString()}");
        public static Texture2D Load(string path) => Settings.Content.Load<Texture2D>(path);

        public static Texture2D Pixel { get; private set; }
        public static Texture2D Player { get; private set; }
        public static Texture2D Seeker { get; private set; }
        public static Texture2D Wanderer { get; private set; }
        public static Texture2D BlackHole { get; private set; }
        public static Texture2D Bullet { get; private set; }
        public static Texture2D Laser { get; private set; }
        public static Texture2D Glow { get; private set; }

        public static void LoadContent()
        {
            Pixel = new Texture2D(Settings.Graphics.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
            Player = Load(TextureType.Player);
            Seeker = Load(TextureType.Seeker);
            Wanderer = Load(TextureType.Wanderer);
            BlackHole = Load(TextureType.BlackHole);
            Bullet = Load(TextureType.Bullet);
            Laser = Load(TextureType.Laser);
            Glow = Load(TextureType.Glow);
        }
    }

    public enum TextureType
    {
        Pixel,
        Player,
        Seeker,
        Wanderer,
        BlackHole,
        Bullet,
        Laser,
        Glow
    }
}
