using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SuperMetroid.PlayerStates;
using SuperMetroid.Components;
using SuperMetroid.TileEngine;
using SuperMetroid.Animations;

namespace SuperMetroid.ActorComponents
{
    public class PlayerAnimation : IGraphicsComponent
    {
        #region Fields

        protected Game1 gameRef;
        protected IState m_state;
        protected IState m_previousState;

        protected Dictionary<string, Animation> m_animationDict;
        private string m_currentAnimationKey;

        #endregion

        #region Events

        public event EventHandler StateChanged;

        #endregion

        #region Properties

        public IState State
        {
            get { return m_state; }
            set
            {
                if (value != m_state)
                {
                    m_previousState = m_state;
                    m_state = value;
                    OnStateChanged();
                }
            }
        }

        public IState PreviousState { get { return m_previousState; } }

        public Dictionary<string, Animation> AnimationDict { get { return m_animationDict; } }
        public Animation CurrentAnimationz { get { return m_animationDict[m_currentAnimationKey]; } }
        public string CurrentAnimationKey { get { return m_currentAnimationKey; } set { m_currentAnimationKey = value; } }
        public int CurrentFrameIndex { get { return m_animationDict[m_currentAnimationKey].CurrentFrameIndex; } }
        public int SpriteWidth { get { return CurrentAnimationz.FrameWidth; } }
        public int SpriteHeight { get { return CurrentAnimationz.FrameHeight; } }

        public bool DoneAnimating { get { return false; } }

        #endregion

        #region Constructor

        public PlayerAnimation(Game game, Dictionary<string, Animation> animationDict)
        {
            gameRef = (Game1)game;
            m_animationDict = animationDict;
            m_currentAnimationKey = "IdleRight";
        }

        #endregion

        #region XNA Methods

        public void Update(GameTime gameTime, Actor player)
        {
            CurrentAnimationz.Update(gameTime, player);

            if (m_state != null)
            {
                if (player.Transitioning)
                    m_state.Transition(player, this, m_previousState);

                if (!player.Transitioning)
                    m_state.Update(gameTime, player, this);
            }

            gameRef.GamePlayState.Debug.AddString("Current Animation: " + CurrentAnimationKey);
        }

        public void Draw(SpriteBatch spriteBatch, Actor player)
        {
            CurrentAnimationz.Draw(spriteBatch, player.Position);
        }

        #endregion

        #region Virtual Methods

        public virtual void OnStateChanged()
        {
            if (StateChanged != null)
            {
                StateChanged(this, null);
            }
        }

        public virtual void OnFrameChanged()
        {
            
        }

        #endregion

        #region Public Methods

        public void ResetAnimation()
        {
            CurrentAnimationz.Reset();
        }

        public void ReceiveMessage(int messageType, string messageInfo)
        {

        }

        #endregion
    }
}
