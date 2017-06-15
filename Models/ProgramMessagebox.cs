using System;
using System.Windows.Forms;

namespace Maszyna.Models
{
    class ProgramMessageBox
    {
        public static void ShowError(String text)
        {
            MessageBox.Show(text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowInfo(String text)
        {
            MessageBox.Show(text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult ShowQuestion(String text)
        {
            return MessageBox.Show(text, System.Reflection.Assembly.GetExecutingAssembly().GetName().Name,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
