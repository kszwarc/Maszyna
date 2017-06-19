using System;
using System.Windows.Forms;
using Maszyna.Models;
using System.Text;

namespace Maszyna.Forms
{
    public partial class MainForm
    {
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
            else if (keyData == (Keys.Control | Keys.M) && toolStripButtonMusic.Visible)
                toolStripButtonMusic_Click(null, null);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ShowToolStripConnectedToSimulation()
        {
            toolStripButtonAnimation.Visible = true;
            toolStripButtonMusic.Visible = true;
        }

        private void SetConfigurationStatus()
        {
            toolStripStatusLabelConfigStatus.Text = ConfigModel.GenerateConditionsToShowSimulationTab(_turingMachine);
        }

        private void HideSimulationTabPageAndToolStripsImage()
        {
            _simulationTabPage = _simulationTabPage ?? tabPageSimulation;
            tabControl.TabPages.Remove(_simulationTabPage);
            HideToolStripsConnetedToSimulateTab();
        }

        private void HideToolStripsConnetedToSimulateTab()
        {
            toolStripButtonReport.Visible = false;
            toolStripButtonAnimation.Visible = false;
            toolStripButtonMusic.Visible = false;
        }

        private void toolStripButtonLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialogForConfig.ShowDialog() == DialogResult.OK)
                LoadConfigFromFileFile(openFileDialogForConfig.FileName);
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialogForConfig.ShowDialog() == DialogResult.OK)
            {
                if (DataExportCommand.SaveFile(saveFileDialogForConfig.FileName, _turingMachine))
                    ProgramMessageBox.ShowInfo("Konfiguracja została zapisana.");
                else
                    ProgramMessageBox.ShowError("Nie udało się zapisać konfiguracji.");
            }
        }

        private void toolStripButtonRaport_Click(object sender, EventArgs e)
        {
            if (saveFileDialogForReport.ShowDialog() == DialogResult.OK)
            {
                ReportStructure structureForReport = GenerateReportStructure();
                ReportGenerator generator = new ReportGenerator(saveFileDialogForReport.FileName, structureForReport);
                if (generator.GenerateReport())
                    ReactOnValidReportSave(saveFileDialogForReport.FileName);
                else
                    ProgramMessageBox.ShowError("Nie udało się zapisać raportu.");
            }
        }

        private void toolStripButtonAnimation_Click(object sender, EventArgs e)
        {
            if (_animationEnabled)
            {
                toolStripButtonAnimation.Image = Properties.Resources.animateOff;
                toolStripButtonAnimation.Text = "Włącz animację (ctrl+n)";
            }
            else
            {
                toolStripButtonAnimation.Image = Properties.Resources.animateOn;
                toolStripButtonAnimation.Text = "Wyłącz animację (ctrl+n)";
            }
            _animationEnabled = !_animationEnabled;
            toolStripButtonMusic.Visible = _animationEnabled;
        }

        private void toolStripButtonMusic_Click(object sender, EventArgs e)
        {
            if (_generateForm.PlayMusic)
            {
                toolStripButtonMusic.Text = "Włącz dźwięk (ctrl+m)";
                toolStripButtonMusic.Image = Properties.Resources.mute;
            }
            else
            {
                toolStripButtonMusic.Text = "Wyłącz dźwięk (ctrl+m)";
                toolStripButtonMusic.Image = Properties.Resources.soundIcon;
            }
            _generateForm.PlayMusic = !_generateForm.PlayMusic;
        }

        private void SetToolTipForHeadConfiguration()
        {
            StringBuilder textToShow = new StringBuilder("Pierwszy symbol z ");
            textToShow.Append((String)comboBoxHead.SelectedItem == "Lewa" ? "lewej strony" : "prawej strony");
            toolTipForComboBox.SetToolTip(comboBoxHead, textToShow.ToString());
        }

    }
}
