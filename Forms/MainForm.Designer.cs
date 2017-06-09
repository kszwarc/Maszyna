namespace Maszyna.Forms
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConfig = new System.Windows.Forms.TabPage();
            this.labelTable = new System.Windows.Forms.Label();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.Symbols = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelHead = new System.Windows.Forms.Label();
            this.comboBoxHead = new System.Windows.Forms.ComboBox();
            this.buttonFinalStates = new System.Windows.Forms.Button();
            this.labelFinalStates = new System.Windows.Forms.Label();
            this.buttonEntrySymbols = new System.Windows.Forms.Button();
            this.labelEntrySymbols = new System.Windows.Forms.Label();
            this.labelFormalForInput = new System.Windows.Forms.Label();
            this.labelFormal = new System.Windows.Forms.Label();
            this.statusStripConfig = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelConfigStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.numericUpDownFirstStateNumber = new System.Windows.Forms.NumericUpDown();
            this.labelBeginningState = new System.Windows.Forms.Label();
            this.numericUpDownStateNumbers = new System.Windows.Forms.NumericUpDown();
            this.labelStateNumbers = new System.Windows.Forms.Label();
            this.textBoxEmptySymbol = new System.Windows.Forms.TextBox();
            this.labelEmptySymbol = new System.Windows.Forms.Label();
            this.tabPageSimulation = new System.Windows.Forms.TabPage();
            this.buttonSimulate = new System.Windows.Forms.Button();
            this.labelExit = new System.Windows.Forms.Label();
            this.textBoxExit = new System.Windows.Forms.TextBox();
            this.labelEnter = new System.Windows.Forms.Label();
            this.textBoxEnter = new System.Windows.Forms.TextBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.labelState = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.statusStripConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFirstStateNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStateNumbers)).BeginInit();
            this.tabPageSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConfig);
            this.tabControl.Controls.Add(this.tabPageSimulation);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(584, 454);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConfig
            // 
            this.tabPageConfig.AutoScroll = true;
            this.tabPageConfig.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageConfig.Controls.Add(this.labelTable);
            this.tabPageConfig.Controls.Add(this.dataGridViewTable);
            this.tabPageConfig.Controls.Add(this.labelHead);
            this.tabPageConfig.Controls.Add(this.comboBoxHead);
            this.tabPageConfig.Controls.Add(this.buttonFinalStates);
            this.tabPageConfig.Controls.Add(this.labelFinalStates);
            this.tabPageConfig.Controls.Add(this.buttonEntrySymbols);
            this.tabPageConfig.Controls.Add(this.labelEntrySymbols);
            this.tabPageConfig.Controls.Add(this.labelFormalForInput);
            this.tabPageConfig.Controls.Add(this.labelFormal);
            this.tabPageConfig.Controls.Add(this.statusStripConfig);
            this.tabPageConfig.Controls.Add(this.numericUpDownFirstStateNumber);
            this.tabPageConfig.Controls.Add(this.labelBeginningState);
            this.tabPageConfig.Controls.Add(this.numericUpDownStateNumbers);
            this.tabPageConfig.Controls.Add(this.labelStateNumbers);
            this.tabPageConfig.Controls.Add(this.textBoxEmptySymbol);
            this.tabPageConfig.Controls.Add(this.labelEmptySymbol);
            this.tabPageConfig.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfig.Name = "tabPageConfig";
            this.tabPageConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConfig.Size = new System.Drawing.Size(576, 428);
            this.tabPageConfig.TabIndex = 0;
            this.tabPageConfig.Text = "Konfiguracja maszyny";
            // 
            // labelTable
            // 
            this.labelTable.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTable.AutoSize = true;
            this.labelTable.Location = new System.Drawing.Point(150, 204);
            this.labelTable.Name = "labelTable";
            this.labelTable.Size = new System.Drawing.Size(294, 13);
            this.labelTable.TabIndex = 11116;
            this.labelTable.Text = "Format zapisu w tabeli stanów: stan/symbol/kierunek (L, P, -)";
            this.labelTable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.AllowUserToAddRows = false;
            this.dataGridViewTable.AllowUserToDeleteRows = false;
            this.dataGridViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbols});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTable.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTable.Location = new System.Drawing.Point(3, 226);
            this.dataGridViewTable.Name = "dataGridViewTable";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTable.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridViewTable.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTable.Size = new System.Drawing.Size(570, 174);
            this.dataGridViewTable.TabIndex = 11115;
            this.dataGridViewTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.UpdateStateTable);
            // 
            // Symbols
            // 
            this.Symbols.HeaderText = "Σ\\Q";
            this.Symbols.MaxInputLength = 1;
            this.Symbols.Name = "Symbols";
            this.Symbols.ReadOnly = true;
            // 
            // labelHead
            // 
            this.labelHead.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelHead.AutoSize = true;
            this.labelHead.Location = new System.Drawing.Point(62, 167);
            this.labelHead.Name = "labelHead";
            this.labelHead.Size = new System.Drawing.Size(103, 13);
            this.labelHead.TabIndex = 11114;
            this.labelHead.Text = "Lokalizacja głowicy:";
            // 
            // comboBoxHead
            // 
            this.comboBoxHead.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.comboBoxHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHead.FormattingEnabled = true;
            this.comboBoxHead.Items.AddRange(new object[] {
            "Lewa",
            "Prawa"});
            this.comboBoxHead.Location = new System.Drawing.Point(171, 164);
            this.comboBoxHead.Name = "comboBoxHead";
            this.comboBoxHead.Size = new System.Drawing.Size(71, 21);
            this.comboBoxHead.TabIndex = 6;
            this.comboBoxHead.SelectedIndexChanged += new System.EventHandler(this.TriggerConfigurationChanges);
            // 
            // buttonFinalStates
            // 
            this.buttonFinalStates.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonFinalStates.Location = new System.Drawing.Point(171, 135);
            this.buttonFinalStates.Name = "buttonFinalStates";
            this.buttonFinalStates.Size = new System.Drawing.Size(71, 23);
            this.buttonFinalStates.TabIndex = 5;
            this.buttonFinalStates.Text = "Dodaj";
            this.buttonFinalStates.UseVisualStyleBackColor = true;
            this.buttonFinalStates.Click += new System.EventHandler(this.buttonFinalStates_Click);
            // 
            // labelFinalStates
            // 
            this.labelFinalStates.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelFinalStates.AutoSize = true;
            this.labelFinalStates.Location = new System.Drawing.Point(81, 140);
            this.labelFinalStates.Name = "labelFinalStates";
            this.labelFinalStates.Size = new System.Drawing.Size(84, 13);
            this.labelFinalStates.TabIndex = 11113;
            this.labelFinalStates.Text = "Stany końcowe:";
            // 
            // buttonEntrySymbols
            // 
            this.buttonEntrySymbols.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonEntrySymbols.Location = new System.Drawing.Point(172, 48);
            this.buttonEntrySymbols.Name = "buttonEntrySymbols";
            this.buttonEntrySymbols.Size = new System.Drawing.Size(70, 23);
            this.buttonEntrySymbols.TabIndex = 2;
            this.buttonEntrySymbols.Text = "Dodaj";
            this.buttonEntrySymbols.UseVisualStyleBackColor = true;
            this.buttonEntrySymbols.Click += new System.EventHandler(this.buttonEntrySymbols_Click);
            // 
            // labelEntrySymbols
            // 
            this.labelEntrySymbols.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelEntrySymbols.AutoSize = true;
            this.labelEntrySymbols.Location = new System.Drawing.Point(64, 53);
            this.labelEntrySymbols.Name = "labelEntrySymbols";
            this.labelEntrySymbols.Size = new System.Drawing.Size(102, 13);
            this.labelEntrySymbols.TabIndex = 9;
            this.labelEntrySymbols.Text = "Symbole wejściowe:";
            // 
            // labelFormalForInput
            // 
            this.labelFormalForInput.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelFormalForInput.AutoSize = true;
            this.labelFormalForInput.Location = new System.Drawing.Point(373, 61);
            this.labelFormalForInput.Name = "labelFormalForInput";
            this.labelFormalForInput.Size = new System.Drawing.Size(0, 13);
            this.labelFormalForInput.TabIndex = 8;
            // 
            // labelFormal
            // 
            this.labelFormal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelFormal.AutoSize = true;
            this.labelFormal.Location = new System.Drawing.Point(373, 34);
            this.labelFormal.Name = "labelFormal";
            this.labelFormal.Size = new System.Drawing.Size(98, 13);
            this.labelFormal.TabIndex = 7;
            this.labelFormal.Text = "M=<Q,Σ,Γ,δ,q,B,F>";
            // 
            // statusStripConfig
            // 
            this.statusStripConfig.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelConfigStatus});
            this.statusStripConfig.Location = new System.Drawing.Point(3, 403);
            this.statusStripConfig.Name = "statusStripConfig";
            this.statusStripConfig.Size = new System.Drawing.Size(570, 22);
            this.statusStripConfig.TabIndex = 6;
            // 
            // toolStripStatusLabelConfigStatus
            // 
            this.toolStripStatusLabelConfigStatus.Name = "toolStripStatusLabelConfigStatus";
            this.toolStripStatusLabelConfigStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // numericUpDownFirstStateNumber
            // 
            this.numericUpDownFirstStateNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownFirstStateNumber.Location = new System.Drawing.Point(171, 109);
            this.numericUpDownFirstStateNumber.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownFirstStateNumber.Name = "numericUpDownFirstStateNumber";
            this.numericUpDownFirstStateNumber.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownFirstStateNumber.TabIndex = 4;
            this.numericUpDownFirstStateNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownFirstStateNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFirstStateNumber.ValueChanged += new System.EventHandler(this.FirstStateChanges);
            // 
            // labelBeginningState
            // 
            this.labelBeginningState.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelBeginningState.AutoSize = true;
            this.labelBeginningState.Location = new System.Drawing.Point(73, 111);
            this.labelBeginningState.Name = "labelBeginningState";
            this.labelBeginningState.Size = new System.Drawing.Size(92, 13);
            this.labelBeginningState.TabIndex = 4;
            this.labelBeginningState.Text = "Stan początkowy:";
            // 
            // numericUpDownStateNumbers
            // 
            this.numericUpDownStateNumbers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.numericUpDownStateNumbers.Location = new System.Drawing.Point(171, 77);
            this.numericUpDownStateNumbers.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownStateNumbers.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStateNumbers.Name = "numericUpDownStateNumbers";
            this.numericUpDownStateNumbers.Size = new System.Drawing.Size(71, 20);
            this.numericUpDownStateNumbers.TabIndex = 3;
            this.numericUpDownStateNumbers.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownStateNumbers.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownStateNumbers.ValueChanged += new System.EventHandler(this.StateNumbersChanged);
            // 
            // labelStateNumbers
            // 
            this.labelStateNumbers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelStateNumbers.AutoSize = true;
            this.labelStateNumbers.Location = new System.Drawing.Point(87, 79);
            this.labelStateNumbers.Name = "labelStateNumbers";
            this.labelStateNumbers.Size = new System.Drawing.Size(78, 13);
            this.labelStateNumbers.TabIndex = 2;
            this.labelStateNumbers.Text = "Liczba stanów:";
            // 
            // textBoxEmptySymbol
            // 
            this.textBoxEmptySymbol.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxEmptySymbol.Location = new System.Drawing.Point(172, 22);
            this.textBoxEmptySymbol.MaxLength = 1;
            this.textBoxEmptySymbol.Name = "textBoxEmptySymbol";
            this.textBoxEmptySymbol.Size = new System.Drawing.Size(70, 20);
            this.textBoxEmptySymbol.TabIndex = 1;
            this.textBoxEmptySymbol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxEmptySymbol.TextChanged += new System.EventHandler(this.TriggerConfigurationChanges);
            this.textBoxEmptySymbol.Leave += new System.EventHandler(this.UpdateEmptySymbolInformationForGUI);
            // 
            // labelEmptySymbol
            // 
            this.labelEmptySymbol.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelEmptySymbol.AutoSize = true;
            this.labelEmptySymbol.Location = new System.Drawing.Point(94, 25);
            this.labelEmptySymbol.Name = "labelEmptySymbol";
            this.labelEmptySymbol.Size = new System.Drawing.Size(72, 13);
            this.labelEmptySymbol.TabIndex = 11111;
            this.labelEmptySymbol.Text = "Symbol pusty:";
            // 
            // tabPageSimulation
            // 
            this.tabPageSimulation.AutoScroll = true;
            this.tabPageSimulation.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSimulation.Controls.Add(this.labelState);
            this.tabPageSimulation.Controls.Add(this.textBoxState);
            this.tabPageSimulation.Controls.Add(this.buttonSimulate);
            this.tabPageSimulation.Controls.Add(this.labelExit);
            this.tabPageSimulation.Controls.Add(this.textBoxExit);
            this.tabPageSimulation.Controls.Add(this.labelEnter);
            this.tabPageSimulation.Controls.Add(this.textBoxEnter);
            this.tabPageSimulation.Location = new System.Drawing.Point(4, 22);
            this.tabPageSimulation.Name = "tabPageSimulation";
            this.tabPageSimulation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSimulation.Size = new System.Drawing.Size(576, 428);
            this.tabPageSimulation.TabIndex = 1;
            this.tabPageSimulation.Text = "Symulacja";
            // 
            // buttonSimulate
            // 
            this.buttonSimulate.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonSimulate.Location = new System.Drawing.Point(248, 211);
            this.buttonSimulate.Name = "buttonSimulate";
            this.buttonSimulate.Size = new System.Drawing.Size(75, 23);
            this.buttonSimulate.TabIndex = 4;
            this.buttonSimulate.Text = "Generuj";
            this.buttonSimulate.UseVisualStyleBackColor = true;
            this.buttonSimulate.Click += new System.EventHandler(this.buttonSimulate_Click);
            // 
            // labelExit
            // 
            this.labelExit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelExit.AutoSize = true;
            this.labelExit.Location = new System.Drawing.Point(245, 117);
            this.labelExit.Name = "labelExit";
            this.labelExit.Size = new System.Drawing.Size(84, 13);
            this.labelExit.TabIndex = 3;
            this.labelExit.Text = "Dane wyjściowe";
            // 
            // textBoxExit
            // 
            this.textBoxExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExit.Location = new System.Drawing.Point(8, 94);
            this.textBoxExit.Name = "textBoxExit";
            this.textBoxExit.ReadOnly = true;
            this.textBoxExit.Size = new System.Drawing.Size(560, 20);
            this.textBoxExit.TabIndex = 2;
            this.textBoxExit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelEnter
            // 
            this.labelEnter.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelEnter.AutoSize = true;
            this.labelEnter.Location = new System.Drawing.Point(245, 63);
            this.labelEnter.Name = "labelEnter";
            this.labelEnter.Size = new System.Drawing.Size(85, 13);
            this.labelEnter.TabIndex = 1;
            this.labelEnter.Text = "Dane wejściowe";
            // 
            // textBoxEnter
            // 
            this.textBoxEnter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnter.Location = new System.Drawing.Point(8, 40);
            this.textBoxEnter.Name = "textBoxEnter";
            this.textBoxEnter.Size = new System.Drawing.Size(560, 20);
            this.textBoxEnter.TabIndex = 0;
            this.textBoxEnter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // textBoxState
            // 
            this.textBoxState.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBoxState.Location = new System.Drawing.Point(248, 147);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.ReadOnly = true;
            this.textBoxState.Size = new System.Drawing.Size(75, 20);
            this.textBoxState.TabIndex = 2;
            // 
            // labelState
            // 
            this.labelState.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(249, 170);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(75, 13);
            this.labelState.TabIndex = 5;
            this.labelState.Text = "Stan końcowy";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 454);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageConfig.ResumeLayout(false);
            this.tabPageConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.statusStripConfig.ResumeLayout(false);
            this.statusStripConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFirstStateNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStateNumbers)).EndInit();
            this.tabPageSimulation.ResumeLayout(false);
            this.tabPageSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConfig;
        private System.Windows.Forms.TabPage tabPageSimulation;
        private System.Windows.Forms.NumericUpDown numericUpDownFirstStateNumber;
        private System.Windows.Forms.Label labelBeginningState;
        private System.Windows.Forms.NumericUpDown numericUpDownStateNumbers;
        private System.Windows.Forms.Label labelStateNumbers;
        private System.Windows.Forms.TextBox textBoxEmptySymbol;
        private System.Windows.Forms.Label labelEmptySymbol;
        private System.Windows.Forms.StatusStrip statusStripConfig;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelConfigStatus;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label labelFormalForInput;
        private System.Windows.Forms.Label labelFormal;
        private System.Windows.Forms.Button buttonFinalStates;
        private System.Windows.Forms.Label labelFinalStates;
        private System.Windows.Forms.Button buttonEntrySymbols;
        private System.Windows.Forms.Label labelEntrySymbols;
        private System.Windows.Forms.Label labelHead;
        private System.Windows.Forms.ComboBox comboBoxHead;
        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.Label labelTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Symbols;
        private System.Windows.Forms.Label labelExit;
        private System.Windows.Forms.TextBox textBoxExit;
        private System.Windows.Forms.Label labelEnter;
        private System.Windows.Forms.TextBox textBoxEnter;
        private System.Windows.Forms.Button buttonSimulate;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.TextBox textBoxState;
    }
}

