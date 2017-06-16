namespace Maszyna.Forms
{
    partial class Generating
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Generating));
            this.pictureBoxTuring = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTuring)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxTuring
            // 
            this.pictureBoxTuring.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTuring.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxTuring.Image")));
            this.pictureBoxTuring.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTuring.Name = "pictureBoxTuring";
            this.pictureBoxTuring.Size = new System.Drawing.Size(321, 181);
            this.pictureBoxTuring.TabIndex = 0;
            this.pictureBoxTuring.TabStop = false;
            // 
            // Generating
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 181);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBoxTuring);
            this.Name = "Generating";
            this.Text = "Generowanie rozwiązania. Proszę czekać...";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTuring)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxTuring;
    }
}