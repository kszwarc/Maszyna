using System;
using System.Windows.Forms;
using Maszyna.Models;

namespace Maszyna.Forms
{
    public partial class MainForm : Window, IObserver<TuringElement<String[]>>
    {
        private TabPage _simulationTabPage = null;
        private TuringMachine _turingMachine = new TuringMachine();

        public MainForm() : base("Symulator Maszyny Turinga")
        {
            InitializeComponent();
            HideSimulationTabPage();
            SetMaximumAvailableBeginningStateNumber();
            SetConfigurationStatus();
            TriggerConfigurationChanges(null, null);
            UpdateEmptySymbolInformationForGUI(null, null);
            UpdateTable();
            comboBoxHead.SelectedIndex = 0;
        }

        public void OnCompleted() { }

        public void OnError(Exception ex)
        {
            ProgramMessageBox.showError(ex.Message);
        }

        public void OnNext(TuringElement<String[]> receivedElement)
        {
            if (receivedElement.Element == TuringMachineModifiedElements.EntrySymbols)
                _turingMachine.Symbols = receivedElement.Values;
            else if (receivedElement.Element == TuringMachineModifiedElements.FinalStates)
                _turingMachine.FinalStates = receivedElement.Values;
            TriggerConfigurationChanges(null, null);
        }

        private void numericUpDownStateNumbers_ValueChanged(object sender, EventArgs e)
        {
            SetMaximumAvailableBeginningStateNumber();
            TriggerConfigurationChanges(sender, e);
            UpdateTableColumns();
        }

        private void UpdateTable()
        {
            UpdateTableColumns();
        }

        private void UpdateTableColumns()
        {
            const byte reservedColumns = 1;
            int demandedNumberOfColumns = (int)numericUpDownStateNumbers.Value;
            int actualNumberOfColumns = dataGridViewTable.Columns.Count - reservedColumns;
            int columnsToManipulate = (int)Math.Abs(demandedNumberOfColumns - actualNumberOfColumns);
            if (actualNumberOfColumns > demandedNumberOfColumns)
                RemoveColumns(columnsToManipulate);
            else
                AddColumns(columnsToManipulate);
        }

        private void AddColumns(int columnsToAdd)
        {
            for (int i = 0; i < columnsToAdd; i++)
            {
                DataGridViewColumn columnToAdd = new DataGridViewColumn();
                columnToAdd.HeaderText = "Q" + dataGridViewTable.Columns.Count;
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
            SetConfigurationStatus();
        }

        private void UpdateTuringMachine()
        {
            char emptySymbol = textBoxEmptySymbol.Text.Length == 1 ? textBoxEmptySymbol.Text[0] : ' ';
            _turingMachine.EmptySymbol = emptySymbol;
            _turingMachine.NumberOfStates = (int)numericUpDownStateNumbers.Value;
            _turingMachine.FirstStateIndex = (int)numericUpDownFirstStateNumber.Value;
            _turingMachine.HeadPosition = comboBoxHead.SelectedText == "Lewa" ? 
                TuringHeadPosition.FirstSymbolFromLeft : TuringHeadPosition.FirstSymbolFromRight;
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
    }
}
