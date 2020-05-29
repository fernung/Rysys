using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.ECS;

namespace Rysys.Graphics
{
    public interface ISprite : IComponent
    {
        Texture2D Texture { get; }
        Rectangle Bounds { get; }
        Vector2 Size { get; }
        Vector2 Origin { get; set; }
        Rectangle Source { get; set; }
        Color Color { get; set; }
        float Orientation { get; set; }
        float Depth { get; set; }
        int Scale { get; set; }

        void LoadContent(string path);
        void LoadContent(Texture2D texture);
    }

    public class Sprite : Component, ISprite
    {
        public Texture2D Texture { get; protected set; }
        public Rectangle Bounds { get; protected set; }
        public Vector2 Size { get; protected set; }
        public Vector2 Origin { get; set; }
        public Rectangle Source { get; set; }
        public Color Color { get; set; }
        public float Orientation { get; set; }
        public float Depth { get; set; }
        public int Scale { get; set; }

        public Sprite(string path) : base() { LoadContent(path); }
        public Sprite(Texture2D texture) : base() { LoadContent(texture); }
        public Sprite() : base()
        {
            Texture = new Texture2D(Settings.Graphics.GraphicsDevice, 1, 1);
            Texture.SetData(new[] { Color.White });
            LoadContent(Texture);
        }

        public void LoadContent(string path) => LoadContent(TextureManager.Load(path));
        public void LoadContent(Texture2D texture)
        {
            Texture = texture;
            Bounds = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Size = new Vector2(Texture.Width, Texture.Height);
            Origin = new Vector2(0, 0);
            Source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Color = Color.White;
            Orientation = 0.0f;
            Depth = 0.0f;
            Scale = 1;
        }
    }
}
