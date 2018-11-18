using TrackerLib.Interfaces;

namespace Tracker.Implementations
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string title, string message, string buttonText, int durationInSeconds)
        {
           // throw new System.NotImplementedException();
        }

        public bool ShowAlertReturnOkWasPressed(string title, string message, string buttonNoText, string buttonYesText)
        {
           // throw new System.NotImplementedException();
            return true;
        }

        public bool ShowAlertReturnOkWasPressed(string title, string message, string buttonNoText, string buttonYesText,
            int durationInSeconds)
        {
            //throw new System.NotImplementedException();
            return true;
        }
    }
}