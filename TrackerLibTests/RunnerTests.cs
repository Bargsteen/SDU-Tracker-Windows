using System;
using Moq;
using TrackerLib.Enums;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
using TrackerLib.Constants;
using TrackerLib.Models;
using Xunit;

namespace TrackerLibTests
{
    // Naming convention: MethodName__ConditionOne_ConditionTwo__ExpectedResult
    public class RunnerTests : IDisposable
    {
        public RunnerTests()
        {
            _alertHandler = new Mock<IAlertHandler>();
            _appTracker = new Mock<IAppTracker>();
            _dateTimeHandler = new Mock<IDateTimeHandler>();
            _deviceTracker = new Mock<IDeviceTracker>();
            _launchAtLoginHandler = new Mock<ILaunchAtLoginHandler>();
            _logger = new Mock<ILogger>();
            _sendOrSaveHandler = new Mock<ISendOrSaveHandler>();
            _settings = new Mock<ISettings>();
            _resendHandler = new Mock<IResendHandler>();
            _usageBuilder = new Mock<IUsageBuilder>();
            _userHandler = new Mock<IUserHandler>();


            _runner = new Runner(_alertHandler.Object, _appTracker.Object, _dateTimeHandler.Object,
                _deviceTracker.Object,
                _launchAtLoginHandler.Object, _logger.Object, _sendOrSaveHandler.Object, _settings.Object,
                _resendHandler.Object, _usageBuilder.Object, _userHandler.Object);
        }

        public void Dispose()
        {
            // Nothing to do
        }

        private readonly IRunner _runner;

        private readonly Mock<IAlertHandler> _alertHandler;
        private readonly Mock<IAppTracker> _appTracker;
        private readonly Mock<IDateTimeHandler> _dateTimeHandler;
        private readonly Mock<IDeviceTracker> _deviceTracker;
        private readonly Mock<ILaunchAtLoginHandler> _launchAtLoginHandler;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<ISendOrSaveHandler> _sendOrSaveHandler;
        private readonly Mock<ISettings> _settings;
        private readonly Mock<IResendHandler> _resendHandler;
        private readonly Mock<IUsageBuilder> _usageBuilder;
        private readonly Mock<IUserHandler> _userHandler;

        [Fact]
        public void Run__IsSetup_TrackingDateNotReached_AppAndDeviceTracking__StartsTrackingBoth()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _settings.Setup(s => s.StopTrackingDate).Returns(DateTimeOffset.MaxValue);
            _settings.Setup(s => s.TrackingType).Returns(TrackingType.AppAndDevice);

            // Act
            _runner.Run();

            // Assert
            _appTracker.Verify(a => a.StartTracking(), Times.Once);
            _deviceTracker.Verify(a => a.StartTracking(), Times.Once);
        }

        [Fact]
        public void Run__IsSetup_TrackingDateNotReached_DeviceTracking__StartsTrackingDevice()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _settings.Setup(s => s.StopTrackingDate).Returns(DateTimeOffset.MaxValue);
            _settings.Setup(s => s.TrackingType).Returns(TrackingType.Device);

            // Act
            _runner.Run();

            // Assert
            _appTracker.Verify(a => a.StartTracking(), Times.Never);
            _deviceTracker.Verify(a => a.StartTracking(), Times.Once);
        }

        [Fact]
        public void Run__IsSetup_TrackingDateNotReached__StartsResender()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _settings.Setup(s => s.StopTrackingDate).Returns(DateTimeOffset.MaxValue);
            _settings.Setup(s => s.TrackingType).Returns(TrackingType.AppAndDevice);

            // Act
            _runner.Run();

            // Assert
            _resendHandler.Verify(r => r.StartPeriodicResendingOfSavedUsages(), Times.Once);
        }

        [Fact]
        public void Run__IsSetup_TrackingDateReached__ShowsAlertStopAutoLaunch()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _settings.Setup(s => s.StopTrackingDate).Returns(DateTimeOffset.MinValue);
            _dateTimeHandler.Setup(d => d.CurrentTime).Returns(DateTimeOffset.MaxValue);

            // Act
            _runner.Run();

            // Assert
            _alertHandler.Verify(a => a.ShowAlert(AlertsConstants.TrackingHasEndedTitle, AlertsConstants.TrackingHasEndedMessage, 
                AlertsConstants.OkButtonText, AlertsConstants.LongAlertTime));
            _launchAtLoginHandler.VerifySet(l => l.LaunchAtLoginIsEnabled = false);
        }

        [Fact]
        public void Run__NotSetup__DisplaysReadyForSetupAlert()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(false);

            // Act
            _runner.Run();

            // Assert
            _alertHandler.Verify(a => a.ShowAlert(AlertsConstants.ReadyForSetupTitle, AlertsConstants.ReadyForSetupMessage, 
                AlertsConstants.OkButtonText, AlertsConstants.LongAlertTime));
        }

        [Fact]
        public void Terminate__IsSetup__SendsDeviceEndedUsage()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _sendOrSaveHandler.Setup(s => s.SendOrSaveUsage(It.IsAny<Usage>(), It.IsAny<bool>()));
            _usageBuilder.Setup(u => u.MakeDeviceUsage(EventType.Ended))
                .Returns(new DeviceUsage("Test", "Test", DateTimeOffset.Now, 1, EventType.Ended));

            // Act
            _runner.Terminate();

            // Assert
            _sendOrSaveHandler.Verify(s => 
                s.SendOrSaveUsage(It.Is<DeviceUsage>(d => d.EventType == EventType.Ended.GetHashCode()), false), Times.Once);
        }

        [Fact]
        public void Terminate__NotSetup__SimplyTerminates()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(false);

            // Act
            _runner.Terminate();

            // Assert
            _sendOrSaveHandler.Verify(s => s.SendOrSaveUsage(It.IsAny<Usage>(), It.IsAny<bool>()), Times.Never);
        }
    }
}