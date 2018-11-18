using TrackerLib.Enums;
using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IUsageBuilder
    {
        DeviceUsage MakeDeviceUsage(EventType eventType);
        DeviceUsage MakeDeviceUsage(EventType eventType, string participantIdentifier);

        AppUsage MakeAppUsage(ActiveWindow activeWindow);
    }
}