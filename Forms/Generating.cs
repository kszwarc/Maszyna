using System;

namespace Maszyna.Forms
{
    public partial class Generating : Window
    {
        private System.Media.SoundPlayer _sound;
        public Boolean PlayMusic { get; set; } = false;

        public Generating() : base("Generowanie rozwiązania")
        {
            InitializeComponent();
            System.IO.Stream stream = Properties.Resources.Sound;
            _sound = new System.Media.SoundPlayer(stream);
        }

        private void Generating_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && PlayMusic)
                _sound.PlayLooping();
            else if (PlayMusic)
                _sound.Stop();
        }

        private void Generating_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (PlayMusic)
                _sound.Stop();
            e.Cancel = true;
            this.Hide();
        }
    }
}
