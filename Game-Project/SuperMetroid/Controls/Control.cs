using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.Controls
{
    /// <summary>
    /// Abstract base class for controls.
    /// </summary>
    public abstract class Control
    {
        #region Fields
        protected Color m_color;
        protected bool m_enabled;
        protected bool m_hasFocus;
        protected string m_name;
        protected Vector2 m_position;
        protected Vector2 m_size;
        protected SpriteFont m_spriteFont;
        protected bool m_tabStop;
        protected string m_text;
        protected string m_type;
        protected object m_value;
        protected bool m_visible;
        #endregion

        #region Events
        public event EventHandler Selected;
        #endregion

        #region Properties
        public Color Color { get { return m_color; } set { m_color = value; } }
        public virtual bool HasFocus { get { return m_hasFocus; } set { m_hasFocus = value; } }
        public bool IsEnabled { get { return m_enabled; } set { m_enabled = value; } }
        public bool IsTabStop { get { return m_tabStop; } set { m_tabStop = value; } }
        public bool IsVisible { get { return m_visible; } set { m_visible = value; } }
        public string Name { get { return m_name; } set { m_name = value; } }       
        public Vector2 Position 
        { 
            get { return m_position; } 
            set   
            {
                m_position = value;
                m_position.Y = (int)m_position.Y;
            }
        }
        public Vector2 Size { get { return m_size; } set { m_size = value; } }
        public SpriteFont SpriteFont
        {
            get { return m_spriteFont; }
            set { m_spriteFont = value; }
        }
        public string Text { get { return m_text; } set { m_text = value; } }
        public string Type { get { return m_type; } set { m_type = value; } }
        public object Value { get { return m_value; } set { m_value = value; } }
        #endregion

        #region Constructor
        public Control()
        {
            Color = Color.White;
            IsEnabled = true;
            IsVisible = true;
            SpriteFont = ControlManager.SpriteFont;
        }
        #endregion

        #region Abstract Methods
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void HandleInput(PlayerIndex playerIndex);
        #endregion

        #region Virtual Methods
        /// <summary>
        /// Triggers an event when a control is selected.
        /// </summary>
        /// <param name="e">An event.</param>
        protected virtual void OnSelected(EventArgs e)
        {
            Selected?.Invoke(this, e);
        }
        #endregion
    }
}
