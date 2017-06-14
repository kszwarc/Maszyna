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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageNormal = new System.Windows.Forms.TabPage();
            this.tabPageFinish = new System.Windows.Forms.TabPage();
            this.labelFinish = new System.Windows.Forms.Label();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.comboBoxFinal = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPageNormal.SuspendLayout();
            this.tabPageFinish.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxState
            // 
            this.comboBoxState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxState.FormattingEnabled = true;
            this.comboBoxState.Location = new System.Drawing.Point(42, 38);
            this.comboBoxState.Name = "comboBoxState";
            this.comboBoxState.Size = new System.Drawing.Size(254, 21);
            this.comboBoxState.TabIndex = 0;
            // 
            // labelState
            // 
            this.labelState.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelState.AutoSize = true;
            this.labelState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelState.Location = new System.Drawing.Point(135, 62);
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
            this.labelSymbol.Location = new System.Drawing.Point(113, 112);
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
            this.comboBoxSymbol.Location = new System.Drawing.Point(42, 88);
            this.comboBoxSymbol.Name = "comboBoxSymbol";
            this.comboBoxSymbol.Size = new System.Drawing.Size(254, 21);
            this.comboBoxSymbol.TabIndex = 2;
            // 
            // labelMovement
            // 
            this.labelMovement.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelMovement.AutoSize = true;
            this.labelMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMovement.Location = new System.Drawing.Point(129, 168);
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
            this.comboBoxMovement.Location = new System.Drawing.Point(42, 144);
            this.comboBoxMovement.Name = "comboBoxMovement";
            this.comboBoxMovement.Size = new System.Drawing.Size(254, 21);
            this.comboBoxMovement.TabIndex = 4;
            // 
            // buttonReady
            // 
            this.buttonReady.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonReady.Location = new System.Drawing.Point(132, 201);
            this.buttonReady.Name = "buttonReady";
            this.buttonReady.Size = new System.Drawing.Size(75, 23);
            this.buttonReady.TabIndex = 6;
            this.buttonReady.Text = "Gotowe";
            this.buttonReady.UseVisualStyleBackColor = true;
            this.buttonReady.Click += new System.EventHandler(this.buttonReady_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageNormal);
            this.tabControl1.Controls.Add(this.tabPageFinish);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(348, 277);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageNormal
            // 
            this.tabPageNormal.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageNormal.Controls.Add(this.labelState);
            this.tabPageNormal.Controls.Add(this.buttonReady);
            this.tabPageNormal.Controls.Add(this.comboBoxState);
            this.tabPageNormal.Controls.Add(this.labelMovement);
            this.tabPageNormal.Controls.Add(this.comboBoxSymbol);
            this.tabPageNormal.Controls.Add(this.comboBoxMovement);
            this.tabPageNormal.Controls.Add(this.labelSymbol);
            this.tabPageNormal.Location = new System.Drawing.Point(4, 22);
            this.tabPageNormal.Name = "tabPageNormal";
            this.tabPageNormal.Size = new System.Drawing.Size(340, 251);
            this.tabPageNormal.TabIndex = 0;
            this.tabPageNormal.Text = "Przejście";
            // 
            // tabPageFinish
            // 
            this.tabPageFinish.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageFinish.Controls.Add(this.labelFinish);
            this.tabPageFinish.Controls.Add(this.buttonFinish);
            this.tabPageFinish.Controls.Add(this.comboBoxFinal);
            this.tabPageFinish.Location = new System.Drawing.Point(4, 22);
            this.tabPageFinish.Name = "tabPageFinish";
            this.tabPageFinish.Size = new System.Drawing.Size(340, 251);
            this.tabPageFinish.TabIndex = 0;
            this.tabPageFinish.Text = "Stan końcowy";
            // 
            // labelFinish
            // 
            this.labelFinish.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelFinish.AutoSize = true;
            this.labelFinish.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFinish.Location = new System.Drawing.Point(131, 104);
            this.labelFinish.Name = "labelFinish";
            this.labelFinish.Size = new System.Drawing.Size(82, 15);
            this.labelFinish.TabIndex = 8;
            this.labelFinish.Text = "Stan końcowy";
            // 
            // buttonFinish
            // 
            this.buttonFinish.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonFinish.Location = new System.Drawing.Point(133, 143);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(75, 23);
            this.buttonFinish.TabIndex = 9;
            this.buttonFinish.Text = "Gotowe";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.buttonFinish_Click);
            // 
            // comboBoxFinal
            // 
            this.comboBoxFinal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFinal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFinal.FormattingEnabled = true;
            this.comboBoxFinal.Location = new System.Drawing.Point(43, 80);
            this.comboBoxFinal.Name = "comboBoxFinal";
            this.comboBoxFinal.Size = new System.Drawing.Size(254, 21);
            this.comboBoxFinal.TabIndex = 7;
            // 
            // NextState
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 277);
            this.Controls.Add(this.tabControl1);
            this.Name = "NextState";
            this.Text = "NextState";
            this.tabControl1.ResumeLayout(false);
            this.tabPageNormal.ResumeLayout(false);
            this.tabPageNormal.PerformLayout();
            this.tabPageFinish.ResumeLayout(false);
            this.tabPageFinish.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelSymbol;
        private System.Windows.Forms.ComboBox comboBoxSymbol;
        private System.Windows.Forms.Label labelMovement;
        private System.Windows.Forms.ComboBox comboBoxMovement;
        private System.Windows.Forms.Button buttonReady;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageNormal;
        private System.Windows.Forms.TabPage tabPageFinish;
        private System.Windows.Forms.Label labelFinish;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.ComboBox comboBoxFinal;
    }
}