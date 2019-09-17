using System;
using System.Windows.Forms;

using SuperMetroid.TileEngine;

namespace MapEditor
{
    /// <summary>
    /// Form used to change the properties of a map.
    /// </summary>
    public partial class FormMapProperties : Form
    {
        #region Fields
        private int m_tilesWide, m_tilesHigh;
        #endregion

        #region Properties
        public string MapName { get; private set; }
        public int TilesWide { get { return m_tilesWide; } }
        public int TilesHigh { get { return m_tilesHigh; } }
        public bool OKPressed { get; private set; }
        #endregion

        #region Constructor
        public FormMapProperties(TileMap tileMap)
        {
            InitializeComponent();

            tbName.Text = tileMap.Name;
            nudTilesWide.Value = tileMap.TilesWide;
            nudTilesHigh.Value = tileMap.TilesHigh;
            nudTileWidth.Value = tileMap.TileWidth;
            nudTileHeight.Value = tileMap.TileHeight;

            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }
        #endregion

        #region Button Event Handlers
        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("You must enter a name for the map.", "Missing Map Name");
                return;
            }

            if (!int.TryParse(nudTilesWide.Text, out m_tilesWide) || m_tilesWide < nudTilesWide.Minimum ||
                m_tilesWide > nudTilesHigh.Maximum)
            {
                MessageBox.Show("Map width must be an integer between 1 and 255.", "Invalid Map Width");
                return;
            }

            if (!int.TryParse(nudTilesHigh.Text, out m_tilesHigh) || m_tilesHigh < nudTilesHigh.Minimum ||
                m_tilesHigh > nudTilesHigh.Maximum)
            {
                MessageBox.Show("Map height must be an integer between 1 and 255.", "Invalid Map Height");
                return;
            }

            MapName = tbName.Text;
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
