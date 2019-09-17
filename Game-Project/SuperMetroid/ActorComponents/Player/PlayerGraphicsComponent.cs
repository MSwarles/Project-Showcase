using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.Animations;
using SuperMetroid.PlayerStates;
using SuperMetroid.TileEngine;

namespace SuperMetroid.ActorComponents
{
    /// <summary>
    /// Graphics component used for the player character. Handles the drawing of the player.
    /// </summary>
    public class PlayerGraphicsComponent : IGraphicsComponent
    {
        #region Fields
        private string DEFAULT_ANIMATION = "IdleRight";

        private Game1 m_gameRef;
        private Dictionary<string, Animation> m_animationDict;
        private Texture2D m_texture;
        #endregion

        #region Properties
        public bool ChangeFacing { get; set; }
        public Animation CurrentAnimation
        {
            get
            {
                return CurrentAnimationKey != null ? m_animationDict[CurrentAnimationKey] : null;
            }
        }
        public string CurrentAnimationKey { get; set; }
        public int? CurrentFrameIndex { get { return CurrentAnimation?.CurrentFrameIndex; } }
        public bool? IsDoneAnimating { get { return CurrentAnimation?.IsDoneAnimating; } }
        public bool IsFacingRight { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public IState NextState { get; set; } = null;
        public IState State { get; set; } = new NormalState();
        #endregion

        #region Constructor
        public PlayerGraphicsComponent(Game game, Dictionary<string, Animation> animationDict, Texture2D texture)
        {
            m_gameRef = (Game1)game;
            m_animationDict = animationDict;
            m_texture = texture;
            CurrentAnimationKey = DEFAULT_ANIMATION;
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime, LevelManager lm, Actor player)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Update(gameTime, player);
            }

            if (State != null)
            {
                State.Update(gameTime, player);

                if (NextState != null && State.IsDoneTransitioning)
                {
                    State = NextState;
                    NextState = null;
                    State.Update(gameTime, player);
                    ResetAnimation();
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Actor player)
        {
            if (CurrentAnimation != null && IsVisible)
            {
                CurrentAnimation.Draw(spriteBatch, player.Position);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Changes animation to a new animation while maintaining the frame index
        /// </summary>
        /// <param name="animationKey"></param>
        public void AdjustAnimation(string animationKey)
        {
            int currentFrame = CurrentAnimation.CurrentFrameIndex;
            CurrentAnimationKey = animationKey;
            CurrentAnimation.CurrentFrameIndex = currentFrame;
        }

        /// <summary>
        /// Changes animation to a new animation
        /// </summary>
        /// <param name="animationKey"></param>
        public void ChangeAnimation(string animationKey)
        {
            if (CurrentAnimationKey != animationKey)
            {
                CurrentAnimationKey = animationKey;
                ResetAnimation();
            }
        }

        public void ResetAnimation()
        {
            CurrentAnimation.Reset();
        }
        #endregion
    }
}
