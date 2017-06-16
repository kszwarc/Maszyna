using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Maszyna.Models;
using System.Drawing;
using System.Diagnostics;
using System.Text;

namespace Maszyna.Forms
{
    public partial class MainForm : Window, IObserver<TuringElement<List<String>>>
    {
        private TabPage _simulationTabPage = null;
        private TuringMachine _turingMachine = new TuringMachine();
        private DateTime _executionTimeBeginning;
        private String[] _args;
        private Generating _generateForm = new Generating();
        private Boolean _animationEnabled = true;
        private const byte ReservedColumns = 1;
        private const byte TabPagesWithoutSimulationTab = 1;

        public MainForm(String[] args) : base("Symulator Maszyny Turinga")
        {
            InitializeComponent();
            _args = args;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.S) && toolStripButtonSave.Visible)
                toolStripButtonSave_Click(null, null);
            else if (keyData == (Keys.Control | Keys.L) && toolStripButtonLoad.Visible)
                toolStripButtonLoad_Click(null, null);
            else if (keyData == (Keys.Control | Keys.R) && toolStripButtonReport.Visible)
                toolStripButtonRaport_Click(null, null);
            else if (keyData == (Keys.Control | Keys.N) && toolStripButtonAnimation.Visible)
                toolStripButtonAnimation_Click(null, null);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void OnCompleted() { }

        public void OnError(Exception ex)
        {
            ProgramMessageBox.ShowError(ex.Message);
        }

        public void OnNext(TuringElement<List<String>> receivedElement)
        {
            if (receivedElement.Element == TuringMachineModifiedElements.EntrySymbols)
                _turingMachine.Symbols = receivedElement.Values;
            else if (receivedElement.Element == TuringMachineModifiedElements.FinalStates)
                _turingMachine.FinalStates = receivedElement.Values;
            TriggerConfigurationChanges(null, null);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HideSimulationTabPageAndToolStripsImage();
            UpdateEmptySymbolInformationForGUI(null, null);
            UpdateTable();
            UpdateFirstStateColor();
            comboBoxHead.SelectedIndex = 0;
            numericUpDownExecutionTime.Value = 10000;
            UpdatePictureBoxesBackgroundColor();
            TriggerConfigurationChanges(null, null);
            TakeCareOfArgs();
        }

        private void UpdatePictureBoxesBackgroundColor()
        {
            pictureBoxActualState.BackColor = _turingMachine.ActualStateColor;
            pictureBoxActualSymbol.BackColor = _turingMachine.ActualSymbolColor;
        }

        private void FirstStateChanges(object sender, EventArgs e)
        {
            TriggerConfigurationUpdateWithoutDataGridViewChanges();
            ResetFontForHeaders();
            UpdateFirstStateColor();
        }

        private void UpdateFirstStateColor()
        {
            int columnIndexToChange = ((int)numericUpDownFirstStateNumber.Value) + ReservedColumns;
            if (columnIndexToChange < dataGridViewTable.Columns.Count)
            {
                Font actualFont = dataGridViewTable.ColumnHeadersDefaultCellStyle.Font;
                dataGridViewTable.Columns[columnIndexToChange].HeaderCell.Style.Font = new Font(actualFont, FontStyle.Bold);
            }
        }

        private void ResetFontForHeaders()
        {
            foreach (DataGridViewColumn column in dataGridViewTable.Columns)
                column.HeaderCell.Style.Font = dataGridViewTable.ColumnHeadersDefaultCellStyle.Font;
        }

        private void StateNumbersChanged(object sender, EventArgs e)
        {
            SetMaximumAvailableBeginningStateNumber();
            TriggerConfigurationChanges(sender, e);
            UpdateTableColumns();
            ((DataGridViewWithPaste)dataGridViewTable).AdjustColumnsWidth(dataGridViewTable, e);
        }

        private void UpdateTable()
        {
            UpdateTableColumns();
            UpdateTableRows();
        }

        private void UpdateTableRows()
        {
            dataGridViewTable.Rows.Clear();
            if (_turingMachine.EmptySymbol != ' ')
                AddRowWithSymbol(_turingMachine.EmptySymbol);
            AddAvailableSymbolRows();
            dataGridViewTable.Refresh();
        }

        private void AddAvailableSymbolRows()
        {
            foreach (var symbol in _turingMachine.Symbols)
                AddRowWithSymbol(symbol[0]);
        }

        private void AddRowWithSymbol(char symbol)
        {
            DataGridViewRow row = new DataGridViewRow();
            dataGridViewTable.Rows.Add(row);
            int lastRowIndex = dataGridViewTable.Rows.Count - 1;
            dataGridViewTable.Rows[lastRowIndex].Cells[0].Value = symbol;
        }

        private void UpdateTableColumns()
        {
            int demandedNumberOfColumns = (int)numericUpDownStateNumbers.Value;
            int columnsToRemove = dataGridViewTable.Columns.Count - ReservedColumns;
            RemoveColumns(columnsToRemove);
            AddColumns(demandedNumberOfColumns);
            FirstStateChanges(null, null);
            dataGridViewTable.Refresh();
        }

        private void AddColumns(int columnsToAdd)
        {
            const byte MaxInputLengthForElement = 32;
            for (int i = 0; i < columnsToAdd; i++)
            {
                DataGridViewTextBoxColumn columnToAdd = new DataGridViewTextBoxColumn();
                columnToAdd.HeaderText = "q" + (dataGridViewTable.Columns.Count - 1);
                columnToAdd.MaxInputLength = MaxInputLengthForElement;
                columnToAdd.ReadOnly = !checkBoxManualTable.Checked;
                dataGridViewTable.Columns.Add(columnToAdd);
            }
        }

        private void RemoveColumns(int columnsToRemove)
        {
            for (int i = columnsToRemove; i >= ReservedColumns; i--)
                dataGridViewTable.Columns.RemoveAt(dataGridViewTable.Columns.Count - i);
        }

        private void UpdateEmptySymbolInformationForGUI(object sender, EventArgs e)
        {
            String errorText = textBoxEmptySymbol.Text.Length != 1 ? "Brak symbolu" : null;
            errorProvider.SetError(textBoxEmptySymbol, errorText);
        }

        private void TriggerConfigurationChanges(object sender, EventArgs e)
        {
            TriggerConfigurationUpdateWithoutDataGridViewChanges();
            UpdateTableRows();
        }

        private void TriggerConfigurationUpdateWithoutDataGridViewChanges()
        {
            UpdateTuringMachine();
            UpdateFormulation();
            SetConfigurationStatus();
            UnlockOrLockTabWithSimulation();
        }

        private void UnlockOrLockTabWithSimulation()
        {
            bool isSimulationTabAdded = tabControl.TabPages.Count != TabPagesWithoutSimulationTab;
            bool shouldSimulationTabBeVisible = ConfigModel.ShouldSimulationTabBeVisible(_turingMachine);
            if (shouldSimulationTabBeVisible)
            {
                _turingMachine.GenerateTransitionsFromPotential();
                toolStripButtonAnimation.Visible = true;
                CopyDataGridViewToActualTuringState();
                if (!isSimulationTabAdded)
                    tabControl.TabPages.Add(_simulationTabPage);
            }
            else if (!shouldSimulationTabBeVisible && isSimulationTabAdded)
                HideSimulationTabPageAndToolStripsImage();
        }

        private void UpdateTuringMachine()
        {
            char emptySymbol = textBoxEmptySymbol.Text.Length == 1 ? textBoxEmptySymbol.Text[0] : ' ';
            _turingMachine.EmptySymbol = emptySymbol;
            _turingMachine.NumberOfStates = (int)numericUpDownStateNumbers.Value;
            _turingMachine.FirstStateIndex = (int)numericUpDownFirstStateNumber.Value;
            _turingMachine.Symbols.Remove(_turingMachine.EmptySymbol.ToString());
            HeadConfigurationUpdate(null, null);
        }

        private void UpdateFormulation()
        {
            labelFormalForInput.Text = ConfigModel.GenerateFormalSymbols(_turingMachine);
        }

        private void SetConfigurationStatus()
        {
            toolStripStatusLabelConfigStatus.Text = ConfigModel.GenerateConditionsToShowSimulationTab(_turingMachine);
        }

        private void ShowSimulationTabPage()
        {
            if (_simulationTabPage != null)
                tabControl.TabPages.Insert(1, _simulationTabPage);
        }

        private void HideSimulationTabPageAndToolStripsImage()
        {
            _simulationTabPage = _simulationTabPage ?? tabPageSimulation;
            tabControl.TabPages.Remove(_simulationTabPage);
            toolStripButtonReport.Visible = false;
            toolStripButtonAnimation.Visible = false;
        }

        private void SetMaximumAvailableBeginningStateNumber()
        {
            const byte ShiftForStateNumberIndexing = 1;
            numericUpDownFirstStateNumber.Maximum = numericUpDownStateNumbers.Value - ShiftForStateNumberIndexing;
        }

        private void buttonEntrySymbols_Click(object sender, EventArgs e)
        {
            EntrySymbolForm symbolForm = new EntrySymbolForm(_turingMachine.Symbols);
            symbolForm.Show();
            symbolForm.Subscribe(this);
        }

        private void buttonFinalStates_Click(object sender, EventArgs e)
        {
            FinalStatesForm finalStatesForm = new FinalStatesForm(_turingMachine.FinalStates);
            finalStatesForm.Show();
            finalStatesForm.Subscribe(this);
        }

        private List<PotentialTransition> GeneratePotentialTransitions()
        {
            List<PotentialTransition> potentialTransitions = new List<PotentialTransition>();
            foreach (DataGridViewRow row in dataGridViewTable.Rows)
            {
                for (int i = ReservedColumns; i < dataGridViewTable.Columns.Count; i++)
                {
                    String cellValue = GetCellValue(row.Cells[i]);
                    String entrySymbol = GetCellValue(row.Cells[0]);
                    char entrySymbolToPass = entrySymbol.Length == 1 ? entrySymbol[0] : ' ';
                    int actualStateNumber = i - ReservedColumns;
                    PotentialTransition potentialTransition = new PotentialTransition(cellValue, actualStateNumber, 
                        entrySymbolToPass);
                    potentialTransitions.Add(potentialTransition);
                }
            }
            return potentialTransitions;
        }

        private String GetCellValue(DataGridViewCell cell)
        {
            return cell.Value == null ? "" : cell.Value.ToString();
        }

        private void UpdateStateTable(object sender, DataGridViewCellEventArgs e)
        {
            _turingMachine.PotentialTransitions = GeneratePotentialTransitions();
            SetConfigurationStatus();
            UnlockOrLockTabWithSimulation();
        }

        private void checkBoxManualTable_CheckedChanged(object sender, EventArgs e)
        {
            labelTable.Visible = checkBoxManualTable.Checked;
            EnabledOrDisableCellEditing(checkBoxManualTable.Checked);
            if (checkBoxManualTable.Checked && Application.OpenForms.OfType<NextState>().Any())
                Application.OpenForms.OfType<NextState>().First().Close();
        }

        private void EnabledOrDisableCellEditing(bool enable)
        {
            for (int i = ReservedColumns; i < dataGridViewTable.Columns.Count; i++)
                dataGridViewTable.Columns[i].ReadOnly = !enable;
        }

        private void dataGridViewTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            const byte FirstFreeColumnIndex = ReservedColumns - 1;
            if (!checkBoxManualTable.Checked && e.ColumnIndex > FirstFreeColumnIndex && e.RowIndex >= 0)
                OpenNewNextStateWindow(((DataGridView)sender).Rows[e.RowIndex].Cells[e.ColumnIndex]);
        }

        private void OpenNewNextStateWindow(DataGridViewCell cellToEdit)
        {
            if (Application.OpenForms.OfType<NextState>().Any())
                Application.OpenForms.OfType<NextState>().First().Focus();
            else
            {
                NextState nextState = new NextState(cellToEdit, _turingMachine);
                nextState.Show();
            }
        }

        private void buttonStepNext_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelExecution.Text = "";
            if (ValidateTuringProgram())
            {
                ProgramResult result = _turingMachine.ExecuteStepNext();
                TakeCareOfResults(result);
                toolStripButtonReport.Visible = false;
            }
        }

        private void buttonStepNextWithTape_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelExecution.Text = "";
            if (ValidateTuringProgram())
            {
                ProgramResult result = _turingMachine.ExecuteStepNext(textBoxEnter.Text);
                TakeCareOfResults(result);
                toolStripButtonReport.Visible = false;
            }
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            toolStripStatusLabelExecution.Text = "";
            if (ValidateTuringProgram() && !backgroundWorkerProgram.IsBusy)
            {
                buttonSimulate.Enabled = false;
                buttonStepNextWithTape.Enabled = false;
                buttonStepNext.Enabled = false;
                TakeCareOfGUIWhenSimulationStarted();
                _executionTimeBeginning = DateTime.Now;
                backgroundWorkerProgram.RunWorkerAsync();
                SetIntervalForTimer(); 
                timerForProgram.Start();
            }
        }

        private void TakeCareOfGUIWhenSimulationStarted()
        {
            if (_animationEnabled)
            {
                _generateForm.Show();
                this.Hide();
            }
            else
                this.UseWaitCursor = true;
        }

        private void SetIntervalForTimer()
        {
            if (numericUpDownExecutionTime.Value.ToString().Length == 0)
                numericUpDownExecutionTime.Value = 10000;
            timerForProgram.Interval = (int)numericUpDownExecutionTime.Value;
        }

        private void TakeCareOfResults(ProgramResult result)
        {
            WriteResults(result);
            EnableOrDisableButtonWithStepNext();
        }

        private void WriteResults(ProgramResult result)
        {
            richTextBoxExit.Text = result.Tape;
            textBoxState.Text = result.FinishedStateSymbol;
            RemoveBackgroundColorFromCells();
            ColorActualCell(result);
            ResetColorForSymbols();
            ColorActualSymbol();
        }

        private void ColorActualSymbol()
        {
            richTextBoxExit.Select(_turingMachine.ActualCharIndex,1);
            richTextBoxExit.SelectionColor = _turingMachine.ActualSymbolColor;
        }

        private void ResetColorForSymbols()
        {
            richTextBoxExit.SelectAll();
            richTextBoxExit.SelectionColor = Color.Black;
        }

        private void ColorActualCell(ProgramResult result)
        {
            DataGridViewCell cellToColor = dataGridViewActualTuring.Rows[result.SymbolIndex].
                Cells[result.StateIndex + ReservedColumns];
            cellToColor.Style.BackColor = _turingMachine.ActualStateColor;
        }

        private void RemoveBackgroundColorFromCells()
        {
            for (int i = 0; i < dataGridViewActualTuring.Rows.Count; i++)
                for (int j = 0; j < dataGridViewActualTuring.Columns.Count; j++)
                    dataGridViewActualTuring.Rows[i].Cells[j].Style.BackColor = dataGridViewTable.Rows[0].Cells[0].
                        Style.BackColor;
        }

        private void EnableOrDisableButtonWithStepNext()
        {
            buttonStepNext.Enabled = !_turingMachine.IsActualCharIndexLaterThanTape();
        }

        private bool ValidateTuringProgram()
        {
            if (!Validator.AreEntryDataForMachineValid(textBoxEnter.Text, _turingMachine))
            {
                ProgramMessageBox.ShowError("Dane wejściowe zawierają niedopuszczalne symbole.");
                return false;
            }
            else if (textBoxEnter.Text.Length == 0)
            {
                ProgramMessageBox.ShowError("Taśma jest pusta.");
                return false;
            }
            else
                return true;
        }

        private void backgroundWorkerProgram_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ProgramResult result = _turingMachine.ExecuteProgram(textBoxEnter.Text);
            backgroundWorkerProgram.ReportProgress(100, result);
        }

        private void backgroundWorkerProgram_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            long executionTimeInMiliseconds = (DateTime.Now - _executionTimeBeginning).Milliseconds;
            toolStripStatusLabelExecution.Text = "Czas wykonania programu: " + executionTimeInMiliseconds + " ms.";
            toolStripButtonReport.Visible = true;
            TakeCareOfResults((ProgramResult)e.UserState);
            timerForProgram.Stop();
            buttonSimulate.Enabled = true;
            buttonStepNextWithTape.Enabled = true;
            TakeCareOfGUIWhenSimulationEnded(executionTimeInMiliseconds);
        }

        private void TakeCareOfGUIWhenSimulationEnded(long executionTimeInMiliseconds)
        {
            if (_animationEnabled)
            {
                if (executionTimeInMiliseconds < 1000)
                    timerShowWorking.Start();
                else
                    HideWorkingFormAndShowThisForm();
            }
            else
                this.UseWaitCursor = false;
        }

        private void HideWorkingFormAndShowThisForm()
        {
            _generateForm.Hide();
            this.Show();
        }

        private void timerForProgram_Tick(object sender, EventArgs e)
        {
            _turingMachine.ForceToFinishExecution();
            timerForProgram.Stop();
            ProgramMessageBox.ShowError("Upłynął limit czasu na wykonanie programu.");
            buttonStepNext.Enabled = false;
        }

        private void CopyDataGridViewToActualTuringState()
        {
            dataGridViewActualTuring.Columns.Clear();
            CopyColumnsBetweenDataGridViews();
            CopyRowsBetweenDataGridViews();
            CopyCellsBetweenDataGridViews();
        }

        private void CopyCellsBetweenDataGridViews()
        {
            for (int i=0; i<dataGridViewTable.Rows.Count; i++)
                for (int j = 0; j < dataGridViewTable.Columns.Count; j++)
                    dataGridViewActualTuring.Rows[i].Cells[j].Value = dataGridViewTable.Rows[i].Cells[j].Value;
        }

        private void CopyRowsBetweenDataGridViews()
        {
            foreach (DataGridViewRow row in dataGridViewTable.Rows)
            {
                DataGridViewRow rowToAdd = (DataGridViewRow)row.Clone();
                dataGridViewActualTuring.Rows.Add(rowToAdd);
            }
        }

        private void CopyColumnsBetweenDataGridViews()
        {
            foreach (DataGridViewColumn column in dataGridViewTable.Columns)
            {
                DataGridViewColumn columnToAdd = (DataGridViewColumn)column.Clone();
                columnToAdd.HeaderCell.Style.Font = dataGridViewTable.ColumnHeadersDefaultCellStyle.Font;
                columnToAdd.ReadOnly = true;
                dataGridViewActualTuring.Columns.Add(columnToAdd);
            }
        }

        private void toolStripButtonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialogForConfig.ShowDialog() == DialogResult.OK)
                LoadConfigFromFileFile(openFileDialogForConfig.FileName);
        }

        private void LoadConfigFromFileFile(String filePath)
        {
            TuringMachine newTuringMachine = new TuringMachine();
            if (DataExportQuery.LoadFile(filePath, ref newTuringMachine))
            {
                _turingMachine = newTuringMachine;
                UpdateGUIFromTuringMachine();
                ProgramMessageBox.ShowInfo("Konfiguracja została odczytana.");
            }
            else
                ProgramMessageBox.ShowError("Nie udało się odczytać konfiguracji.");
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialogForConfig.ShowDialog()==DialogResult.OK)
            {
                if (DataExportCommand.SaveFile(saveFileDialogForConfig.FileName,  _turingMachine))
                    ProgramMessageBox.ShowInfo("Konfiguracja została zapisana.");
                else
                    ProgramMessageBox.ShowError("Nie udało się zapisać konfiguracji.");
            }
        }

        private void UpdateGUIFromTuringMachine()
        {
            comboBoxHead.Focus(); /// When focus on entry symbol load is corrupted
            TuringMachine machineToOperate = _turingMachine;
            _turingMachine = new TuringMachine(); /// Events will work on _turingMachine object
            _turingMachine.FinalStates = machineToOperate.FinalStates;
            _turingMachine.Symbols = machineToOperate.Symbols;
            _turingMachine.ActualStateColor = machineToOperate.ActualStateColor;
            _turingMachine.ActualSymbolColor = machineToOperate.ActualSymbolColor;
            textBoxEmptySymbol.Text = new String(machineToOperate.EmptySymbol, 1);
            UpdateEmptySymbolInformationForGUI(null, null);
            numericUpDownStateNumbers.Value = machineToOperate.NumberOfStates;
            numericUpDownFirstStateNumber.Value = machineToOperate.FirstStateIndex;
            comboBoxHead.SelectedIndex = (int)machineToOperate.HeadPosition;
            PopulateDataGridViewFromTuringMachine(machineToOperate);
            pictureBoxActualState.BackColor = _turingMachine.ActualStateColor;
            pictureBoxActualSymbol.BackColor = _turingMachine.ActualSymbolColor;
        }

        private void PopulateDataGridViewFromTuringMachine(TuringMachine machineToOperate)
        {
            int index = 0;
            for (int i = 0; i < dataGridViewTable.Rows.Count; i++)
            {
                for (int j = ReservedColumns; j < dataGridViewTable.Columns.Count; j++)
                {
                    dataGridViewTable.Rows[i].Cells[j].Value = machineToOperate.PotentialTransitions[index].Instruction;
                    index++;
                }
            }
        }

        private void HeadConfigurationUpdate(object sender, EventArgs e)
        {
            _turingMachine.HeadPosition = (String)comboBoxHead.SelectedItem == "Lewa" ?
                TuringHeadPosition.FirstSymbolFromLeft : TuringHeadPosition.FirstSymbolFromRight;
            SetToolTipForHeadConfiguration();
        }

        private void SetToolTipForHeadConfiguration()
        {
            StringBuilder textToShow = new StringBuilder("Pierwszy symbol z ");
            textToShow.Append((String)comboBoxHead.SelectedItem == "Lewa" ? "lewej strony" : "prawej strony");
            toolTipForComboBox.SetToolTip(comboBoxHead, textToShow.ToString());
        }

        private void richTextBoxExit_TextChanged(object sender, EventArgs e)
        {
            richTextBoxExit.SelectAll();
            richTextBoxExit.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void pictureBoxActualState_Click(object sender, EventArgs e)
        {
            _turingMachine.ActualStateColor = GetColorFromUser(_turingMachine.ActualStateColor);
            pictureBoxActualState.BackColor = _turingMachine.ActualStateColor;
        }

        private void pictureBoxActualSymbol_Click(object sender, EventArgs e)
        {
            _turingMachine.ActualSymbolColor = GetColorFromUser(_turingMachine.ActualSymbolColor);
            pictureBoxActualSymbol.BackColor = _turingMachine.ActualSymbolColor;
        }

        private Color GetColorFromUser(Color entryColor)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
                return colorDialog.Color;
            else
                return entryColor;
        }

        private void tabPageConfig_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void tabPageConfig_DragDrop(object sender, DragEventArgs e)
        {
            String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
            LoadConfigFromFileFile(files[0]);
        }

        private void TakeCareOfArgs()
        {
            if (_args.Length > 1)
                LoadConfigFromFileFile(_args[1]);
        }

        private void toolStripButtonRaport_Click(object sender, EventArgs e)
        {
            if (saveFileDialogForReport.ShowDialog()==DialogResult.OK)
            {
                ReportStructure structureForReport = GenerateReportStructure();
                ReportGenerator generator = new ReportGenerator(saveFileDialogForReport.FileName, structureForReport);
                if (generator.GenerateReport())
                    ReactOnValidReportSave(saveFileDialogForReport.FileName);
                else
                    ProgramMessageBox.ShowError("Nie udało się zapisać raportu.");
            }
        }

        private ReportStructure GenerateReportStructure()
        {
            ReportStructure structureForReport = new ReportStructure();
            structureForReport.TuringMachine = _turingMachine;
            structureForReport.ExecutionTimeText = toolStripStatusLabelExecution.Text;
            structureForReport.EntryTape = textBoxEnter.Text;
            structureForReport.ExitTape = richTextBoxExit.Text;
            structureForReport.LastState = textBoxState.Text;
            return structureForReport;
        }

        private void ReactOnValidReportSave(String filePath)
        {
            if (ProgramMessageBox.ShowQuestion("Raport został zapisany. Chcesz go teraz otworzyć?") == DialogResult.Yes)
                Process.Start(filePath);
        }

        private void textBoxEnter_TextChanged(object sender, EventArgs e)
        {
            toolStripButtonReport.Visible = false;
        }

        private void EmptySymbolTextChanged(object sender, EventArgs e)
        {
            UpdateEmptySymbolInformationForGUI(sender, e);
            TriggerConfigurationChanges(sender, e);
        }

        private void timerShowWorking_Tick(object sender, EventArgs e)
        {
            HideWorkingFormAndShowThisForm();
            timerShowWorking.Stop();
        }

        private void toolStripButtonAnimation_Click(object sender, EventArgs e)
        {
            if (_animationEnabled)
            {
                _animationEnabled = false;
                toolStripButtonAnimation.Image = Properties.Resources.animateOff;
                toolStripButtonAnimation.Text = "Włącz animację (ctrl+n)";
            }
            else
            {
                _animationEnabled = true;
                toolStripButtonAnimation.Image = Properties.Resources.animateOn;
                toolStripButtonAnimation.Text = "Wyłącz animację (ctrl+n)";
            }
        }
    }
}
