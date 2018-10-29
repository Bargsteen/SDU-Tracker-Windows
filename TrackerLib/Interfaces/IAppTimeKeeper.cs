using TrackerLib.Models;

namespace TrackerLib.Interfaces
{
    public interface IAppTimeKeeper
    {
        ActiveWindow MaybeGetLastActiveWindow();
    }
}
