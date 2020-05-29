using Microsoft.Xna.Framework.Graphics;
using Rysys.ECS;
using Rysys.Physics;

namespace Rysys.Graphics
{
    public interface IRender : IComponent
    {
        ISprite Sprite { get; }
        ITransform Transform { get; }
        SpriteEffects Effect { get; set; }
        int Layer { get; set; }

        void Set(ISprite sprite, ITransform transform);
        void Draw(SpriteBatch spriteBatch);
    }

    public class Render : Component, IRender
    {
        public ISprite Sprite { get; protected set; }
        public ITransform Transform { get; protected set; }
        public SpriteEffects Effect { get; set; }
        public int Layer { get; set; }

        public Render(ISprite sprite, ITransform transform) : base()
        {
            Set(sprite, transform);
        }

        public void Set(ISprite sprite, ITransform transform)
        {
            Sprite = sprite;
            Transform = transform;
            Effect = SpriteEffects.None;
            Layer = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw
            (
                Sprite.Texture, 
                Transform.Position, 
                Sprite.Bounds, 
                Sprite.Color,
                Transform.Orientation, 
                Sprite.Origin, 
                Transform.Scale, 
                Effect, 
                Layer
            );
        }
    }
}
