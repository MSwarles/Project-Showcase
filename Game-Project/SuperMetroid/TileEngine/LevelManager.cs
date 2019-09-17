using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// Manager for <c>Level</c> class.
    /// </summary>
    public class LevelManager : DrawableGameComponent
    {
        #region Fields
        private Game1 m_gameRef;
        #endregion

        #region Properties
        public int HeightInPixels { get { return Level.HeightInPixels; } }
        public Level Level { get; set; }
        public Level NewLevel { get; set; }
        public Vector2 NewPlayerPos { get; set; }
        public Actor Player { get { return Level.Player; } }
        public int TileHeight { get { return Level.TileHeight; } }
        public int TileWidth { get { return Level.TileWidth; } }
        public int WidthInPixels { get { return Level.WidthInPixels; } }                     
        #endregion

        #region Constructor
        public LevelManager(Game game)
            : base(game)
        {
            m_gameRef = (Game1)game;
        }
        #endregion

        #region XNA Methods
        public void Update(GameTime gameTime, Camera camera)
        {
            if (Level == null) { return; }

            if (Level.IsChangingLevel && (!Level.IsFadingIn && !Level.IsFadingOut))
            {
                Level = NewLevel;
                Level.Player.Position = NewPlayerPos;
            }
            Level.Update(gameTime, this);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            if (Level == null) { return; }

            base.Draw(gameTime);
            Level.Draw(gameTime, spriteBatch, camera);
        }
        #endregion

        #region Custom Methods
        public void LoadLevel(string levelPath)
        {
            Level l = new Level(m_gameRef, this, levelPath);
            if (Level == null)
            {
                l.SpawnPlayer(levelPath);
            }
            Level = l;
        }

        public void ChangeLevel(Level l, Vector2 playerPos)
        {
            Level.IsChangingLevel = true;
            Level.IsFadingOut = true;
            NewLevel = l;
            NewPlayerPos = playerPos;
        }

        public void AddActor(Actor a)
        {
            Level.AddActor(a);
        }

        public void AddBullet(Vector2 pos, Vector2 velocity)
        {
            Level.AddBullet(pos, velocity);
        }
        #endregion
    }
}