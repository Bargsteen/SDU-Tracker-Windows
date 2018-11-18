using System;

namespace TrackerLib.Events
{
    public class ParticipantIdentifierChangedEventArgs : EventArgs
    {
        public string PreviousParticipantIdentifier { get; set; }
        public string NewParticipantIdentifier { get; set; }

        public ParticipantIdentifierChangedEventArgs(string previousParticipantIdentifier, string newParticipantIdentifier)
        {
            PreviousParticipantIdentifier = previousParticipantIdentifier;
            NewParticipantIdentifier = newParticipantIdentifier;
        }
    }
}