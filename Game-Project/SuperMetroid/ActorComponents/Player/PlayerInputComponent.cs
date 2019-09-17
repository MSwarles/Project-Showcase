using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SuperMetroid.Commands;
using SuperMetroid.Components;
using SuperMetroid.PlayerStates;
using SuperMetroid.TileEngine;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Input component used to control the player character. Handles input from the user.
    /// </summary>
    public class PlayerInputComponent : IInputComponent
    {
        #region TODO LIST
        // TODO: INPUT - fix jumping
        // TODO: INPUT - disable firing / jumping / running while player is changing directions
        // TODO: INPUT - when running and jumping, you should jump higher the faster you're running
        // TODO: INPUT - implement command design pattern
        // TODO: INPUT - implement gun object (to use for initial bullet positions)
        // TODO: INPUT - add code for bombs when in ball state
        #endregion

        #region Fields
        private Game1 m_gameRef;
        private bool m_canShoot = true;
        private bool m_infiniteJumps = false;
        private bool m_jumping = false;
        private int m_jumpsRemaining = 1;
        private float m_jumpTimer = 0f;
        private float m_jumpTimerMax = 300f;
        private float m_shootTimer = 0f;
        private float m_shootTimerMax = 400f;

        private JumpCommand m_jumpCommand = new JumpCommand();
        private MoveLeftCommand m_moveLeftCommand = new MoveLeftCommand();
        private MoveRightCommand m_moveRightCommand = new MoveRightCommand();
        private ShootCommand m_shootCommand = new ShootCommand();
        #endregion

        #region Properties
        public Keys UpKey { get; set; } = Keys.W;
        public Keys DownKey { get; set; } = Keys.S;
        public Keys LeftKey { get; set; } = Keys.A;
        public Keys RightKey { get; set; } = Keys.D;
        public Keys JumpKey { get; set; } = Keys.Space;
        public Keys RunKey { get; set; } = Keys.LeftShift;
        public Keys ShootKey { get; set; } = Keys.F;
        public Keys AimHighKey { get; set; } = Keys.J;
        public Keys AimLowKey { get; set; } = Keys.K;

        public bool SpinJump = false;
        public bool HasShot = false;

        public bool IsUpKeyDown { get { return Xin.KeyDown(UpKey); } }
        public bool IsDownKeyDown { get { return Xin.KeyDown(DownKey); } }
        public bool IsLeftKeyDown { get { return Xin.KeyDown(LeftKey); } }
        public bool IsRightKeyDown { get { return Xin.KeyDown(RightKey); } }
        public bool IsJumpKeyDown { get { return Xin.KeyDown(JumpKey); } }
        public bool IsRunKeyDown { get { return Xin.KeyDown(RunKey); } }
        public bool IsShootKeyDown { get { return Xin.KeyDown(ShootKey); } }
        public bool IsAimHighKeyDown { get { return Xin.KeyDown(AimHighKey); } }
        public bool IsAimLowKeyDown { get { return Xin.KeyDown(AimLowKey); } }

        public bool IsUpKeyPressed { get { return Xin.KeyPressed(UpKey); } }
        public bool IsDownKeyPressed { get { return Xin.KeyPressed(DownKey); } }
        public bool IsLeftKeyPressed { get { return Xin.KeyPressed(LeftKey); } }
        public bool IsRightKeyPressed { get { return Xin.KeyPressed(RightKey); } }
        public bool IsJumpKeyPressed { get { return Xin.KeyPressed(JumpKey); } }
        public bool IsRunKeyPressed { get { return Xin.KeyPressed(RunKey); } }
        public bool IsShootKeyPressed { get { return Xin.KeyPressed(ShootKey); } }
        public bool IsAimHighKeyPressed { get { return Xin.KeyPressed(AimHighKey); } }
        public bool IsAimLowKeyPressed { get { return Xin.KeyPressed(AimLowKey); } }

        public bool IsUpKeyReleased { get { return Xin.KeyReleased(UpKey); } }
        public bool IsDownKeyReleased { get { return Xin.KeyReleased(DownKey); } }
        public bool IsLeftKeyReleased { get { return Xin.KeyReleased(LeftKey); } }
        public bool IsRightKeyReleased { get { return Xin.KeyReleased(RightKey); } }
        public bool IsJumpKeyReleased { get { return Xin.KeyReleased(JumpKey); } }
        public bool IsRunKeyReleased { get { return Xin.KeyReleased(RunKey); } }
        public bool IsShootKeyReleased { get { return Xin.KeyReleased(ShootKey); } }
        public bool IsAimHighKeyReleased { get { return Xin.KeyReleased(AimHighKey); } }
        public bool IsAimLowKeyReleased { get { return Xin.KeyReleased(AimLowKey); } }
        #endregion

        #region Constructor
        public PlayerInputComponent(Game game)
        {
            m_gameRef = (Game1)game;
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime, LevelManager world, Actor player)
        {
            PlayerGraphicsComponent graphics = (PlayerGraphicsComponent)player.Graphics;
            PlayerPhysicsComponent physics = (PlayerPhysicsComponent)player.Physics;
            IState state = graphics.State;
            HasShot = false;

            #region Movement
            if (IsRightKeyDown == IsLeftKeyDown)
            {
                physics.Velocity = new Vector2(0, physics.Velocity.Y);
            }
            else if (IsLeftKeyDown)
            {
                m_moveLeftCommand.Execute(player);

            }
            else if (IsRightKeyDown)
            {
                m_moveRightCommand.Execute(player);

            }

            if (IsUpKeyDown)
            {
                // send message
            }
            else if (IsDownKeyDown)
            {
                // send message
            }
            #endregion

            #region Jumping
            if (IsJumpKeyDown)
            {
                if (physics.IsOnGround || m_infiniteJumps)
                {

                }
                if (!m_jumping || m_infiniteJumps)
                {
                    if (!m_infiniteJumps)
                    {
                        m_jumpTimer += gameTime.ElapsedGameTime.Milliseconds;
                    }

                    physics.Velocity = new Vector2(physics.Velocity.X, physics.JUMP_SPEED);
                    m_jumping = true;
                    if (physics.Velocity.X != 0)
                    {
                        SpinJump = true;
                    }
                }
            }

            if (m_jumpTimer >= m_jumpTimerMax)
            {
                m_jumpTimer = 0f;
                m_jumping = false;
            }

            if (IsJumpKeyReleased && m_jumping || m_jumpTimer >= m_jumpTimerMax)
            {
                m_jumpTimer = 0f;
                m_jumping = false;
                physics.Velocity = new Vector2(physics.Velocity.X, 0);
            }
            #endregion

            #region Shooting
            m_shootTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (m_shootTimer > m_shootTimerMax)
            {
                m_canShoot = true;
            }

            if (IsShootKeyDown && m_canShoot)
            {
                Vector2 gunPos = Vector2.Zero;
                Vector2 bulletVel = Vector2.Zero;
                float bulletBaseSpeed = 5f;

                if (state is BallState)
                {
                    // bomb code
                }
                else
                {
                    m_shootTimer = 0;
                    m_canShoot = false;

                    if (IsAimHighKeyDown && IsAimLowKeyDown || IsUpKeyDown)
                    {
                        if (state is NormalState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 19, player.Position.Y) :
                                new Vector2(player.Position.X + 15, player.Position.Y);
                        }
                        else if (state is CrouchingState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 19, player.Position.Y + 6) :
                                new Vector2(player.Position.X + 15, player.Position.Y + 6);
                        }
                        bulletVel = new Vector2(0f, -bulletBaseSpeed);
                    }
                    else if (IsAimHighKeyDown)
                    {
                        if (state is NormalState)
                        {
                            if (physics.Velocity.X == 0)
                            {
                                gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 32, player.Position.Y + 6) :
                                    new Vector2(player.Position.X + 1, player.Position.Y + 6);
                            }
                            else
                            {
                                gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 29, player.Position.Y + 6) :
                                    new Vector2(player.Position.X + 5, player.Position.Y + 6);
                            }
                        }
                        else if (state is CrouchingState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 32, player.Position.Y + 15) :
                                new Vector2(player.Position.X + 1, player.Position.Y + 12);
                        }
                        else if (state is AirState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 32, player.Position.Y + 6) :
                                new Vector2(player.Position.X + 1, player.Position.Y + 6);
                        }
                        bulletVel = graphics.IsFacingRight ? new Vector2(bulletBaseSpeed + physics.Velocity.X, -bulletBaseSpeed) :
                            new Vector2(-bulletBaseSpeed + physics.Velocity.X, -bulletBaseSpeed);
                    }
                    else if (IsAimLowKeyDown)
                    {
                        if (state is NormalState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 31, player.Position.Y + 25) :
                                new Vector2(player.Position.X + 5, player.Position.Y + 25);
                        }
                        else if (state is CrouchingState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 31, player.Position.Y + 31) :
                                new Vector2(player.Position.X + 1, player.Position.Y + 31);
                        }
                        else if (state is AirState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 31, player.Position.Y + 25) :
                                new Vector2(player.Position.X + 5, player.Position.Y + 25);
                        }
                        bulletVel = graphics.IsFacingRight ? new Vector2(bulletBaseSpeed + physics.Velocity.X, bulletBaseSpeed) :
                            new Vector2(-bulletBaseSpeed + physics.Velocity.X, bulletBaseSpeed);
                    }
                    else if (state is CrouchingState)
                    {
                        gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 21, player.Position.Y + 30) :
                            new Vector2(player.Position.X + 10, player.Position.Y + 35);
                        bulletVel = new Vector2(0f, bulletBaseSpeed);
                    }
                    else
                    {
                        if (state is NormalState)
                        {
                            if (physics.Velocity.X == 0)
                            {
                                gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 28, player.Position.Y + 22) :
                                    new Vector2(player.Position.X + 5, player.Position.Y + 22);
                            }
                            else
                            {
                                gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 28, player.Position.Y + 19) :
                                    new Vector2(player.Position.X + 5, player.Position.Y + 19);
                            }
                        }
                        else if (state is CrouchingState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 28, player.Position.Y + 32) :
                                    gunPos = new Vector2(player.Position.X + 5, player.Position.Y + 32);
                        }
                        else if (state is AirState)
                        {
                            gunPos = graphics.IsFacingRight ? new Vector2(player.Position.X + 28, player.Position.Y + 22) :
                                gunPos = new Vector2(player.Position.X + 5, player.Position.Y + 22);
                        }

                        bulletVel = graphics.IsFacingRight ? new Vector2(bulletBaseSpeed + physics.Velocity.X, 0f) :
                            new Vector2(-bulletBaseSpeed + physics.Velocity.X, 0f);
                    }

                    if (gunPos != Vector2.Zero)
                    {
                        world.AddBullet(gunPos, bulletVel);
                        HasShot = true;
                        m_gameRef.AudioManager.PlaySound("shoot");
                    }
                }
            }
            #endregion

            #region Running
            if (IsRunKeyDown)
            {
                if (physics.Velocity.X < 0)
                {
                    physics.Acceleration = new Vector2(-physics.RUN_ACCEL, physics.Acceleration.Y);
                }
                else if (physics.Velocity.X > 0)
                {
                    physics.Acceleration = new Vector2(physics.RUN_ACCEL, physics.Acceleration.Y);
                }
            }
            else
            {
                physics.Acceleration = Vector2.Zero;
            }
            #endregion

            // REMOVE LATER - USED FOR DEBUGGING
            if (Xin.KeyReleased(Keys.H)) { physics.ShowHitbox = !physics.ShowHitbox; }
            if (Xin.KeyReleased(Keys.P)) { m_infiniteJumps = !m_infiniteJumps; }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Actor actor)
        {
        }
        #endregion
    }
}