using System.Windows.Forms;

namespace Maszyna.Models
{
    class UpdateManagerFacade
    {
        private static UpdateManager _manager = new UpdateManager();

        public static void DownloadNewVersionIfNeeded()
        {
            if (!_manager.CheckIfProgramIsActual())
                if (_manager.AskUserForDecisionWithMessageFromWebPage() == DialogResult.Yes)
                    if (!_manager.DownloadNewVersion())
                        ProgramMessageBox.ShowError("Nie udało się uruchomić updatera. Sprawdź czy w katalogu "+
                            "z programem znajduje się plik 'Updater.exe'.");
        }
    }
}
