using Microsoft.Xna.Framework;
using Rysys.Input;
using Rysys.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rysys.Actors
{
    public interface IPlayer : IActor
    {

    }

    public class Player : Actor, IPlayer
    {
        public PlayerIndex Index { get; protected set; }

        public Player(ActorType type, PlayerIndex index) : base(type)
        {
            Index = index;
        }

        public override void Initialize()
        {
            base.Initialize();

            AddComponent(new MouseInput());
            AddComponent(new KeyboardInput());
            AddComponent(new GamePadInput(Index));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 input = GetComponent<GamePadInput>().LeftStickDirection() * GetComponent<Kinematics>().Speed;
            GetComponent<Kinematics>().Accelerate(input.X, input.Y);
        }
    }
}
