using System.Collections.Generic;

namespace MyXMLData.WorldClasses
{
    /// <summary>
    /// Holds data for tile maps.
    /// </summary>
    public class TileMapData
    {
        public MapLayerData[] MapLayers;
        public TilesetData[] TileSets;
        public string MapName;
        public int TilesWide;
        public int TilesHigh;
        public int TileWidth;
        public int TileHeight;

        private TileMapData()
        {
        }

        public TileMapData(List<MapLayerData> mapLayers, List<TilesetData> tileSets, string mapName, 
            int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            MapLayers = mapLayers.ToArray();
            TileSets = tileSets.ToArray();
            MapName = mapName;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }
    }
}