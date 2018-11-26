using System;
using Moq;
using Tracker.Enums;
using Tracker.Events;
using Tracker.Implementations;
using Tracker.Interfaces;
using Tracker.Models;
using Xunit;

namespace TrackerTests
{
    public class DeviceTrackerTests
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
            _userService = new Mock<IUserService>();

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



        ///////////////////////////////////////////////////////////////////////
        //                          INITIALIZATION                           //
        ///////////////////////////////////////////////////////////////////////

        [Fact]
        public void Initialized__SystemIsResumed__DoesNothing()
        {
            // Act
            RaiseSystemResumedEvent();

            // Assert
            VerifyNothingWasDone();
        }

        [Fact]
        public void Initialized__SystemIsSuspended__DoesNothing()
        {
            // Act
            RaiseSystemSuspendedEvent();

            // Assert
            VerifyNothingWasDone();
        }

        ///////////////////////////////////////////////////////////////////////
        //                          START TRACKING                           //
        ///////////////////////////////////////////////////////////////////////

        [Fact]
        public void StartTracking__NoConditions__InvokesCheckIfUserHasChanged()
        {
            // Act
            _deviceTracker.StartTracking();

            // Assert
            VerifyCheckIfUserHasChanged(Times.Once());
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
            VerifyCheckIfUserHasChanged(Times.Exactly(2));
        }

        [Fact]
        public void StartTracking__ComputerSleeps__SendsDeviceUsage()
        {
            // Arrange
            _deviceTracker.StartTracking();

            // Act
            RaiseSystemSuspendedEvent();

            // Assert
            VerifyDeviceUsageSentXTimes(EventType.Ended, Times.Once());
        }


        [Fact]
        public void StartTracking__UserSessionStartedInvoked__SendsDeviceStartedUsage()
        {
            // Arrange
            const string userId = "U";
            const string user = "A";
            SetupSettingsMakeParticipantIdentifierForUser(userId);

            // Act
            _deviceTracker.StartTracking();
            _userService.Raise(u => u.OnUserSessionStarted += null, new UserSessionChangeEventArgs(user));

            // Assert
            VerifyDeviceUsageForParticipantIdSentXTimes($"{userId}:{user}", EventType.Started, Times.Once());
        }

        [Fact]
        public void StartTracking__UserSessionEndedInvoked__SendsDeviceEndedUsage()
        {
            // Arrange
            const string userId = "U";
            const string user = "A";
            SetupSettingsMakeParticipantIdentifierForUser(userId);

            // Act
            _deviceTracker.StartTracking();
            _userService.Raise(u => u.OnUserSessionEnded += null, new UserSessionChangeEventArgs(user));

            // Assert
            VerifyDeviceUsageForParticipantIdSentXTimes($"{userId}:{user}", EventType.Ended, Times.Once());
        }

        ///////////////////////////////////////////////////////////////////////
        //                           STOP TRACKING                           //
        ///////////////////////////////////////////////////////////////////////

        [Fact]
        public void StopTracking__WasNotStarted__DoesNothing()
        {
            // Act
            _deviceTracker.StopTracking();

            // Assert
            VerifyNothingWasDone();
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


        ///////////////////////////////////////////////////////////////////////
        //                              HELPERS                              //
        ///////////////////////////////////////////////////////////////////////

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

        private void VerifyCheckIfUserHasChanged(Times times)
        {
            _userService.Verify(u => u.CheckIfUserHasChanged(), times);
        }

        private void VerifyDeviceUsageForParticipantIdSentXTimes(string participantId, EventType eventType, Times times)
        {
            _sendOrSaveService.Verify(s => s.SendOrSaveUsage(It.Is<DeviceUsage>(u => u.EventType == eventType.GetHashCode() 
                                                                                     && u.ParticipantIdentifier == participantId), false), 
                                                                                     times);
        }

        private void VerifyNothingWasDone()
        {
            VerifyDeviceUsageSentXTimes(EventType.Ended, Times.Never());
            VerifyDeviceUsageSentXTimes(EventType.Started, Times.Never());
            VerifyCheckIfUserHasChanged(Times.Never());
        }

        private void SetupSettingsMakeParticipantIdentifierForUser(string userId)
        {
            _settings.Setup(s => s.MakeParticipantIdentifierForUser(It.IsAny<string>()))
                .Returns<string>(u => $"{userId}:{u}");
        }
    }
}