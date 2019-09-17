using System;
using System.Windows.Forms;

using GDIColor = System.Drawing.Color;

using Microsoft.Xna.Framework;

namespace MapEditor
{
    /// <summary>
    /// Form used to change map editor settings.
    /// </summary>
    public partial class FormPreferences : Form
    {
        #region Properties
        public Color MapGridColor { get; private set; }
        public Color MapBgColor { get; private set; }
        public Color MapBackdropColor { get; private set; }
        public GDIColor TsGridColor { get; private set; }

        public event EventHandler changeMapGridColor;
        public event EventHandler changeMapBgColor;
        public event EventHandler changeMapBackdropColor;
        public event EventHandler changeTsGridColor;
        #endregion

        #region Constructor
        public FormPreferences(Color mapGridColor, Color mapBgColor, Color mapBackdropColor, GDIColor tsGridColor)
        {
            InitializeComponent();

            MapGridColor = mapGridColor;
            MapBgColor = mapBgColor;
            MapBackdropColor = mapBackdropColor;
            TsGridColor = tsGridColor;

            btnMapGridColor.BackColor = ConvertColorToGDIColor(mapGridColor);
            btnMapBackgroundColor.BackColor = ConvertColorToGDIColor(mapBgColor);
            btnMapBackdropColor.BackColor = ConvertColorToGDIColor(mapBackdropColor);
            btnTilesetGridColor.BackColor = tsGridColor;

            btnMapGridColor.Click += new EventHandler(btnMapGridColor_Click);
            btnMapBackgroundColor.Click += new EventHandler(btnMapBackgroundColor_Click);
            btnMapBackdropColor.Click += new EventHandler(btnMapBackdropColor_Click);
            btnTilesetGridColor.Click += new EventHandler(btnTilesetGridColor_Click);         
            btnClose.Click += new EventHandler(btnClose_Click);

            colorDialog.FullOpen = true;
        }
        #endregion

        void btnMapGridColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = btnMapGridColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnMapGridColor.BackColor = colorDialog.Color;
                MapGridColor = ConvertGDIColorToColor(colorDialog.Color);
                ChangeMapGridColor(null);
            }
        }

        void btnMapBackgroundColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = btnMapBackgroundColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnMapBackgroundColor.BackColor = colorDialog.Color;
                MapBgColor = ConvertGDIColorToColor(colorDialog.Color);
                ChangeMapBgColor(null);
            }
        }

        void btnMapBackdropColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = btnMapBackdropColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnMapBackdropColor.BackColor = colorDialog.Color;
                MapBackdropColor = ConvertGDIColorToColor(colorDialog.Color);
                ChangeMapBackdropColor(null);
            }
        }

        void btnTilesetGridColor_Click(object sender, EventArgs e)
        {
            colorDialog.Color = btnTilesetGridColor.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                btnTilesetGridColor.BackColor = colorDialog.Color;
                TsGridColor = colorDialog.Color;
                ChangeTsGridColor(null);
            }
        }

        void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void ChangeMapGridColor(EventArgs e)
        {
            EventHandler eh = changeMapGridColor;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        public virtual void ChangeMapBgColor(EventArgs e)
        {
            EventHandler eh = changeMapBgColor;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        public virtual void ChangeMapBackdropColor(EventArgs e)
        {
            EventHandler eh = changeMapBackdropColor;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        public virtual void ChangeTsGridColor(EventArgs e)
        {
            EventHandler eh = changeTsGridColor;
            if (eh != null)
            {
                eh(this, e);
            }
        }

        private Color ConvertGDIColorToColor(GDIColor GDIColor)
        {
            Color color = new Color(
                GDIColor.R,
                GDIColor.G,
                GDIColor.B);
            return color;
        }

        private GDIColor ConvertColorToGDIColor(Color color)
        {
            GDIColor gdiColor = GDIColor.FromArgb(
                color.R,
                color.G,
                color.B);
            return gdiColor;
        }
    }
}
