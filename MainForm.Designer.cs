namespace GetScreenshot
{
    partial class MainForm
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
            this.btnCrop = new System.Windows.Forms.Button();
            this.btnScreenshot2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCrop
            // 
            this.btnCrop.Location = new System.Drawing.Point(7, 52);
            this.btnCrop.Name = "btnCrop";
            this.btnCrop.Size = new System.Drawing.Size(239, 23);
            this.btnCrop.TabIndex = 1;
            this.btnCrop.Text = "Crop Image";
            this.btnCrop.UseVisualStyleBackColor = true;
            this.btnCrop.Click += new System.EventHandler(this.btnCrop_Click);
            // 
            // btnScreenshot2
            // 
            this.btnScreenshot2.Location = new System.Drawing.Point(7, 12);
            this.btnScreenshot2.Name = "btnScreenshot2";
            this.btnScreenshot2.Size = new System.Drawing.Size(239, 23);
            this.btnScreenshot2.TabIndex = 4;
            this.btnScreenshot2.Text = "Get Screenshot";
            this.btnScreenshot2.UseVisualStyleBackColor = true;
            this.btnScreenshot2.Click += new System.EventHandler(this.btnScreenshot_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(253, 88);
            this.Controls.Add(this.btnScreenshot2);
            this.Controls.Add(this.btnCrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCrop;
        private System.Windows.Forms.Button btnScreenshot2;
    }
}

