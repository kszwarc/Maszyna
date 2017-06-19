using System;
using System.Windows.Forms;
using Maszyna.Models;

namespace Maszyna.Forms
{
    public partial class MainForm
    {
        private void tabPageConfig_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            TabControlModel.ChangeTabControlsVisibility(tabPageConfig, false);
            tabPageConfig.BackgroundImage = Properties.Resources.drop;
        }

        private void tabPageConfig_DragLeave(object sender, EventArgs e)
        {
            TabControlModel.ChangeTabControlsVisibility(tabPageConfig, true);
            tabPageConfig.BackgroundImage = null;
            checkBoxManualTable_CheckedChanged(null, null);
        }

        private void tabPageConfig_DragDrop(object sender, DragEventArgs e)
        {
            String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
            LoadConfigFromFileFile(files[0]);
            tabPageConfig_DragLeave(sender, null);
        }
    }
}
