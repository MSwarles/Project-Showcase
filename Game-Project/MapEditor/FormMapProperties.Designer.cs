namespace MapEditor
{
    partial class FormMapProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMapProperties));
            this.lblMapSize = new System.Windows.Forms.Label();
            this.lblTilesWide = new System.Windows.Forms.Label();
            this.lblTilesHigh = new System.Windows.Forms.Label();
            this.nudTilesWide = new System.Windows.Forms.NumericUpDown();
            this.nudTilesHigh = new System.Windows.Forms.NumericUpDown();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTiles1 = new System.Windows.Forms.Label();
            this.lblTiles2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTileSize = new System.Windows.Forms.Label();
            this.lblPixels2 = new System.Windows.Forms.Label();
            this.lblPixels1 = new System.Windows.Forms.Label();
            this.nudTileHeight = new System.Windows.Forms.NumericUpDown();
            this.nudTileWidth = new System.Windows.Forms.NumericUpDown();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.lblTileWidth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesWide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesHigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMapSize
            // 
            this.lblMapSize.AutoSize = true;
            this.lblMapSize.Location = new System.Drawing.Point(7, 37);
            this.lblMapSize.Name = "lblMapSize";
            this.lblMapSize.Size = new System.Drawing.Size(51, 13);
            this.lblMapSize.TabIndex = 0;
            this.lblMapSize.Text = "Map Size";
            // 
            // lblTilesWide
            // 
            this.lblTilesWide.AutoSize = true;
            this.lblTilesWide.Location = new System.Drawing.Point(22, 57);
            this.lblTilesWide.Name = "lblTilesWide";
            this.lblTilesWide.Size = new System.Drawing.Size(38, 13);
            this.lblTilesWide.TabIndex = 1;
            this.lblTilesWide.Text = "Width:";
            // 
            // lblTilesHigh
            // 
            this.lblTilesHigh.AutoSize = true;
            this.lblTilesHigh.Location = new System.Drawing.Point(22, 80);
            this.lblTilesHigh.Name = "lblTilesHigh";
            this.lblTilesHigh.Size = new System.Drawing.Size(41, 13);
            this.lblTilesHigh.TabIndex = 2;
            this.lblTilesHigh.Text = "Height:";
            // 
            // nudTilesWide
            // 
            this.nudTilesWide.Location = new System.Drawing.Point(79, 53);
            this.nudTilesWide.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudTilesWide.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTilesWide.Name = "nudTilesWide";
            this.nudTilesWide.Size = new System.Drawing.Size(56, 20);
            this.nudTilesWide.TabIndex = 1;
            this.nudTilesWide.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTilesWide.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudTilesHigh
            // 
            this.nudTilesHigh.Location = new System.Drawing.Point(79, 78);
            this.nudTilesHigh.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudTilesHigh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTilesHigh.Name = "nudTilesHigh";
            this.nudTilesHigh.Size = new System.Drawing.Size(56, 20);
            this.nudTilesHigh.TabIndex = 2;
            this.nudTilesHigh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTilesHigh.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSeparator
            // 
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator.Location = new System.Drawing.Point(56, 44);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(141, 2);
            this.lblSeparator.TabIndex = 10;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(19, 198);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(108, 198);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblTiles1
            // 
            this.lblTiles1.AutoSize = true;
            this.lblTiles1.Location = new System.Drawing.Point(141, 55);
            this.lblTiles1.Name = "lblTiles1";
            this.lblTiles1.Size = new System.Drawing.Size(25, 13);
            this.lblTiles1.TabIndex = 13;
            this.lblTiles1.Text = "tiles";
            // 
            // lblTiles2
            // 
            this.lblTiles2.AutoSize = true;
            this.lblTiles2.Location = new System.Drawing.Point(141, 80);
            this.lblTiles2.Name = "lblTiles2";
            this.lblTiles2.Size = new System.Drawing.Size(25, 13);
            this.lblTiles2.TabIndex = 14;
            this.lblTiles2.Text = "tiles";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(90, 11);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(99, 20);
            this.tbName.TabIndex = 16;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(22, 14);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(62, 13);
            this.lblName.TabIndex = 15;
            this.lblName.Text = "Map Name:";
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(52, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 2);
            this.label2.TabIndex = 31;
            // 
            // lblTileSize
            // 
            this.lblTileSize.AutoSize = true;
            this.lblTileSize.Location = new System.Drawing.Point(7, 111);
            this.lblTileSize.Name = "lblTileSize";
            this.lblTileSize.Size = new System.Drawing.Size(47, 13);
            this.lblTileSize.TabIndex = 30;
            this.lblTileSize.Text = "Tile Size";
            // 
            // lblPixels2
            // 
            this.lblPixels2.AutoSize = true;
            this.lblPixels2.Location = new System.Drawing.Point(141, 157);
            this.lblPixels2.Name = "lblPixels2";
            this.lblPixels2.Size = new System.Drawing.Size(33, 13);
            this.lblPixels2.TabIndex = 29;
            this.lblPixels2.Text = "pixels";
            // 
            // lblPixels1
            // 
            this.lblPixels1.AutoSize = true;
            this.lblPixels1.Location = new System.Drawing.Point(141, 132);
            this.lblPixels1.Name = "lblPixels1";
            this.lblPixels1.Size = new System.Drawing.Size(33, 13);
            this.lblPixels1.TabIndex = 28;
            this.lblPixels1.Text = "pixels";
            // 
            // nudTileHeight
            // 
            this.nudTileHeight.Enabled = false;
            this.nudTileHeight.Location = new System.Drawing.Point(79, 155);
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
            this.nudTileHeight.TabIndex = 25;
            this.nudTileHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTileHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudTileWidth
            // 
            this.nudTileWidth.Enabled = false;
            this.nudTileWidth.Location = new System.Drawing.Point(79, 130);
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
            this.nudTileWidth.TabIndex = 24;
            this.nudTileWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nudTileWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Location = new System.Drawing.Point(22, 157);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(41, 13);
            this.lblTileHeight.TabIndex = 26;
            this.lblTileHeight.Text = "Height:";
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Location = new System.Drawing.Point(22, 132);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(38, 13);
            this.lblTileWidth.TabIndex = 27;
            this.lblTileWidth.Text = "Width:";
            // 
            // FormMapProperties
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(202, 233);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTileSize);
            this.Controls.Add(this.lblPixels2);
            this.Controls.Add(this.lblPixels1);
            this.Controls.Add(this.nudTileHeight);
            this.Controls.Add(this.nudTileWidth);
            this.Controls.Add(this.lblTileHeight);
            this.Controls.Add(this.lblTileWidth);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTiles2);
            this.Controls.Add(this.lblTiles1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblSeparator);
            this.Controls.Add(this.nudTilesHigh);
            this.Controls.Add(this.nudTilesWide);
            this.Controls.Add(this.lblTilesHigh);
            this.Controls.Add(this.lblTilesWide);
            this.Controls.Add(this.lblMapSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMapProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Properties";
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesWide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTilesHigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMapSize;
        private System.Windows.Forms.Label lblTilesWide;
        private System.Windows.Forms.Label lblTilesHigh;
        private System.Windows.Forms.NumericUpDown nudTilesWide;
        private System.Windows.Forms.NumericUpDown nudTilesHigh;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTiles1;
        private System.Windows.Forms.Label lblTiles2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTileSize;
        private System.Windows.Forms.Label lblPixels2;
        private System.Windows.Forms.Label lblPixels1;
        private System.Windows.Forms.NumericUpDown nudTileHeight;
        private System.Windows.Forms.NumericUpDown nudTileWidth;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.Label lblTileWidth;
    }
}