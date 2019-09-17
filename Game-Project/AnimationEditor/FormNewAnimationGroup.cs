using System;
using System.Windows.Forms;

namespace AnimationEditor
{
    /// <summary>
    /// Form used to create a new animation group.
    /// </summary>
    public partial class FormNewAnimationGroup : Form
    {
        #region Properties
        public string GroupName { get; private set; }
        public string TextureFilePath { get; private set; }
        public bool OKPressed { get; private set; }
        #endregion

        #region Constructor
        public FormNewAnimationGroup()
        {
            InitializeComponent();

            btnSelectImage.Click += new EventHandler(btnSelectImage_Click);
            btnOK.Click += new EventHandler(btnOK_Click);
            btnCancel.Click += new EventHandler(btnCancel_Click);

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            tbGroupName.Text = "default";
        }
        #endregion

        #region Button Event Handlers
        void btnSelectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.InitialDirectory = "C:\\Users\\Matt\\Desktop\\Monogame\\SuperMetroid\\AnimationEditor\\Content";
            ofDialog.Filter = "Image Files|*.BMP;*.GIF;*.JPG;*.TGA;*.PNG";
            ofDialog.CheckFileExists = true;
            ofDialog.CheckPathExists = true;
            ofDialog.RestoreDirectory = false;
            ofDialog.Multiselect = false;

            DialogResult result = ofDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbAnimationImage.Text = ofDialog.FileName;
            }
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbGroupName.Text))
            {
                MessageBox.Show("You must enter a name for the map.", "Missing Group Name");
                return;
            }

            if (string.IsNullOrEmpty(tbAnimationImage.Text))
            {
                MessageBox.Show("You must select an image for the animation.");
                return;
            }

            GroupName = tbGroupName.Text;
            TextureFilePath = tbAnimationImage.Text;
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
