﻿namespace TrackerLib.Constants
{
    public static class AlertConstants
    {
        // Generic Time
        public static int LongAlertTime = 10; // Seconds


        // Ready For Setup
        public static string ReadyForSetupTitle = "Klar til opsætning";

        public static string ReadyForSetupMessage =
            "ActivityTrackerSDU er installeret, men mangler at blive sat op via.linket, som du har modtaget på e-mail.";


        // Tracking Has Ended
        public static string TrackingHasEndedTitle = "Tracking perioden er afsluttet";

        public static string TrackingHasEndedMessage = "Tak for din deltagelse.";


        // Change User Alert
        public static string ChangeUserAlertTitle(string user) => $"Er du {user}?";

        public static string ChangeUserAlertMessage = "Hvis ikke, så tryk nej for at skifte bruger.";
    }
}