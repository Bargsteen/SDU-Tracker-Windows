using Newtonsoft.Json;
using TrackerLib.Models;

namespace TrackerLib
{

    public static class Extensions
    {
        public static string ToJson(this IUsage usage)
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
