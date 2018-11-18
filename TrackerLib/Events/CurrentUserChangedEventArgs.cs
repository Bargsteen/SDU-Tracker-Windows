using System;

namespace TrackerLib.Events
{
    public class CurrentUserChangedEventArgs : EventArgs
    {
        public string PreviousCurrentUser { get; set; }
        public string NewCurrentUser { get; set; }

        public CurrentUserChangedEventArgs(string previousCurrentUser, string newCurrentUser)
        {
            PreviousCurrentUser = previousCurrentUser;
            NewCurrentUser = newCurrentUser;
        }
    }
}