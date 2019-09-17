
namespace MyXMLData.WorldClasses
{    
    public struct Tile
    {
        public int TileIndex;
        public int TileSetIndex;
        public int CollisionType;

        public Tile(int tileIndex, int tileSetIndex)
        {
            TileIndex = tileIndex;
            TileSetIndex = tileSetIndex;
            CollisionType = -1;
        }

        public Tile(int tileIndex, int tileSetIndex, int collisionType)
        {
            TileIndex = tileIndex;
            TileSetIndex = tileSetIndex;
            CollisionType = collisionType;
        }
    }

    /// <summary>
    /// Holds data for map layers.
    /// </summary>
    public class MapLayerData
    {
        public string Name;
        public int TilesWide;
        public int TilesHigh;
        public int TileWidth;
        public int TileHeight;
        public Tile[] Layer;

        private MapLayerData()
        {
        }

        public MapLayerData(string name, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            Name = name;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            Layer = new Tile[tilesHigh * tilesWide];
        }

        public MapLayerData(int width, int height, int tileIndex, int tileSet, string mapLayerName)
        {
            Name = mapLayerName;
            TilesWide = width;
            TilesHigh = height;

            Layer = new Tile[height * width];

            Tile tile = new Tile(tileIndex, tileSet);

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    SetTile(x, y, tile);
        }

        public void SetTile(int x, int y, Tile tile)
        {
            Layer[y * TilesWide + x] = tile;
        }

        public void SetTile(int x, int y, int tileIndex, int tileSet)
        {
            Layer[y * TilesWide + x] = new Tile(tileIndex, tileSet);
        }

        public void SetTile(int x, int y, int tileIndex, int tileSet, int collisionType)
        {
            Layer[y * TilesWide + x] = new Tile(tileIndex, tileSet, collisionType);
        }

        public Tile GetTile(int x, int y)
        {
            return Layer[y * TilesWide + x];
        }
    }
}