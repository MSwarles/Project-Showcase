using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyXMLData.AnimationClasses;

namespace SuperMetroid.Animations
{
    /// <summary>
    /// Updates and draws an animation through use of a sprite sheet texture and list of <c>AnimationFrames</c>.
    /// </summary>
    public class Animation
    {
        #region Fields
        private int m_currentFrameIndex;
        private int m_framesPerSecond;
        private TimeSpan m_frameTimer;
        #endregion

        #region Properties
        public List<AnimationFrame> AnimationFrames { get; } = new List<AnimationFrame>();
        public Vector2 Center { get { return new Vector2(FrameWidth / 2, FrameHeight / 2); } }
        public AnimationFrame CurrentFrame
        {
            get
            {
                if (AnimationFrames.Count > CurrentFrameIndex)
                {
                    // Current frame index is valid, so return current frame
                    return AnimationFrames[CurrentFrameIndex];
                }
                else
                {
                    // Current frame index is invalid, so return null
                    return null;
                }
            }
        }
        public int CurrentFrameIndex
        {
            get { return m_currentFrameIndex; }
            set
            {
                // clamp value to ensure frame index is always valid
                MathHelper.Clamp(value, 0, AnimationFrames.Count - 1);
                m_currentFrameIndex = value;
            }
        }
        public int FrameHeight { get; set; }
        public TimeSpan FrameDuration { get; private set; }
        public int FramesPerSecond
        {
            get { return m_framesPerSecond; }
            set
            {
                MathHelper.Clamp(value, 1, 60);
                m_framesPerSecond = value;
                FrameDuration = TimeSpan.FromSeconds(1 / (double)m_framesPerSecond);
            }
        }
        public int FrameWidth { get; set; }
        public bool IsDoneAnimating { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsVisible { get; set; }
        public bool LoopAnimation { get; set; }
        public float Rotation { get { return CurrentFrame.Rotation; } set { CurrentFrame.Rotation = value; } }
        public SpriteEffects SpriteEffects { get { return CurrentFrame.SpriteEffects; } set { CurrentFrame.SpriteEffects = value; } }
        public Texture2D Texture { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="texture">A sprite sheet.</param>
        /// <param name="frameWidth">The frame width.</param>
        /// <param name="frameHeight">The frame height.</param>
        /// <param name="framesPerSecond">The speed of the animation.</param>
        /// <param name="loop">Whether animation loops or not.</param>
        public Animation(Texture2D texture, int frameWidth, int frameHeight, int framesPerSecond, bool loop)
        {
            Texture = texture;
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            FramesPerSecond = framesPerSecond;
            LoopAnimation = loop;
            CurrentFrameIndex = 0;
            IsDoneAnimating = false;
            IsEnabled = true;
            IsVisible = true;
        }

        // Private constructor used for cloning
        private Animation(Animation animation)
        {
            AnimationFrames = new List<AnimationFrame>();
            foreach (AnimationFrame f in animation.AnimationFrames)
            {
                AnimationFrames.Add((AnimationFrame)f.Clone());
            }

            m_frameTimer = animation.m_frameTimer;
            CurrentFrameIndex = animation.CurrentFrameIndex;
            FrameWidth = animation.FrameWidth;
            FrameHeight = animation.FrameHeight;
            LoopAnimation = animation.LoopAnimation;
            IsEnabled = animation.IsEnabled;
            IsVisible = animation.IsVisible;
            Texture = animation.Texture;
        }
        #endregion

        #region XNA Methods
        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="gameTime">The current game time.</param>
        /// <param name="player">The player.</param>
        public void Update(GameTime gameTime, Actor player)
        {
            if (!IsEnabled)
            {
                return;
            }

            // increment the frame timer by the elapsed time
            m_frameTimer += gameTime.ElapsedGameTime;          
            if (m_frameTimer >= FrameDuration)
            {
                m_frameTimer -= FrameDuration;
                // if animation has reached its last frame
                if (CurrentFrameIndex == AnimationFrames.Count - 1)
                {
                    IsDoneAnimating = true;
                    if (LoopAnimation)
                    {
                        CurrentFrameIndex = 0;
                    }
                }
                else
                {
                    // animation not done so increment frame index
                    CurrentFrameIndex += 1;
                }
            }
        }

        // used in AnimationEditor; uses fixed time step instead of GameTime.
        public void Update()
        {
            m_frameTimer += TimeSpan.FromSeconds(.0167);
            if (CurrentFrame != null && m_frameTimer >= FrameDuration)
            {
                m_frameTimer -= FrameDuration;
                if (CurrentFrameIndex == AnimationFrames.Count - 1)
                {
                    if (LoopAnimation)
                    {
                        CurrentFrameIndex = 0;
                    }
                }
                else
                {
                    CurrentFrameIndex += 1;
                }
            }
        }

        /// <summary>
        /// Draws the animation.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch.</param>
        /// <param name="position">The drawing location on screen.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // if animation isn't visible, has no texture, or has no frame - skip drawing
            if (!IsVisible || Texture == null || CurrentFrame == null)
            {
                return;
            }

            // get center position of the animation (adjusted for scale)
            Vector2 centerPosition = new Vector2(
                    position.X + Center.X * CurrentFrame.Scale,
                    position.Y + Center.Y * CurrentFrame.Scale);

            // create frame rectangle using current frame offsets and frame width / height 
            Rectangle currentFrameRect = new Rectangle(
                    CurrentFrame.XOffset,
                    CurrentFrame.YOffset,
                    FrameWidth,
                    FrameHeight);
            
            // draw current frame of animation
            spriteBatch.Draw(
                Texture,
                centerPosition,
                currentFrameRect,
                CurrentFrame.Color,
                CurrentFrame.Rotation,
                Center,
                CurrentFrame.Scale,
                CurrentFrame.SpriteEffects,
                0f);            
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Adds an AnimationFrame to animation.
        /// </summary>
        /// <param name="frame">frame to be added to Animation</param>
        public void AddFrame(AnimationFrame frame) => AnimationFrames.Add(frame);

        /// <summary>
        /// Removes frame from animation.
        /// </summary>
        /// <param name="frame">frame to be removed from Animation</param>
        public void RemoveFrame(AnimationFrame frame)
        {
            if (AnimationFrames.Contains(frame))
            {
                AnimationFrames.Remove(frame);
                MathHelper.Clamp(CurrentFrameIndex, -1, AnimationFrames.Count() - 1);
            }
        }

        /// <summary>
        /// Removes frame at index from animation.
        /// </summary>
        /// <param name="index">index of frame to be removed from Animation</param>
        public void RemoveFrame(int index)
        {
            if (AnimationFrames.Count >= index)
            {
                AnimationFrames.RemoveAt(index);
                MathHelper.Clamp(CurrentFrameIndex, -1, AnimationFrames.Count() - 1);
            }
        }

        /// <summary>
        /// Resets the animation.
        /// </summary>
        public void Reset()
        {
            CurrentFrameIndex = 0;
            m_frameTimer = TimeSpan.Zero;
            IsDoneAnimating = false;
        }

        /// <summary>
        /// Creates a clone of animation.
        /// </summary>
        /// <returns>Clone of the animation</returns>
        public object Clone() => new Animation(this);
        #endregion

        #region Data Methods
        /// <summary>
        /// Creates Animation object from AnimationData.
        /// </summary>
        /// <param name="data">Animation data.</param>
        /// <returns>Animation created from data.</returns>
        public static Animation FromAnimationData(AnimationData data)
        {
            // Create Animation from data
            Animation animation = new Animation(
                null,
                data.FrameWidth,
                data.FrameHeight,
                data.FramesPerSecond,
                data.IsLoop);

            // Add animation frames to animation from data
            foreach (AnimationFrameData f in data.AnimationFrames)
            {
                animation.AddFrame(AnimationFrame.FromAnimationFrameData(f));
            }

            return animation;
        }
        #endregion
    }
}