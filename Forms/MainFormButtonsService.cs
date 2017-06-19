using System;
using Maszyna.Models;

namespace Maszyna.Forms
{
    public partial class MainForm
    {
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
                TakeCareOfGUIWhenSimulationStarted();
                _executionTimeBeginning = DateTime.Now;
                backgroundWorkerProgram.RunWorkerAsync();
                SetIntervalForTimer();
                timerForProgram.Start();
            }
        }
    }
}
