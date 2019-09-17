using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SuperMetroid.Components;
using SuperMetroid.ControlComponents;

namespace SuperMetroid.GameStates
{
    /// <summary>
    /// GameState that allows the selection of starting a new game, loading a previous save,
    /// changing game settings, or exiting of the game.
    /// </summary>
    public interface IMainMenuState : IGameState { }

    public class MainMenuState : BaseGameState, IMainMenuState
    {
        #region Fields
        private Texture2D m_background;
        private Game1 m_gameRef;
        private MenuButton m_menuComponent;
        private SpriteFont m_spriteFont;
        #endregion

        #region Constructor
        public MainMenuState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IMainMenuState), this);
            m_gameRef = (Game1)game;
        }
        #endregion

        #region XNA Methods
        protected override void LoadContent()
        {
            string[] menuItems = { "NEW GAME", "CONTINUE", "OPTIONS", "EXIT" };
            m_background = m_gameRef.Content.Load<Texture2D>(@"GameScreens\menuscreen");
            m_spriteFont = m_gameRef.Content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            Texture2D texture = m_gameRef.Content.Load<Texture2D>(@"Misc\menu-button");

            m_menuComponent = new MenuButton(m_spriteFont, texture, menuItems);
            m_menuComponent.Position = new Vector2(1260 - m_menuComponent.Width, 110);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            m_menuComponent.Update(gameTime);
            GameRef.GamePlayState.SetUpNewGame();
            manager.PushState((GamePlayState)GameRef.GamePlayState, PlayerIndexInControl);

            if (Xin.KeyPressed(Keys.Space) || Xin.KeyPressed(Keys.Enter) || (m_menuComponent.IsMouseOver && Xin.CheckMouseReleased(MouseButtons.Left)))
            {
                if (m_menuComponent.SelectedIndex == 0)
                {
                    Xin.FlushInput();

                    GameRef.GamePlayState.SetUpNewGame();
                    manager.PushState((GamePlayState)GameRef.GamePlayState, PlayerIndexInControl);
                }
                else if (m_menuComponent.SelectedIndex == 1)
                {
                    Xin.FlushInput();

                    //GameRef.GamePlayState.LoadExistingGame();
                    //manager.PushState((GamePlayState)GameRef.GamePlayState, PlayerIndexInControl);
                }
                else if (m_menuComponent.SelectedIndex == 2)
                {
                    Xin.FlushInput();
                }
                else if (m_menuComponent.SelectedIndex == 3)
                {
                    GameRef.Exit();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();
            GameRef.SpriteBatch.Draw(m_background, Vector2.Zero, Color.White);
            base.Draw(gameTime);
            m_menuComponent.Draw(GameRef.SpriteBatch);
            GameRef.SpriteBatch.End();
        }
        #endregion
    }
}