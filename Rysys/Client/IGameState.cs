using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rysys.ECS;
using Rysys.Graphics;
using Rysys.Particles;
using Rysys.Physics;
using System.Collections.Generic;

namespace Rysys.Client
{
    public interface IGameState
    {
        Viewport Viewport { get; }
        Vector2 ScreenSize { get; }

        List<IComponent> Components { get; }

        void Initialize();
        void LoadContent(ContentManager content);
        void UnloadContent(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }

    public class GameState : IGameState
    {
        protected GraphicsDevice Graphics { get; set; }
        public Viewport Viewport { get => Graphics.Viewport; }
        public Vector2 ScreenSize { get => new Vector2(Graphics.Viewport.Width, Graphics.Viewport.Height); }

        public List<IComponent> Components { get; private set; }

        public GameState() 
        { 
            Components = new List<IComponent>();
        }

        public virtual void Initialize() { Graphics = Settings.Graphics.GraphicsDevice; }
        public virtual void LoadContent(ContentManager content) { }
        public virtual void UnloadContent(ContentManager content) { }
        public virtual void Update(GameTime gameTime) 
        { 
            foreach(var component in Components)
            {
                component.Entity.Update(gameTime);
                component.Update(gameTime);
            }
        }
        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.Additive);
            foreach (var component in Components)
            {
                if(component.HasComponent<Render>())
                {
                    component.GetComponent<Render>().Draw(spriteBatch);
                }
                if(component.HasComponent<ParticleManager>())
                {
                    component.GetComponent<ParticleManager>().Draw(spriteBatch);
                }
            }
            spriteBatch.End();
        }
    }
}
