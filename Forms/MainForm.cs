using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Maszyna.Models;
using System.Drawing;

namespace Maszyna.Forms
{
    public partial class MainForm : Window, IObserver<TuringElement<List<String>>>
    {
        private TabPage _simulationTabPage = null;
        private TuringMachine _turingMachine = new TuringMachine();
        private const byte ReservedColumns = 1;
        private const byte TabPagesWithoutSimulationTab = 1;

        public MainForm() : base("Symulator Maszyny Turinga")
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            HideSimulationTabPage();
            UpdateEmptySymbolInformationForGUI(null, null);
            UpdateTable();
            UpdateFirstStateColor();
            comboBoxHead.SelectedIndex = 0;
            numericUpDownExecutionTime.Value = 10000;
        }

        public void OnCompleted() { }

        public void OnError(Exception ex)
        {
            ProgramMessageBox.showError(ex.Message);
        }

        public void OnNext(TuringElement<List<String>> receivedElement)
        {
            if (receivedElement.Element == TuringMachineModifiedElements.EntrySymbols)
                _turingMachine.Symbols = receivedElement.Values;
            else if (receivedElement.Element == TuringMachineModifiedElements.FinalStates)
                _turingMachine.FinalStates = receivedElement.Values;
            TriggerConfigurationChanges(null, null);
        }

        private void FirstStateChanges(object sender, EventArgs e)
        {
            TriggerConfigurationChanges(sender, e);
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
            int actualNumberOfColumns = dataGridViewTable.Columns.Count - ReservedColumns;
            int columnsToManipulate = (int)Math.Abs(demandedNumberOfColumns - actualNumberOfColumns);
            if (actualNumberOfColumns > demandedNumberOfColumns)
                RemoveColumns(columnsToManipulate);
            else
                AddColumns(columnsToManipulate);
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
            for (int i = 1; i <= columnsToRemove; i++)
                dataGridViewTable.Columns.RemoveAt(dataGridViewTable.Columns.Count - i);
        }

        private void UpdateEmptySymbolInformationForGUI(object sender, EventArgs e)
        {
            String errorText = textBoxEmptySymbol.Text.Length != 1 ? "Brak symbolu" : null;
            errorProvider.SetError(textBoxEmptySymbol, errorText);
        }

        private void TriggerConfigurationChanges(object sender, EventArgs e)
        {
            UpdateTuringMachine();
            UpdateFormulation();
            UpdateTableRows();
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
                if (!isSimulationTabAdded)
                    tabControl.TabPages.Add(_simulationTabPage);
            }
            else if (!shouldSimulationTabBeVisible && isSimulationTabAdded)
                HideSimulationTabPage();
        }

        private void UpdateTuringMachine()
        {
            char emptySymbol = textBoxEmptySymbol.Text.Length == 1 ? textBoxEmptySymbol.Text[0] : ' ';
            _turingMachine.EmptySymbol = emptySymbol;
            _turingMachine.NumberOfStates = (int)numericUpDownStateNumbers.Value;
            _turingMachine.FirstStateIndex = (int)numericUpDownFirstStateNumber.Value;
            _turingMachine.HeadPosition = (String)comboBoxHead.SelectedItem == "Lewa" ?
                TuringHeadPosition.FirstSymbolFromLeft : TuringHeadPosition.FirstSymbolFromRight;
            _turingMachine.Symbols.Remove(_turingMachine.EmptySymbol.ToString());
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

        private void HideSimulationTabPage()
        {
            _simulationTabPage = _simulationTabPage ?? tabPageSimulation;
            tabControl.TabPages.Remove(_simulationTabPage);
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
                    PotentialTransition potentialTransition = new PotentialTransition(cellValue, actualStateNumber, entrySymbolToPass);
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
            if (ValidateTuringProgram())
            {
                ProgramResult result = _turingMachine.ExecuteStepNext();
                TakeCareOfResults(result);
            }
        }

        private void buttonStepNextWithTape_Click(object sender, EventArgs e)
        {
            if (ValidateTuringProgram())
            {
                ProgramResult result = _turingMachine.ExecuteStepNext(textBoxEnter.Text);
                TakeCareOfResults(result);
            }
        }

        private void buttonSimulate_Click(object sender, EventArgs e)
        {
            if (ValidateTuringProgram() && !backgroundWorkerProgram.IsBusy)
            {
                backgroundWorkerProgram.RunWorkerAsync();
                SetIntervalForTimer(); 
                timerForProgram.Start();
            }
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
            textBoxExit.Text = result.Tape;
            textBoxState.Text = result.FinishedStateSymbol;
        }

        private void EnableOrDisableButtonWithStepNext()
        {
            buttonStepNext.Enabled = !_turingMachine.IsActualCharIndexLaterThanTape();
        }

        private bool ValidateTuringProgram()
        {
            if (!Validator.AreEntryDataForMachineValid(textBoxEnter.Text, _turingMachine))
            {
                ProgramMessageBox.showError("Dane wejściowe zawierają niedopuszczalne symbole.");
                return false;
            }
            else if (textBoxEnter.Text.Length == 0)
            {
                ProgramMessageBox.showError("Taśma jest pusta.");
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
            TakeCareOfResults((ProgramResult)e.UserState);
            timerForProgram.Stop();
        }

        private void timerForProgram_Tick(object sender, EventArgs e)
        {
            _turingMachine.ForceToFinishExecution();
            timerForProgram.Stop();
            ProgramMessageBox.showError("Upłynął limit czasu na wykonanie programu.");
        }
    }
}
