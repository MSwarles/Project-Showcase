using Microsoft.Xna.Framework;

using SuperMetroid.ActorComponents;

namespace SuperMetroid.PlayerStates
{
    /// <summary>
    /// State used by graphics component for handling the player's ball animations and transitions.
    /// </summary>
    public class BallState : IState
    {
        public bool IsDoneTransitioning { get; set; }

        public BallState()
        {
        }

        #region XNA Methods
        public void Update(GameTime gameTime, Actor player)
        {
            PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
            PlayerInputComponent input = (PlayerInputComponent)player.Input;
            PlayerPhysicsComponent physics = (PlayerPhysicsComponent)player.Physics;

            if (graphics.NextState != null)
            {
                OnExit(player);
            }
            else if ((input.IsLeftKeyPressed && graphics.IsFacingRight) || (input.IsRightKeyPressed && !graphics.IsFacingRight))
            {
                graphics.IsFacingRight = !graphics.IsFacingRight;
                graphics.ChangeFacing = false;
            }
            else if (physics.State is State.Crouching)
            {
                graphics.NextState = new CrouchingState();
            }
            else 
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "BallRight" : "BallLeft");
            }
        }
        #endregion

        public void OnExit(Actor player)
        {
            PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
            if (graphics.NextState is CrouchingState)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "TransCrouchFromBallRight" : "TransCrouchFromBallLeft");

                if ((graphics.IsDoneAnimating ?? true) == true)
                {
                    IsDoneTransitioning = true;
                }
            }
        }

        public override string ToString()
        {
            return "Ball";
        }
    }
}
