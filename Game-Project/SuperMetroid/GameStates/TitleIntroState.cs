using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SuperMetroid.Components;

namespace SuperMetroid.GameStates
{
    /// <summary>
    /// GameState that shows the game introduction when the game is initially loaded.
    /// </summary>
    public interface ITitleIntroState : IGameState
    {
    }

    public class TitleIntroState : BaseGameState, ITitleIntroState
    {
        #region Fields
        private Texture2D m_background;
        private Rectangle m_backgroundDest;
        private TimeSpan m_elapsedTime;
        private SpriteFont m_font;
        private string m_message;
        private Vector2 m_position;
        #endregion

        #region Constructor
        public TitleIntroState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(ITitleIntroState), this);
        }
        #endregion

        #region XNA Methods
        public override void Initialize()
        {
            m_backgroundDest = GameRef.ScreenRectangle;
            m_elapsedTime = TimeSpan.Zero;
            m_message = "PRESS SPACE TO CONTINUE";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            m_background = content.Load<Texture2D>(@"GameScreens\titlescreen");
            m_font = content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            Vector2 stringSize = m_font.MeasureString(m_message);
            m_position = new Vector2((GameRef.ScreenRectangle.Width - stringSize.X) / 2, GameRef.ScreenRectangle.Bottom - 50 - m_font.LineSpacing);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            PlayerIndex? index = null;
            m_elapsedTime += gameTime.ElapsedGameTime;

            if (Xin.KeyReleased(Keys.Space) || Xin.KeyReleased(Keys.Enter) || Xin.CheckMouseReleased(MouseButtons.Left))
            {
                manager.ChangeState((MainMenuState)GameRef.StartMenuState, index);
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Color color = new Color(1f, 1f, 1f) * (float)Math.Abs(Math.Sin(m_elapsedTime.TotalSeconds * 2));

            GameRef.SpriteBatch.Begin();
            GameRef.SpriteBatch.Draw(m_background, m_backgroundDest, Color.White);
            GameRef.SpriteBatch.DrawString(m_font, m_message, m_position, color);
            GameRef.SpriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}