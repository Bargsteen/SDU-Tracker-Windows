using TrackerLib.Interfaces;
using System.Windows.Forms;

namespace Tracker.Implementations
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string title, string message, string buttonText)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public bool ShowAlertReturnYesWasPressed(string title, string message, string buttonNoText, string buttonYesText)
        {
            throw new System.NotImplementedException();	
        }
    }
}