using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyXMLData.WorldClasses;

namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// The actual level, comprised of <c>MapLayer</c>s.
    /// </summary>
    public class TileMap
    {
        #region Properties
        public int HeightInPixels { get { return TilesHigh * TileHeight; } }
        public List<MapLayer> LayerList { get; } = new List<MapLayer>();
        public string Name { get; set; }
        public List<TileSet> TileSetList { get; } = new List<TileSet>();
        public int TileHeight { get; }
        public int TilesHigh { get; set; }
        public int TilesWide { get; set; }
        public int TileWidth { get; }
        public int WidthInPixels { get { return TilesWide * TileWidth; } }      
        #endregion

        #region Constructors
        public TileMap()
        {
            TilesWide = 100;
            TilesHigh = 100;
            TileWidth = 16;
            TileHeight = 16;
        }

        public TileMap(string name, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            Name = name;
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }
        #endregion

        #region XNA Methods
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (LayerList == null || LayerList.Count == 0) { return; }

            foreach (MapLayer layer in LayerList)
            { 
                layer.Draw(spriteBatch, camera, TileSetList);
            }
        }

        public void DrawBackgroundTiles(SpriteBatch spriteBatch, Camera camera)
        {
            if (LayerList == null || LayerList.Count == 0) { return; }

            LayerList[0].Draw(spriteBatch, camera, TileSetList);
            LayerList[1].Draw(spriteBatch, camera, TileSetList);
        }

        public void DrawForegroundTiles(SpriteBatch spriteBatch, Camera camera)
        {
            if (LayerList == null || LayerList.Count == 0) { return; }

            LayerList[2].Draw(spriteBatch, camera, TileSetList);
        }

        public void DrawDoors(SpriteBatch spriteBatch, Camera camera)
        {
            if (LayerList == null || LayerList.Count == 0) { return; }

            LayerList[3].Draw(spriteBatch, camera, TileSetList);
        }
        #endregion

        #region Custom Methods
        public void AddLayer(MapLayer layer)
        {
            LayerList.Add(layer);
        }

        public void AddTileset(TileSet tileSet)
        {
            TileSetList.Add(tileSet);
        }

        public void RemoveLayer(MapLayer layer)
        {
            LayerList.Remove(layer);
        }

        public void RemoveTileset(TileSet tileSet)
        {
            TileSetList.Remove(tileSet);
        }

        public void ChangeSize(int newTilesWide, int newTilesHigh)
        {
            TilesWide = newTilesWide;
            TilesHigh = newTilesHigh;

            foreach (MapLayer l in LayerList)
            {
                l.ChangeSize(newTilesWide, newTilesHigh);
            }
        }

        public Point VectorToCell(Vector2 position, float zoom)
        {
            if (position.X < 0) { position.X -= 1 * TileWidth * zoom; }
            if (position.Y < 0) { position.Y -= 1 * TileHeight * zoom; }

            return new Point((int)(position.X / (TileWidth * zoom)), 
                (int)(position.Y / (TileHeight * zoom)));
        }

        public Vector2 GetCenter()
        {
            return new Vector2(WidthInPixels / 2, HeightInPixels / 2);
        }

        public static TileMap FromTileMapData(TileMapData tileMapData)
        {
            TileMap tileMap = new TileMap(tileMapData.MapName,
                tileMapData.TilesWide,
                tileMapData.TilesHigh,
                tileMapData.TileWidth,
                tileMapData.TileHeight);

            return tileMap;
        }
        #endregion
    }
}