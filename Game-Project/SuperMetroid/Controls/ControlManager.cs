using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.Controls
{
    /// <summary>
    /// Manager for <c>Control</c> objects.
    /// </summary>
    public class ControlManager : List<Control>
    {
        #region Fields
        private int m_selectedControl = 0;
        #endregion

        #region Properties
        public bool AcceptInput { get; set; } = true;
        public static SpriteFont SpriteFont { get; private set; }
        #endregion

        #region Events
        public event EventHandler FocusChanged;
        #endregion

        #region Constructors
        public ControlManager(SpriteFont spriteFont)
            : base()
        {
            SpriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, int capacity)
            : base(capacity)
        {
            SpriteFont = spriteFont;
        }

        public ControlManager(SpriteFont spriteFont, IEnumerable<Control> collection) :
            base(collection)
        {
            SpriteFont = spriteFont;
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime, PlayerIndex playerIndex)
        {
            if (Count == 0) { return; }

            foreach (Control c in this)
            {
                if (c.IsEnabled) { c.Update(gameTime); }
            }

            foreach (Control c in this)
            {
                if (c.HasFocus)
                {
                    c.HandleInput(playerIndex);
                    break;
                }
            }

            if (!AcceptInput) { return; }

            // TODO: CONTROL MANAGER - add different input controls
            /*
            if (Xin.ButtonPressed(Buttons.LeftThumbstickUp, playerIndex) ||
                Xin.ButtonPressed(Buttons.DPadUp, playerIndex) ||
                Xin.KeyPressed(Keys.Up))
                PreviousControl();

            if (Xin.ButtonPressed(Buttons.LeftThumbstickDown, playerIndex) ||
                Xin.ButtonPressed(Buttons.DPadDown, playerIndex) ||
                Xin.KeyPressed(Keys.Down))
                NextControl();
             */
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Control c in this)
            {
                if (c.IsVisible) { c.Draw(spriteBatch); }
            }
        }
        #endregion

        #region General Methods
        public void NextControl()
        {
            if (Count == 0) { return; }

            int currentControl = m_selectedControl;
            this[m_selectedControl].HasFocus = false;

            do
            {
                m_selectedControl++;
                if (m_selectedControl == Count) { m_selectedControl = 0; }

                if (this[m_selectedControl].IsTabStop && this[m_selectedControl].IsEnabled)
                {
                    FocusChanged?.Invoke(this[m_selectedControl], null);
                    break;
                }

            } while (currentControl != m_selectedControl);

            this[m_selectedControl].HasFocus = true;
        }

        public void PreviousControl()
        {
            if (Count == 0) { return; }

            int currentControl = m_selectedControl;
            this[m_selectedControl].HasFocus = false;

            do
            {
                m_selectedControl--;

                if (m_selectedControl < 0) { m_selectedControl = Count - 1; }

                if (this[m_selectedControl].IsTabStop && this[m_selectedControl].IsEnabled)
                {
                    FocusChanged?.Invoke(this[m_selectedControl], null);
                    break;
                }
            } while (currentControl != m_selectedControl);

            this[m_selectedControl].HasFocus = true;
        }
        #endregion
    }
}