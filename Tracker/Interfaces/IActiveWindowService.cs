using Tracker.Models;

namespace Tracker.Interfaces
{
    public interface IActiveWindowService
    {
        ActiveWindow MaybeGetLastActiveWindow();
    }
}
