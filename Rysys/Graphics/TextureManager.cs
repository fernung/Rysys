using Microsoft.Xna.Framework.Graphics;
using Rysys.Actors;

namespace Rysys.Graphics
{
    public static class TextureManager
    {
        public static Texture2D Load(ActorType actor) => Load($"Textures\\{actor.ToString()}");
        public static Texture2D Load(string path) => Settings.Content.Load<Texture2D>(path);

        public static Texture2D Pixel { get; private set; } = Load(ActorType.Pixel);
        public static Texture2D Player { get; private set; } = Load(ActorType.Player);
        public static Texture2D Seeker { get; private set; } = Load(ActorType.Seeker);
        public static Texture2D Wanderer { get; private set; } = Load(ActorType.Wanderer);
        public static Texture2D BlackHole { get; private set; } = Load(ActorType.BlackHole);
        public static Texture2D Bullet { get; private set; } = Load(ActorType.Bullet);
    }
}
