namespace MapEditor
{
    partial class FormLayerProperties
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLayerProperties));
            this.lblName = new System.Windows.Forms.Label();
            this.lblOpacity = new System.Windows.Forms.Label();
            this.cbIsVisible = new System.Windows.Forms.CheckBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.nudOpacity = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblGeneral = new System.Windows.Forms.Label();
            this.lblSplitter = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBlending = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(25, 29);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // lblOpacity
            // 
            this.lblOpacity.AutoSize = true;
            this.lblOpacity.Location = new System.Drawing.Point(22, 104);
            this.lblOpacity.Name = "lblOpacity";
            this.lblOpacity.Size = new System.Drawing.Size(46, 13);
            this.lblOpacity.TabIndex = 1;
            this.lblOpacity.Text = "Opacity:";
            // 
            // cbIsVisible
            // 
            this.cbIsVisible.AutoSize = true;
            this.cbIsVisible.Location = new System.Drawing.Point(25, 52);
            this.cbIsVisible.Name = "cbIsVisible";
            this.cbIsVisible.Size = new System.Drawing.Size(67, 17);
            this.cbIsVisible.TabIndex = 2;
            this.cbIsVisible.Text = "Is Visible";
            this.cbIsVisible.UseVisualStyleBackColor = true;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(78, 26);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(110, 20);
            this.tbName.TabIndex = 1;
            // 
            // nudOpacity
            // 
            this.nudOpacity.Location = new System.Drawing.Point(78, 102);
            this.nudOpacity.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudOpacity.Name = "nudOpacity";
            this.nudOpacity.Size = new System.Drawing.Size(56, 20);
            this.nudOpacity.TabIndex = 3;
            this.nudOpacity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(19, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(105, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblGeneral
            // 
            this.lblGeneral.AutoSize = true;
            this.lblGeneral.Location = new System.Drawing.Point(7, 9);
            this.lblGeneral.Name = "lblGeneral";
            this.lblGeneral.Size = new System.Drawing.Size(44, 13);
            this.lblGeneral.TabIndex = 6;
            this.lblGeneral.Text = "General";
            // 
            // lblSplitter
            // 
            this.lblSplitter.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSplitter.Location = new System.Drawing.Point(50, 15);
            this.lblSplitter.Name = "lblSplitter";
            this.lblSplitter.Size = new System.Drawing.Size(145, 2);
            this.lblSplitter.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(54, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 2);
            this.label1.TabIndex = 9;
            // 
            // lblBlending
            // 
            this.lblBlending.AutoSize = true;
            this.lblBlending.Location = new System.Drawing.Point(7, 81);
            this.lblBlending.Name = "lblBlending";
            this.lblBlending.Size = new System.Drawing.Size(48, 13);
            this.lblBlending.TabIndex = 8;
            this.lblBlending.Text = "Blending";
            // 
            // FormLayerProperties
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(199, 179);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBlending);
            this.Controls.Add(this.lblSplitter);
            this.Controls.Add(this.lblGeneral);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.nudOpacity);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.cbIsVisible);
            this.Controls.Add(this.lblOpacity);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLayerProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Layer Properties";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.nudOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblOpacity;
        private System.Windows.Forms.CheckBox cbIsVisible;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.NumericUpDown nudOpacity;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblGeneral;
        private System.Windows.Forms.Label lblSplitter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBlending;
    }
}