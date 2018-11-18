namespace TrackerLib.Events
{

    public delegate void CurrentUserChangedHandler(object sender, CurrentUserChangedEventArgs args);

    public delegate void ParticipantIdentifierChangedHandler(object sender,
        ParticipantIdentifierChangedEventArgs args);
}