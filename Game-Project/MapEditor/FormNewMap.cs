using System;
using System.Collections.Generic;
using System.Windows.Forms;

using MyXMLData.WorldClasses;

namespace MapEditor
{
    /// <summary>
    /// Form used to create a new map.
    /// </summary>
    public partial class FormNewMap : Form
    {
        #region Properties
        public bool OKPressed { get; private set; }
        public TileMapData TileMapData { get; private set; }
        #endregion

        #region Constructor
        public FormNewMap()
        {
            InitializeComponent();

            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            tbMapName.Text = "default";
            nudTilesWide.Text = "40";
            nudTilesHigh.Text = "30";
            nudTileWidth.Text = "16";
            nudTileHeight.Text = "16";
        }
        #endregion

        #region Button Event Handlers
        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbMapName.Text))
            {
                MessageBox.Show("You must enter a name for the map.", "Missing Map Name");
                return;
            }

            int tilesWide = 0;
            int tilesHigh = 0;
            int tileWidth = 0;
            int tileHeight = 0;

            if (!int.TryParse(nudTilesWide.Text, out tilesWide) || tilesWide < 1)
            {
                MessageBox.Show("Width of map must be 1 or greater.", "Map Size Error");
                return;
            }

            if (!int.TryParse(nudTilesHigh.Text, out tilesHigh) || tilesHigh < 1)
            {
                MessageBox.Show("Height of map must be 1 or greater.", "Map Size Error");
                return;
            }

            if (!int.TryParse(nudTileWidth.Text, out tileWidth) || tileWidth < 1)
            {
                MessageBox.Show("Width of map must be 1 or greater.", "Map Size Error");
                return;
            }

            if (!int.TryParse(nudTileHeight.Text, out tileHeight) || tileHeight < 1)
            {
                MessageBox.Show("Height of map must be 1 or greater.", "Map Size Error");
                return;
            }

            TileMapData = new TileMapData(
                new List<MapLayerData>(),
                new List<TilesetData>(),
                tbMapName.Text,
                tilesWide,
                tilesHigh,
                tileWidth,
                tileHeight);

            OKPressed = true;
            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            OKPressed = false;
            this.Close();
        }
        #endregion
    }
}
