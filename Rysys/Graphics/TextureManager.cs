using Microsoft.Xna.Framework.Graphics;
using Rysys.Actors;

namespace Rysys.Graphics
{
    public static class TextureManager
    {
        public static Texture2D Load(ActorType actor) => Load($"Textures\\{actor.ToString()}");
        public static Texture2D Load(string path) => Settings.Content.Load<Texture2D>(path);
    }
}
