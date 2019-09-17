using System;

using SuperMetroid.Components;
using SuperMetroid.GameStates;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMetroid
{
    /// <summary>
    /// The main game class.
    /// </summary>
    public class Game1 : Game
    {
        #region General TODO List
        // TODO: GUI - add in-game menu
        #endregion

        #region Fields and Properties
        // Graphics Fields and Properties
        private GraphicsDeviceManager m_graphics;
        public SpriteBatch SpriteBatch { get; private set; }
        public SpriteFont Font { get; private set; }

        // GameState Fields and Properties
        private GameStateManager m_gameStateManager;
        public ITitleIntroState TitleIntroState { get; private set; }
        public IMainMenuState StartMenuState { get; private set; }
        public IGamePlayState GamePlayState { get; private set; }

        // Screen Fields and Properties
        private const int m_screenWidth = 1280;
        private const int m_screenHeight = 960;
        public Rectangle ScreenRectangle { get; }

        // Audio Properties
        public AudioManager AudioManager { get; }

        // FPS Display Fields
        private int m_lastTick;
        private int m_lastFrameRate;
        private int m_frameRate;
        private int m_framesPerSec = 60;
        private float m_frameTimer;
        private float m_gameSpeed = 4f;

        public float GameSpeed
        {
            get { return m_gameSpeed; }
            set
            {
                m_gameSpeed = MathHelper.Clamp(value, 0.0625f, 4f);
            }
        }
        #endregion

        #region Constructor
        public Game1()
        {
            m_graphics = new GraphicsDeviceManager(this);
            m_graphics.PreferredBackBufferWidth = m_screenWidth;
            m_graphics.PreferredBackBufferHeight = m_screenHeight;
            ScreenRectangle = new Rectangle(0, 0, m_screenWidth, m_screenHeight);

            Content.RootDirectory = "Content";

            m_gameStateManager = new GameStateManager(this);
            Components.Add(m_gameStateManager);
            TitleIntroState = new TitleIntroState(this);
            StartMenuState = new MainMenuState(this);
            GamePlayState = new GamePlayState(this);
            m_gameStateManager.ChangeState((MainMenuState)StartMenuState, PlayerIndex.One);

            AudioManager = new AudioManager(this);

            IsMouseVisible = true;
        }
        #endregion

        #region XNA Methods
        protected override void Initialize()
        {
            Components.Add(new Xin(this));
            base.Initialize();
        }

        // Spritebatch requires GraphicsDevice, which isn't created until base.Initialize(),
        // so it must be created in LoadContent()
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Font = Content.Load<SpriteFont>(@"Fonts\GameFont");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            if (Xin.ButtonPressed(Buttons.Back, PlayerIndex.One) || Xin.KeyDown(Keys.Escape))
            {
                Exit();
            }
          
            // Update the window title with the current FPS
            Window.Title = "Super Metroid  -  FPS " + m_lastFrameRate;

            // Update the game
            m_frameTimer += gameTime.ElapsedGameTime.Milliseconds;
            if (m_frameTimer / m_gameSpeed > (1000 / m_framesPerSec))
            {
                CalculateFrameRate();
                base.Update(gameTime);
                m_frameTimer -= (m_frameTimer / m_gameSpeed);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            Color metroidBrown = new Color(64, 56, 56);
            GraphicsDevice.Clear(metroidBrown);

            base.Draw(gameTime);
        }
        #endregion

        /// <summary>
        /// Calculates the game's current FPS.
        /// </summary>
        private void CalculateFrameRate()
        {
            if (Environment.TickCount - m_lastTick >= 1000)
            {
                m_lastFrameRate = m_frameRate;
                m_frameRate = 0;
                m_lastTick = Environment.TickCount;
            }

            m_frameRate++;
        }
        
    }
}