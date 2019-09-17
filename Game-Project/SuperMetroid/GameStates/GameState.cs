using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SuperMetroid.GameStates
{
    public interface IGameState
    {
        GameState Tag { get; }
        PlayerIndex? PlayerIndexInControl { get; set; }
    }

    /// <summary>
    /// Abstract partial class for other GameStates.
    /// </summary>
    public abstract partial class GameState : DrawableGameComponent, IGameState
    {
        #region Fields
        protected ContentManager content;
        protected readonly List<GameComponent> childComponents;
        protected PlayerIndex? indexInControl;
        protected readonly IStateManager manager;
        protected GameState tag;
        #endregion

        #region Properties
        public List<GameComponent> Components { get { return childComponents; } }
        public PlayerIndex? PlayerIndexInControl
        {
            get { return indexInControl; }
            set { indexInControl = value; }
        }
        public GameState Tag { get { return tag; } }
        #endregion

        #region Constructor
        public GameState(Game game)
            : base(game)
        {
            tag = this;

            childComponents = new List<GameComponent>();
            content = Game.Content;

            manager = (IStateManager)Game.Services.GetService(typeof(IStateManager));
        }
        #endregion

        #region XNA Methods
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                {
                    component.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent && ((DrawableGameComponent)component).Visible)
                {
                    ((DrawableGameComponent)component).Draw(gameTime);
                }
            }
        }
        #endregion

        #region Custom Methods
        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (manager.CurrentState == tag)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = true;
                }
            }
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                {
                    ((DrawableGameComponent)component).Visible = false;
                }
            }
        }
        #endregion
    }
}