using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SuperMetroid.Controls;
using SuperMetroid.Components;

namespace SuperMetroid.ControlComponents
{
    /// <summary>
    /// MenuButton control draws texture with a string in the center. The string changes color when hovered over.
    /// </summary>
    public class MenuButton : Control
    {
        #region Fields
        private readonly List<string> m_menuItems = new List<string>();
        private int m_selectedIndex = -1;
        private Texture2D m_texture;
        #endregion

        #region Properties
        public int Height { get; private set; }
        public Color HighlightColor { get; set; } = Color.Red;
        public bool IsMouseOver { get; private set; }
        public Color NormalColor { get; set; } = Color.White;
        public int SelectedIndex
        {
            get { return m_selectedIndex; }
            set
            {
                m_selectedIndex = MathHelper.Clamp(value, 0, m_menuItems.Count - 1);
            }
        }
        public int Width { get; private set; }
        #endregion

        #region Constructors
        public MenuButton(SpriteFont spriteFont, Texture2D texture)
        {
            IsMouseOver = false;
            m_spriteFont = spriteFont;
            m_texture = texture;
        }

        public MenuButton(SpriteFont spriteFont, Texture2D texture, string[] menuItems)
            : this(spriteFont, texture)
        {
            m_selectedIndex = 0;
            foreach (string s in menuItems)
            {
               m_menuItems.Add(s);
            }
            MeasureMenu();
        }
        #endregion

        #region XNA Methods
        public override void Update(GameTime gameTime)
        {
            Vector2 menuPosition = Position;
            Point p = Xin.MouseState.Position;
            Rectangle buttonRect;
            IsMouseOver = false;

            for (int i = 0; i < m_menuItems.Count; i++)
            {
                buttonRect = new Rectangle((int)menuPosition.X, (int)menuPosition.Y, m_texture.Width, m_texture.Height);

                if (buttonRect.Contains(p))
                {
                    m_selectedIndex = i;
                    IsMouseOver = true;
                }

                menuPosition.Y += m_texture.Height + 50;
            }

            if (!IsMouseOver && (Xin.KeyReleased(Keys.Up)))
            {
                m_selectedIndex--;
                if (m_selectedIndex < 0) { m_selectedIndex = m_menuItems.Count - 1; }
            }
            else if (!IsMouseOver && (Xin.KeyReleased(Keys.Down)))
            {
                m_selectedIndex++;
                if (m_selectedIndex > m_menuItems.Count - 1) { m_selectedIndex = 0; }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 menuPosition = Position;
            Color color;

            for (int i = 0; i < m_menuItems.Count; i++)
            {
                if (i == SelectedIndex)
                {
                    color = HighlightColor;
                }
                else
                {
                    color = NormalColor;
                }

                spriteBatch.Draw(m_texture, menuPosition, Color.White);

                Vector2 textSize = m_spriteFont.MeasureString(m_menuItems[i]);

                Vector2 textPosition = menuPosition + new Vector2((int)(m_texture.Width - textSize.X) / 2, (int)(m_texture.Height - textSize.Y) / 2);
                spriteBatch.DrawString(m_spriteFont,
                    m_menuItems[i],
                    textPosition,
                    color);

                menuPosition.Y += m_texture.Height + 50;
            }
        }
        #endregion

        #region Custom Methods
        public void SetMenuItems(string[] items)
        {
            m_menuItems.Clear();
            m_menuItems.AddRange(items);
            MeasureMenu();

            m_selectedIndex = 0;
        }

        private void MeasureMenu()
        {
            Width = m_texture.Width;
            Height = 0;

            foreach (string s in m_menuItems)
            {
                Vector2 size = m_spriteFont.MeasureString(s);
                Height += m_texture.Height + 50;

                if (size.X > Width) { Width = (int)size.X; }
            }

            Height -= 50;
        }

        public override void HandleInput(PlayerIndex playerIndex) { }
        #endregion
    }
}