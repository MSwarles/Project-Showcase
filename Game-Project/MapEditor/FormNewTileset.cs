using System;
using System.Drawing;
using System.Windows.Forms;

using MyXMLData.WorldClasses;

namespace MapEditor
{
    /// <summary>
    /// Form used to create a new tile set.
    /// </summary>
    public partial class FormNewTileset : Form
    {
        #region Properties
        public TilesetData TilesetData { get; private set; }
        public bool OKPressed { get; private set; }
        #endregion

        #region Constructor
        public FormNewTileset()
        {
            InitializeComponent();

            btnSelectImage.Click += new EventHandler(btnSelectImage_Click);
            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            tbTilesetName.Text = "default";
            nudTileWidth.Text = "16";
            nudTileHeight.Text = "16";
        }
        #endregion

        #region Button Event Handlers
        void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = "C:\\Users\\Matt\\Desktop\\Tilesets";
            ofDialog.Filter = "Image Files|*.BMP;*.GIF;*.JPG;*.TGA;*.PNG";
            ofDialog.CheckFileExists = true;
            ofDialog.CheckPathExists = true;
            ofDialog.RestoreDirectory = false;
            ofDialog.Multiselect = false;

            DialogResult result = ofDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbTilesetImage.Text = ofDialog.FileName;
            }
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbTilesetName.Text))
            {
                MessageBox.Show("You must enter a name for the tileset.");
                return;
            }

            if (string.IsNullOrEmpty(tbTilesetImage.Text))
            {
                MessageBox.Show("You must select an image for the tileset.");
                return;
            }

            int tileWidth = 0;
            int tileHeight = 0;
            int tilesWide = 0;
            int tilesHigh = 0;

            if (!int.TryParse(nudTileWidth.Text, out tileWidth))
            {
                MessageBox.Show("Tile width must be an integer value.");
                return;
            }
            else if (tileWidth < 1)
            {
                MessageBox.Show("Tile width must be 1 or greater.");
                return;
            }

            if (!int.TryParse(nudTileHeight.Text, out tileHeight))
            {
                MessageBox.Show("Tile height must be an integer value.");
                return;
            }
            else if (tileHeight < 1)
            {
                MessageBox.Show("Tile height must be 1 or greater.");
                return;
            }

            Image tileSet = (Image)Bitmap.FromFile(tbTilesetImage.Text);
            tilesWide = tileSet.Width / tileWidth;
            tilesHigh = tileSet.Height / tileHeight;

            TilesetData = new TilesetData();
            TilesetData.Name = tbTilesetName.Text;
            TilesetData.FilePath = tbTilesetImage.Text;
            TilesetData.TileWidth = tileWidth;
            TilesetData.TileHeight = tileHeight;
            TilesetData.TilesWide = tilesWide;
            TilesetData.TilesHigh = tilesHigh;

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
