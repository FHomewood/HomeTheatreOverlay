namespace HTOInstaller_V1._0
{
    partial class Installer
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
            this.NextButton = new System.Windows.Forms.Button();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.DirectoryTbx = new System.Windows.Forms.TextBox();
            this.DirectoryLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(429, 109);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 0;
            this.NextButton.Text = "Install";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.OnClick);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(13, 13);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(241, 30);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "Home Theatre Overlay";
            // 
            // DirectoryTbx
            // 
            this.DirectoryTbx.Location = new System.Drawing.Point(113, 57);
            this.DirectoryTbx.Name = "DirectoryTbx";
            this.DirectoryTbx.Size = new System.Drawing.Size(371, 20);
            this.DirectoryTbx.TabIndex = 2;
            this.DirectoryTbx.Visible = false;
            // 
            // DirectoryLabel
            // 
            this.DirectoryLabel.AutoSize = true;
            this.DirectoryLabel.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DirectoryLabel.Location = new System.Drawing.Point(26, 54);
            this.DirectoryLabel.Name = "DirectoryLabel";
            this.DirectoryLabel.Size = new System.Drawing.Size(81, 21);
            this.DirectoryLabel.TabIndex = 3;
            this.DirectoryLabel.Text = "Directory: ";
            this.DirectoryLabel.Visible = false;
            // 
            // Installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 144);
            this.Controls.Add(this.DirectoryLabel);
            this.Controls.Add(this.DirectoryTbx);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.NextButton);
            this.Name = "Installer";
            this.Text = "Installer";
            this.Load += new System.EventHandler(this.Initialise);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.TextBox DirectoryTbx;
        private System.Windows.Forms.Label DirectoryLabel;
    }
}

