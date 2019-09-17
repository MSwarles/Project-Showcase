using Microsoft.Xna.Framework;

using SuperMetroid.ActorComponents;

namespace SuperMetroid.PlayerStates
{
    /// <summary>
    /// State used by graphics component for handling the player's in air animations and transitions.
    /// </summary>
    public class AirState : IState
    {
        private bool m_aimDown;
        private bool m_hasShot;
        private bool m_spinJump;

        public bool IsDoneTransitioning { get; set; }
        
        public AirState(bool spinJump)
        {
            m_spinJump = spinJump;
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

            if (input.IsDownKeyPressed)
            {
                if (!m_aimDown)
                {
                    m_aimDown = true;
                }
                else
                {
                    graphics.NextState = new BallState();
                }                        
            }

            if (graphics.NextState != null)
            {
                OnExit(player);
            }
            else if (graphics.ChangeFacing)
            {
                if (!m_spinJump && ((graphics.IsDoneAnimating ?? true) == false))
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "TransJumpRightToLeft" : "TransJumpLeftToRight");
                }
                else
                {
                    graphics.IsFacingRight = !graphics.IsFacingRight;
                    graphics.ChangeFacing = false;
                    m_aimDown = false;
                }
            }
            else if ((input.IsLeftKeyPressed && graphics.IsFacingRight) || (input.IsRightKeyPressed && !graphics.IsFacingRight))
            {
                graphics.ChangeFacing = true;
            }
            else if (physics.IsOnGround)
            {
                graphics.NextState = new NormalState();
            }
            else if (m_spinJump && !m_hasShot)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "JumpSpinRight" : "JumpSpinLeft");
            }
            else if (m_aimDown)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "JumpAimDownRight" : "JumpAimDownLeft");
            }
            else if (input.IsAimHighKeyDown)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "JumpAimHighRight" : "JumpAimHighLeft");
            }
            else if (input.IsAimLowKeyDown)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "JumpAimLowRight" : "JumpAimLowLeft");
            }
            else if (m_hasShot)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "JumpShootRight" : "JumpShootLeft");
            }
            else if (physics.Velocity.Y < -0.5)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "TransJumpFromIdleRight" : "TransJumpFromIdleLeft");
            }
            else if (physics.Velocity.Y < 2.25)
            {
                graphics.ChangeAnimation(graphics.IsFacingRight ? "TransJumpToFallRight" : "TransJumpToFallLeft");
            }
            else
            {      
                if (physics.Velocity.Y < 0)
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "JumpIdleRight" : "JumpIdleLeft");
                }
                else
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "JumpIdleFallRight" : "JumpIdleFallLeft");
                }        
            }
        }
        #endregion

        public void OnExit(Actor player)
        {
            PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
            PlayerPhysicsComponent physics = (PlayerPhysicsComponent)player.Physics;
            if (graphics.NextState is NormalState)
            {
                if (physics.Velocity.X != 0)
                {
                    IsDoneTransitioning = true;
                }
                else
                {
                    graphics.ChangeAnimation(graphics.IsFacingRight ? "TransIdleFromJumpRight" : "TransIdleFromJumpLeft");
                    if ((graphics.IsDoneAnimating ?? true) == true)
                    {
                        IsDoneTransitioning = true;
                    }
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
            return "Air";
        }
    }
}
