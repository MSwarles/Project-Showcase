using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using SuperMetroid.ActorComponents;
using SuperMetroid.Components;
using SuperMetroid.TileEngine;

namespace SuperMetroid.GameStates
{
    /// <summary>
    /// GameState where the game is played. Handles loading of levels, controls what the player sees, 
    /// and displays the players health, missiles, and map.
    /// </summary>
    public interface IGamePlayState
    {
        DebugComponent Debug { get; }
        void SetUpNewGame();
        void LoadExistingGame();
    }
    
    public class GamePlayState : BaseGameState, IGamePlayState
    {
        #region Fields
        private Camera m_camera;
        private SpriteFont m_font;
        private HUD m_hud;
        private LevelManager m_levelManager;
        private string m_levelToLoad;

        // TODO: REMOVE LATER; USED FOR TESTING
        private static string mapDir = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game\\Maps\\";
        private string crateria = mapDir + "crateria.xml";
        private string physicsTest = mapDir + "physicsTest.xml";
        private string testRoom = mapDir + "testRoom.xml";
        #endregion

        #region Properties
        public DebugComponent Debug { get; private set; }
        #endregion

        #region Constructor
        public GamePlayState(Game game)
            : base(game)
        {
            game.Services.AddService(typeof(IGamePlayState), this);
            
            m_camera = new Camera(GameRef.ScreenRectangle);
            m_camera.ZoomOnPosition(4.25f, Vector2.Zero);
            m_levelToLoad = testRoom;    // change this to set what level is loaded
        }
        #endregion

        #region XNA Methods
        protected override void LoadContent()
        {
            Debug = new DebugComponent(content);
            m_font = content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            m_hud = new HUD(GameRef);
        }

        public override void Update(GameTime gameTime)
        {
            if (Xin.KeyReleased(Keys.Z))
            {
                m_camera.ZoomIn();
            }
            else if (Xin.KeyReleased(Keys.C))
            {
                m_camera.ZoomOut();
            }

            // TODO: REMOVE LATER; FOR TESTING ONLY
            if (Xin.KeyReleased(Keys.F5))
            {
                m_levelManager.LoadLevel(crateria);
                m_levelManager.Level.SpawnPlayer(crateria);
            }
            else if (Xin.KeyReleased(Keys.F6))
            { 
                m_levelManager.LoadLevel(physicsTest);
                m_levelManager.Level.SpawnPlayer(physicsTest);
            }   
            else if (Xin.KeyReleased(Keys.F7))
            {
                m_levelManager.LoadLevel(testRoom);
                m_levelManager.Level.SpawnPlayer(testRoom);
            }

            if (Xin.KeyReleased(Keys.G))
            {
                m_levelManager.Level.IsFadingOut = !m_levelManager.Level.IsFadingOut;
            }

            if (Xin.KeyReleased(Keys.OemMinus))
            {
                GameRef.GameSpeed /= 2;
            }
            else if (Xin.KeyReleased(Keys.OemPlus))
            {
                GameRef.GameSpeed *= 2;
            }
            
            m_levelManager.Update(gameTime, m_camera);
            m_camera.Update(gameTime);
            m_camera.LockToActor(m_levelManager.Player);
            
            // TODO: GAMEPLAY STATE - adjust camera and world lock to accomodate HUD size
            m_camera.LockToLevel(m_levelManager.WidthInPixels, m_levelManager.HeightInPixels);
            m_hud.Update(m_camera);
            Debug.Update(gameTime, m_levelManager.Player, m_camera, m_levelManager);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GameRef.SpriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null, 
                null, 
                null,
                m_camera.Transformation);
            m_levelManager.Draw(gameTime, GameRef.SpriteBatch, m_camera);
            GameRef.SpriteBatch.End();

            m_hud.Draw(GameRef.SpriteBatch, m_camera);
            Debug.Draw(gameTime, GameRef.SpriteBatch);                  
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a new game.
        /// </summary>
        public void SetUpNewGame()
        {
            m_levelManager = new LevelManager(GameRef);
            m_levelManager.LoadLevel(m_levelToLoad);
        }

        /// <summary>
        /// Loads an existing game from a save file.
        /// </summary>
        public void LoadExistingGame()
        {
        }
        #endregion
    }
}