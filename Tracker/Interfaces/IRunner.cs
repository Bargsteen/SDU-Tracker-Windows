using Tracker.Enums;

namespace Tracker.Interfaces
{
    public interface IRunner
    {
        RunnerResponse Run();
        void Terminate();
    }
}