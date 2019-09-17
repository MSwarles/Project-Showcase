using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.Controls
{
    /// <summary>
    /// Draws a picture to the screen using a texture, source rectangle, and destination rectangle.
    /// </summary>
    public class PictureBox : Control
    {
        private Texture2D m_activeTexture;

        public bool Active { get; set; }
        public Rectangle DestinationRectangle { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Texture2D Texture { get; set; }

        #region Constructors
        public PictureBox(Texture2D texture, Rectangle destinationRect)
        {
            Texture = texture;
            DestinationRectangle = destinationRect;

            SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            IsTabStop = false;
            HasFocus = false;
            Size = new Vector2(texture.Width, texture.Height);
            Color = Color.White;
            Active = false;
        }

        public PictureBox(Texture2D texture, Rectangle destinationRect, Rectangle sourceRect)
        {
            Texture = texture;
            DestinationRectangle = destinationRect;
            SourceRectangle = sourceRect;

            IsTabStop = false;
            HasFocus = false;
            Size = new Vector2(texture.Width, texture.Height);
            Color = Color.White;
            Active = false;
        }

        public PictureBox(Texture2D activeTexture, Texture2D texture, Rectangle destinationRect)
        {
            m_activeTexture = activeTexture;
            Texture = texture;
            DestinationRectangle = destinationRect;
            SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);

            IsTabStop = false;
            HasFocus = false;
            Size = new Vector2(texture.Width, texture.Height);
            Color = Color.White;
            Active = false;
        }

        public PictureBox(Texture2D activeTexture, Texture2D texture, Rectangle destinationRect, Rectangle sourceRect)
        {
            m_activeTexture = activeTexture;
            Texture = texture;
            DestinationRectangle = destinationRect;
            SourceRectangle = sourceRect;

            IsTabStop = false;
            HasFocus = false;
            Size = new Vector2(texture.Width, texture.Height);
            Color = Color.White;
            Active = false;
        }
        #endregion

        #region Abstract Method
        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Active && m_activeTexture != null ||
                m_hasFocus && m_activeTexture != null)
            {
                spriteBatch.Draw(
                    m_activeTexture,
                    new Rectangle((int)m_position.X, (int)m_position.Y, DestinationRectangle.Width, DestinationRectangle.Height),
                    SourceRectangle,
                    Color);
            }
            else
            {
                spriteBatch.Draw(
                    Texture, 
                    new Rectangle((int)m_position.X, (int)m_position.Y, DestinationRectangle.Width, DestinationRectangle.Height), 
                    SourceRectangle, 
                    Color);
            }
        }

        public override void HandleInput(PlayerIndex playerIndex) { }
        #endregion

        #region PictureBox Methods
        public void SetPosition(Vector2 position)
        {
            DestinationRectangle = new Rectangle(
                (int)position.X,
                (int)position.Y,
                SourceRectangle.Width,
                SourceRectangle.Height);
        }
        #endregion
    }
}
