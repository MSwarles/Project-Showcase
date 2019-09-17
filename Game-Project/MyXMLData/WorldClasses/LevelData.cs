using System.Collections.Generic;

namespace MyXMLData.WorldClasses
{
    /// <summary>
    /// Holds data for levels.
    /// </summary>
    public class LevelData
    {
        readonly Dictionary<string, TilesetData> m_tilesets;
        readonly Dictionary<string, MapLayerData> m_mapLayers;
        readonly Dictionary<string, TileMapData> m_mapData;

        public Dictionary<string, TilesetData> Tilesets { get { return m_tilesets; } }
        public Dictionary<string, MapLayerData> MapLayers { get { return m_mapLayers; } }
        public Dictionary<string, TileMapData> Maps { get { return m_mapData; } }

        public LevelData()
        {
            m_tilesets = new Dictionary<string, TilesetData>();
            m_mapLayers = new Dictionary<string, MapLayerData>();
            m_mapData = new Dictionary<string, TileMapData>();
        }
    }
}