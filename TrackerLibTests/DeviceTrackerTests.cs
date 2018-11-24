using System;
using Moq;
using TrackerLib.Enums;
using TrackerLib.Events;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
using TrackerLib.Models;
using Xunit;

namespace TrackerLibTests
{
    public class DeviceTrackerTests : IDisposable
    {
        private readonly IDeviceTracker _deviceTracker;

        private readonly Mock<ISendOrSaveService> _sendOrSaveService;
        private readonly Mock<ISystemEventService> _systemEventService;
        private readonly Mock<ISettings> _settings;
        private readonly Mock<IUserService> _userService;

        public DeviceTrackerTests()
        {
            _sendOrSaveService = new Mock<ISendOrSaveService>();
            _systemEventService = new Mock<ISystemEventService>();
            _settings = new Mock<ISettings>();

            var usageBuilder = new Mock<IUsageBuilder>();

            usageBuilder.Setup(u => u.MakeDeviceUsage(It.IsAny<EventType>()))
                .Returns((EventType eventType) => TestHelper.MakeTestDeviceUsage(eventType));

            usageBuilder.Setup(u => u.MakeDeviceUsage(It.IsAny<EventType>(), It.IsAny<string>()))
                .Returns((EventType eventType, string participantId) 
                    => TestHelper.MakeTestDeviceUsage(eventType, participantId));

            _deviceTracker =
                new DeviceTracker(_sendOrSaveService.Object, _settings.Object, _systemEventService.Object, 
                    usageBuilder.Object, _userService.Object);
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

        [Fact]
        public void StartTracking__CurrentUserChanges__SendsDeviceEndAndStart()
        {
            // Arrange
            const string previousParticipantId = "PreviousParticipantId";
            const string newParticipantId = "NewParticipantId";
            _settings.SetupGet(s => s.ParticipantIdentifier).Returns(previousParticipantId);
            _deviceTracker.StartTracking();
            
            // Act
            _settings.Raise(s => s.OnParticipantIdentifierChanged += null, 
               new ParticipantIdentifierChangedEventArgs(previousParticipantId, newParticipantId));

            // Assert

            // Should check participantIdentifier to be thorough.
            VerifyDeviceUsageSentXTimes(EventType.Started, Times.Exactly(2)); // Once for StartTracking, once for Event
            VerifyDeviceUsageSentXTimes(EventType.Ended, Times.Once()); // Once for event
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
            _sendOrSaveService.Verify(s =>
                s.SendOrSaveUsage(It.Is<DeviceUsage>(u => u.EventType == eventType.GetHashCode()), false), times);
        }

        private void VerifyDeviceUsageForParticipantIdSentXTimes(string participantId, EventType eventType, Times times)
        {
            _sendOrSaveService.Verify(s => s.SendOrSaveUsage(It.Is<DeviceUsage>(u => u.EventType == eventType.GetHashCode() 
                                                                                     && u.ParticipantIdentifier == participantId), false), 
                                                                                     times);
        }
    }
}