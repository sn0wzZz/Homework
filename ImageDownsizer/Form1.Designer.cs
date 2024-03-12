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
            this.pictureBoxSequential = new System.Windows.Forms.PictureBox();
            this.labelParallelTime = new System.Windows.Forms.Label(); // Add this line
            this.labelSequentialTime = new System.Windows.Forms.Label(); // Add this line
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownsized)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSequential)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxOriginal
            // 
            this.pictureBoxOriginal.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxOriginal.Location = new System.Drawing.Point(23, 24);
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
            this.labelFactor.Location = new System.Drawing.Point(301, 520);
            this.labelFactor.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelFactor.Name = "labelFactor";
            this.labelFactor.Size = new System.Drawing.Size(124, 32);
            this.labelFactor.TabIndex = 2;
            this.labelFactor.Text = "Factor (%):";
            this.labelFactor.Click += new System.EventHandler(this.labelFactor_Click);
            // 
            // textBoxFactor
            // 
            this.textBoxFactor.Location = new System.Drawing.Point(431, 518);
            this.textBoxFactor.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.textBoxFactor.Name = "textBoxFactor";
            this.textBoxFactor.Size = new System.Drawing.Size(95, 39);
            this.textBoxFactor.TabIndex = 3;
            this.textBoxFactor.TextChanged += new System.EventHandler(this.textBoxFactor_TextChanged);
            // 
            // buttonOpenImage
            // 
            this.buttonOpenImage.Location = new System.Drawing.Point(536, 509);
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
            this.buttonDownsize.Location = new System.Drawing.Point(708, 509);
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
            this.progressBar.Location = new System.Drawing.Point(883, 509);
            this.progressBar.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(402, 56);
            this.progressBar.TabIndex = 7;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Enabled = false;
            this.buttonSave.Location = new System.Drawing.Point(1295, 509);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(162, 56);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.DownloadImage_Click);
            // 
            // pictureBoxSequential
            // 
            this.pictureBoxSequential.BackColor = System.Drawing.Color.LightGray;
            this.pictureBoxSequential.Location = new System.Drawing.Point(1186, 24);
            this.pictureBoxSequential.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.pictureBoxSequential.Name = "pictureBoxSequential";
            this.pictureBoxSequential.Size = new System.Drawing.Size(577, 398);
            this.pictureBoxSequential.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxSequential.TabIndex = 9;
            this.pictureBoxSequential.TabStop = false;
            // 
            // labelParallelTime
            // 
            this.labelParallelTime.AutoSize = true;
            this.labelParallelTime.ForeColor = System.Drawing.Color.LightGray;
            this.labelParallelTime.Location = new System.Drawing.Point(602, 429);
            this.labelParallelTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelParallelTime.Name = "labelParallelTime";
            this.labelParallelTime.Size = new System.Drawing.Size(0, 32);
            this.labelParallelTime.TabIndex = 10;
            // 
            // labelSequentialTime
            // 
            this.labelSequentialTime.AutoSize = true;
            this.labelSequentialTime.ForeColor = System.Drawing.Color.LightGray;
            this.labelSequentialTime.Location = new System.Drawing.Point(1186, 429);
            this.labelSequentialTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelSequentialTime.Name = "labelSequentialTime";
            this.labelSequentialTime.Size = new System.Drawing.Size(0, 32);
            this.labelSequentialTime.TabIndex = 11;

            // 
            // labelParallelTime
            // 
            this.labelParallelTime.AutoSize = true;
            this.labelParallelTime.ForeColor = System.Drawing.Color.LightGray;
            this.labelParallelTime.Location = new System.Drawing.Point(602, 429);
            this.labelParallelTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelParallelTime.Name = "labelParallelTime";
            this.labelParallelTime.Size = new System.Drawing.Size(0, 32);
            this.labelParallelTime.TabIndex = 10;
            // 
            // labelSequentialTime
            // 
            this.labelSequentialTime.AutoSize = true;
            this.labelSequentialTime.ForeColor = System.Drawing.Color.LightGray;
            this.labelSequentialTime.Location = new System.Drawing.Point(1186, 429);
            this.labelSequentialTime.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labelSequentialTime.Name = "labelSequentialTime";
            this.labelSequentialTime.Size = new System.Drawing.Size(0, 32);
            this.labelSequentialTime.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1787, 586);
            this.Controls.Add(this.labelSequentialTime); // Add this line
            this.Controls.Add(this.labelParallelTime); // Add this line
            this.Controls.Add(this.pictureBoxSequential);
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
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownsized)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSequential)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.PictureBox pictureBoxSequential;
        private System.Windows.Forms.PictureBox pictureBoxOriginal;
        private System.Windows.Forms.PictureBox pictureBoxDownsized;
        private System.Windows.Forms.Label labelFactor;
        private System.Windows.Forms.Label labelParallelTime;
        private System.Windows.Forms.Label labelSequentialTime;
        private System.Windows.Forms.TextBox textBoxFactor;
        private System.Windows.Forms.Button buttonOpenImage;
        private System.Windows.Forms.Button buttonDownsize;
        //private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button buttonSave;
    }
}
