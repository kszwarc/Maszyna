using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Maszyna.Models
{
    class UpdateManager
    {
        private static WebClient _client = new WebClient();
        private static String _versionURL = "http://www.szwarc.net.pl/app/TuringWersja.txt";
        private static String _messageURL = "http://www.szwarc.net.pl/app/TuringWiadomosc.txt";
        private static String _appURL = "http://www.szwarc.net.pl/app/Maszyna.exe";

        public UpdateManager()
        {
            _client.Proxy = null;
        }

        public bool CheckIfProgramIsActual()
        {
            return ReadTextFromWebSite(_versionURL) == Application.ProductVersion;
        }

        public DialogResult AskUserForDecisionWithMessageFromWebPage()
        {
            DialogResult question = DialogResult.No;
            String message = ReadTextFromWebSite(_messageURL);
            SynchronizationContext context = SynchronizationContext.Current ?? new SynchronizationContext();
            context.Send(s =>
            {
                question = ProgramMessageBox.ShowQuestion(message);
            }, null);
            return question;
        }

        public bool DownloadNewVersion()
        {
            try
            {
                String[] arguments = new String[2];
                arguments[0] = String.Format("\"{0}\"", Application.ExecutablePath);
                arguments[1] = _appURL;
                System.Diagnostics.Process.Start(Path.Combine(Application.StartupPath, "Updater.exe"), 
                    String.Join(" ", arguments));
                Application.Exit();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private String ReadTextFromWebSite(String URL)
        {
            String text = "";
            try
            {
                Stream stream = _client.OpenRead(URL);
                StreamReader reader = new StreamReader(stream);
                text = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception) { }
            return text;
        }
    }
}
