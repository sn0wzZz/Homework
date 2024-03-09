namespace ImageDownSizer
{
    partial class Form1
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
            this.pictureBoxOriginal = new System.Windows.Forms.PictureBox();
            this.pictureBoxDownsized = new System.Windows.Forms.PictureBox();
            this.labelFactor = new System.Windows.Forms.Label();
            this.textBoxFactor = new System.Windows.Forms.TextBox();
            this.buttonOpenImage = new System.Windows.Forms.Button();
            this.buttonDownsize = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownsized)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(20, 24);
            this.pictureBoxOriginal.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pictureBoxOriginal.Name = "pictureBoxOriginal";
            this.pictureBoxOriginal.Size = new System.Drawing.Size(569, 398);
            this.pictureBoxOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxOriginal.TabIndex = 0;
            this.pictureBoxOriginal.TabStop = false;
            // 
            // pictureBoxDownsized
            // 
            this.pictureBoxDownsized.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxDownsized.Location = new System.Drawing.Point(602, 24);
            this.pictureBoxDownsized.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pictureBoxDownsized.Name = "pictureBoxDownsized";
            this.pictureBoxDownsized.Size = new System.Drawing.Size(574, 398);
            this.pictureBoxDownsized.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxDownsized.TabIndex = 1;
            this.pictureBoxDownsized.TabStop = false;
            // 
            // labelFactor
            // 
            this.labelFactor.AutoSize = true;
            this.labelFactor.ForeColor = System.Drawing.Color.LightGray;
            this.labelFactor.Location = new System.Drawing.Point(20, 459);
            this.labelFactor.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelFactor.Name = "labelFactor";
            this.labelFactor.Size = new System.Drawing.Size(124, 32);
            this.labelFactor.TabIndex = 2;
            this.labelFactor.Text = "Factor (%):";
            // 
            // textBoxFactor
            // 
            this.textBoxFactor.Location = new System.Drawing.Point(150, 457);
            this.textBoxFactor.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxFactor.Name = "textBoxFactor";
            this.textBoxFactor.Size = new System.Drawing.Size(95, 39);
            this.textBoxFactor.TabIndex = 3;
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Location = new System.Drawing.Point(255, 448);
            this.buttonOpenImage.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonOpenImage.Name = "buttonOpenImage";
            this.buttonOpenImage.Size = new System.Drawing.Size(162, 56);
            this.buttonOpenImage.TabIndex = 4;
            this.buttonOpenImage.Text = "Open Image";
            this.buttonOpenImage.UseVisualStyleBackColor = true;
            this.buttonOpenImage.Click += new System.EventHandler(this.OpenImage_Click);
            // 
            // buttonDownsize
            // 
            this.buttonDownsize.Location = new System.Drawing.Point(427, 448);
            this.buttonDownsize.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonDownsize.Name = "buttonDownsize";
            this.buttonDownsize.Size = new System.Drawing.Size(162, 56);
            this.buttonDownsize.TabIndex = 5;
            this.buttonDownsize.Text = "Downsize";
            this.buttonDownsize.UseVisualStyleBackColor = true;
            this.buttonDownsize.Click += new System.EventHandler(this.DownsizeImage_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(602, 448);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(402, 56);
            this.progressBar.TabIndex = 7;
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(1014, 448);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(162, 56);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.DownloadImage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1196, 520);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonDownsize);
            this.Controls.Add(this.buttonOpenImage);
            this.Controls.Add(this.textBoxFactor);
            this.Controls.Add(this.labelFactor);
            this.Controls.Add(this.pictureBoxDownsized);
            this.Controls.Add(this.pictureBoxOriginal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form1";
            this.Text = "Image Downsizer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownsized)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.PictureBox pictureBoxDownsized;
        private System.Windows.Forms.Label labelFactor;
        private System.Windows.Forms.TextBox textBoxFactor;
        private System.Windows.Forms.Button buttonOpenImage;
        private System.Windows.Forms.Button buttonDownsize;
        //private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonSave;
    }
}
