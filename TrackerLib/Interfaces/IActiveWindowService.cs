using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IActiveWindowService
    {
        ActiveWindow MaybeGetLastActiveWindow();
    }
}
