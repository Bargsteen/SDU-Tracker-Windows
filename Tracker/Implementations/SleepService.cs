using System;
using System.Threading;
using Tracker.Interfaces;

namespace Tracker.Implementations
{
    public class SleepService : ISleepService
    {
        public void SleepFor(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

    }
}