using System;
using System.Windows.Forms;

namespace Maszyna
{
    public partial class Window : Form
    {
        public Window()
        {
            InitializeComponent();
        }

        public Window(String title)
        {
            InitializeComponent();
            this.Text = title;
        }

        private void Window_KeyPress(object sender, KeyPressEventArgs e)
        {
            const char EscapeChar = (char)27;
            if (e.KeyChar == EscapeChar)
                this.Close();
        }
    }
}
