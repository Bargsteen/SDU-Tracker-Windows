using Tracker.Enums;
using Tracker.Models;

namespace Tracker.Interfaces
{
    public interface IUsageBuilder
    {
        DeviceUsage MakeDeviceUsage(EventType eventType);
        DeviceUsage MakeDeviceUsage(EventType eventType, string participantIdentifier);

        AppUsage MakeAppUsage(ActiveWindow activeWindow);
    }
}