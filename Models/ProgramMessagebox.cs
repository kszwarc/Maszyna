using System;
using System.Windows.Forms;

namespace Maszyna.Models
{
    class ProgramMessageBox
    {
        private static String ProgramName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public static void ShowError(String text)
        {
            MessageBox.Show(text, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(String text)
        {
            MessageBox.Show(text, ProgramName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowQuestion(String text)
        {
            return MessageBox.Show(text, ProgramName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
