using Tracker.Events;

namespace Tracker.Interfaces
{
    public interface IUserService
    {
        event UserSessionChangeHandler OnUserSessionStarted;
        event UserSessionChangeHandler OnUserSessionEnded;

        void CheckIfUserHasChanged();
        void ShowUserWindow();
    }
}