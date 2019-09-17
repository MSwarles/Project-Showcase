using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// Image drawn in the background of a level.
    /// </summary>
    public class BackgroundImage
    {
        //TODO: BACKGROUND IMAGE - switch to Actor / Component
        #region Fields
        private float m_horizontalOffset;
        private float m_horizontalSpeed;
        private Vector2 m_startPosition;
        private Texture2D m_texture;
        private int m_totalHeight;
        private int m_totalWidth;
        #endregion

        #region Constructor
        // for static images
        public BackgroundImage(Texture2D texture, Vector2 position, int totalWidth, int totalHeight)
        {
            m_texture = texture;
            position.X -= texture.Width;
            m_startPosition = position;
            m_totalWidth = totalWidth;
            m_totalHeight = totalHeight;
            m_horizontalOffset = 0f;
            m_horizontalSpeed = 0f;
        }
        
        // for scrolling images
        public BackgroundImage(Texture2D texture, Vector2 position, int totalWidth, int totalHeight, 
            float horizontalSpeed)
        {
            m_texture = texture;
            position.X -= texture.Width;
            m_startPosition = position;
            m_totalWidth = totalWidth;
            m_totalHeight = totalHeight;
            m_horizontalSpeed = horizontalSpeed;
            m_horizontalOffset = 0f;
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime)
        {
            // update the offset.x for scrolling images
            m_horizontalOffset += m_horizontalSpeed;
            if (m_horizontalOffset > m_startPosition.X + m_texture.Width)
            {
                m_horizontalOffset -= m_texture.Width;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            if (camera == null) { return; }

            Vector2 position = m_startPosition;
            position.X += m_horizontalOffset;
            if (m_horizontalSpeed != 0) // scroll image across screen
            {   
                Rectangle drawRect = new Rectangle((int)position.X, (int)position.Y, m_texture.Width, m_totalHeight);

                while (drawRect.X < (camera.Position.X + camera.ViewportRectangle.Width))
                {
                    spriteBatch.Draw(
                        m_texture,
                        drawRect,
                        Color.White);

                    drawRect.X += m_texture.Width;
                }
            }
            else // image follows camera, looks like it doesn't move
            {    
                position = new Vector2(camera.Position.X / camera.Zoom, m_startPosition.Y);

                while (position.X < (camera.Position.X + camera.ViewportRectangle.Width))
                {
                    spriteBatch.Draw(
                        m_texture,
                        position,
                        Color.White);

                    position.X += m_texture.Width;
                }
            }
        }
        #endregion
    }
}