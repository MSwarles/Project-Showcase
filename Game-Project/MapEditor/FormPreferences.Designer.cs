namespace MapEditor
{
    partial class FormPreferences
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPreferences));
            this.btnClose = new System.Windows.Forms.Button();
            this.tcPreferences = new System.Windows.Forms.TabControl();
            this.tpColors = new System.Windows.Forms.TabPage();
            this.tpTBD = new System.Windows.Forms.TabPage();
            this.btnMapBackdropColor = new System.Windows.Forms.Button();
            this.btnMapBackgroundColor = new System.Windows.Forms.Button();
            this.lblMapBackdrop = new System.Windows.Forms.Label();
            this.lblMapBg = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMapGridColor = new System.Windows.Forms.Button();
            this.btnTilesetGridColor = new System.Windows.Forms.Button();
            this.lblTilesetGrid = new System.Windows.Forms.Label();
            this.lblMapColors = new System.Windows.Forms.Label();
            this.lblSeparator1 = new System.Windows.Forms.Label();
            this.separator2 = new System.Windows.Forms.Label();
            this.lblTilesetColors = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tcPreferences.SuspendLayout();
            this.tpColors.SuspendLayout();
            this.tpTBD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(204, 291);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 33;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tcPreferences
            // 
            this.tcPreferences.Controls.Add(this.tpColors);
            this.tcPreferences.Controls.Add(this.tpTBD);
            this.tcPreferences.Location = new System.Drawing.Point(3, 5);
            this.tcPreferences.Name = "tcPreferences";
            this.tcPreferences.SelectedIndex = 0;
            this.tcPreferences.Size = new System.Drawing.Size(282, 273);
            this.tcPreferences.TabIndex = 34;
            // 
            // tpColors
            // 
            this.tpColors.Controls.Add(this.separator2);
            this.tpColors.Controls.Add(this.lblTilesetColors);
            this.tpColors.Controls.Add(this.btnTilesetGridColor);
            this.tpColors.Controls.Add(this.lblTilesetGrid);
            this.tpColors.Controls.Add(this.lblSeparator1);
            this.tpColors.Controls.Add(this.lblMapColors);
            this.tpColors.Controls.Add(this.btnMapBackdropColor);
            this.tpColors.Controls.Add(this.btnMapBackgroundColor);
            this.tpColors.Controls.Add(this.lblMapBackdrop);
            this.tpColors.Controls.Add(this.lblMapBg);
            this.tpColors.Controls.Add(this.label1);
            this.tpColors.Controls.Add(this.btnMapGridColor);
            this.tpColors.Location = new System.Drawing.Point(4, 22);
            this.tpColors.Name = "tpColors";
            this.tpColors.Padding = new System.Windows.Forms.Padding(3);
            this.tpColors.Size = new System.Drawing.Size(274, 247);
            this.tpColors.TabIndex = 0;
            this.tpColors.Text = "Colors";
            this.tpColors.UseVisualStyleBackColor = true;
            // 
            // tpTBD
            // 
            this.tpTBD.Controls.Add(this.pictureBox1);
            this.tpTBD.Location = new System.Drawing.Point(4, 22);
            this.tpTBD.Name = "tpTBD";
            this.tpTBD.Padding = new System.Windows.Forms.Padding(3);
            this.tpTBD.Size = new System.Drawing.Size(274, 247);
            this.tpTBD.TabIndex = 1;
            this.tpTBD.Text = "Future";
            this.tpTBD.UseVisualStyleBackColor = true;
            // 
            // btnMapBackdropColor
            // 
            this.btnMapBackdropColor.Location = new System.Drawing.Point(135, 83);
            this.btnMapBackdropColor.Name = "btnMapBackdropColor";
            this.btnMapBackdropColor.Size = new System.Drawing.Size(47, 23);
            this.btnMapBackdropColor.TabIndex = 38;
            this.btnMapBackdropColor.UseVisualStyleBackColor = true;
            // 
            // btnMapBackgroundColor
            // 
            this.btnMapBackgroundColor.Location = new System.Drawing.Point(135, 53);
            this.btnMapBackgroundColor.Name = "btnMapBackgroundColor";
            this.btnMapBackgroundColor.Size = new System.Drawing.Size(47, 23);
            this.btnMapBackgroundColor.TabIndex = 37;
            this.btnMapBackgroundColor.UseVisualStyleBackColor = true;
            // 
            // lblMapBackdrop
            // 
            this.lblMapBackdrop.AutoSize = true;
            this.lblMapBackdrop.Location = new System.Drawing.Point(27, 88);
            this.lblMapBackdrop.Name = "lblMapBackdrop";
            this.lblMapBackdrop.Size = new System.Drawing.Size(56, 13);
            this.lblMapBackdrop.TabIndex = 36;
            this.lblMapBackdrop.Text = "Backdrop:";
            // 
            // lblMapBg
            // 
            this.lblMapBg.AutoSize = true;
            this.lblMapBg.Location = new System.Drawing.Point(27, 58);
            this.lblMapBg.Name = "lblMapBg";
            this.lblMapBg.Size = new System.Drawing.Size(68, 13);
            this.lblMapBg.TabIndex = 35;
            this.lblMapBg.Text = "Background:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Grid:";
            // 
            // btnMapGridColor
            // 
            this.btnMapGridColor.Location = new System.Drawing.Point(135, 23);
            this.btnMapGridColor.Name = "btnMapGridColor";
            this.btnMapGridColor.Size = new System.Drawing.Size(47, 23);
            this.btnMapGridColor.TabIndex = 34;
            this.btnMapGridColor.UseVisualStyleBackColor = true;
            // 
            // btnTilesetGridColor
            // 
            this.btnTilesetGridColor.Location = new System.Drawing.Point(135, 134);
            this.btnTilesetGridColor.Name = "btnTilesetGridColor";
            this.btnTilesetGridColor.Size = new System.Drawing.Size(47, 23);
            this.btnTilesetGridColor.TabIndex = 42;
            this.btnTilesetGridColor.UseVisualStyleBackColor = true;
            // 
            // lblTilesetGrid
            // 
            this.lblTilesetGrid.AutoSize = true;
            this.lblTilesetGrid.Location = new System.Drawing.Point(27, 139);
            this.lblTilesetGrid.Name = "lblTilesetGrid";
            this.lblTilesetGrid.Size = new System.Drawing.Size(29, 13);
            this.lblTilesetGrid.TabIndex = 41;
            this.lblTilesetGrid.Text = "Grid:";
            // 
            // lblMapColors
            // 
            this.lblMapColors.AutoSize = true;
            this.lblMapColors.Location = new System.Drawing.Point(8, 6);
            this.lblMapColors.Name = "lblMapColors";
            this.lblMapColors.Size = new System.Drawing.Size(28, 13);
            this.lblMapColors.TabIndex = 39;
            this.lblMapColors.Text = "Map";
            // 
            // lblSeparator1
            // 
            this.lblSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator1.Location = new System.Drawing.Point(34, 13);
            this.lblSeparator1.Name = "lblSeparator1";
            this.lblSeparator1.Size = new System.Drawing.Size(234, 2);
            this.lblSeparator1.TabIndex = 40;
            // 
            // separator2
            // 
            this.separator2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separator2.Location = new System.Drawing.Point(43, 123);
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(225, 2);
            this.separator2.TabIndex = 44;
            // 
            // lblTilesetColors
            // 
            this.lblTilesetColors.AutoSize = true;
            this.lblTilesetColors.Location = new System.Drawing.Point(6, 117);
            this.lblTilesetColors.Name = "lblTilesetColors";
            this.lblTilesetColors.Size = new System.Drawing.Size(38, 13);
            this.lblTilesetColors.TabIndex = 43;
            this.lblTilesetColors.Text = "Tileset";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 40);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(260, 162);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FormPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(284, 326);
            this.Controls.Add(this.tcPreferences);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPreferences";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preferences";
            this.tcPreferences.ResumeLayout(false);
            this.tpColors.ResumeLayout(false);
            this.tpColors.PerformLayout();
            this.tpTBD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tcPreferences;
        private System.Windows.Forms.TabPage tpColors;
        private System.Windows.Forms.Label separator2;
        private System.Windows.Forms.Label lblTilesetColors;
        private System.Windows.Forms.Button btnTilesetGridColor;
        private System.Windows.Forms.Label lblTilesetGrid;
        private System.Windows.Forms.Label lblSeparator1;
        private System.Windows.Forms.Label lblMapColors;
        private System.Windows.Forms.Button btnMapBackdropColor;
        private System.Windows.Forms.Button btnMapBackgroundColor;
        private System.Windows.Forms.Label lblMapBackdrop;
        private System.Windows.Forms.Label lblMapBg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnMapGridColor;
        private System.Windows.Forms.TabPage tpTBD;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}