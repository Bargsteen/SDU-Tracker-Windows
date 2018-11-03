using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IActiveWindowHandler
    {
        ActiveWindow MaybeGetLastActiveWindow();
    }
}
