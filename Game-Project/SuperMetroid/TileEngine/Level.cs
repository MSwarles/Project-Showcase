using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid.ActorComponents;
using SuperMetroid.Animations;

using MyXMLData.AnimationClasses;
using MyXMLData.WorldClasses;

namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// Responsible for the updating and drawing the level and its actors.
    /// </summary>
    public class Level : DrawableGameComponent
    {
        // TODO: LEVEL - convert Triggers to Actor / Component
        #region Fields
        private List<Actor> m_actorList = new List<Actor>();
        private float m_alpha = 0f;
        private List<BackgroundImage> m_bgImageList = new List<BackgroundImage>();
        private List<Actor> m_bulletList = new List<Actor>();
        private List<Actor> m_bulletsToRemove = new List<Actor>();
        private Color m_color = Color.White;
        private Game1 m_gameRef;
        private Texture2D m_hitboxTexture;
        private LevelManager m_lmRef;
        private Texture2D m_transitionTexture;      
        private float m_transitionTimer;
        
        private string mapDir = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game\\Maps\\";
        // TEMP VECTORS FOR PLAYER STARTING POSITIONS - REMOVE LATER
        private Vector2 physTestStartPos { get { return new Vector2(72, 370); } }
        private Vector2 physRampStartPos { get { return new Vector2(1648, 370); } }
        private Vector2 crateriaStartPos { get { return new Vector2(1500, 1150); } }
        private Vector2 testRoomStartPos { get { return new Vector2(256, 176); } }
        #endregion

        #region Properties
        public float Alpha { get { return m_alpha; } set { m_alpha = MathHelper.Clamp(value, 0f, 2f); } }
        public int HeightInPixels { get { return TileMap.HeightInPixels; } }
        public bool IsChangingLevel { get; set; } = false;
        public bool IsFadingIn { get; set; } = false;
        public bool IsFadingOut { get; set; } = false;
        public Actor Player { get; set; }
        public int TileHeight { get { return TileMap.TileHeight; } }
        public TileMap TileMap { get; private set; }
        public int TileWidth { get { return TileMap.TileWidth; } }
        public List<DoorTrigger> Triggers { get; } = new List<DoorTrigger>();
        public int WidthInPixels { get { return TileMap.WidthInPixels; } }
        #endregion

        #region Constructor
        public Level(Game game, LevelManager lm, TileMap tileMap)
            : base(game)
        {
            m_gameRef = (Game1)game;
            m_lmRef = lm;
            TileMap = tileMap;
            LoadContent();
        }

        public Level(Game game, LevelManager lm, string levelPath)
            : base(game)
        {
            m_gameRef = (Game1)game;
            m_lmRef = lm;

            LoadTileMap(levelPath);
            SpawnTriggers(levelPath);
            LoadContent();
        }
        #endregion

        #region XNA Methods
        protected override void LoadContent()
        {
            m_hitboxTexture = m_gameRef.Content.Load<Texture2D>(@"Misc\boundingBox");
            m_transitionTexture = m_gameRef.Content.Load<Texture2D>(@"Game\Backgrounds\black");
            Texture2D bgTexture = m_gameRef.Content.Load<Texture2D>(@"Game\Backgrounds\crateriaBG");
            Texture2D lbCloudBottom = m_gameRef.Content.Load<Texture2D>(@"Game\Backgrounds\lbBottom");
            Texture2D lbCloudMiddle = m_gameRef.Content.Load<Texture2D>(@"Game\Backgrounds\lbMiddle");
            Texture2D lbCloudTop = m_gameRef.Content.Load<Texture2D>(@"Game\Backgrounds\lbTop");

            //TODO: LEVEL - move bgImageList to level creation data / implement using Actors and Components
            // BACKGROUND mountains
            BackgroundImage mountains = new BackgroundImage(
                bgTexture,
                new Vector2(0, HeightInPixels - bgTexture.Height - 32),
                m_gameRef.ScreenRectangle.Width, bgTexture.Height);
            m_bgImageList.Add(mountains);

            // BACKGROUND first cloud
            BackgroundImage partOfCloud = new BackgroundImage(
                lbCloudBottom,
                new Vector2(0, HeightInPixels - bgTexture.Height - 41),
                m_gameRef.ScreenRectangle.Width, lbCloudBottom.Height, -0.65f);
            m_bgImageList.Add(partOfCloud);

            partOfCloud = new BackgroundImage(
                lbCloudMiddle,
                new Vector2(0, HeightInPixels - bgTexture.Height - 55),
                m_gameRef.ScreenRectangle.Width, 14, 0.8f);
            m_bgImageList.Add(partOfCloud);

            partOfCloud = new BackgroundImage(
                lbCloudTop,
                new Vector2(0, HeightInPixels - bgTexture.Height - 56),
                m_gameRef.ScreenRectangle.Width, lbCloudTop.Height, -0.9f);
            m_bgImageList.Add(partOfCloud);

            // BACKGROUND second cloud
            partOfCloud = new BackgroundImage(
                lbCloudBottom,
                new Vector2(0, HeightInPixels - bgTexture.Height - 88),
                m_gameRef.ScreenRectangle.Width, lbCloudBottom.Height, -0.50f);
            m_bgImageList.Add(partOfCloud);

            partOfCloud = new BackgroundImage(
                lbCloudMiddle,
                new Vector2(0, HeightInPixels - bgTexture.Height - 120),
                m_gameRef.ScreenRectangle.Width, 32, 0.8f);
            m_bgImageList.Add(partOfCloud);

            partOfCloud = new BackgroundImage(
                lbCloudTop,
                new Vector2(0, HeightInPixels - bgTexture.Height - 121),
                m_gameRef.ScreenRectangle.Width, lbCloudTop.Height, -0.70f);
            m_bgImageList.Add(partOfCloud);
        }

        public void Update(GameTime gameTime, LevelManager world)
        {
            if (IsChangingLevel)
            {
                Transition(gameTime);
            }
            else
            {
                // Update background images
                foreach (BackgroundImage bi in m_bgImageList)
                {
                    bi.Update(gameTime);
                }

                // Update the actors
                foreach (Actor a in m_actorList)
                {
                    a.Update(gameTime, world);
                }

                // Remove bullets from list
                foreach (Actor b in m_bulletsToRemove)
                {
                    m_bulletList.Remove(b);
                }

                // Clear removal list
                m_bulletsToRemove.Clear();

                // Check bullet list for bullets ready for removal, and update them
                foreach (Actor b in m_bulletList)
                {
                    b.Update(gameTime, world);
                }
            }
        }

        /*
         * Draw order should be: background images, background tiles, actors, 
         * foreground tiles, bullets, doors
         */
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera camera)
        {
            foreach (BackgroundImage bi in m_bgImageList)       // draw background images
            {
                bi.Draw(gameTime, spriteBatch, camera);
            }
            TileMap.DrawBackgroundTiles(spriteBatch, camera);   // draw background tiles
            foreach (Actor a in m_actorList)                    // draw actors
            {                  
                a.Draw(gameTime, spriteBatch);
            }
            TileMap.DrawForegroundTiles(spriteBatch, camera);   // draw foreground tiles
            foreach (Actor b in m_bulletList)                   // draw bullets
            {                
                b.Draw(gameTime, spriteBatch);
            }

            if (Alpha > 0f)                                   // draw transition screen
            {
                spriteBatch.Draw(
                    m_transitionTexture,
                    new Rectangle((int)(camera.Position.X / camera.Zoom), (int)(camera.Position.Y / camera.Zoom), camera.ViewportRectangle.Width, camera.ViewportRectangle.Height), 
                    m_color * Alpha);
            }

            TileMap.DrawDoors(spriteBatch, camera);             // draw doors

            /*
            if (Player != null && Player.ShowHitbox)            // draw hitboxes (if enabled)
            {
                for (int i = 0; i < Player.TileRectList.Count; i++)
                {
                    spriteBatch.Draw(
                        m_hitboxTexture,
                        new Rectangle(
                            Player.TileRectList[i].X,
                            Player.TileRectList[i].Y,
                            Player.TileRectList[i].Width,
                            Player.TileRectList[i].Height),
                        Player.TileColorList[i]);
                }
            }
            */

            foreach (DoorTrigger t in Triggers)
            {
                t.Draw(gameTime, spriteBatch);
            }
        }
        #endregion

        /// <summary>
        /// Adds an actor.
        /// </summary>
        /// <param name="actor"></param>
        #region Add Actors
        public void AddActor(Actor actor)
        {
            m_actorList.Add(actor);
            Player = actor;
        }

        /// <summary>
        /// Adds a bullet object.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        public void AddBullet(Vector2 position, Vector2 velocity)
        {
            Dictionary<string, Animation> animationDict = new Dictionary<string, Animation>();
            Texture2D texture = null;
            AnimationGroupData animationGroupData = null;
            string path = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game\\Animations\\bullet.xml";     
            // read animation data from xml file
            try
            {
                animationGroupData = XnaSerializer.Deserialize<AnimationGroupData>(path);
                Stream stream = new FileStream(animationGroupData.TextureFilePath, FileMode.Open, FileAccess.Read);
                texture = Texture2D.FromStream(GraphicsDevice, stream);
                stream.Close();
                stream.Dispose();
            }
            catch (Exception exc)
            {
                throw new Exception("Error creating bullet in AddBullet method in Level class.", exc);
            }

            // load animation data into dictionary
            List<string> animationNames = animationGroupData.AnimationNames.ToList<string>();
            List<AnimationData> animationData = animationGroupData.AnimationData.ToList<AnimationData>();
            for (int i = 0; i < animationGroupData.AnimationNames.Count(); i++)
            {
                Animation animation = Animation.FromAnimationData(animationData[i]);
                animation.Texture = texture;
                animationDict.Add(animationNames[i], animation);
            }

            Actor bullet = new Actor(m_gameRef);
            bullet.Position = position;
            BulletPhysicsComponent physics = new BulletPhysicsComponent(m_gameRef);
            physics.Velocity = velocity;
            bullet.AddComponent(physics);
            bullet.AddComponent(new BulletGraphicsComponent(m_gameRef, animationDict, texture));
            m_bulletList.Add(bullet);
        }

        /// <summary>
        /// Adds a trigger.
        /// </summary>
        /// <param name="t"></param>
        public void AddTrigger(DoorTrigger t)
        {
            Triggers.Add(t);
        }

        /// <summary>
        /// Removes a bullet.
        /// </summary>
        /// <param name="bullet"></param>
        public void RemoveBullet(Actor bullet)
        {
            m_bulletsToRemove.Add(bullet);
        }

        /// <summary>
        /// Fades level in and out when switching levels. 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Transition(GameTime gameTime)
        {
            if (IsFadingOut)
            {
                if (Alpha < 2f)
                {
                    Alpha += 0.05f;
                }
                else
                {
                    IsFadingOut = false;
                }
            }
            else if (m_transitionTimer < 1000f)
            {
                // TODO: LEVEL - add transition animation
                m_transitionTimer += gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (IsFadingIn)
            {
                if (Alpha > 0f)
                {
                    Alpha -= 0.05f;
                }
                else
                {
                    IsFadingIn = false;
                    IsChangingLevel = false;
                    m_transitionTimer = 0;
                }
            }
        }

        /// <summary>
        /// Loads a tile map.
        /// </summary>
        /// <param name="tileMapPath"></param>
        private void LoadTileMap(string tileMapPath)
        {
            TileMapData tileMapData = null;
            TileMap tileMap;
            try
            {
                tileMapData = XnaSerializer.Deserialize<TileMapData>(tileMapPath);
            }
            catch (Exception exc)
            {
                throw new Exception("Error loading tile map data in LoadLevel method in LevelManager class", exc);
            }
            tileMap = new TileMap(
                tileMapData.MapName,
                tileMapData.TilesWide,
                tileMapData.TilesHigh,
                tileMapData.TileWidth,
                tileMapData.TileHeight);

            foreach (TilesetData tileSetData in tileMapData.TileSets)
            {
                Texture2D texture = null;
                using (Stream stream = new FileStream(tileSetData.FilePath, FileMode.Open,
                    FileAccess.Read))
                {
                    texture = Texture2D.FromStream(GraphicsDevice, stream);
                    tileMap.TileSetList.Add(
                        new TileSet(
                            texture,
                            tileSetData.Name,
                            tileSetData.FilePath,
                            tileSetData.TilesWide,
                            tileSetData.TilesHigh,
                            tileSetData.TileWidth,
                            tileSetData.TileHeight));
                }
            }

            foreach (MapLayerData layerData in tileMapData.MapLayers)
            {
                tileMap.LayerList.Add(MapLayer.FromMapLayerData(layerData));
            }
            TileMap = tileMap;
        }

        /// <summary>
        /// Spawns a player.
        /// </summary>
        /// <param name="levelPath"></param>
        public void SpawnPlayer(string levelPath)
        {
            Dictionary<string, Animation> animationDict = new Dictionary<string, Animation>();
            Texture2D playerTexture = null;
            AnimationGroupData animationGroupData = null;
            string path = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game\\Animations\\samus.xml";
            // read player animation data from xml file
            try
            {
                animationGroupData = XnaSerializer.Deserialize<AnimationGroupData>(path);

                Stream stream = new FileStream(animationGroupData.TextureFilePath, FileMode.Open, FileAccess.Read);
                playerTexture = Texture2D.FromStream(GraphicsDevice, stream);

                stream.Close();
                stream.Dispose();
            }
            catch (Exception exc)
            {
                throw new Exception("Error creating player", exc);
            }

            // load player animation data into animation dictionary
            List<string> animationNames = animationGroupData.AnimationNames.ToList<string>();
            List<AnimationData> animationData = animationGroupData.AnimationData.ToList<AnimationData>();
            for (int i = 0; i < animationGroupData.AnimationNames.Count(); i++)
            {
                Animation animation = Animation.FromAnimationData(animationData[i]);
                animation.Texture = playerTexture;
                animationDict.Add(animationNames[i], animation);
            }

            // create the player
            Actor player = new Actor(m_gameRef);
            player.AddComponent(new PlayerInputComponent(m_gameRef));
            player.AddComponent(new PlayerPhysicsComponent(m_gameRef));
            player.AddComponent(new PlayerGraphicsComponent(m_gameRef, animationDict, playerTexture));

            // PLAYER POSITIONING USED FOR TESTING LEVELS - REMOVE LATER
            if (levelPath.Equals(mapDir + "crateria.xml"))
            {
                player.Position = crateriaStartPos;
            }
            else if (levelPath.Equals(mapDir + "physicsTest.xml"))
            {
                player.Position = physTestStartPos;
            }
            else if (levelPath.Equals(mapDir + "testRoom.xml"))
            {
                player.Position = testRoomStartPos;
            }

            AddActor(player);
        }

        /// <summary>
        /// Loads triggers for a level.
        /// TEMPORARY - Used until level editor supports triggers.
        /// </summary>
        /// <param name="levelPath"></param>
        private void SpawnTriggers(string levelPath)
        {
            if (levelPath.Equals(mapDir + "crateria.xml"))
            {
                AddTrigger(new DoorTrigger(
                    m_gameRef,
                    new Rectangle(0, HeightInPixels - 160, 16, 64),
                    mapDir + "testRoom.xml",
                    new Vector2(446, 169)));
                AddTrigger(new DoorTrigger(
                    m_gameRef,
                    new Rectangle(WidthInPixels - 16, HeightInPixels - 160, 16, 64),
                    mapDir + "testRoom.xml",
                    new Vector2(16, 169)));
            }
            else if (levelPath.Equals(mapDir + "physicsTest.xml"))
            {

            }
            else if (levelPath.Equals(mapDir + "testRoom.xml"))
            {
                AddTrigger(new DoorTrigger(
                    m_gameRef,
                    new Rectangle(0, HeightInPixels - 128, 16, 64),
                    mapDir + "crateria.xml",
                    new Vector2(2236, 1129)));
                AddTrigger(new DoorTrigger(
                    m_gameRef,
                    new Rectangle(WidthInPixels - 16, HeightInPixels - 128, 16, 64),
                    mapDir + "crateria.xml",
                    new Vector2(16, 1129)));
            }
        }
        #endregion
    }
}
