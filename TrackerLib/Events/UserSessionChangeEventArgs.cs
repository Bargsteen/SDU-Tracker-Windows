using System;

namespace TrackerLib.Events
{
    public class UserSessionChangeEventArgs : EventArgs
    {
        public string User { get; set; }

        public UserSessionChangeEventArgs(string user)
        {
            User = user;
        }
    }
}