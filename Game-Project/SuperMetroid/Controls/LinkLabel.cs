using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.Controls
{
    /// <summary>
    /// Draws a string to screen using font, position, and color. May be selected by the user.
    /// </summary>
    public class LinkLabel : Control
    {
        public Color SelectedColor { get; set; } = Color.Red;

        public LinkLabel()
        {
            IsTabStop = true;
            HasFocus = false;
            Position = Vector2.Zero;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (m_hasFocus)
            {
                spriteBatch.DrawString(SpriteFont, Text, Position, SelectedColor);
            }
            else
            {
                spriteBatch.DrawString(SpriteFont, Text, Position, Color);
            }
        }

        public override void HandleInput(PlayerIndex playerIndex)
        {
            if (!HasFocus) { return; }

            /*
            if (Xin.KeyReleased(Keys.Enter) ||
                Xin.ButtonReleased(Buttons.A, playerIndex))
                base.OnSelected(null);
             */
        }
    }
}