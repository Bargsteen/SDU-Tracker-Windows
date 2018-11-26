using System.Windows.Forms;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ShowAlertReturnYesWasPressed(string title, string message)
        {
            var alertResult = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                       MessageBoxDefaultButton.Button1);
            return alertResult == DialogResult.Yes;
        }
    }
}