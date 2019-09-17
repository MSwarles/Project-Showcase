using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.TileEngine;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Physics component used for bullets. Handles physics and collision detection.
    /// </summary>
    public class BulletPhysicsComponent : IPhysicsComponent
    {
        #region Fields
        private Game1 gameRef;
        private Texture2D m_hitboxTexture;
        private int m_hitboxOffsetX = 4;
        private int m_hitboxOffsetY = 4;
        private int m_width = 16;
        private int m_height = 16;
        #endregion

        #region Properties
        public int Type { get; } = 0;
        public Vector2 Velocity { get; set; } = Vector2.Zero;
        public Vector2 Acceleration { get; set; } = Vector2.Zero;
        public float BASE_RUN_SPEED { get; } = 5f;
        public float MAX_RUN_SPEED { get; } = 0f;
        public float RUN_ACCEL { get; } = 0f;
        public float MAX_FALL_SPEED { get; } = 0f;
        public float JUMP_SPEED { get; } = 0f;
        public bool IsColliding { get; private set; } = false;
        public bool ShowHitbox { get; set; } = false;
        public Rectangle Hitbox { get; private set; }
        public List<Rectangle> TileRectList { get { return TileRectList; } }
        public List<Color> TileColorList { get { return TileColorList; } }
        #endregion

        #region Constructor
        public BulletPhysicsComponent(Game game)
        {
            gameRef = (Game1)game;
            Hitbox = new Rectangle(0, 0, m_width - m_hitboxOffsetX * 2, m_height - m_hitboxOffsetY * 2);
            m_hitboxTexture = game.Content.Load<Texture2D>(@"Misc\boundingBox");
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime, LevelManager world, Actor bullet)
        {
            Rectangle hitbox = new Rectangle(
                (int)(bullet.Position.X + m_hitboxOffsetX),
                (int)(bullet.Position.Y + m_hitboxOffsetY),
                Hitbox.Width,
                Hitbox.Height);

            BulletPhysicsComponent physics = (BulletPhysicsComponent)bullet.Physics;

            Vector2 velocity = physics.Velocity;
            if (IsColliding)
            {
                velocity = Vector2.Zero;
            }

            if (velocity != Vector2.Zero)
            {
                int leftTile = (int)Math.Floor(((float)hitbox.Left / world.TileWidth) - 1);
                leftTile = MathHelper.Clamp(leftTile, 0, world.WidthInPixels / world.TileWidth);

                int rightTile = (int)Math.Ceiling((float)hitbox.Right / world.TileWidth);
                rightTile = MathHelper.Clamp(rightTile, 0, world.WidthInPixels / world.TileWidth);

                int topTile = (int)Math.Floor(((float)hitbox.Top / world.TileHeight) - 1);
                topTile = MathHelper.Clamp(topTile, 0, world.HeightInPixels / world.TileHeight);

                int bottomTile = (int)Math.Ceiling((float)hitbox.Bottom / world.TileHeight);
                bottomTile = MathHelper.Clamp(bottomTile, 0, world.HeightInPixels / world.TileHeight);

                hitbox.X += (int)velocity.X;
                hitbox.Y += (int)velocity.Y;

                for (int y = topTile; y <= bottomTile; ++y)
                {
                    for (int x = leftTile; x <= rightTile; ++x)
                    {
                        Tile currentTile = world.Level.TileMap.LayerList[2].GetTile(x, y);

                        if (currentTile == null || currentTile.CollisionType == -1 || currentTile.TileIndex == -1)
                            continue;

                        Rectangle currentTileRect = new Rectangle(x * world.TileWidth, y * world.TileHeight, world.TileWidth, world.TileHeight);

                        Vector2 distance = new Vector2(Math.Abs(hitbox.Center.X - currentTileRect.Center.X), Math.Abs(hitbox.Center.Y - currentTileRect.Center.Y));
                        Vector2 minDistance = new Vector2((hitbox.Width / 2) + (currentTileRect.Width / 2), (hitbox.Height / 2) + (currentTileRect.Height / 2));

                        if (distance.X < minDistance.X && distance.Y < minDistance.Y)
                        {
                            velocity = Vector2.Zero;
                            IsColliding = true;
                            gameRef.AudioManager.PlaySound("shotHit");
                        }
                    }
                }
            }

            bullet.Position = new Vector2(hitbox.X - m_hitboxOffsetX, hitbox.Y - m_hitboxOffsetY);
            Hitbox = hitbox;
            physics.Velocity = velocity;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Actor bullet)
        {
            if (ShowHitbox)
            {
                spriteBatch.Draw(m_hitboxTexture, Hitbox, Color.White);
            }
        }
        #endregion
    }
}