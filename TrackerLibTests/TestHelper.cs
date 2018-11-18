using System;
using System.Collections.Generic;
using TrackerLib.Enums;
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

        public static DeviceUsage MakeTestDeviceUsage(EventType eventType, string participantIdentifier)
        {
            return new DeviceUsage(participantIdentifier, "TestDevice", DateTimeOffset.UtcNow, 1, eventType);
        }

        public static DeviceUsage MakeTestDeviceUsage(string participantIdentifier = "TestId")
        {
            return MakeTestDeviceUsage(participantIdentifier, DateTimeOffset.Now);
        }

        public static DeviceUsage MakeTestDeviceUsage(string participantIdentifier, DateTimeOffset timeStamp)
        {
            return new DeviceUsage(participantIdentifier, "TestDevice", timeStamp, 1, EventType.Started);
        }

        public static List<DeviceUsage> MakeTestDeviceUsages(int amount)
        {
            List<DeviceUsage> deviceUsages = new List<DeviceUsage>();

            for (int i = 0; i < amount; i++)
            {
                deviceUsages.Add(MakeTestDeviceUsage(EventType.Started));
            }

            return deviceUsages;
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

        public static List<AppUsage> MakeTestAppUsages(int amount)
        {
            List<AppUsage> appUsages = new List<AppUsage>();

            for (int i = 0; i < amount; i++)
            {
                appUsages.Add(MakeTestAppUsage());
            }

            return appUsages;
        }
    }
}