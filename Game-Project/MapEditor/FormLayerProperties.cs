using System;
using System.Windows.Forms;

using SuperMetroid.TileEngine;

namespace MapEditor
{
    /// <summary>
    /// Form used to change properties of a layer.
    /// </summary>
    public partial class FormLayerProperties : Form
    {
        #region Properties
        public MapLayer Layer { get; }
        public bool OKPressed { get; private set; }
        #endregion

        #region Constructor
        public FormLayerProperties(MapLayer layer)
        {
            InitializeComponent();

            Layer = layer;
            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            SetDefaultValues();
        }
        private void SetDefaultValues()
        {
            tbName.Text = Layer.Name;
            cbIsVisible.Checked = Layer.IsVisible;
            nudOpacity.Value = Layer.GetAlpha();
        }
        #endregion

        #region Button Event Handlers
        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("You must enter a name for the layer.", "Missing Layer Name");
                return;
            }

            int alpha;
            if (!int.TryParse(nudOpacity.Text, out alpha) || alpha < nudOpacity.Minimum || 
                alpha > nudOpacity.Maximum)
            {
                MessageBox.Show("Opacity must be an integer between 0 and 255.", "Invalid Opacity");
                return;
            }

            Layer.Name = tbName.Text;
            Layer.IsVisible = cbIsVisible.Checked;
            Layer.SetAlpha(alpha);
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
