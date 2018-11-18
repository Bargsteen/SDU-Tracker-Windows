using System;
using Moq;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
using TrackerLib.Models;
using Xunit;

namespace TrackerLibTests
{
    public class DeviceTrackerTests : IDisposable
    {
        private readonly IDeviceTracker _deviceTracker;

        private readonly Mock<ISendOrSaveService> _sendOrSaveHandler;
        private readonly Mock<ISystemEventService> _systemEventService;

        public DeviceTrackerTests()
        {
            _sendOrSaveHandler = new Mock<ISendOrSaveService>();
            _systemEventService = new Mock<ISystemEventService>();
            var usageBuilder = new Mock<IUsageBuilder>();

            usageBuilder.Setup(u => u.MakeDeviceUsage(EventType.Started))
                .Returns(TestHelper.MakeTestDeviceUsage(EventType.Started));

            usageBuilder.Setup(u => u.MakeDeviceUsage(EventType.Ended))
                .Returns(TestHelper.MakeTestDeviceUsage(EventType.Ended));


            _deviceTracker =
                new DeviceTracker(_sendOrSaveHandler.Object, _systemEventService.Object, usageBuilder.Object);
        }

        public void Dispose()
        {
        }


        [Fact]
        public void Initialized__UserLogsOut__DoesNothing()
        {
            // Act
            RaiseSystemSuspendedEvent();

            // Assert
            VerifyDeviceUsageSentXTimes(EventType.Ended, Times.Never());
        }

        [Fact]
        public void StartTracking__ComputerSleeps__SendsDeviceEndedUsage()
        {
            // Arrange
            _deviceTracker.StartTracking();

            // Act
            RaiseSystemSuspendedEvent();
            

            // Assert
            VerifyDeviceUsageSentXTimes(EventType.Ended, Times.Once());
        }

        [Fact]
        public void StartTracking__ComputerWakes__SendsDeviceStartedUsage()
        {
            // Arrange
            _deviceTracker.StartTracking();

            // Act
            RaiseSystemResumedEvent();

            // Assert

            // Once for just starting tracking, and once for the event
            VerifyDeviceUsageSentXTimes(EventType.Started, Times.Exactly(2));
        }

        //[Fact]
        public void StartTracking__CurrentUserChanges__SendsDeviceEndAndStart()
        {
        }


        [Fact]
        public void StartTracking__NoConditions__SendsDeviceStarted()
        {
            // Act
            _deviceTracker.StartTracking();

            // Assert
            VerifyDeviceUsageSentXTimes(EventType.Started, Times.Once());
        }

        [Fact]
        public void StopTracking__WasStarted__SendsDeviceEnded()
        {
            // Arrange
            _deviceTracker.StartTracking();

            // Act
            _deviceTracker.StopTracking();

            // Assert
            VerifyDeviceUsageSentXTimes(EventType.Ended, Times.Once());
        }

        private void RaiseSystemResumedEvent()
        {
            _systemEventService.Raise(s => s.SystemStartedOrResumed += null, new EventArgs());
        }

        private void RaiseSystemSuspendedEvent()
        {
            _systemEventService.Raise(s => s.SystemSuspended += null, new EventArgs());
        }

        private void VerifyDeviceUsageSentXTimes(EventType eventType, Times times)
        {
            _sendOrSaveHandler.Verify(s =>
                s.SendOrSaveUsage(It.Is<DeviceUsage>(u => u.EventType == eventType.GetHashCode()), false), times);
        }
    }
}