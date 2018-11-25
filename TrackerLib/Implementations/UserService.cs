using System;
using Tracker;
using TrackerLib.Constants;
using TrackerLib.Events;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
{
    public class UserService : IUserService
    {
        private readonly IAlertService _alertService;
        private readonly ISettings _settings;
        private readonly IUserWindow _userWindow;

        public UserService(IAlertService alertService, ISettings settings, IUserWindow userWindow)
        {
            _alertService = alertService;
            _settings = settings;
            _userWindow = userWindow;
        }

        public event UserSessionChangeHandler OnUserSessionStarted;
        public event UserSessionChangeHandler OnUserSessionEnded;

        public void CheckIfUserHasChanged()
        {
            if (_settings.UserCount > 1) // Multiple users
            {
                bool shouldShowUserWindow = !_alertService.ShowAlertReturnYesWasPressed(AlertConstants.ChangeUserAlertTitle(_settings.CurrentUser),
                    AlertConstants.ChangeUserAlertTitle(_settings.CurrentUser));

                if (shouldShowUserWindow)
                {
                    _userWindow.ShowWindow();
                }
            }
            OnUserSessionStarted?.Invoke(this, new UserSessionChangeEventArgs(_settings.CurrentUser));
        }

        public void ShowUserWindow()
        {
            string userBefore = _settings.CurrentUser;
            string userAfter = _userWindow.ShowWindow();

            if (userAfter == userBefore) return;

            OnUserSessionEnded?.Invoke(this, new UserSessionChangeEventArgs(userBefore));
            OnUserSessionStarted?.Invoke(this, new UserSessionChangeEventArgs(userAfter));
        }
    }
}