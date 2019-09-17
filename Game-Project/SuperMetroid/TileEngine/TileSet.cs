using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// Separates a Texture2D image into an array of rectangles, which are then used as tiles by <c>TileMap</c> class.
    /// </summary>
    public class TileSet
    {
        #region Fields
        private Rectangle[] m_sourceRectangles;
        #endregion

        #region Properties        
        public string FilePath { get; }
        public string Name { get; set; }
        public Rectangle[] SourceRectangles { get { return (Rectangle[])m_sourceRectangles.Clone(); } }
        public Texture2D Texture { get; }
        public int TileHeight { get; }
        public int TilesHigh { get; }
        public int TilesWide { get; }
        public int TileWidth { get; }
        #endregion

        #region Constructor
        public TileSet(Texture2D image, string name, string filePath, int tilesWide, int tilesHigh, 
            int tileWidth, int tileHeight)
        {
            Texture = image;
            Name = name;
            FilePath = filePath;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
 
            int numOfTiles = tilesWide * tilesHigh;
            m_sourceRectangles = new Rectangle[numOfTiles];
            SetSourceRectangles();
        }
        #endregion

        #region Custom Methods
        private void SetSourceRectangles()
        {
            int tile = 0;

            for (int y = 0; y < TilesHigh; y++)
            {
                for (int x = 0; x < TilesWide; x++)
                {
                    m_sourceRectangles[tile] = new Rectangle(
                        x * TileWidth,
                        y * TileHeight,
                        TileWidth,
                        TileHeight);

                    tile++;
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}