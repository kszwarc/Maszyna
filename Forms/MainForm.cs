using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using Maszyna.Models;

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
        private Label[] _turingMachineGraphicEmptySymbolLabels;
        private Label[] _turingMachineGraphicNormalSymbolLabels;
        private const byte ReservedColumns = 1;
        private const byte TabPagesWithoutSimulationTab = 1;

        public MainForm(String[] args) : base("Symulator Maszyny Turinga")
        {
            InitializeComponent();
            _turingMachineGraphicEmptySymbolLabels = new Label[]{ labelTuringMachineEmptySymbol1,
                labelTuringMachineEmptySymbol2 };
            _turingMachineGraphicNormalSymbolLabels = new Label[] {labelTuringMachineNormalSymbol1,
                labelTuringMachineNormalSymbol2, labelTuringMachineNormalSymbol3 };
            _args = args;
        }

        public void OnCompleted() { }

        public void OnError(Exception ex)
        {
            ProgramMessageBox.ShowError(ex.Message);
        }

        public void OnNext(TuringElement<List<String>> receivedElement)
        {
            if (receivedElement.Element == TuringMachineModifiedElements.EntrySymbols)
            {
                _turingMachine.Symbols = receivedElement.Values;
                TriggerConfigurationChanges(null, null);
            }
            else if (receivedElement.Element == TuringMachineModifiedElements.FinalStates)
            {
                _turingMachine.FinalStates = receivedElement.Values;
                TriggerConfigurationUpdateWithoutDataGridViewChanges();
                UpdateStateTable(null, null);
            }
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
            backgroundWorkerUpdater.RunWorkerAsync();
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

        private void StateNumbersChanged(object sender, EventArgs e)
        {
            SetMaximumAvailableBeginningStateNumber();
            UpdateTableColumns();
            TriggerConfigurationUpdateWithoutDataGridViewChanges();
            ((DataGridViewWithPaste)dataGridViewTable).AdjustColumnsWidth(dataGridViewTable, e);
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
            SetNormalSymbolLabels();
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
                ShowToolStripConnectedToSimulation();
                CopyDataGridViewToActualTuringState();
                ResetControlsForSimulation();
                if (!isSimulationTabAdded)
                    tabControl.TabPages.Add(_simulationTabPage);
            }
            else if (!shouldSimulationTabBeVisible && isSimulationTabAdded)
                HideSimulationTabPageAndToolStripsImage();
        }

        private void ResetControlsForSimulation()
        {
            textBoxEnter.Text = "";
            richTextBoxExit.Text = "";
            textBoxState.Text = "";
            buttonStepNext.Enabled = false;
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

        private void ShowSimulationTabPage()
        {
            if (_simulationTabPage != null)
                tabControl.TabPages.Insert(1, _simulationTabPage);
        }

        private void SetMaximumAvailableBeginningStateNumber()
        {
            const byte ShiftForStateNumberIndexing = 1;
            numericUpDownFirstStateNumber.Maximum = numericUpDownStateNumbers.Value - ShiftForStateNumberIndexing;
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

        private void TakeCareOfGUIWhenSimulationStarted()
        {
            buttonSimulate.Enabled = false;
            buttonStepNextWithTape.Enabled = false;
            buttonStepNext.Enabled = false;
            textBoxEnter.Enabled = false;
            this.UseWaitCursor = true;
            if (_animationEnabled)
            {
                _generateForm.Show();
                this.Hide();
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
            long executionTimeInMiliseconds = (long)(DateTime.Now - _executionTimeBeginning).TotalMilliseconds;
            TakeCareOfGUIWhenSimulationEnded(executionTimeInMiliseconds);
            toolStripStatusLabelExecution.Text = "Czas wykonania programu: " + executionTimeInMiliseconds + " ms.";
            toolStripButtonReport.Visible = true;
            TakeCareOfResults((ProgramResult)e.UserState);
            timerForProgram.Stop();
            buttonSimulate.Enabled = true;
            buttonStepNextWithTape.Enabled = true;
            textBoxEnter.Enabled = true;
        }

        private void TakeCareOfGUIWhenSimulationEnded(long executionTimeInMiliseconds)
        {
            if (_animationEnabled)
                TakeCareOfGUIWhenSimulationEndedForAnimation(executionTimeInMiliseconds);
            this.UseWaitCursor = false;
            dataGridViewActualTuring.Cursor = this.Cursor;
        }

        private void TakeCareOfGUIWhenSimulationEndedForAnimation(long executionTimeInMiliseconds)
        {
            if (executionTimeInMiliseconds < 1000)
                timerShowWorking.Start();
            else
                HideWorkingFormAndShowThisForm();
        }

        private void HideWorkingFormAndShowThisForm()
        {
            _generateForm.Hide();
            this.Show();
            this.BringToFront();
        }

        private void timerForProgram_Tick(object sender, EventArgs e)
        {
            _turingMachine.ForceToFinishExecution();
            timerForProgram.Stop();
            ProgramMessageBox.ShowError("Upłynął limit czasu na wykonanie programu.");
            buttonStepNext.Enabled = false;
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

        private void UpdateGUIFromTuringMachine()
        {
            comboBoxHead.Focus(); /// When focus on entry symbol load is corrupted
            TuringMachine machineToOperate = _turingMachine;
            _turingMachine = new TuringMachine(); /// Events will work on _turingMachine object
            _turingMachine.FinalStates = machineToOperate.FinalStates;
            _turingMachine.Symbols = machineToOperate.Symbols;
            _turingMachine.ActualStateColor = machineToOperate.ActualStateColor;
            _turingMachine.ActualSymbolColor = machineToOperate.ActualSymbolColor;
            _turingMachine.EmptySymbol = machineToOperate.EmptySymbol;
            _turingMachine.NumberOfStates = machineToOperate.NumberOfStates;
            _turingMachine.FirstStateIndex = machineToOperate.FirstStateIndex;
            _turingMachine.HeadPosition = machineToOperate.HeadPosition;
            _turingMachine.PotentialTransitions = machineToOperate.PotentialTransitions;
            textBoxEmptySymbol.Text = new String(machineToOperate.EmptySymbol, 1);
            UpdateEmptySymbolInformationForGUI(null, null);
            numericUpDownStateNumbers.Value = machineToOperate.NumberOfStates;
            numericUpDownFirstStateNumber.Value = machineToOperate.FirstStateIndex;
            comboBoxHead.SelectedIndex = (int)machineToOperate.HeadPosition;
            TriggerConfigurationChanges(null, null);
            PopulateDataGridViewFromTuringMachine(machineToOperate);
            pictureBoxActualState.BackColor = _turingMachine.ActualStateColor;
            pictureBoxActualSymbol.BackColor = _turingMachine.ActualSymbolColor;
            ResetControlsForSimulation();
            _turingMachine.PotentialTransitions = GeneratePotentialTransitions();
            _turingMachine.GenerateTransitionsFromPotential();
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
            bool isLeftPosition = (String)comboBoxHead.SelectedItem == "Lewa";
            if (isLeftPosition)
            {
                _turingMachine.HeadPosition = TuringHeadPosition.FirstSymbolFromLeft;
                pictureBoxTuringMachineHead.Image = Properties.Resources.leftHead;
            }
            else
            {
                _turingMachine.HeadPosition = TuringHeadPosition.FirstSymbolFromRight;
                pictureBoxTuringMachineHead.Image = Properties.Resources.rightHead;
            }
            SetToolTipForHeadConfiguration();
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
            colorDialog.Color = entryColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
                return colorDialog.Color;
            else
                return entryColor;
        }

        private void TakeCareOfArgs()
        {
            if (_args.Length > 1)
                LoadConfigFromFileFile(_args[1]);
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
            if (textBoxEnter.Text.Length==0)
            {
                buttonStepNextWithTape.Enabled = false;
                buttonStepNext.Enabled = false;
                buttonSimulate.Enabled = false;
            }
            else
            {
                buttonStepNextWithTape.Enabled = true;
                buttonSimulate.Enabled = true;
            }
        }

        private void EmptySymbolTextChanged(object sender, EventArgs e)
        {
            UpdateEmptySymbolInformationForGUI(sender, e);
            SetEmptySymbolLabels();
            TriggerConfigurationChanges(sender, e);
        }

        private void SetEmptySymbolLabels()
        {
            foreach (Label label in _turingMachineGraphicEmptySymbolLabels)
                label.Text = textBoxEmptySymbol.Text;
        }

        private void SetNormalSymbolLabels()
        {
            String symbolToWrite = "";
            if (_turingMachine.Symbols.Count > 0)
                symbolToWrite = _turingMachine.Symbols[0];
            foreach (Label label in _turingMachineGraphicNormalSymbolLabels)
                label.Text = symbolToWrite;
        }

        private void timerShowWorking_Tick(object sender, EventArgs e)
        {
            HideWorkingFormAndShowThisForm();
            timerShowWorking.Stop();
        }

        private void textBoxEnter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Validator.AreEntryDataForMachineValid(e.KeyChar, _turingMachine))
                e.Handled = true;
        }

        private void ClearSelectionOfDataGridViewActualTuring(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dataGridViewActualTuring.ClearSelection();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _generateForm.Close();
        }

        private void backgroundWorkerUpdater_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            UpdateManagerFacade.DownloadNewVersionIfNeeded();
        }
    }
}
