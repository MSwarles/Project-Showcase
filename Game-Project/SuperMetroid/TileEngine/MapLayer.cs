using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MyXMLData.WorldClasses;

namespace SuperMetroid.TileEngine
{
    /// <summary>
    /// Contains a 2D array of <c>Tiles</c>, which are used by <c>Level</c>s.
    /// </summary>
    public class MapLayer
    {
        #region Properties
        public bool IsVisible { get; set; }
        public string Name { get; set; }
        public Tile[,] TileArray { get; set; }
        public int TileHeight { get; }
        public int TilesHigh { get { return TileArray.GetLength(0); } }
        public int TilesWide { get { return TileArray.GetLength(1); } }
        public int TileWidth { get; }
        public Color Tint { get; set; }
        public int ZValue { get; private set; }     
        #endregion

        #region Constructors
        // Creates layer based on given tile array
        public MapLayer(Tile[,] tileArray)
        {
            TileArray = (Tile[,])tileArray.Clone();
            SetDefaultValues();
        }

        // Creates layer with tile array made by given width and height
        public MapLayer(string name, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
        {
            Name = name;
            TileWidth = tileWidth;
            TileHeight = tileHeight;

            TileArray = new Tile[tilesHigh, tilesWide];

            for (int y = 0; y < tilesHigh; y++)
                for (int x = 0; x < tilesWide; x++)
                    TileArray[y, x] = new Tile(-1, -1);
 
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            ZValue = 0;
            IsVisible = true;
            Tint = Color.White;
        }
        #endregion

        #region XNA Methods
        public void Draw(SpriteBatch spriteBatch, Camera camera, List<TileSet> tileSets)
        {
            if (tileSets.Count == 0 || !IsVisible) { return; }

            Point cameraPoint = VectorToCell(camera.Position * (1 / camera.Zoom));
            Point viewPoint = VectorToCell(new Vector2(
                    (camera.Position.X + camera.ViewportRectangle.Width) * (1 / camera.Zoom),
                    (camera.Position.Y + camera.ViewportRectangle.Height) * (1 / camera.Zoom)));

            Point min = new Point();
            Point max = new Point();

            min.X = Math.Max(0, cameraPoint.X - 1);
            min.Y = Math.Max(0, cameraPoint.Y - 1);
            max.X = Math.Min(viewPoint.X + 1, TilesWide);
            max.Y = Math.Min(viewPoint.Y + 1, TilesHigh);

            Rectangle destRect = new Rectangle(0, 0, TileWidth, TileHeight);
            Tile tile;

            for (int y = min.Y; y < max.Y; y++)
            {
                destRect.Y = y * TileHeight;

                for (int x = min.X; x < max.X; x++)
                {
                    tile = GetTile(x, y);

                    if (tile.TileIndex == -1 || tile.TileSet == -1) { continue; }

                    destRect.X = x * TileHeight;

                    spriteBatch.Draw(
                        tileSets[tile.TileSet].Texture,
                        destRect,
                        tileSets[tile.TileSet].SourceRectangles[tile.TileIndex],
                        Tint);                    
                }
            }
        }
        #endregion

        #region Custom Methods
        /// <summary>
        /// Gets the tile with coordinates of x, y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            if (x >= TileArray.GetLength(1) || x < 0 ||
                y >= TileArray.GetLength(0) || y < 0)
            {
                return null;
            }
            
            return TileArray[y, x];
        }

        /// <summary>
        /// Sets the tile at coordinates of x, y. 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tile"></param>
        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= TileArray.GetLength(1) || x < 0 ||
                y >= TileArray.GetLength(0) || y < 0)
            {
                return;
            }

            TileArray[y, x] = tile;
        }

        public void SetTile(int x, int y, int tileIndex, int tileSet)
        {
            if (x >= TileArray.GetLength(1) || x < 0 ||
                y >= TileArray.GetLength(0) || y < 0)
            {
                return;
            }

            TileArray[y, x] = new Tile(tileIndex, tileSet);
        }

        public void SetTile(int x, int y, int tileIndex, int tileSet, int collisionType)
        {
            if (x >= TileArray.GetLength(1) || x < 0 ||
                y >= TileArray.GetLength(0) || y < 0)
            {
                return;
            }

            TileArray[y, x] = new Tile(tileIndex, tileSet, collisionType);
        }

        /// <summary>
        /// Changes the size of the map layer.
        /// </summary>
        /// <param name="tilesWide"></param>
        /// <param name="tilesHigh"></param>
        public void ChangeSize(int tilesWide, int tilesHigh)
        {
            Tile[,] newTileArray = new Tile[tilesHigh, tilesWide];

            for (int y = 0; y < tilesHigh; y++)
            {
                for (int x = 0; x < tilesWide; x++)
                {
                    Tile newTile = GetTile(x, y);

                    if (newTile == null) { newTile = new Tile(-1, -1); }

                    newTileArray[y, x] = newTile;
                }
            }

            TileArray = newTileArray;
        }

        public void Hide() { IsVisible = false; }

        public void Show() { IsVisible = true; }

        /// <summary>
        /// Gets the alpha value of the layer.
        /// </summary>
        /// <returns> the alpha value of the layer </returns>
        public int GetAlpha() { return Tint.A; }

        /// <summary>
        /// Sets the alpha value of the layer.
        /// </summary>
        /// <param name="alpha"> alpha value, between 0-255 </param>
        public void SetAlpha(int alpha)
        {
            MathHelper.Clamp(alpha, 0, 255);
            Tint = new Color(Tint, alpha);
        }

        /// <summary>
        /// Sets collision type of tile at coordinate x, y.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="collisionType"></param>
        public void SetCurrentCollision(int x, int y, int collisionType)
        {
            if (TileArray[y, x] != null)
            {
                TileArray[y, x].CollisionType = collisionType;
            }
        }

        /// <summary>
        /// Sets collision type of all tiles of a specific type.
        /// </summary>
        /// <param name="tileIndex"></param>
        /// <param name="tileset"></param>
        /// <param name="collisionType"></param>
        public void SetAsDefaultCollision(int tileIndex, int tileset, int collisionType)
        {
            foreach (Tile t in TileArray)
            {
                if (t.TileSet == tileset && t.TileIndex == tileIndex)
                {
                    t.CollisionType = collisionType;
                }
            }
        }

        public Point VectorToCell(Vector2 position)
        {
            return new Point((int)position.X / TileWidth, (int)position.Y / TileHeight);
        }

        public override string ToString() { return Name; }
        #endregion

        #region Data Methods
        public static MapLayer FromMapLayerData(MapLayerData data)
        {
            MapLayer layer = new MapLayer(
                data.Name,
                data.TilesWide, 
                data.TilesHigh, 
                data.TileWidth, 
                data.TileHeight);

            for (int y = 0; y < data.TilesHigh; y++)
            {
                for (int x = 0; x < data.TilesWide; x++)
                {
                    layer.SetTile(
                        x,
                        y,
                        data.GetTile(x, y).TileIndex,
                        data.GetTile(x, y).TileSetIndex,
                        data.GetTile(x, y).CollisionType);
                }
            }

            return layer;
        }

        public MapLayerData ToMapLayerData()
        {
            MapLayerData data = new MapLayerData( 
                Name, 
                TilesWide, 
                TilesHigh,
                TileWidth,
                TileHeight);

            for (int y = 0; y < TilesHigh; y++)
            {
                for (int x = 0; x < TilesWide; x++)
                {
                    data.SetTile(
                        x,
                        y,
                        GetTile(x, y).TileIndex,
                        GetTile(x, y).TileSet);
                }
            }

            return data;
        }
        #endregion
    }
}