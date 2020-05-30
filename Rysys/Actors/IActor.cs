using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rysys.ECS;
using Rysys.Graphics;
using Rysys.Physics;

namespace Rysys.Actors
{
    public interface IActor : IComponent
    {
        TextureType Type { get; }
        bool Alive { get; set; }

        void Draw(SpriteBatch spriteBatch);
    }
    public class Actor : Component, IActor
    {
        public TextureType Type { get; protected set; }
        public bool Alive { get; set; }

        public Actor(TextureType type) : base()
        {
            Type = type;
            Alive = true;
        }

        public override void Initialize()
        {
            Entity.Component(new Kinematics());
            Entity.Component(new Sprite(TextureManager.Load(Type)));
            Entity.Component
            (
                new Render(Entity.Component<Sprite>(), 
                Entity.Component<Kinematics>())
            );

            Entity.Component<Sprite>().Origin = Entity.Component<Sprite>().Size / 2;

            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Entity.Component<Kinematics>().Position = Vector2.Clamp
            (
                Entity.Component<Kinematics>().Position,
                Entity.Component<Sprite>().Size * 2,
                Settings.WorldSize - Entity.Component<Sprite>().Size * 2
            );
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Entity.Component<Render>().Draw(spriteBatch);
        }
    }
}
