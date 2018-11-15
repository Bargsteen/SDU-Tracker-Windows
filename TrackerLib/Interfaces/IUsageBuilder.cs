using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IUsageBuilder
    {
        DeviceUsage MakeDeviceUsage(EventType eventType);
        AppUsage MakeAppUsage(ActiveWindow activeWindow);
    }
}