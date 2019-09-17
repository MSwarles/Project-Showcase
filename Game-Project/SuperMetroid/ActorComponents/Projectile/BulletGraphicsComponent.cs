using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.Animations;
using SuperMetroid.PlayerStates;
using SuperMetroid.TileEngine;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Graphics component used for bullets. Handles the drawing of bullets.
    /// </summary>
    public class BulletGraphicsComponent : IGraphicsComponent
    {
        #region Fields
        private Dictionary<string, Animation> m_animationDict;
        private Texture2D m_texture;
        private float m_timer;
        private float m_timerMax = 20f;
        #endregion

        #region Properties        
        public Animation CurrentAnimation
        {
            get
            {
                if (m_animationDict[CurrentAnimationKey] != null)
                {
                    return m_animationDict[CurrentAnimationKey];
                }
                else
                {
                    return null;
                }
            }
        }
        public string CurrentAnimationKey { get; set; }
        public int? CurrentFrameIndex { get { return CurrentAnimation?.CurrentFrameIndex; } }
        public bool? IsDoneAnimating { get { return CurrentAnimation?.IsDoneAnimating; } }
        public bool IsVisible { get; set; } = true;
        public IState State { get; set; }
        #endregion

        #region Constructor
        public BulletGraphicsComponent(Game game, Dictionary<string, Animation> animationDict, Texture2D texture)
        {
            Game1 gameRef = (Game1)game;

            m_animationDict = animationDict;
            m_texture = texture;
            CurrentAnimationKey = "bullet";
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime, LevelManager lm, Actor bullet)
        {
            BulletPhysicsComponent physics = (BulletPhysicsComponent)bullet.Physics;
            SetRotation(bullet);

            // give bullet 'flash' effect
            if (CurrentAnimationKey == "bullet")
            {
                m_timer += gameTime.ElapsedGameTime.Milliseconds;

                if (m_timer >= m_timerMax)
                {
                    m_timer -= m_timerMax;
                    IsVisible = !IsVisible;
                }
            }
            else
            {
                IsVisible = true;
            }

            CurrentAnimation.Update(gameTime, bullet);

            if (physics.IsColliding)
            {
                CurrentAnimationKey = "bulletExplode";
            }

            if (physics.IsColliding && (IsDoneAnimating ?? true))
            {
                lm.Level.RemoveBullet(bullet);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Actor bullet)
        {
            if (IsVisible)
            {
                CurrentAnimation.Draw(spriteBatch, bullet.Position);
            }
        }
        #endregion

        // Sets bullet rotation based on bullet direction (velocity)
        public void SetRotation(Actor bullet)
        {
            BulletPhysicsComponent physics = (BulletPhysicsComponent)bullet.Physics;
            CurrentAnimation.Rotation = 0f;
            if (physics.Velocity.X > 0)
            {
                if (physics.Velocity.Y < 0)
                {
                    CurrentAnimation.Rotation = (float)(Math.PI * 7 / 4);
                }
                else if (physics.Velocity.Y > 0)
                {
                    CurrentAnimation.Rotation = (float)(Math.PI / 4);
                }
            }
            else if (physics.Velocity.X < 0)
            {
                CurrentAnimation.SpriteEffects = SpriteEffects.FlipHorizontally;

                if (physics.Velocity.Y < 0)
                {
                    CurrentAnimation.Rotation = (float)(Math.PI / 4);
                }
                else if (physics.Velocity.Y > 0)
                {
                    CurrentAnimation.Rotation = (float)(Math.PI * 7 / 4);
                }
            }
            else if (physics.Velocity.X == 0)
            {
                if (physics.Velocity.Y < 0)
                {
                    CurrentAnimation.Rotation = (float)(Math.PI * 3 / 2);
                }
                else if (physics.Velocity.Y > 0)
                {
                    CurrentAnimation.Rotation = (float)(Math.PI / 2);
                }
            }
        }

        public void ResetAnimation()
        {
            CurrentAnimation.Reset();
        }

        public void ChangeAnimation(string newAnimation)
        {
        }

        public void AdjustAnimation(string newAnimation)
        {
        }
    }
}