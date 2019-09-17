using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.Controls
{
    /// <summary>
    /// Draws a string to screen using font, position, and color.
    /// </summary>
    public class Label : Control
    {
        public Label()
        {
            m_tabStop = false;
        }

        public override void Update(GameTime gameTime) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(SpriteFont, Text, Position, Color);
        }

        public override void HandleInput(PlayerIndex playerIndex) { }
    }
}
