
namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// A single map square. Holds info that tells what <c>TileSet</c> it belongs to and its collision type.
    /// </summary>
    public class Tile
    {
        public int CollisionType { get; set; }
        public int TileIndex { get; }
        public int TileSet { get; }
        
        public Tile(int tileIndex, int tileset)
        {
            TileIndex = tileIndex;
            TileSet = tileset;
            CollisionType = -1;
        }

        public Tile(int tileIndex, int tileset, int collisionType)
        {
            TileIndex = tileIndex;
            TileSet = tileset;
            CollisionType = collisionType;
        }
    }
}