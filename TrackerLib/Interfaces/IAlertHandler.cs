namespace TrackerLib.Interfaces
{
    public interface IAlertHandler
    {
        void ShowAlert(string title, string message, string buttonText, int durationInSeconds);
        bool ShowAlertReturnOkWasPressed(string title, string message, string buttonNoText, string buttonYesText);
        bool ShowAlertReturnOkWasPressed(string title, string message, string buttonNoText, string buttonYesText, int durationInSeconds);
    }
}