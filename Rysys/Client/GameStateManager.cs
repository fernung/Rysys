using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Rysys.Client
{
    public static class GameStateManager
    {
        public static Stack<IGameState> Screens { get; private set; } = new Stack<IGameState>();
        public static IGameState Current { get => HasScreen ? Screens.Peek() : null; }
        public static bool HasScreen { get => Screens.Count > 0; }
        public static bool RequestExit { get; set; } = false;

        public static void Add(IGameState screen)
        {
            Screens.Push(screen); 
            Current.Initialize();
            Current.LoadContent(Settings.Content);
        }
        public static void Remove()
        {
            if(HasScreen)
            {
                Current.UnloadContent(Settings.Content);
                Screens.Pop();
            }
        }
        public static void Clear()
        {
            while (HasScreen) Remove();
        }
        public static void Change(IGameState screen)
        {
            Clear();
            Add(screen);
        }

        public static void Update(GameTime gameTime)
        {
            if(HasScreen)
            {
                Current.Update(gameTime);
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if(HasScreen)
            {
                Current.Draw(spriteBatch);
            }
        }
    }
}
