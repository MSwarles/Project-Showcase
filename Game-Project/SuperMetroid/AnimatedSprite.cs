using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid
{
    public enum AnimationKey
    {
        IdleLeft,
        IdleRight,
        IdleShootLeft,
        IdleShootRight,
        IdleAimUpLeft,
        IdleAimUpRight,
        IdleAimHighLeft,
        IdleAimHighRight,
        IdleAimLowLeft,
        IdleAimLowRight,
        TransIdleLeftToRight,
        TransIdleRightToLeft,
        TransIdleFromCrouchLeft,
        TransIdleFromCrouchRight,
        TransIdleFromJumpLeft,               
        TransIdleFromJumpRight,

        RunLeft,
        RunRight,
        RunAimHighLeft,
        RunAimHighRight,
        RunShootLeft,
        RunShootRight,
        RunAimLowLeft,
        RunAimLowRight,
        
        JumpIdleLeft,
        JumpIdleRight,
        JumpIdleFallLeft,
        JumpIdleFallRight,
        JumpSpinLeft,
        JumpSpinRight,
        JumpShootLeft,
        JumpShootRight,
        JumpAimHighLeft,
        JumpAimHighRight,
        JumpAimLowLeft,
        JumpAimLowRight,
        JumpAimDownLeft,
        JumpAimDownRight,
        TransJumpLeftToRight,
        TransJumpRightToLeft,
        TransJumpToFallLeft,
        TransJumpToFallRight,
        TransJumpFromIdleLeft,
        TransJumpFromIdleRight,
        TransJumpFromFallLeft,
        TransJumpFromFallRight,

        CrouchLeft,
        CrouchRight,
        CrouchAimUpLeft,
        CrouchAimUpRight,
        CrouchAimHighLeft,
        CrouchAimHighRight,
        CrouchAimLowLeft,
        CrouchAimLowRight,
        TransCrouchLeftToRight,
        TransCrouchRightToLeft,
        TransCrouchFromIdleLeft,
        TransCrouchFromIdleRight,
        TransCrouchFromBallLeft,     
        TransCrouchFromBallRight,

        BallLeft,
        BallRight,
        TransBallFromCrouchLeft,
        TransBallFromCrouchRight,
        
        Dying,

        Bullet,
        BulletExplode
    }

    public class AnimatedSprite
    {
        #region Fields

        private Dictionary<AnimationKey, Animation> m_animationsDict;
        private AnimationKey m_currentAnimationKey;
        private Texture2D m_texture;
        private Color m_color;
        private float m_rotation;
        private float m_scale;
        private bool m_flip;
        private bool m_isEnabled;
        
        #endregion

        #region Properties

        public AnimationKey CurrentAnimationKey { get { return m_currentAnimationKey; } set { m_currentAnimationKey = value; } }
        public Animation CurrentAnimation { get { return m_animationsDict[m_currentAnimationKey]; } }
        public int Width { get { return m_animationsDict[m_currentAnimationKey].FrameWidth; } }
        public int Height { get { return m_animationsDict[m_currentAnimationKey].FrameHeight; } }

        public Color Color { get { return m_color; } }
        public float Rotation { get { return m_rotation; } set { m_rotation = value; } }
        public float Scale { get { return m_scale; } }
        public bool Flip { get { return m_flip; } set { m_flip = value; } }
        public bool IsEnabled { get { return m_isEnabled; } set { m_isEnabled = value; } }
        public bool DoneAnimating { get { return CurrentAnimation.IsDoneAnimating; } }
        
        #endregion

        #region Constructor

        public AnimatedSprite(Texture2D texture, Dictionary<AnimationKey, Animation> animation)
        {
            m_texture = texture;
            m_animationsDict = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
                m_animationsDict.Add(key, (Animation)animation[key].Clone());

            m_color = Color.White;
            m_rotation = 0f;
            m_scale = 1f;
        }

        #endregion

        #region XNA Methods

        public virtual void Update(GameTime gameTime)
        {
            if (m_isEnabled)
                m_animationsDict[m_currentAnimationKey].Update(gameTime);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Actor actor)
        {
            if (CurrentAnimation.IsDoneAnimating)
                return;

            SpriteEffects spriteEffects;

            if (m_flip)
                spriteEffects = SpriteEffects.FlipHorizontally;
            else
                spriteEffects = SpriteEffects.None;

                spriteBatch.Draw(
                    m_texture,
                    new Vector2(actor.Position.X + CurrentAnimation.Center.X, actor.Position.Y + CurrentAnimation.Center.Y),
                    CurrentAnimation.CurrentFrameRect,
                    m_color,
                    m_rotation,
                    CurrentAnimation.Center,
                    m_scale,
                    spriteEffects,
                    0f);                   
        }

        #endregion

        #region Custom Methods

        public void ResetAnimation()
        {
            CurrentAnimation.Reset();
        }

        #endregion
    }
}