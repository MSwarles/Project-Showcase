using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using GDIBitmap = System.Drawing.Bitmap;
using GDIColor = System.Drawing.Color;
using GDIImage = System.Drawing.Image;
using GDIGraphics = System.Drawing.Graphics;
using GDIGraphicsUnit = System.Drawing.GraphicsUnit;
using GDIRectangle = System.Drawing.Rectangle;
using GDISolidBrush = System.Drawing.SolidBrush;
using GDIPen = System.Drawing.Pen;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using SuperMetroid;
using SuperMetroid.TileEngine;

using MyXMLData.WorldClasses;

namespace MapEditor
{
    public partial class FormMain : Form
    {
        #region TODO List
        // 'general'
        // TODO: MAP EDITOR - add events for switching levels - 5/23/19
        // TODO: MAP EDITOR - add way to add BackgroundImages through editor - 5/23/19
        // TODO: MAP EDITOR - add map background color selection - 5/23/19
        // TODO: MAP EDITOR - redesign using command pattern (for undo / redo options) - 5/23/19
        // TODO: MAP EDITOR - fix bar scrolling with zoom out
        // TODO: MAP EDITOR - fix map scaling with window
        // TODO: MAP EDITOR - add animated tiles

        // 'edit'
        // TODO: MAP EDITOR - clean up event code for preferences on both forms

        // 'map'
        // TODO: MAP EDITOR - refine grid code / make grid lines thinner? (render 2D?)

        // 'layer'
        // TODO: MAP EDITOR - fix 'move layer' arrows on/off (on first and last layers, allows for out of bounds error / crash) - 5/23/19

        // 'tileset'
        // TODO: MAP EDITOR - add zooming feature for tileset
        // TODO: MAP EDITOR - add group selection for tiles
        #endregion

        #region Fields
        SpriteBatch spriteBatch;

        Camera m_camera;

        TileMap m_tileMap;
        List<TilesetData> m_tileSetDataList = new List<TilesetData>();
        List<GDIImage> m_tileSetImageList = new List<GDIImage>();
        List<string> m_debugTextList = new List<string>();

        Texture2D m_cursorTexture;
        Texture2D m_highlightTexture;
        Texture2D m_gridTexture;
        Texture2D m_pixelTexture;

        Vector2 m_cursorPos = Vector2.Zero;

        Color m_mapBackdropColor = Color.DimGray;
        Color m_mapBgColor = Color.DarkGray;
        Color m_gridColor = Color.White;
        Color m_cursorHighlightColor;
        GDIColor m_tileSetGridColor = GDIColor.White;
        GDIColor m_selectedTileColor = GDIColor.CornflowerBlue;

        Point m_selectedTile;
        Point m_mouse = new Point();
        bool m_isLeftMouseDown = false;
        bool m_isRightMouseDown = false;
        bool m_trackMouse = false;
        bool m_highlightSelectedLayer = false;

        bool m_isZooming = false;

        int m_frameCount = 0;
        int m_brushSize = 1;
        #endregion

        #region Properties
        public GraphicsDevice GraphicsDevice { get { return mapDisplay.GraphicsDevice; } }       
        #endregion

        #region Constructor
        public FormMain()
        {
            InitializeComponent();

            #region Form Events
            this.Load += new EventHandler(FormMain_Load);
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
            this.KeyDown += new KeyEventHandler(FormMain_KeyDown);
            this.MouseWheel += new MouseEventHandler(FormMain_MouseWheel);
            #endregion

            #region File Menu Events
            newMapToolStripMenuItem.Click += new EventHandler(newMapToolStripMenuItem_Click);
            openMapToolStripMenuItem.Click += new EventHandler(openMapToolStripMenuItem_Click);
            saveMapToolStripMenuItem.Click += new EventHandler(saveMapToolStripMenuItem_Click);
            saveAsToolStripMenuItem.Click += new EventHandler(saveMapToolStripMenuItem_Click);
            exitToolStripMenuItem.Click += new EventHandler(exitToolStripMenuItem_Click);
            #endregion

            #region Edit Menu Events
            preferencesToolStripMenuItem.Click += new EventHandler(preferencesToolStripMenuItem_Click);
            #endregion

            #region View Menu Events
            displayGridToolStripMenuItem.Click += new EventHandler(displayGridToolStripMenuItem_Click);
            displayTilesetGridToolStripMenuItem.Click += new EventHandler(displayTilesetGridToolStripMenuItem_Click);
            highlightSelectedLayerToolStripMenuItem.Click += new EventHandler(highlightSelectedLayerToolStripMenuItem_Click);
            #endregion

            #region Map Menu Events
            mapPropertiesToolStripMenuItem.Click += new EventHandler(mapPropertiesToolStripMenuItem_Click);
            #endregion

            #region Layer Menu Events
            newLayerToolStripMenuItem.Click += new EventHandler(newLayerToolStripMenuItem_Click);
            removeLayerToolStripMenuItem.Click += new EventHandler(removeLayerToolStripMenuItem_Click);
            layerPropertiesToolStripMenuItem.Click += new EventHandler(layerPropertiesToolStripMenuItem_Click);
            #endregion

            #region Tileset Menu Events
            newTilesetToolStripMenuItem.Click += new EventHandler(newTilesetToolStripMenuItem_Click);
            openTilesetToolStripMenuItem.Click += new EventHandler(openTilesetToolStripMenuItem_Click);
            saveTilesetToolStripMenuItem.Click += new EventHandler(saveTilesetToolStripMenuItem_Click);
            removeTilesetToolStripMenuItem.Click += new EventHandler(removeTilesetToolStripMenuItem_Click);
            #endregion            

            #region Main ToolStrip Events
            newMapToolStripMenuButton.Click += new EventHandler(newMapToolStripMenuItem_Click);
            openMapToolStripMenuButton.Click += new EventHandler(openMapToolStripMenuItem_Click);
            saveMapToolStripMenuButton.Click += new EventHandler(saveMapToolStripMenuItem_Click);

            displayGridToolStripButton.Click += new EventHandler(displayGridToolStripMenuItem_Click);

            zoomInToolStripMenuButton.Click += new EventHandler(zoomInToolStripMenuButton_Click);
            zoomOutToolStripMenuButton.Click += new EventHandler(zoomOutToolStripMenuButton_Click);

            drawToolStripMenuButton.Click += new EventHandler(drawToolStripMenuButton_Click);
            eraseToolStripMenuButton.Click += new EventHandler(eraseToolStripMenuButton_Click);

            decreaseBrushSizeToolStripMenuButton.Click += new EventHandler(decreaseBrushSizeToolStripMenuButton_Click);
            increaseBrushSizeToolStripMenuButton.Click += new EventHandler(increaseBrushSizeToolStripMenuButton_Click);
            #endregion

            #region Map Display Events
            mapDisplay.OnInitialize += new EventHandler(mapDisplay_OnInitialize);
            mapDisplay.OnDraw += new EventHandler(mapDisplay_OnDraw);
            #endregion

            #region Layer Tool Strip Events
            newLayerToolStripButton.Click += new EventHandler(newLayerToolStripMenuItem_Click);
            removeLayerToolStripButton.Click += new EventHandler(removeLayerToolStripMenuItem_Click);
            layerMoveUpToolStripButton.Click += new EventHandler(layerMoveUpToolStripButton_Click);
            layerMoveDownToolStripButton.Click += new EventHandler(layerMoveDownToolStripButton_Click);
            layerPropertiesToolStripButton.Click += new EventHandler(layerPropertiesToolStripMenuItem_Click);
            #endregion

            #region Tileset Tool Strip Events
            newTilesetToolStripButton.Click += new EventHandler(newTilesetToolStripMenuItem_Click);
            openTilesetToolStripButton.Click += new EventHandler(openTilesetToolStripMenuItem_Click);
            saveTilesetToolStripButton.Click += new EventHandler(saveTilesetToolStripMenuItem_Click);
            removeTilesetToolStripButton.Click += new EventHandler(removeTilesetToolStripMenuItem_Click);
            displayTilesetGridToolStripButton.Click += new EventHandler(displayTilesetGridToolStripMenuItem_Click);
            renameTilesetToolStripButton.Click += new EventHandler(renameTilesetToolStripButton_Click);
            btnSetCurrentCollision.Click += new EventHandler(btnSetCurrentCollision_Click);
            btnSetDefaultCollision.Click += new EventHandler(btnSetDefaultCollision_Click);
            #endregion
        }
        #endregion

        #region Form Main Event Handlers

        void FormMain_Load(object sender, EventArgs e)
        {
            colorDialog.FullOpen = true;

            controlTimer.Tick += new EventHandler(controlTimer_Tick);
            controlTimer.Enabled = true;
            controlTimer.Interval = 6;

            cbZoom.SelectedIndexChanged += new EventHandler(zoomToolStripComboBox_SelectedIndexChanged);
            cbZoom.SelectedIndex = 3;

            cbBrushSize.SelectedIndexChanged += new EventHandler(cbBrushSize_SelectedIndexChanged);
            cbBrushSize.SelectedIndex = 0;

            mapDisplay.SizeChanged += new EventHandler(mapDisplay_SizeChanged);
            hScrollBar.ValueChanged += new EventHandler(hScrollBar_CursorChanged);
            vScrollBar.ValueChanged += new EventHandler(vScrollBar_CursorChanged);

            clbLayers.ItemCheck += new ItemCheckEventHandler(clbLayers_ItemCheck);
            clbLayers.SelectedIndexChanged += new EventHandler(clbLayers_SelectedIndexChanged);

            lbTileset.SelectedIndexChanged += new EventHandler(lbTileset_SelectedIndexChanged);
            nudCurrentTile.ValueChanged += new EventHandler(nudCurrentTile_ValueChanged);

            pbTilesetPreview.Paint += new PaintEventHandler(pbTilesetPreview_Paint);
            pbTilesetPreview.MouseDown += new MouseEventHandler(pbTilesetPreview_MouseDown);
        }

        void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Text.EndsWith("*"))
            {
                var result = MessageBox.Show("Map is not saved! Are you sure you want to exit?",
                    "Unsaved Changes",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation);

                if (result == DialogResult.No) { e.Cancel = true; }
            }
        }

        void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.N && e.Modifiers == (Keys.Control | Keys.Shift) && newLayerToolStripMenuItem.Enabled)
            {
                newLayerToolStripMenuItem_Click(sender, new EventArgs());
            }
            else if (e.KeyCode == Keys.N && e.Modifiers == Keys.Control)
            {
                newMapToolStripMenuItem_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
            {
                openMapToolStripMenuItem_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                saveMapToolStripMenuItem_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.Delete && e.Modifiers == (Keys.Control | Keys.Shift) && removeLayerToolStripButton.Enabled)
            {
                removeLayerToolStripMenuItem_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.G && e.Modifiers == (Keys.Control | Keys.Shift))
            {
                displayTilesetGridToolStripMenuItem_Click(sender, new EventArgs());
            }
            else if (e.KeyCode == Keys.G)
            {
                displayGridToolStripMenuItem_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.H)
            {
                highlightSelectedLayerToolStripMenuItem_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.F4 && clbLayers.SelectedIndex != -1 && layerPropertiesToolStripMenuItem.Enabled)
            {
                layerPropertiesToolStripMenuItem_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.Z && cbZoom.Enabled)
            {
                zoomInToolStripMenuButton_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.X && cbZoom.Enabled)
            {
                zoomOutToolStripMenuButton_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.D)
            {
                drawToolStripMenuButton_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.E)
            {
                eraseToolStripMenuButton_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.OemOpenBrackets)
            {
                decreaseBrushSizeToolStripMenuButton_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.OemCloseBrackets)
            {
                increaseBrushSizeToolStripMenuButton_Click(sender, new EventArgs());
            }

            if (e.KeyCode == Keys.C && m_trackMouse)
            {
                m_camera.SnapToPosition(new Vector2(
                    m_camera.Position.X + m_mouse.X,
                    m_camera.Position.Y + m_mouse.Y));

                Cursor.Position = new System.Drawing.Point(
                    Cursor.Position.X - (m_mouse.X - (mapDisplay.Width / 2)),
                    Cursor.Position.Y - (m_mouse.Y - (mapDisplay.Height / 2)));
            }
        }

        void FormMain_MouseWheel(object sender, MouseEventArgs e)
        {
            // e.Delta value is 120;
            if (ModifierKeys == Keys.Control)
            {
                if (e.Delta < 0 && cbZoom.SelectedIndex > 0)
                {
                    cbZoom.SelectedIndex -= 1;
                }
                else if (e.Delta > 0 && cbZoom.SelectedIndex < cbZoom.Items.Count - 1)
                {
                    cbZoom.SelectedIndex += 1;
                }
            }
            else if (ModifierKeys == Keys.Shift)
            {
                // scrolling up is +e.Delta, scrolling down is -e.Delta
                if (e.Delta < 0)
                {
                    hScrollBar.Value -= MathHelper.Clamp(
                        -hScrollBar.LargeChange / 4, 
                        hScrollBar.Value - (hScrollBar.Maximum - hScrollBar.LargeChange), 
                        0);
                }
                else
                {
                    hScrollBar.Value -= MathHelper.Clamp(
                        hScrollBar.LargeChange / 4, 
                        0, 
                        hScrollBar.Value - hScrollBar.Minimum);
                }
            }
            else
            {
                if (e.Delta < 0)
                {
                    vScrollBar.Value -= MathHelper.Clamp(
                        -hScrollBar.LargeChange / 4,
                        vScrollBar.Value - (vScrollBar.Maximum - vScrollBar.LargeChange),
                        0);
                }
                else
                {
                    vScrollBar.Value -= MathHelper.Clamp(
                        hScrollBar.LargeChange / 4, 
                        0, 
                        vScrollBar.Value - vScrollBar.Minimum);
                }
            }
        }

        void controlTimer_Tick(object sender, EventArgs e)
        {
            m_frameCount = ++m_frameCount % 6;
            mapDisplay.Invalidate();
            Logic();
        }
        #endregion

        #region New Menu Item Event Handlers
        void newMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormNewMap frmNewMap = new FormNewMap())
            {
                frmNewMap.ShowDialog();

                if (frmNewMap.OKPressed)
                {
                    m_tileMap = TileMap.FromTileMapData(frmNewMap.TileMapData);
                    this.Text = "Map Editor  -  " + m_tileMap.Name + ".map*";
                    lbTileset.Items.Clear();
                    clbLayers.Items.Clear();
                    m_tileSetDataList.Clear();
                    m_tileSetImageList.Clear();

                    Rectangle viewPort = new Rectangle(
                        0,
                        0,
                        mapDisplay.Width,
                        mapDisplay.Height);

                    m_camera = new Camera(viewPort);
                    m_camera.SnapToPosition(m_tileMap.GetCenter());

                    hScrollBar.Enabled = true;
                    SetHScrollMax();

                    vScrollBar.Enabled = true;
                    SetVScrollMax();

                    saveMapToolStripMenuItem.Enabled = true;
                    saveMapToolStripMenuButton.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    mapToolStripMenuItem.Enabled = true;

                    layerToolStripMenuItem.Enabled = true;
                    newLayerToolStripButton.Enabled = true;

                    tilesetToolStripMenuItem.Enabled = true;
                    newTilesetToolStripButton.Enabled = true;
                    openTilesetToolStripButton.Enabled = true;

                    cbZoom.Enabled = true;
                }
            }
        }

        void newLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_tileMap == null) { return; }

            MapLayer mapLayer = new MapLayer(
                "default" + m_tileMap.LayerList.Count,
                m_tileMap.TilesWide,
                m_tileMap.TilesHigh,
                m_tileMap.TileWidth,
                m_tileMap.TileHeight);

            m_tileMap.AddLayer(mapLayer);

            clbLayers.Items.Add(mapLayer.Name, true);
            clbLayers.SelectedIndex = clbLayers.Items.Count - 1;

            removeLayerToolStripMenuItem.Enabled = true;
            removeLayerToolStripButton.Enabled = true;
            layerPropertiesToolStripMenuItem.Enabled = true;
            layerPropertiesToolStripButton.Enabled = true;

            if (!this.Text.EndsWith("*")) { this.Text += "*"; }
        }

        void newTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormNewTileset frmNewTileset = new FormNewTileset())
            {
                frmNewTileset.ShowDialog();

                if (frmNewTileset.OKPressed)
                {
                    TilesetData tileSetData = frmNewTileset.TilesetData;
                    Tileset tileSet;
                    GDIImage tileSetImage;

                    try
                    {
                        tileSetImage = (GDIImage)GDIBitmap.FromFile(tileSetData.FilePath);
                        Stream stream = new FileStream(tileSetData.FilePath, FileMode.Open, FileAccess.Read);
                        Texture2D tileSetTexture = Texture2D.FromStream(GraphicsDevice, stream);

                        tileSet = new Tileset(
                            tileSetTexture,
                            tileSetData.Name,
                            tileSetData.FilePath,
                            tileSetData.TilesWide,
                            tileSetData.TilesHigh,
                            tileSetData.TileWidth,
                            tileSetData.TileHeight);

                        stream.Close();
                        stream.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error reading file. \n" + ex.Message, "Error reading image");
                        return;
                    }

                    m_tileSetImageList.Add(tileSetImage);
                    m_tileSetDataList.Add(tileSetData);
                    m_tileMap.AddTileset(tileSet);
                    lbTileset.Items.Add(tileSetData.Name);

                    if (lbTileset.SelectedItem == null) { lbTileset.SelectedIndex = 0; }

                    saveTilesetToolStripMenuItem.Enabled = true;
                    saveTilesetToolStripButton.Enabled = true;
                    removeTilesetToolStripMenuItem.Enabled = true;
                    removeTilesetToolStripButton.Enabled = true;
                    renameTilesetToolStripButton.Enabled = true;

                    if (!this.Text.EndsWith("*")) { this.Text += "*"; }
                }
            }
        }
        #endregion

        #region Open Menu Item Event Handlers
        void openMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game";
            ofDialog.Filter = "Map Files (*.xml)|*.xml";
            ofDialog.CheckFileExists = true;
            ofDialog.CheckPathExists = true;
            ofDialog.RestoreDirectory = false;
            ofDialog.Multiselect = false;

            DialogResult result = ofDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            string path = Path.GetDirectoryName(ofDialog.FileName);

            TileMapData tileMapData = null;

            try
            {
                tileMapData = XnaSerializer.Deserialize<TileMapData>(ofDialog.FileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error reading map");
                return;
            }

            m_tileMap = new TileMap(
                tileMapData.MapName,
                tileMapData.TilesWide,
                tileMapData.TilesHigh,
                tileMapData.TileWidth,
                tileMapData.TileHeight);

            m_tileSetImageList.Clear();
            m_tileSetDataList.Clear();
            m_tileMap.TilesetList.Clear();
            m_tileMap.LayerList.Clear();
            lbTileset.Items.Clear();
            clbLayers.Items.Clear();

            this.Text = "Map Editor  -  " + m_tileMap.Name + ".map";

            foreach (TilesetData tileSetData in tileMapData.TileSets)
            {
                Texture2D texture = null;

                m_tileSetDataList.Add(tileSetData);
                lbTileset.Items.Add(tileSetData.Name);

                GDIImage image = (GDIImage)GDIBitmap.FromFile(tileSetData.FilePath);
                m_tileSetImageList.Add(image);

                using (Stream stream = new FileStream(tileSetData.FilePath, FileMode.Open,
                    FileAccess.Read))
                {
                    texture = Texture2D.FromStream(GraphicsDevice, stream);

                    m_tileMap.TilesetList.Add(
                        new Tileset(
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
                clbLayers.Items.Add(layerData.Name, true);
                m_tileMap.LayerList.Add(MapLayer.FromMapLayerData(layerData));
            }

            if (lbTileset.Items.Count > 0)
            {
                lbTileset.SelectedIndex = 0;
                nudCurrentTile.Value = 0;
            }

            if (clbLayers.Items.Count > 0)
            {
                clbLayers.SelectedIndex = 0;
            }
            
            Rectangle viewPort = new Rectangle(
                0,
                0,
                mapDisplay.Width,
                mapDisplay.Height);

            m_camera = new Camera(viewPort);
            m_camera.SnapToPosition(new Vector2(m_tileMap.WidthInPixels / 2, m_tileMap.HeightInPixels / 2));

            hScrollBar.Enabled = true;
            SetHScrollMax();
            
            vScrollBar.Enabled = true;
            SetVScrollMax();

            saveMapToolStripMenuItem.Enabled = true;
            saveMapToolStripMenuButton.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;
            mapToolStripMenuItem.Enabled = true;

            layerToolStripMenuItem.Enabled = true;
            newLayerToolStripButton.Enabled = true;

            if (m_tileMap.LayerList.Count > 0)
            {
                removeLayerToolStripMenuItem.Enabled = true;
                removeLayerToolStripButton.Enabled = true;
                layerPropertiesToolStripMenuItem.Enabled = true;
                layerPropertiesToolStripButton.Enabled = true;
            }

            tilesetToolStripMenuItem.Enabled = true;
            newTilesetToolStripButton.Enabled = true;
            openTilesetToolStripButton.Enabled = true;

            if (m_tileMap.TilesetList.Count > 0)
            {
                saveTilesetToolStripMenuItem.Enabled = true;
                saveTilesetToolStripButton.Enabled = true;
                removeTilesetToolStripMenuItem.Enabled = true;
                removeTilesetToolStripButton.Enabled = true;
                renameTilesetToolStripButton.Enabled = true;
            }

            cbZoom.Enabled = true;
        }

        void openTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game";
            ofDialog.Filter = "Tileset Data (*.tdat)|*.tdat";
            ofDialog.CheckPathExists = true;
            ofDialog.CheckFileExists = true;
            ofDialog.RestoreDirectory = false;
            ofDialog.Multiselect = false;

            DialogResult result = ofDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            TilesetData tileSetData = null;
            Texture2D texture = null;
            Tileset tileset = null;
            GDIImage image = null;

            try
            {
                tileSetData = XnaSerializer.Deserialize<TilesetData>(ofDialog.FileName);

                using (Stream stream = new FileStream(tileSetData.FilePath, FileMode.Open,
                    FileAccess.Read))
                {
                    texture = Texture2D.FromStream(GraphicsDevice, stream);
                    stream.Close();
                }

                image = (GDIImage)GDIBitmap.FromFile(tileSetData.FilePath);

                tileset = new Tileset(
                    texture,
                    tileSetData.Name,
                    tileSetData.FilePath,
                    tileSetData.TilesWide,
                    tileSetData.TilesHigh,
                    tileSetData.TileWidth,
                    tileSetData.TileHeight);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error Reading Tileset Data");
                return;
            }

            for (int i = 0; i < lbTileset.Items.Count; i++)
            {
                if (lbTileset.Items[i].ToString() == tileSetData.Name)
                {
                    MessageBox.Show("Map already contains a tileset with this name.", "Existing Tileset");
                    return;
                }
            }

            m_tileSetDataList.Add(tileSetData);
            m_tileMap.TilesetList.Add(tileset);

            lbTileset.Items.Add(tileSetData.Name);

            pbTilesetPreview.Image = image;
            m_tileSetImageList.Add(image);

            lbTileset.SelectedIndex = lbTileset.Items.Count - 1;
            nudCurrentTile.Value = 0;

            if (!this.Text.EndsWith("*"))
            {
                this.Text += "*";
            }
        }
        #endregion

        #region Save Menu Item Event Handlers
        void saveMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_tileMap == null)
            {
                return;
            }

            List<MapLayerData> mapLayerData = new List<MapLayerData>();

            foreach (MapLayer l in m_tileMap.LayerList)
            {
                MapLayerData data = new MapLayerData(
                    l.Name,
                    l.TilesWide,
                    l.TilesHigh,
                    l.TileWidth,
                    l.TileHeight);

                for (int y = 0; y < l.TilesHigh; y++)
                {
                    for (int x = 0; x < l.TilesWide; x++)
                    {
                        data.SetTile(
                            x,
                            y,
                            l.GetTile(x, y).TileIndex,
                            l.GetTile(x, y).TileSet,
                            l.GetTile(x, y).CollisionType);
                    }
                }

                mapLayerData.Add(data);
            }

            List<TilesetData> tileSetData = new List<TilesetData>();

            foreach (Tileset ts in m_tileMap.TilesetList)
            {
                TilesetData data = new TilesetData();
                data.Name = ts.Name;
                data.FilePath = ts.FilePath;
                data.TileWidth = ts.TileWidth;
                data.TileHeight = ts.TileHeight;
                data.TilesWide = ts.TilesWide;
                data.TilesHigh = ts.TilesHigh;
                tileSetData.Add(data);
            }

            TileMapData tileMapData = new TileMapData(
                mapLayerData, 
                tileSetData, 
                m_tileMap.Name,
                m_tileMap.TilesWide,
                m_tileMap.TilesHigh,
                m_tileMap.TileWidth,
                m_tileMap.TileHeight);

            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.InitialDirectory = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game\\Maps";
            sfDialog.Title = "Save Map";
            sfDialog.FileName = tileMapData.MapName;
            sfDialog.Filter = "Map Data (*.xml)|*.xml";
            sfDialog.CheckPathExists = true;
            sfDialog.OverwritePrompt = true;
            sfDialog.ValidateNames = true;

            DialogResult result = sfDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }
            
            try
            {
                string fileName = Path.GetFileNameWithoutExtension(sfDialog.FileName);
                this.Text = "Map Editor  -  " + fileName  + ".map";
                m_tileMap.Name = fileName;
                tileMapData.MapName = fileName;

                XnaSerializer.Serialize<TileMapData>(sfDialog.FileName, tileMapData);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error saving map!");
            }
        }

        void saveTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_tileSetDataList.Count == 0)
            {
                return;
            }

            SaveFileDialog sfDialog = new SaveFileDialog();
            sfDialog.Title = "Save Tileset";
            sfDialog.InitialDirectory = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\SuperMetroid\\Content\\Game";
            sfDialog.Filter = "Tileset Data (*.tdat)|*.tdat";
            sfDialog.CheckPathExists = true;
            sfDialog.OverwritePrompt = true;
            sfDialog.ValidateNames = true;

            DialogResult result = sfDialog.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                XnaSerializer.Serialize<TilesetData>(
                    sfDialog.FileName,
                    m_tileSetDataList[lbTileset.SelectedIndex]);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error saving tileset!");
            }
        }
        #endregion

        #region Remove Menu Item Event Handlers   
        void removeLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clbLayers.SelectedIndex == -1)
            {
                return;
            }

            var result = MessageBox.Show("Are you sure you want to delete the layer \'" + clbLayers.Text + "\' ?",
                "Delete Layer",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            int selectedIndex = clbLayers.SelectedIndex;
            m_tileMap.LayerList.Remove(m_tileMap.LayerList[selectedIndex]);
            clbLayers.Items.RemoveAt(selectedIndex);

            if (!this.Text.EndsWith("*"))
            {
                this.Text += "*";
            }
        }

        void removeTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbTileset.SelectedIndex == -1)
            {
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete the tileset \'" + lbTileset.Text + "\' ?",
                "Delete Tileset",
                MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                return;
            }

            int selectedIndex = lbTileset.SelectedIndex;

            foreach (MapLayer l in m_tileMap.LayerList)
            {
                for (int y = 0; y < l.TilesHigh; y++)
                {
                    for (int x = 0; x < l.TilesWide; x++)
                    {
                        SuperMetroid.TileEngine.Tile t = l.GetTile(x, y);
                        if (t.TileSet == selectedIndex)
                        {
                            l.SetTile(x, y, -1, -1);
                        }
                    }
                }
            }

            m_tileSetDataList.Remove(m_tileSetDataList[selectedIndex]);
            m_tileMap.TilesetList.Remove(m_tileMap.TilesetList[selectedIndex]);
            m_tileSetImageList.Remove(m_tileSetImageList[selectedIndex]);

            lbTileset.Items.RemoveAt(selectedIndex);

            pbTilePreview.Image = null;
            pbTilesetPreview.Image = null;

            if (!this.Text.EndsWith("*"))
            {
                this.Text += "*";
            }
        }
        #endregion

        #region Edit Menu Item Event Handlers

        void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormPreferences frmPreferences = new FormPreferences(m_gridColor, m_mapBgColor, m_mapBackdropColor, m_tileSetGridColor))
            {
                frmPreferences.changeMapGridColor += new EventHandler(child_changeMapGridColor);
                frmPreferences.changeMapBgColor += new EventHandler(child_changeMapBgColor);
                frmPreferences.changeMapBackdropColor += new EventHandler(child_changeMapBackdropColor);
                frmPreferences.changeTsGridColor += new EventHandler(child_changeTsGridColor);
                frmPreferences.ShowDialog();
            }
        }

        void child_changeMapGridColor(object sender, EventArgs e)
        {
            FormPreferences frmPreferences = sender as FormPreferences;

            if (frmPreferences != null)
            {
                m_gridColor = frmPreferences.MapGridColor;
            }
        }

        void child_changeMapBgColor(object sender, EventArgs e)
        {
            FormPreferences frmPreferences = sender as FormPreferences;

            if (frmPreferences != null)
            {
                m_mapBgColor = frmPreferences.MapBgColor;
            }
        }

        void child_changeMapBackdropColor(object sender, EventArgs e)
        {
            FormPreferences frmPreferences = sender as FormPreferences;

            if (frmPreferences != null)
            {
                m_mapBackdropColor = frmPreferences.MapBackdropColor;
            }
        }

        void child_changeTsGridColor(object sender, EventArgs e)
        {
            FormPreferences frmPreferences = sender as FormPreferences;

            if (frmPreferences != null)
            {
                m_tileSetGridColor = frmPreferences.TsGridColor;
                FillPreviews();
            }
        }
        #endregion

        #region Map Menu Item Event Handlers
        void mapPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_tileMap == null)
            {
                return;
            }

            using (FormMapProperties frmMapProperties = new FormMapProperties(m_tileMap))
            {
                frmMapProperties.ShowDialog();

                if (frmMapProperties.OKPressed)
                {
                    this.Text = "Map Editor  -  " + frmMapProperties.MapName + ".map";
                    m_tileMap.Name = frmMapProperties.MapName;
                    m_tileMap.ChangeSize(frmMapProperties.TilesWide, frmMapProperties.TilesHigh);
                    m_camera.LockToMap(m_tileMap.WidthInPixels, m_tileMap.HeightInPixels);

                    SetHScrollMax();
                    SetVScrollMax();
                }
            }

            if (!this.Text.EndsWith("*"))
            {
                this.Text += "*";
            }
        }
        #endregion

        #region Tileset Menu Item Event Handlers
        void renameTilesetToolStripButton_Click(object sender, EventArgs e)
        {
            if (lbTileset.SelectedIndex == -1)
            {
                return;
            }

            using (FormRenameTileset frmRenameTileset = new FormRenameTileset(lbTileset.GetItemText(lbTileset.SelectedItem)))
            {
                frmRenameTileset.ShowDialog();

                if (frmRenameTileset.OKPressed)
                {
                    int index = lbTileset.SelectedIndex;

                    lbTileset.Items[index] = frmRenameTileset.TilesetName;
                    m_tileMap.TilesetList[index].Name = frmRenameTileset.TilesetName;

                    if (!this.Text.EndsWith("*"))
                    {
                        this.Text += "*";
                    }
                }
            }
        }
        #endregion

        #region View Menu Item Event Handlers
        void displayGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayGridToolStripMenuItem.Checked = !displayGridToolStripMenuItem.Checked;
            displayGridToolStripButton.Checked = !displayGridToolStripButton.Checked;
        }

        void displayTilesetGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayTilesetGridToolStripMenuItem.Checked = !displayTilesetGridToolStripMenuItem.Checked;
            displayTilesetGridToolStripButton.Checked = !displayTilesetGridToolStripButton.Checked;
            FillPreviews();
        }

        void highlightSelectedLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            highlightSelectedLayerToolStripMenuItem.Checked = !highlightSelectedLayerToolStripMenuItem.Checked;
            m_highlightSelectedLayer = highlightSelectedLayerToolStripMenuItem.Checked;
         
            foreach (MapLayer l in m_tileMap.LayerList)
            {
                if (!m_highlightSelectedLayer)
                {
                    l.Tint = Color.White;
                    l.SetAlpha(255);
                }
                else
                {
                    if (l.Name == clbLayers.Text)
                    {
                        continue;
                    }

                    l.Tint = Color.Black;
                    l.SetAlpha(40);
                }
            }
        }
        #endregion

        #region Main Tool Strip Button Event Handlers

        #region Scroll Bar Event Handlers
        void hScrollBar_CursorChanged(object sender, EventArgs e)
        {
            if (!m_isZooming)
            {
                m_camera.Position = new Vector2(hScrollBar.Value, m_camera.Position.Y);
            }
        }

        void vScrollBar_CursorChanged(object sender, EventArgs e)
        {
            if (!m_isZooming)
            {
                m_camera.Position = new Vector2(m_camera.Position.X, vScrollBar.Value);
            }
        }

        private void SetHScrollMax()
        {
            if (mapDisplay.Width >= m_tileMap.WidthInPixels * m_camera.Zoom)
            {
                hScrollBar.Minimum = (int)(m_tileMap.WidthInPixels * m_camera.Zoom / 2) - mapDisplay.Width;
                hScrollBar.Maximum = (int)(m_tileMap.WidthInPixels * m_camera.Zoom / 2);
            }
            else
            {
                hScrollBar.Minimum = -(mapDisplay.Width / 2);
                hScrollBar.Maximum = (int)(m_tileMap.WidthInPixels * m_camera.Zoom) - (mapDisplay.Width / 2);
            }

            if (!(mapDisplay.Width >= m_tileMap.WidthInPixels * m_camera.Zoom))
            {
                hScrollBar.LargeChange = (int)((hScrollBar.Maximum - hScrollBar.Minimum) / m_camera.Zoom / 2);
            }

            hScrollBar.Maximum += hScrollBar.LargeChange;
            hScrollBar.Value = (int)m_camera.Position.X;
        }

        private void SetVScrollMax()
        {
            if (mapDisplay.Height > m_tileMap.HeightInPixels * m_camera.Zoom)
            {
                vScrollBar.Minimum = (int)(m_tileMap.HeightInPixels * m_camera.Zoom / 2) - mapDisplay.Height;
                vScrollBar.Maximum = (int)(m_tileMap.HeightInPixels * m_camera.Zoom / 2);
            }
            else
            {
                vScrollBar.Minimum = -(mapDisplay.Height / 2);
                vScrollBar.Maximum = (int)(m_tileMap.HeightInPixels * m_camera.Zoom) - (mapDisplay.Height / 2);
            }

            if (!(mapDisplay.Height > m_tileMap.HeightInPixels * m_camera.Zoom))
            {
                vScrollBar.LargeChange = (int)((vScrollBar.Maximum - vScrollBar.Minimum) / m_camera.Zoom / 2);
            }

            vScrollBar.Maximum += vScrollBar.LargeChange;
            vScrollBar.Value = (int)m_camera.Position.Y;
        }
        #endregion

        #region Zoom Event Handlers
        void zoomToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_tileMap == null || m_camera == null) { return; }

            Vector2 newPosition = Vector2.Zero;
            float newZoom = (float.Parse(cbZoom.SelectedItem.ToString().TrimEnd('%'))) / 100;
            bool zoomIn = newZoom > m_camera.Zoom;

            if (m_trackMouse && zoomIn)
            {
                newPosition = new Vector2(
                    m_camera.Position.X + m_mouse.X, 
                    m_camera.Position.Y + m_mouse.Y);

                Cursor.Position = new System.Drawing.Point(
                    Cursor.Position.X - (m_mouse.X - (m_camera.ViewportRectangle.Width / 2)),
                    Cursor.Position.Y - (m_mouse.Y - (m_camera.ViewportRectangle.Height / 2)));
            }
            else
            {
                newPosition = new Vector2(
                    m_camera.Position.X + (m_camera.ViewportRectangle.Width / 2),
                    m_camera.Position.Y + (m_camera.ViewportRectangle.Height / 2));
            }

            m_camera.SetZoom(newZoom, newPosition);           
            m_camera.LockToMap(m_tileMap.WidthInPixels, m_tileMap.HeightInPixels);

            m_isZooming = true;
            SetHScrollMax();
            SetVScrollMax();
            m_isZooming = false;

            mapDisplay.Invalidate();
        }

        void zoomInToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (cbZoom.SelectedIndex >= cbZoom.Items.Count - 1 || !cbZoom.Enabled) { return; }

            cbZoom.SelectedIndex += 1;
        }

        void zoomOutToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (cbZoom.SelectedIndex <= 0 || !cbZoom.Enabled) { return; }

            cbZoom.SelectedIndex -= 1;
        }
        #endregion

        #region Draw / Erase Event Handlers
        void drawToolStripMenuButton_Click(object sender, EventArgs e)
        {
            drawToolStripMenuButton.Checked = true;
            eraseToolStripMenuButton.Checked = false;
        }

        void eraseToolStripMenuButton_Click(object sender, EventArgs e)
        {
            drawToolStripMenuButton.Checked = false;
            eraseToolStripMenuButton.Checked = true;
        }
        #endregion

        #region Brush Size Event Handlers
        void decreaseBrushSizeToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (cbBrushSize.SelectedIndex > 0) { cbBrushSize.SelectedIndex -= 1; }
        }

        void increaseBrushSizeToolStripMenuButton_Click(object sender, EventArgs e)
        {
            if (cbBrushSize.SelectedIndex < cbBrushSize.Items.Count - 1) { cbBrushSize.SelectedIndex += 1; }
        }

        void cbBrushSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_brushSize = int.Parse(cbBrushSize.SelectedItem.ToString());
        }
        #endregion

        #endregion

        #region Layer Tab Event Handlers
        void clbLayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clbLayers.SelectedIndex == -1)
            {
                removeLayerToolStripMenuItem.Enabled = false;
                removeLayerToolStripButton.Enabled = false;
                layerMoveUpToolStripButton.Enabled = false;
                layerMoveDownToolStripButton.Enabled = false;
                layerPropertiesToolStripMenuItem.Enabled = false;
                layerPropertiesToolStripButton.Enabled = false;

                return;
            }

            removeLayerToolStripMenuItem.Enabled = true;
            removeLayerToolStripButton.Enabled = true;
            layerPropertiesToolStripMenuItem.Enabled = true;
            layerPropertiesToolStripButton.Enabled = true;

            if (clbLayers.SelectedIndex == 0 && clbLayers.Items.Count > 1)
            {
                layerMoveDownToolStripButton.Enabled = true;
            }
            else if (clbLayers.SelectedIndex == clbLayers.Items.Count - 1)
            {
                layerMoveUpToolStripButton.Enabled = true;
            }
            else
            {
                layerMoveUpToolStripButton.Enabled = true;
                layerMoveDownToolStripButton.Enabled = true;
            }

            if (m_highlightSelectedLayer)
            {
                foreach (MapLayer l in m_tileMap.LayerList)
                {
                    if (l.Name == clbLayers.Text)
                    {
                        l.Tint = Color.White;
                        l.SetAlpha(255);
                    }
                    else
                    {
                        l.Tint = Color.TransparentBlack;
                        l.SetAlpha(40);
                    }
                }
            }            
        }

        void clbLayers_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_tileMap.LayerList.Count == 0 ||
                m_tileMap.LayerList.ElementAtOrDefault(e.Index) == null)
            {
                return;
            }

            m_tileMap.LayerList[e.Index].IsVisible = !clbLayers.GetItemChecked(e.Index);
        }

        void layerMoveUpToolStripButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = clbLayers.SelectedIndex;

            MapLayer tempLayer = m_tileMap.LayerList[selectedIndex];
            m_tileMap.LayerList[selectedIndex] = m_tileMap.LayerList[selectedIndex - 1];
            m_tileMap.LayerList[selectedIndex - 1] = tempLayer;

            object temp = clbLayers.Items[selectedIndex];
            clbLayers.Items[selectedIndex] = clbLayers.Items[selectedIndex - 1];
            clbLayers.Items[selectedIndex - 1] = temp;

            clbLayers.SelectedIndex -= 1;
        }

        void layerMoveDownToolStripButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = clbLayers.SelectedIndex;

            MapLayer tempLayer = m_tileMap.LayerList[selectedIndex];
            m_tileMap.LayerList[selectedIndex] = m_tileMap.LayerList[selectedIndex + 1];
            m_tileMap.LayerList[selectedIndex + 1] = tempLayer;

            object temp = clbLayers.Items[selectedIndex];
            clbLayers.Items[selectedIndex] = clbLayers.Items[selectedIndex + 1];
            clbLayers.Items[selectedIndex + 1] = temp;

            clbLayers.SelectedIndex += 1;
        }

        void layerPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormLayerProperties frmLayerProperties = new FormLayerProperties(
                m_tileMap.LayerList[clbLayers.SelectedIndex]))
            {
                frmLayerProperties.ShowDialog();

                if (frmLayerProperties.OKPressed)
                {
                    m_tileMap.LayerList[clbLayers.SelectedIndex] = frmLayerProperties.Layer;

                    clbLayers.Items[clbLayers.SelectedIndex] = frmLayerProperties.Layer.Name;
                    clbLayers.SetItemChecked(clbLayers.SelectedIndex, frmLayerProperties.Layer.IsVisible);
                }
            }
        }
        #endregion

        #region Tile Tab Event Handlers
        void lbTileset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbTileset.SelectedItem == null || lbTileset.SelectedIndex == -1)
            {
                saveTilesetToolStripMenuItem.Enabled = false;
                saveTilesetToolStripButton.Enabled = false;
                removeTilesetToolStripMenuItem.Enabled = false;
                removeTilesetToolStripButton.Enabled = false;
                renameTilesetToolStripButton.Enabled = false;

                return;
            }

            saveTilesetToolStripMenuItem.Enabled = true;
            saveTilesetToolStripButton.Enabled = true;
            removeTilesetToolStripMenuItem.Enabled = true;
            removeTilesetToolStripButton.Enabled = true;
            renameTilesetToolStripButton.Enabled = true;

            nudCurrentTile.Value = 0;
            nudCurrentTile.Maximum = m_tileMap.TilesetList[lbTileset.SelectedIndex].SourceRectangles.Length - 1;
            FillPreviews();
        }

        private void FillPreviews()
        {
            if (m_tileMap == null || lbTileset.SelectedIndex == -1) { return; }

            int selectedIndex = lbTileset.SelectedIndex;
            int currentTile = (int)nudCurrentTile.Value;

            GDIImage previewImage = (GDIImage)new GDIBitmap(pbTilePreview.Width, pbTilePreview.Height);

            GDIRectangle destRect = new GDIRectangle(0, 0, previewImage.Width, previewImage.Height);
            GDIRectangle sourceRect = new GDIRectangle(
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].X,
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].Y,
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].Width,
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].Height);

            GDIGraphics g = GDIGraphics.FromImage(previewImage);
            g.DrawImage(m_tileSetImageList[selectedIndex], destRect, sourceRect, GDIGraphicsUnit.Pixel);

            pbTilesetPreview.Image = m_tileSetImageList[selectedIndex];
            pbTilePreview.Image = previewImage;
        }

        private void pbTilesetPreview_Paint(object sender, PaintEventArgs e)
        {
            if (m_tileMap == null || m_tileMap.TilesetList.Count == 0 ||
                lbTileset.SelectedIndex == -1)
            {
                return;
            }

            GDIGraphics g = e.Graphics;

            #region Draw Tileset Grid
            if (displayTilesetGridToolStripMenuItem.Checked)
            {

                GDIPen p = new GDIPen(m_tileSetGridColor);

                int tilesWide = m_tileMap.TilesetList[lbTileset.SelectedIndex].TilesWide;
                int tilesHigh = m_tileMap.TilesetList[lbTileset.SelectedIndex].TilesHigh;
                int tileWidth = m_tileMap.TilesetList[lbTileset.SelectedIndex].TileWidth;
                int tileHeight = m_tileMap.TilesetList[lbTileset.SelectedIndex].TileHeight;
                int numOfTiles = tilesWide * tilesHigh;

                for (int y = 0; y < numOfTiles; ++y)
                {
                    g.DrawLine(p, 0, y * tileHeight, numOfTiles * tileHeight, y * tileHeight);
                }

                g.DrawLine(p, 0, (tilesHigh * tileHeight) - 1, numOfTiles * tileHeight, (tilesHigh * tileHeight) - 1);

                for (int x = 0; x < numOfTiles; ++x)
                {
                    g.DrawLine(p, x * tileWidth, 0, x * tileWidth, numOfTiles * tileWidth);
                }

                g.DrawLine(p, (tilesWide * tileWidth) - 1, 0, (tilesWide * tileWidth) - 1, numOfTiles * tileWidth);

            }
            #endregion

            #region Highlight Tileset Selected Tile
            GDIColor color = GDIColor.FromArgb(150, m_selectedTileColor);
            GDISolidBrush brush = new GDISolidBrush(color);
            int selectedIndex = lbTileset.SelectedIndex;
            int currentTile = (int)nudCurrentTile.Value;

            GDIRectangle destRect = new GDIRectangle(
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].X + 1,
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].Y + 1,
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].Width - 1,
                m_tileMap.TilesetList[selectedIndex].SourceRectangles[currentTile].Height - 1);

            g.FillRectangle(brush, destRect);
            #endregion
        }

        void nudCurrentTile_ValueChanged(object sender, EventArgs e)
        {
            if (lbTileset.SelectedIndex != -1) { FillPreviews(); }
        }

        void pbTilesetPreview_MouseDown(object sender, MouseEventArgs e)
        {
            if (lbTileset.SelectedIndex == -1) { return; }
            if (e.Button != System.Windows.Forms.MouseButtons.Left) { return; }

            int index = lbTileset.SelectedIndex;
            float xScale = (float)m_tileSetImageList[index].Width / pbTilesetPreview.Width;
            float yScale = (float)m_tileSetImageList[index].Height / pbTilesetPreview.Height;

            Point previewPoint = new Point(e.X, e.Y);
            Point tilesetPoint = new Point((int)(previewPoint.X * xScale), (int)(previewPoint.Y * yScale));
            Point tile = new Point(
                tilesetPoint.X / m_tileMap.TilesetList[index].TileWidth,
                tilesetPoint.Y / m_tileMap.TilesetList[index].TileHeight);
            nudCurrentTile.Value = tile.Y * m_tileMap.TilesetList[index].TilesWide + tile.X;

            drawToolStripMenuButton.Checked = true;
            eraseToolStripMenuButton.Checked = false;
        }

        void btnSetCurrentCollision_Click(object sender, EventArgs e)
        {
            if (clbLayers.SelectedIndex == -1 || lbTileset.SelectedIndex == -1) { return; }

            m_tileMap.LayerList[clbLayers.SelectedIndex].SetCurrentCollision(
                m_selectedTile.X, m_selectedTile.Y, 
                (int)nudCollisionType.Value);
        }

        void btnSetDefaultCollision_Click(object sender, EventArgs e)
        {
            if (clbLayers.SelectedIndex == -1 || lbTileset.SelectedIndex == -1) { return; }

            m_tileMap.LayerList[clbLayers.SelectedIndex].SetAsDefaultCollision(
                (int)nudCurrentTile.Value, lbTileset.SelectedIndex, 
                (int)nudCollisionType.Value);
        }
        #endregion

        #region Map Display Event Handlers
        void mapDisplay_OnInitialize(object sender, EventArgs e)
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            m_highlightTexture = new Texture2D(GraphicsDevice, 20, 20, false, SurfaceFormat.Color);
            Color[] colorData = new Color[m_highlightTexture.Width * m_highlightTexture.Height];
            Color tint = Color.LightSteelBlue;
            tint.A = 25;

            for (int i = 0; i < m_highlightTexture.Width * m_highlightTexture.Height; i++)
            {
                colorData[i] = tint;
            }

            m_highlightTexture.SetData<Color>(colorData);

            mapDisplay.MouseEnter += new EventHandler(mapDisplay_MouseEnter);
            mapDisplay.MouseLeave += new EventHandler(mapDisplay_MouseLeave);
            mapDisplay.MouseMove += new MouseEventHandler(mapDisplay_MouseMove);
            mapDisplay.MouseUp += new MouseEventHandler(mapDisplay_MouseUp);
            mapDisplay.MouseDown += new MouseEventHandler(mapDisplay_MouseDown);

            try
            {
                using (Stream stream = new FileStream(@"Content\grid.png", FileMode.Open, FileAccess.Read))
                {
                    m_gridTexture = Texture2D.FromStream(GraphicsDevice, stream);
                    stream.Close();
                }

                using (Stream stream = new FileStream(@"Content\cursor.png", FileMode.Open, FileAccess.Read))
                {
                    m_cursorTexture = Texture2D.FromStream(GraphicsDevice, stream);
                    stream.Close();
                }

                using (Stream stream = new FileStream(@"Content\pixel.png", FileMode.Open, FileAccess.Read))
                {
                    m_pixelTexture = Texture2D.FromStream(GraphicsDevice, stream);
                    stream.Close();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error reading images in mapDisplay_OnInitialize method");
                m_gridTexture = null;
                m_cursorTexture = null;
                m_pixelTexture = null;
            }
        }

        void mapDisplay_OnDraw(object sender, EventArgs e)
        {
            GraphicsDevice.Clear(m_mapBackdropColor);
            DrawDisplayMap();
        }

        void mapDisplay_SizeChanged(object sender, EventArgs e)
        {   
            mapDisplay.Invalidate();
        }

        void mapDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { m_isLeftMouseDown = false; }
            if (e.Button == MouseButtons.Right) { m_isRightMouseDown = false; }
        }

        void mapDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { m_isLeftMouseDown = true; }
            if (e.Button == MouseButtons.Right) { m_isRightMouseDown = true; }
        }

        void mapDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            m_mouse.X = e.X;
            m_mouse.Y = e.Y;
        }

        void mapDisplay_MouseEnter(object sender, EventArgs e)
        {
            m_trackMouse = true;
        }

        void mapDisplay_MouseLeave(object sender, EventArgs e)
        {
            m_trackMouse = false;
        }
        #endregion

        #region Display Rendering and Logic
        private void DrawDisplayMap()
        {
            if (m_tileMap == null) { return; }

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                m_camera.Transformation);

            Rectangle bgColorRect = new Rectangle(
                0,
                0,
                (int)(m_tileMap.WidthInPixels),
                (int)(m_tileMap.HeightInPixels));

            spriteBatch.Draw(m_pixelTexture, bgColorRect, m_mapBgColor);
            m_tileMap.Draw(spriteBatch, m_camera);
            spriteBatch.End();

            DrawMapGrid();
            DrawTileHighlight();
            DrawTileOnCursor();
        }

        private void DrawMapGrid()
        {
            if (!displayGridToolStripMenuItem.Checked) { return; }

            Rectangle destRect = new Rectangle(
                0,
                0,
                m_tileMap.TileWidth,
                m_tileMap.TileHeight);

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                m_camera.Transformation);
            
            for (int y = 0; y < m_tileMap.TilesHigh; y++)
            {
                destRect.Y = y * m_tileMap.TileHeight;

                for (int x = 0; x < m_tileMap.TilesWide; x++)
                {
                    destRect.X = x * m_tileMap.TileWidth;
                    spriteBatch.Draw(m_gridTexture, destRect, m_gridColor);
                }
            }

            destRect = new Rectangle(
                0,
                m_tileMap.HeightInPixels,
                1,
                1);

            for (int x = 0; x < m_tileMap.WidthInPixels; x++)
            {
                if (x % 4 == 0)
                {
                    destRect.X = x;
                    spriteBatch.Draw(m_pixelTexture, destRect, m_gridColor);
                }
            }

            destRect = new Rectangle(
                m_tileMap.WidthInPixels,
                0,
                1,
                1);

            for (int y = 0; y <= m_tileMap.HeightInPixels; y++)
            {
                if (y % 4 == 0)
                {
                    destRect.Y = y;
                    spriteBatch.Draw(m_pixelTexture, destRect, m_gridColor);
                }
            }           
            spriteBatch.End();           
        }

        private void DrawTileHighlight()
        {
            if (m_tileMap.LayerList.Count == 0) { return; }

            Rectangle destRect = new Rectangle(
                (int)m_cursorPos.X * m_tileMap.TileWidth,
                (int)m_cursorPos.Y * m_tileMap.TileHeight,
                m_brushSize * m_tileMap.TileWidth,
                m_brushSize * m_tileMap.TileHeight);

            m_cursorHighlightColor = Color.MidnightBlue;

            if (destRect.X < 0 || destRect.X > m_tileMap.WidthInPixels - m_tileMap.TileWidth ||
                destRect.Y < 0 || destRect.Y > m_tileMap.HeightInPixels - m_tileMap.TileHeight ||
                clbLayers.SelectedIndex == -1 ||
                !clbLayers.GetItemChecked(clbLayers.SelectedIndex) ||
                lbTileset.Items.Count == 0)
            {
                m_cursorHighlightColor = Color.Red;
            }
            m_cursorHighlightColor.A = 50;

            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                null,
                null,
                null,
                m_camera.Transformation);
            spriteBatch.Draw(m_highlightTexture, destRect, m_cursorHighlightColor);
            spriteBatch.End();            
        }

        private void DrawTileOnCursor()
        {
            if (lbTileset.Items.Count == 0 || lbTileset.SelectedIndex == -1 ||
                nudCurrentTile.Value > m_tileMap.TilesetList[lbTileset.SelectedIndex].SourceRectangles.Length)
            {
                return;
            }

            Rectangle destRect = new Rectangle(
                m_mouse.X,
                m_mouse.Y,
                (int)(m_tileMap.TileWidth * m_camera.Zoom),
                (int)(m_tileMap.TileHeight * m_camera.Zoom));

            Color lowAlpha = Color.White;
            lowAlpha.A = 5;

            spriteBatch.Begin();
            if (drawToolStripMenuButton.Checked)
            {       
                spriteBatch.Draw(
                    m_tileMap.TilesetList[lbTileset.SelectedIndex].Texture,
                    destRect,
                    m_tileMap.TilesetList[lbTileset.SelectedIndex].SourceRectangles[(int)nudCurrentTile.Value],
                    Color.White);
            }
            spriteBatch.Draw(m_cursorTexture, destRect, lowAlpha);
            spriteBatch.End();            
        }

        private void Logic()
        {
            if (m_tileMap == null || m_camera == null || !m_trackMouse) { return; }

            DrawDebugText();

            Vector2 cameraPos = new Vector2(
                m_mouse.X + m_camera.Position.X, 
                m_mouse.Y + m_camera.Position.Y);
                
            if (m_tileMap.LayerList.Count > 0)
            {
                Point mouseCurrentTile = m_tileMap.VectorToCell(cameraPos, m_camera.Zoom);
                m_cursorPos = new Vector2(mouseCurrentTile.X, mouseCurrentTile.Y);

                statusStripMapDimensions.Text = m_tileMap.TilesWide + " ✕ " + m_tileMap.TilesHigh;
                statusStripMapLocation.Text =
                    "( " + mouseCurrentTile.X.ToString() + ", " + mouseCurrentTile.Y.ToString() + " )  tile" + 
                    "   ( " + (m_camera.Position.X + m_mouse.X) + ", " + (m_camera.Position.Y + m_mouse.Y) + " )  pixel";

                if (m_isLeftMouseDown)
                {
                    if (drawToolStripMenuButton.Checked)
                    {
                        SetTiles(mouseCurrentTile, (int)nudCurrentTile.Value, lbTileset.SelectedIndex);
                    }

                    if (eraseToolStripMenuButton.Checked)
                    {
                        SetTiles(mouseCurrentTile, -1, -1);
                    }

                    if (!this.Text.EndsWith("*"))
                    {
                        this.Text += "*";
                    }
                }

                if (m_isRightMouseDown)
                {
                    if (mouseCurrentTile.X >= 0 && mouseCurrentTile.X < m_tileMap.TilesWide &&
                        mouseCurrentTile.Y >= 0 && mouseCurrentTile.Y < m_tileMap.TilesHigh &&
                        clbLayers.SelectedIndex != -1 && clbLayers.GetItemChecked(clbLayers.SelectedIndex))
                    {
                        SuperMetroid.TileEngine.Tile currentTile = m_tileMap.LayerList[clbLayers.SelectedIndex].GetTile(mouseCurrentTile.X, mouseCurrentTile.Y);
                        m_selectedTile = new Point(mouseCurrentTile.X, mouseCurrentTile.Y);

                        if (currentTile.TileSet != -1)
                        {
                            lbTileset.SelectedIndex = currentTile.TileSet;
                        }

                        if (currentTile.TileIndex >= 0)
                        {
                            drawToolStripMenuButton.Checked = true;
                            eraseToolStripMenuButton.Checked = false;

                            nudCurrentTile.Value = currentTile.TileIndex;
                            nudCollisionType.Value = currentTile.CollisionType;
                        }
                        else if (currentTile.TileIndex == -1)
                        {
                            drawToolStripMenuButton.Checked = false;
                            eraseToolStripMenuButton.Checked = true;
                        }
                    }
                }
            }           
        }

        private void SetTiles(Point tile, int tileIndex, int tileSet)
        {
            if (m_tileMap.LayerList.Count == 0 || clbLayers.SelectedIndex == -1 ||
                    !clbLayers.GetItemChecked(clbLayers.SelectedIndex) ||
                    tile.X < 0 || tile.X > m_tileMap.TilesWide ||
                    tile.Y < 0 || tile.Y > m_tileMap.TilesHigh)
            {
                return;
            }

            int selected = clbLayers.SelectedIndex;

            for (int y = 0; y < m_brushSize; y++)
            {
                if (tile.Y + y >= m_tileMap.LayerList[selected].TilesHigh)
                {
                    break;
                }

                for (int x = 0; x < m_brushSize; x++)
                {
                    if (tile.X + x < (m_tileMap.LayerList[selected]).TilesWide)
                    {
                        (m_tileMap.LayerList[selected]).SetTile(
                            tile.X + x,
                            tile.Y + y,
                            tileIndex,
                            tileSet);
                    }
                }
            }
        }

        void DrawDebugText()
        {
            statusStripDebug.Text = 
                "Debug:" + 
                "  Camera.X:  " + m_camera.Position.X + 
                "  Camera.Y:  " + m_camera.Position.Y + 
                "  HScroll Max: " + hScrollBar.Maximum + 
                "  HScroll.Value: " + hScrollBar.Value + 
                "  VScroll Max: " + vScrollBar.Maximum + 
                "  VScroll.Value: " + vScrollBar.Value; 
        }
        #endregion
    }
}