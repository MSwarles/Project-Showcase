namespace MapEditor
{
    partial class FormNewTileset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNewTileset));
            this.lblTilesetName = new System.Windows.Forms.Label();
            this.lblImageName = new System.Windows.Forms.Label();
            this.lblTileWidth = new System.Windows.Forms.Label();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.tbTilesetName = new System.Windows.Forms.TextBox();
            this.tbTilesetImage = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblSeparator1 = new System.Windows.Forms.Label();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblPixels2 = new System.Windows.Forms.Label();
            this.lblPixels1 = new System.Windows.Forms.Label();
            this.nudTileHeight = new System.Windows.Forms.NumericUpDown();
            this.nudTileWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTileSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTilesetName
            // 
            this.lblTilesetName.AutoSize = true;
            this.lblTilesetName.Location = new System.Drawing.Point(22, 14);
            this.lblTilesetName.Name = "lblTilesetName";
            this.lblTilesetName.Size = new System.Drawing.Size(72, 13);
            this.lblTilesetName.TabIndex = 0;
            this.lblTilesetName.Text = "Tileset Name:";
            // 
            // lblImageName
            // 
            this.lblImageName.AutoSize = true;
            this.lblImageName.Location = new System.Drawing.Point(29, 61);
            this.lblImageName.Name = "lblImageName";
            this.lblImageName.Size = new System.Drawing.Size(39, 13);
            this.lblImageName.TabIndex = 0;
            this.lblImageName.Text = "Image:";
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Location = new System.Drawing.Point(29, 114);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(38, 13);
            this.lblTileWidth.TabIndex = 0;
            this.lblTileWidth.Text = "Width:";
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Location = new System.Drawing.Point(29, 138);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(41, 13);
            this.lblTileHeight.TabIndex = 0;
            this.lblTileHeight.Text = "Height:";
            // 
            // tbTilesetName
            // 
            this.tbTilesetName.Location = new System.Drawing.Point(100, 11);
            this.tbTilesetName.Name = "tbTilesetName";
            this.tbTilesetName.Size = new System.Drawing.Size(108, 20);
            this.tbTilesetName.TabIndex = 1;
            // 
            // tbTilesetImage
            // 
            this.tbTilesetImage.Enabled = false;
            this.tbTilesetImage.Location = new System.Drawing.Point(82, 58);
            this.tbTilesetImage.Name = "tbTilesetImage";
            this.tbTilesetImage.Size = new System.Drawing.Size(95, 20);
            this.tbTilesetImage.TabIndex = 2;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(179, 56);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(28, 23);
            this.btnSelectImage.TabIndex = 3;
            this.btnSelectImage.Text = "...";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(25, 179);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(120, 179);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblSeparator1
            // 
            this.lblSeparator1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator1.Location = new System.Drawing.Point(61, 46);
            this.lblSeparator1.Name = "lblSeparator1";
            this.lblSeparator1.Size = new System.Drawing.Size(151, 2);
            this.lblSeparator1.TabIndex = 24;
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(7, 39);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(55, 13);
            this.lblFileName.TabIndex = 23;
            this.lblFileName.Text = "Image File";
            // 
            // lblPixels2
            // 
            this.lblPixels2.AutoSize = true;
            this.lblPixels2.Location = new System.Drawing.Point(144, 140);
            this.lblPixels2.Name = "lblPixels2";
            this.lblPixels2.Size = new System.Drawing.Size(33, 13);
            this.lblPixels2.TabIndex = 26;
            this.lblPixels2.Text = "pixels";
            // 
            // lblPixels1
            // 
            this.lblPixels1.AutoSize = true;
            this.lblPixels1.Location = new System.Drawing.Point(144, 115);
            this.lblPixels1.Name = "lblPixels1";
            this.lblPixels1.Size = new System.Drawing.Size(33, 13);
            this.lblPixels1.TabIndex = 25;
            this.lblPixels1.Text = "pixels";
            // 
            // nudTileHeight
            // 
            this.nudTileHeight.Location = new System.Drawing.Point(82, 137);
            this.nudTileHeight.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudTileHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTileHeight.Name = "nudTileHeight";
            this.nudTileHeight.Size = new System.Drawing.Size(56, 20);
            this.nudTileHeight.TabIndex = 28;
            this.nudTileHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTileHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudTileWidth
            // 
            this.nudTileWidth.Location = new System.Drawing.Point(82, 112);
            this.nudTileWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudTileWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTileWidth.Name = "nudTileWidth";
            this.nudTileWidth.Size = new System.Drawing.Size(56, 20);
            this.nudTileWidth.TabIndex = 27;
            this.nudTileWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTileWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(52, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 2);
            this.label2.TabIndex = 30;
            // 
            // lblTileSize
            // 
            this.lblTileSize.AutoSize = true;
            this.lblTileSize.Location = new System.Drawing.Point(7, 92);
            this.lblTileSize.Name = "lblTileSize";
            this.lblTileSize.Size = new System.Drawing.Size(47, 13);
            this.lblTileSize.TabIndex = 29;
            this.lblTileSize.Text = "Tile Size";
            // 
            // FormNewTileset
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(218, 214);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTileSize);
            this.Controls.Add(this.nudTileHeight);
            this.Controls.Add(this.nudTileWidth);
            this.Controls.Add(this.lblPixels2);
            this.Controls.Add(this.lblPixels1);
            this.Controls.Add(this.lblSeparator1);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.tbTilesetImage);
            this.Controls.Add(this.tbTilesetName);
            this.Controls.Add(this.lblTileHeight);
            this.Controls.Add(this.lblTileWidth);
            this.Controls.Add(this.lblImageName);
            this.Controls.Add(this.lblTilesetName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNewTileset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Tileset";
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTilesetName;
        private System.Windows.Forms.Label lblImageName;
        private System.Windows.Forms.Label lblTileWidth;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.TextBox tbTilesetName;
        private System.Windows.Forms.TextBox tbTilesetImage;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblSeparator1;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Label lblPixels2;
        private System.Windows.Forms.Label lblPixels1;
        private System.Windows.Forms.NumericUpDown nudTileHeight;
        private System.Windows.Forms.NumericUpDown nudTileWidth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTileSize;
    }
}