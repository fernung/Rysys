using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.Actors;

namespace Rysys.Graphics
{
    public static class TextureManager
    {
        public static Texture2D Load(ActorType actor) => Load($"Textures\\{actor.ToString()}");
        public static Texture2D Load(string path) => Settings.Content.Load<Texture2D>(path);

        public static Texture2D Pixel { get; private set; }
        public static Texture2D Player { get; private set; }
        public static Texture2D Seeker { get; private set; }
        public static Texture2D Wanderer { get; private set; }
        public static Texture2D BlackHole { get; private set; }
        public static Texture2D Bullet { get; private set; }

        public static void LoadContent()
        {
            Pixel = new Texture2D(Settings.Graphics.GraphicsDevice, 1, 1);
            Pixel.SetData(new[] { Color.White });
            Player = Load(ActorType.Player);
            Seeker = Load(ActorType.Seeker);
            Wanderer = Load(ActorType.Wanderer);
            BlackHole = Load(ActorType.BlackHole);
            Bullet = Load(ActorType.Bullet);
            
        }
    }
}
