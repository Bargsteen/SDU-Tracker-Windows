using System;
using System.Threading;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
{
    public class SleepService : ISleepService
    {
        public void SleepFor(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

    }
}