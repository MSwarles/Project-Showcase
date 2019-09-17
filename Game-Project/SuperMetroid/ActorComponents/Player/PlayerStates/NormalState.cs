using Microsoft.Xna.Framework;

using SuperMetroid.ActorComponents;

namespace SuperMetroid.PlayerStates
{
    /// <summary>
    /// State used by graphics component for handling the player's standing / running animations and transitions.
    /// </summary>
    public class NormalState : IState
    {
        private bool m_hasShot;
        public bool IsDoneTransitioning { get; set; }

        public NormalState()
        {
        }

        #region XNA Methods
        public void Update(GameTime gameTime, Actor player)
        {
            PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
            PlayerInputComponent input = (PlayerInputComponent)player.Input;
            PlayerPhysicsComponent physics = (PlayerPhysicsComponent)player.Physics;

            if (input.HasShot)
            {
                m_hasShot = true;
            }
            
            if (physics.Velocity.X == 0)
            {
                m_hasShot = false;
            }

            if (graphics.NextState != null)
            {
                OnExit(player);
            }
            else if (graphics.ChangeFacing)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "TransIdleRightToLeft" : "TransIdleLeftToRight");

                if ((graphics.IsDoneAnimating ?? true) == true)
                {
                    graphics.IsFacingRight = !graphics.IsFacingRight;
                    graphics.ChangeFacing = false;
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "IdleRight" : "IdleLeft");
                }
            }
            else if ((physics.Velocity.X < 0 && graphics.IsFacingRight) || (physics.Velocity.X > 0 && !graphics.IsFacingRight))
            {
                graphics.ChangeFacing = true;
            }
            else if (physics.State == State.Crouching)
            {
                graphics.NextState = new CrouchingState();
            }
            else if (physics.Velocity.Y != 0)
            {
                if (physics.Velocity.Y < 0)
                {
                    graphics.NextState = new AirState(physics.Velocity.X != 0);
                }
                else if (physics.Velocity.Y > 0)
                {
                    graphics.NextState = new AirState(false);
                }
            }
            else if (((input.IsAimHighKeyDown && input.IsAimLowKeyDown) || input.IsUpKeyDown) &&
                physics.Velocity.X == 0)
            {
                if ((graphics.CurrentAnimationKey == "IdleAimHighRight" || graphics.CurrentAnimationKey == "IdleAimHighLeft") ||
                    (graphics.CurrentAnimationKey == "IdleAimUpRight" || graphics.CurrentAnimationKey == "IdleAimUpLeft"))
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "IdleAimUpRight" : "IdleAimUpLeft");
                }
                else if (graphics.CurrentAnimationKey != "IdleAimUpRight" || graphics.CurrentAnimationKey != "IdleAimUpLeft")
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "IdleAimHighRight" : "IdleAimHighLeft");
                }
            }
            else if (input.IsAimHighKeyDown)
            {
                if (physics.Velocity.X == 0)
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "IdleAimHighRight" : "IdleAimHighLeft");
                }
                else
                {
                    graphics.AdjustAnimation(graphics.IsFacingRight ? "RunAimHighRight" : "RunAimHighLeft");
                }
            }
            else if (input.IsAimLowKeyDown)
            {
                if (physics.Velocity.X == 0)
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "IdleAimLowRight" : "IdleAimLowLeft");
                }
                else
                {
                    graphics.AdjustAnimation(graphics.IsFacingRight ? "RunAimLowRight" : "RunAimLowLeft");
                }
            }
            else
            {
                if (physics.Velocity.X == 0)
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "IdleRight" : "IdleLeft");
                }
                else
                {
                    if (m_hasShot)
                    {
                        graphics.AdjustAnimation((graphics.IsFacingRight ? "RunShootRight" : "RunShootLeft"));
                    }
                    else
                    {
                        graphics.ChangeAnimation((graphics.IsFacingRight ? "RunRight" : "RunLeft"));
                    }
                }
            }
        }
        #endregion

        public void OnExit(Actor player)
        {
            PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
            if (graphics.NextState is CrouchingState)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "TransIdleFromCrouchRight" : "TransIdleFromCrouchLeft");
                if ((graphics.IsDoneAnimating ?? true) == true)
                {
                    IsDoneTransitioning = true;
                }
            }
            else if (graphics.NextState is AirState)
            {
                if (!graphics.ChangeFacing)
                {
                    IsDoneTransitioning = true;
                }
            }
        }

        public override string ToString()
        {
            return "Normal";
        }
    }
}
