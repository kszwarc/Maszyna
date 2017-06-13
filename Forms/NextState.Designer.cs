namespace Maszyna.Forms
{
    partial class NextState
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
            this.comboBoxState = new System.Windows.Forms.ComboBox();
            this.labelState = new System.Windows.Forms.Label();
            this.labelSymbol = new System.Windows.Forms.Label();
            this.comboBoxSymbol = new System.Windows.Forms.ComboBox();
            this.labelMovement = new System.Windows.Forms.Label();
            this.comboBoxMovement = new System.Windows.Forms.ComboBox();
            this.buttonReady = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxState
            // 
            this.comboBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxState.FormattingEnabled = true;
            this.comboBoxState.Location = new System.Drawing.Point(10, 39);
            this.comboBoxState.Name = "comboBoxState";
            this.comboBoxState.Size = new System.Drawing.Size(265, 21);
            this.comboBoxState.TabIndex = 0;
            // 
            // labelState
            // 
            this.labelState.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelState.AutoSize = true;
            this.labelState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelState.Location = new System.Drawing.Point(106, 63);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(73, 15);
            this.labelState.TabIndex = 1;
            this.labelState.Text = "Stan kolejny";
            // 
            // labelSymbol
            // 
            this.labelSymbol.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSymbol.AutoSize = true;
            this.labelSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSymbol.Location = new System.Drawing.Point(82, 119);
            this.labelSymbol.Name = "labelSymbol";
            this.labelSymbol.Size = new System.Drawing.Size(117, 15);
            this.labelSymbol.TabIndex = 3;
            this.labelSymbol.Text = "Symbol do wpisania";
            // 
            // comboBoxSymbol
            // 
            this.comboBoxSymbol.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSymbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSymbol.FormattingEnabled = true;
            this.comboBoxSymbol.Location = new System.Drawing.Point(10, 95);
            this.comboBoxSymbol.Name = "comboBoxSymbol";
            this.comboBoxSymbol.Size = new System.Drawing.Size(265, 21);
            this.comboBoxSymbol.TabIndex = 2;
            // 
            // labelMovement
            // 
            this.labelMovement.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMovement.AutoSize = true;
            this.labelMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMovement.Location = new System.Drawing.Point(105, 175);
            this.labelMovement.Name = "labelMovement";
            this.labelMovement.Size = new System.Drawing.Size(79, 15);
            this.labelMovement.TabIndex = 5;
            this.labelMovement.Text = "Ruch głowicy";
            // 
            // comboBoxMovement
            // 
            this.comboBoxMovement.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMovement.FormattingEnabled = true;
            this.comboBoxMovement.Items.AddRange(new object[] {
            "P",
            "L",
            "-"});
            this.comboBoxMovement.Location = new System.Drawing.Point(10, 151);
            this.comboBoxMovement.Name = "comboBoxMovement";
            this.comboBoxMovement.Size = new System.Drawing.Size(265, 21);
            this.comboBoxMovement.TabIndex = 4;
            // 
            // buttonReady
            // 
            this.buttonReady.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonReady.Location = new System.Drawing.Point(106, 212);
            this.buttonReady.Name = "buttonReady";
            this.buttonReady.Size = new System.Drawing.Size(75, 23);
            this.buttonReady.TabIndex = 6;
            this.buttonReady.Text = "Gotowe";
            this.buttonReady.UseVisualStyleBackColor = true;
            this.buttonReady.Click += new System.EventHandler(this.buttonReady_Click);
            // 
            // NextState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.buttonReady);
            this.Controls.Add(this.labelMovement);
            this.Controls.Add(this.comboBoxMovement);
            this.Controls.Add(this.labelSymbol);
            this.Controls.Add(this.comboBoxSymbol);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.comboBoxState);
            this.Name = "NextState";
            this.Text = "NextState";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelSymbol;
        private System.Windows.Forms.ComboBox comboBoxSymbol;
        private System.Windows.Forms.Label labelMovement;
        private System.Windows.Forms.ComboBox comboBoxMovement;
        private System.Windows.Forms.Button buttonReady;
    }
}