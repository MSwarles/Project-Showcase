using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SuperMetroid
{
    public class Animation
    {
        #region Fields

        private Rectangle[] m_framesArray;
        private TimeSpan m_frameLength;
        private TimeSpan m_frameTimer;
        private int m_framesPerSecond;
        private int m_currentFrame;
        private int m_frameWidth;
        private int m_frameHeight;
        private bool m_loop;
        private bool m_isDoneAnimating;
        
        #endregion

        #region Properties

        public Rectangle CurrentFrameRect { get { return m_framesArray[m_currentFrame]; } }

        public int FramesPerSecond
        {
            get { return m_framesPerSecond; }
            set
            {         
                if (value < 1)
                    m_framesPerSecond = 1;
                else if (value > 60)
                    m_framesPerSecond = 60;
                else
                    m_framesPerSecond = value;
               
                m_frameLength = TimeSpan.FromSeconds(1 / (double)m_framesPerSecond);               
            }
        }

        public int CurrentFrame
        {
            get { return m_currentFrame; }
            set { m_currentFrame = (int)MathHelper.Clamp(value, 0, m_framesArray.Length - 1); }
        }

        public int FramesArrayLength { get { return m_framesArray.Length; } }
        public int FrameWidth { get { return m_frameWidth; } }
        public int FrameHeight { get { return m_frameHeight; } }
        public Vector2 Center { get { return new Vector2(m_frameWidth / 2, m_frameHeight / 2); } }

        public bool Loop { get { return m_loop; } }
        public bool IsDoneAnimating { get { return m_isDoneAnimating; } }

        #endregion

        #region Constructors

        public Animation(int frameCount, int frameWidth, int frameHeight, 
            int xOffset, int yOffset, int framesPerSecond, bool loop)
        {
            m_framesArray = new Rectangle[frameCount];
            m_frameWidth = frameWidth;
            m_frameHeight = frameHeight;
            m_framesPerSecond = framesPerSecond;
            m_loop = loop;
            m_isDoneAnimating = false;

            for (int i = 0; i < frameCount; i++)
            {
                m_framesArray[i] = new Rectangle(
                        xOffset + (frameWidth * i),
                        yOffset,
                        frameWidth,
                        frameHeight);
            }

            Reset();
        }

        private Animation(Animation animation, int framesPerSecond)
        {
            m_framesArray = animation.m_framesArray;
            FramesPerSecond = framesPerSecond;
            m_loop = animation.m_loop;
            m_isDoneAnimating = false;            
        }

        #endregion

        #region XNA Methods

        public void Update(GameTime gameTime)
        {
            m_frameTimer += gameTime.ElapsedGameTime;

            if (m_frameTimer >= m_frameLength && !m_isDoneAnimating)
            {
                m_frameTimer = TimeSpan.Zero;
                m_currentFrame = (m_currentFrame + 1) % m_framesArray.Length;

                if (m_currentFrame == 0 && !m_loop)
                {
                    m_isDoneAnimating = true;
                }
            }
        }

        #endregion

        #region Custom Methods

        public void Reset()
        {
            m_currentFrame = 0;
            m_frameTimer = TimeSpan.Zero;
            m_isDoneAnimating = false;
        }

        public object Clone()
        {
            Animation animationClone = new Animation(this, m_framesPerSecond);

            animationClone.m_frameWidth = this.m_frameWidth;
            animationClone.m_frameHeight = this.m_frameHeight;
            animationClone.Reset();

            return animationClone;
        }

        #endregion
    }
}