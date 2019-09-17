using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.TileEngine;

namespace SuperMetroid.ActorComponents
{
    public enum State { Ball, Crouching, Jumping, Standing };

    /// <summary>
    /// Physics component used for the player character. Handles physics and collision detection.
    /// </summary>
    public class PlayerPhysicsComponent : IPhysicsComponent
    {
        #region TODO List
        // TODO: PHYSICS - refactor physics engine code
        // TODO: PHYSICS - add collision checking before switching states / hitboxes
        // TODO: PHYSICS - move hitbox up when player is in jump state
        // TODO: PHYSICS - implement ramp tiles
        // TODO: PHYSICS - add animated collision tiles (doors, destructible blocks)
        // TODO: PHYSICS - add wall jumping
        // TODO: PHYSICS - fix clamping bounds for LockToMap()
        // TODO: PHYSICS - move collision checking to Level class
        #endregion

        #region Fields
        private float GRAVITY_ACCEL = .15f;

        private Game1 m_gameRef;
        private int m_height = 56;
        private Rectangle m_hitboxBall = new Rectangle(0, 0, 12, 16);
        private Rectangle m_hitboxCrouching = new Rectangle(0, 0, 12, 28);
        private Rectangle m_hitboxJumping = new Rectangle(0, 0, 12, 24);
        private Rectangle m_hitboxStanding = new Rectangle(0, 0, 12, 38);
        private Texture2D m_hitboxTexture;
        private Vector2 m_velocity;
        private int m_width = 50;
        #endregion

        #region Properties
        public float BASE_RUN_SPEED { get; } = 3f;
        public float JUMP_SPEED { get; } = -4.15f;
        public float MAX_FALL_SPEED { get; } = 4f;
        public float MAX_RUN_SPEED { get; } = 7.5f;
        public float RUN_ACCEL { get; } = 0.05f;
        
        public Vector2 Acceleration { get; set; } = Vector2.Zero;
        public Rectangle Hitbox { get; private set; }
        public bool IsColliding { get; } = false;
        public bool IsOnGround { get; set; } = false;
        public State State { get; set; } = State.Standing;
        public Vector2 Velocity
        {
            get { return m_velocity; }
            set
            {
                m_velocity = new Vector2(
                    MathHelper.Clamp(value.X, -MAX_RUN_SPEED, MAX_RUN_SPEED),
                    MathHelper.Clamp(value.Y, JUMP_SPEED, MAX_FALL_SPEED));
            }
        }

        // DEBUG Variables
        public bool ShowHitbox { get; set; } = false;
        public List<Rectangle> TileRectList { get; } = new List<Rectangle>();
        public List<Color> TileColorList { get; } = new List<Color>();
        #endregion

        #region Constructor
        public PlayerPhysicsComponent(Game game)
        {
            m_gameRef = (Game1)game;
            LoadContent(game);
        }
        #endregion

        #region XNA Methods
        public void LoadContent(Game game)
        {
            m_hitboxTexture = game.Content.Load<Texture2D>(@"Misc\samusHitbox");
        }

        public void Update(GameTime gameTime, LevelManager world, Actor player)
        {
            CheckState(player);

            Rectangle hitbox = new Rectangle(
                    (int)(player.Position.X + ((m_width - Hitbox.Width) / 2)),
                    (int)(player.Position.Y + (m_height - Hitbox.Height)),
                    Hitbox.Width,
                    Hitbox.Height);

            // TODO: relook at this code - velocity will never be Vector2.Zero since gravity accel is added
            IsOnGround = false;
            Vector2 velocity = Velocity;
            velocity += Acceleration;
            velocity.Y += GRAVITY_ACCEL;
            velocity = new Vector2(
                MathHelper.Clamp(velocity.X, -MAX_RUN_SPEED, MAX_RUN_SPEED),
                MathHelper.Clamp(velocity.Y, JUMP_SPEED, MAX_FALL_SPEED));

            if (velocity != Vector2.Zero)
            {
                TileRectList.Clear();
                TileColorList.Clear();

                int leftTile = (int)Math.Floor(((float)hitbox.Left / world.TileWidth) - 1);
                leftTile = MathHelper.Clamp(leftTile, 0, world.WidthInPixels / world.TileWidth);

                int rightTile = (int)Math.Ceiling((float)hitbox.Right / world.TileWidth);
                rightTile = MathHelper.Clamp(rightTile, 0, world.WidthInPixels / world.TileWidth);

                int topTile = (int)Math.Floor(((float)hitbox.Top / world.TileHeight) - 1);
                topTile = MathHelper.Clamp(topTile, 0, world.HeightInPixels / world.TileHeight);

                int bottomTile = (int)Math.Ceiling((float)hitbox.Bottom / world.TileHeight);
                bottomTile = MathHelper.Clamp(bottomTile, 0, world.HeightInPixels / world.TileHeight);

                hitbox.X += (int)velocity.X;

                for (int y = topTile; y <= bottomTile; ++y)
                {
                    for (int x = leftTile; x <= rightTile; ++x)
                    {
                        Tile currentTile = world.Level.TileMap.LayerList[2].GetTile(x, y);

                        if (currentTile == null || currentTile.CollisionType == -1 || currentTile.TileIndex == -1)
                        {
                            continue;
                        }

                        Rectangle currentTileRect = new Rectangle(x * world.TileWidth, y * world.TileHeight, world.TileWidth, world.TileHeight);
                        TileRectList.Add(currentTileRect);

                        Vector2 distance = new Vector2(Math.Abs(hitbox.Center.X - currentTileRect.Center.X), Math.Abs(hitbox.Center.Y - currentTileRect.Center.Y));
                        Vector2 minDistance = new Vector2((hitbox.Width / 2) + (currentTileRect.Width / 2), (hitbox.Height / 2) + (currentTileRect.Height / 2));

                        if (distance.X < minDistance.X && distance.Y < minDistance.Y)
                        {
                            if (hitbox.Left >= currentTileRect.Center.X && hitbox.Bottom > currentTileRect.Top + 1) // left collision
                            {
                                hitbox.X += (currentTileRect.Right - hitbox.Left);
                                velocity = new Vector2(0f, velocity.Y);
                                TileColorList.Add(Color.Red);
                            }
                            else if (hitbox.Right <= currentTileRect.Center.X && hitbox.Bottom > currentTileRect.Top + 1) // right collision
                            {
                                hitbox.X -= (hitbox.Right - currentTileRect.Left);
                                velocity = new Vector2(0f, velocity.Y);
                                TileColorList.Add(Color.Red);
                            }
                            else
                            {
                                TileColorList.Add(Color.Green);
                            }
                        }
                        else
                        {
                            TileColorList.Add(Color.Green);
                        }
                    }
                }

                hitbox.Y += (int)velocity.Y;

                for (int y = topTile; y <= bottomTile; ++y)
                {
                    for (int x = leftTile; x <= rightTile; ++x)
                    {
                        Tile currentTile = world.Level.TileMap.LayerList[2].GetTile(x, y);

                        if (currentTile == null || currentTile.CollisionType == -1 || currentTile.TileIndex == -1)
                        {
                            continue;
                        }

                        Rectangle currentTileRect = new Rectangle(x * world.TileWidth, y * world.TileHeight, world.TileWidth, world.TileHeight);

                        Vector2 distance = new Vector2(Math.Abs(hitbox.Center.X - currentTileRect.Center.X), Math.Abs(hitbox.Center.Y - currentTileRect.Center.Y));
                        Vector2 minDistance = new Vector2((hitbox.Width / 2) + (currentTileRect.Width / 2), (hitbox.Height / 2) + (currentTileRect.Height / 2));

                        if (distance.X < minDistance.X && distance.Y < minDistance.Y)
                        {
                            if (hitbox.Bottom >= currentTileRect.Top && hitbox.Top < currentTileRect.Top) // bottom collision
                            {
                                hitbox.Y -= (int)((hitbox.Bottom - currentTileRect.Top) - 1);
                                velocity = new Vector2(velocity.X, 0f);
                                IsOnGround = true;
                                TileRectList.Add(currentTileRect);
                                TileColorList.Add(Color.Yellow);
                            }
                            
                            if (hitbox.Top <= currentTileRect.Bottom && hitbox.Bottom > currentTileRect.Bottom) // top collision
                            {
                                hitbox.Y += (int)((currentTileRect.Bottom - hitbox.Top));
                                velocity = new Vector2(velocity.X, 0f);
                                TileRectList.Add(currentTileRect);
                                TileColorList.Add(Color.Blue);
                            }
                        }
                    }
                }
            }

            player.Position = new Vector2(hitbox.X - ((m_width - Hitbox.Width) / 2), hitbox.Bottom - m_height);
            Hitbox = hitbox;
            Velocity = velocity;

            foreach(DoorTrigger t in world.Level.Triggers)
            {
                if (Hitbox.Intersects(t.Position))
                {
                    t.Execute(m_gameRef, world);
                }
            }

            LockToMap(world.Level, player);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Actor player)
        {
            if (ShowHitbox)
            {
                spriteBatch.Draw(m_hitboxTexture, Hitbox, Color.White);
            }
        }
        #endregion

        public void CheckState(Actor player)
        {
            PlayerInputComponent input = (PlayerInputComponent)player.Input;
            if (!IsOnGround)
            {
                if (!(State is State.Ball))
                {
                    State = State.Jumping;
                    Hitbox = m_hitboxJumping;
                }
            }
            else if (IsOnGround && State == State.Jumping)
            {
                State = State.Standing;
                Hitbox = m_hitboxStanding;
            }

            if (input.IsDownKeyPressed)
            {
                if (State is State.Standing)
                {
                    State = State.Crouching;
                    Hitbox = m_hitboxCrouching;
                }
                else if (State is State.Crouching)
                {
                    State = State.Ball;
                    Hitbox = m_hitboxBall;
                }
            }
            else if (input.IsUpKeyPressed)
            {
                if (State is State.Ball)
                {
                    State = State.Crouching;
                    Hitbox = m_hitboxCrouching;
                }
                else if (State is State.Crouching)
                {
                    State = State.Standing;
                    Hitbox = m_hitboxStanding;
                }
            }
            else if (input.IsLeftKeyDown || input.IsRightKeyDown)
            {
                if (State is State.Crouching)
                {
                    State = State.Standing;
                    Hitbox = m_hitboxStanding;
                }
            }
            else if (IsOnGround)
            {
                if (State is State.Jumping)
                {
                    State = State.Standing;
                    Hitbox = m_hitboxStanding;
                }
            }
        }

        #region Private Methods
        private void LockToMap(Level l, Actor player)
        {
            player.Position = new Vector2(
                MathHelper.Clamp(player.Position.X, 0 - Hitbox.X, l.WidthInPixels + Hitbox.X),
                MathHelper.Clamp(player.Position.Y, 0 - Hitbox.Y, l.HeightInPixels + Hitbox.Y));
        }
        #endregion

        #region Unused Methods
        public void ApplyForce()
        {
        }

        public void ApplyImpulse()
        {
        }

        private Vector2 CalcPos(Vector2 pos, Vector2 vel, float gameTime)
        {
            return pos + vel * gameTime;
        }

        private Vector2 CalcVelFromPos(Vector2 pos1, Vector2 pos2, float gameTime)
        {
            return (pos2 - pos1) / gameTime;
        }

        private Vector2 CalcVel(Vector2 vel, Vector2 accel, float gameTime)
        {
            return vel + accel * gameTime;
        }

        private Vector2 CalcAccel(Vector2 vel1, Vector2 vel2, float gameTime)
        {
            return (vel2 - vel1) / gameTime;
        }

        private Vector2 HandleAccel(Vector2 pos, Vector2 vel, Vector2 accel, float gameTime)
        {
            vel += accel * gameTime;
            pos += vel * gameTime;
            return pos;
        }

        private Vector2 AddVectors(Vector2[] f, int n)
        {
            Vector2 F = Vector2.Zero;

            for (int x = 0; x < n; x++)
                F += f[x];

            return F;
        }
        #endregion
    }
}
