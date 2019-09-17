using Microsoft.Xna.Framework;

using SuperMetroid.ActorComponents;

namespace SuperMetroid.PlayerStates
{
    /// <summary>
    /// State used by graphics component for handling the player's crouch animations and transitions.
    /// </summary>
    public class CrouchingState : IState
    {
        public bool IsDoneTransitioning { get; set; }

        public CrouchingState()
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
            else if (graphics.ChangeFacing)
            {
                if ((graphics.IsDoneAnimating ?? true) == false)
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "TransCrouchRightToLeft" : "TransCrouchLeftToRight");
                }
                else
                {
                    graphics.IsFacingRight = !graphics.IsFacingRight;
                    graphics.ChangeFacing = false;
                }
            }
            else if ((input.IsLeftKeyPressed && graphics.IsFacingRight) || (input.IsRightKeyPressed && !graphics.IsFacingRight))
            {
                graphics.ChangeFacing = true;
            }
            else if (input.IsAimHighKeyDown)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "CrouchAimHighRight" : "CrouchAimHighLeft");
            }
            else if (input.IsAimLowKeyDown)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "CrouchAimLowRight" : "CrouchAimLowLeft");
            }
            else if (physics.State == State.Standing)
            {
                graphics.NextState = new NormalState();
            }
            else if (physics.State == State.Ball)
            {
                graphics.NextState = new BallState();
            }
            else
            {
                graphics.ChangeAnimation((graphics.IsFacingRight ? "CrouchRight" : "CrouchLeft"));
            }
        }
        #endregion

        public void OnExit(Actor player)
        {
            PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
            if (graphics.NextState is NormalState)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "TransIdleFromCrouchRight" : "TransIdleFromCrouchLeft");
                if ((graphics.IsDoneAnimating ?? true) == true)
                {
                    IsDoneTransitioning = true;
                }
            }
            else if (graphics.NextState is BallState)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "TransBallFromCrouchRight" : "TransBallFromCrouchLeft");
                if ((graphics.IsDoneAnimating ?? true) == true)
                {
                    IsDoneTransitioning = true;
                }
            }
        }

        public override string ToString()
        {
            return "Crouching";
        }
    }
}
