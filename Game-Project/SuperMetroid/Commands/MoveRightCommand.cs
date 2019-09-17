using Microsoft.Xna.Framework;

using SuperMetroid.ActorComponents;

namespace SuperMetroid.Commands
{
    /// <summary>
    /// Command used to make an actor move right.
    /// </summary>
    public class MoveRightCommand : ICommand
    {
        public void Execute(Actor actor)
        {
            PlayerPhysicsComponent physics = (PlayerPhysicsComponent)actor.Physics;

            if (physics.Velocity.X <= 0)
            {
                physics.Velocity = new Vector2(physics.BASE_RUN_SPEED, physics.Velocity.Y);
            }
        }
    }
}
