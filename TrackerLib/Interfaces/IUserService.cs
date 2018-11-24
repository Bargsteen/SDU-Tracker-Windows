using System;
using TrackerLib.Events;

namespace TrackerLib.Interfaces
{
    public interface IUserService
    {
        event UserSessionChangeHandler OnUserSessionStarted;
        event UserSessionChangeHandler OnUserSessionEnded;

        void CheckIfUserHasChanged();
        void ShowUserWindow();
    }
}