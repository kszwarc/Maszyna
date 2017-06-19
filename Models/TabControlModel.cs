using System.Windows.Forms;

namespace Maszyna.Models
{
    class TabControlModel
    {
        public static void ChangeTabControlsVisibility(TabPage tabPage, bool visible)
        {
            var controls = tabPage.Controls;
            foreach (Control control in controls)
                control.Visible = visible;
        }
    }
}
