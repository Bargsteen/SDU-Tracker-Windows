using System;
using TrackerLib.Models;

namespace TrackerLibTests
{
    public static class TestHelper
    {

        // Device Usages

        public static DeviceUsage MakeTestDeviceUsage(EventType eventType)
        {
            return new DeviceUsage("TestDeviceUsage", "TestDevice", DateTimeOffset.UtcNow, 1, eventType);
        }

        public static DeviceUsage MakeTestDeviceUsage(string participantIdentifier = "TestId")
        {
            return MakeTestDeviceUsage(participantIdentifier, DateTimeOffset.Now);
        }

        public static DeviceUsage MakeTestDeviceUsage(string participantIdentifier, DateTimeOffset timeStamp)
        {
            return new DeviceUsage(participantIdentifier, "TestDevice", timeStamp, 1, EventType.Started);
        }


        // App Usages

        public static AppUsage MakeTestAppUsage(string participantIdentifier = "TestId")
        {
            return MakeTestAppUsage(participantIdentifier, DateTimeOffset.Now);
        }

        public static AppUsage MakeTestAppUsage(string participantIdentifier, DateTimeOffset timeStamp)
        {
            return new AppUsage(participantIdentifier, "TestDevice", timeStamp, 1, "TestPackage", 100);
        }
    }
}