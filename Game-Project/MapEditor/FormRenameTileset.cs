using System;
using System.Windows.Forms;

namespace MapEditor
{
    /// <summary>
    /// Form used to rename a tile set.
    /// </summary>
    public partial class FormRenameTileset : Form
    {
        public string TilesetName { get; private set; }
        public bool OKPressed { get; private set; }

        public FormRenameTileset(string name)
        {
            InitializeComponent();

            tbName.Text = name;

            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("You must enter a name for the tileset.", "Missing Tileset Name");
                return;
            }

            TilesetName = tbName.Text;
            OKPressed = true;
            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            OKPressed = false;
            this.Close();
        }
    }
}
