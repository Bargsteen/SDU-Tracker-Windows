namespace TrackerLib.Interfaces
{
    public interface IAlertService
    {
        void ShowAlert(string title, string message);

        bool ShowAlertReturnYesWasPressed(string title, string message);
    }
}