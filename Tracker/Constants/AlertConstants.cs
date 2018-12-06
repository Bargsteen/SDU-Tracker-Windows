namespace Tracker.Constants
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

        // Setup
        public static string SetupByUriErrorTitle = "Opsætning mislykkedes";

        public static string SetupByUriErrorMessage = "Prøv at åbne linket igen. Hvis det ikke virker, så kontakt SDU.";

        public static string SetupByUriSuccessTitle = "Opsætning færdiggjort";

        public static string SetupByUriSuccessMessage = "Opsætningen er færdiggjort. Der er intet mere at gøre.";
    }
}