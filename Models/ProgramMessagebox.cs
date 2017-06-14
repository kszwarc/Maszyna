using System;

namespace Maszyna.Models
{
    class ProgramMessageBox
    {
        public static void showError(String text)
        {
            System.Windows.Forms.MessageBox.Show(text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static void showInfo(String text)
        {
            System.Windows.Forms.MessageBox.Show(text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
    }
}
