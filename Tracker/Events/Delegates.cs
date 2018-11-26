namespace Tracker.Events
{

    public delegate void CurrentUserChangedHandler(object sender, CurrentUserChangedEventArgs args);

    public delegate void ParticipantIdentifierChangedHandler(object sender,
        ParticipantIdentifierChangedEventArgs args);

    public delegate void UserSessionChangeHandler(object sender, UserSessionChangeEventArgs args);
}