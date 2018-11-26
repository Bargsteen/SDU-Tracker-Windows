namespace Tracker.Interfaces
{
    public interface ILaunchAtLoginService
    {
        bool LaunchAtLoginIsEnabled { get; set; }
    }
}