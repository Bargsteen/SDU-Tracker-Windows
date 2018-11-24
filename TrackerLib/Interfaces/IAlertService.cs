namespace TrackerLib.Interfaces
{
    public interface IAlertService
    {
        void ShowAlert(string title, string message, string buttonText);

        bool ShowAlertReturnYesWasPressed(string title, string message, string buttonNoText, string buttonYesText);
    }
}