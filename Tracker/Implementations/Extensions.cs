using Newtonsoft.Json;
using Tracker.Models;

namespace Tracker.Implementations
{

    public static class Extensions
    {
        public static string ToJson(this Usage usage)
        {
            return JsonConvert.SerializeObject(usage);
        }

        public static string EventTypeToString(this int eventType)
        {
            switch (eventType)
            {
                case 1:
                    return "Started";
                case 0:
                    return "Ended";
                default:
                    return "";
            }
        }
    }
}
