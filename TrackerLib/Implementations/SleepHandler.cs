using System;
using System.Threading;
using TrackerLib.Interfaces;

namespace TrackerLib.Implementations
{
    public class SleepHandler : ISleepHandler
    {
        public void SleepFor(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

    }
}