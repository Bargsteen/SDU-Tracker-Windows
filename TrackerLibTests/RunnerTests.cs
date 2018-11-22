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
            _alertService = new Mock<IAlertService>();
            _appTracker = new Mock<IAppTracker>();
            _dateTimeService = new Mock<IDateTimeService>();
            _deviceTracker = new Mock<IDeviceTracker>();
            _launchAtLoginService = new Mock<ILaunchAtLoginService>();
            _logger = new Mock<ILogger>();
            _settings = new Mock<ISettings>();
            _resendService = new Mock<IResendService>();
            _userService = new Mock<IUserService>();

            _runner = new Runner(_alertService.Object, _appTracker.Object, _dateTimeService.Object,
                _deviceTracker.Object, _launchAtLoginService.Object, _logger.Object, 
                _resendService.Object, _settings.Object, _userService.Object);
        }

        public void Dispose()
        {
            // Nothing to do
        }

        private readonly IRunner _runner;

        private readonly Mock<IAlertService> _alertService;
        private readonly Mock<IAppTracker> _appTracker;
        private readonly Mock<IDateTimeService> _dateTimeService;
        private readonly Mock<IDeviceTracker> _deviceTracker;
        private readonly Mock<ILaunchAtLoginService> _launchAtLoginService;
        private readonly Mock<ILogger> _logger;
        private readonly Mock<ISettings> _settings;
        private readonly Mock<IResendService> _resendService;
        private readonly Mock<IUserService> _userService;

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
            _resendService.Verify(r => r.StartPeriodicResendingOfSavedUsages(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Run__IsSetup_TrackingDateReached__ShowsAlertStopAutoLaunch()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _settings.Setup(s => s.StopTrackingDate).Returns(DateTimeOffset.MinValue);
            _dateTimeService.Setup(d => d.CurrentTime).Returns(DateTimeOffset.MaxValue);

            // Act
            _runner.Run();

            // Assert
            _alertService.Verify(a => a.ShowAlert(AlertsConstants.TrackingHasEndedTitle, AlertsConstants.TrackingHasEndedMessage, 
                AlertsConstants.OkButtonText));
            _launchAtLoginService.VerifySet(l => l.LaunchAtLoginIsEnabled = false);
        }

        [Fact]
        public void Run__NotSetup__DisplaysReadyForSetupAlert()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(false);

            // Act
            _runner.Run();

            // Assert
            _alertService.Verify(a => a.ShowAlert(AlertsConstants.ReadyForSetupTitle, AlertsConstants.ReadyForSetupMessage, 
                AlertsConstants.OkButtonText));
        }

        [Fact]
        public void Terminate__IsSetup_AppAndDeviceTracking__StopsTrackingBoth()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _settings.SetupGet(s => s.TrackingType).Returns(TrackingType.AppAndDevice);

            // Act
            _runner.Terminate();

            // Assert
            _appTracker.Verify(a => a.StopTracking(), Times.Once);
            _deviceTracker.Verify(d => d.StopTracking(), Times.Once);
        }

        [Fact]
        public void Terminate__IsSetup_AppAndDeviceTracking__StopsTrackingDevice()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(true);
            _settings.SetupGet(s => s.TrackingType).Returns(TrackingType.Device);


            // Act
            _runner.Terminate();

            // Assert
            _appTracker.Verify(a => a.StopTracking(), Times.Never);
            _deviceTracker.Verify(d => d.StopTracking(), Times.Once);
        }

        [Fact]
        public void Terminate__NotSetup__SimplyTerminates()
        {
            // Arrange
            _settings.Setup(s => s.AppHasBeenSetup).Returns(false);
            _settings.SetupGet(s => s.TrackingType).Returns(TrackingType.AppAndDevice);

            // Act
            _runner.Terminate();

            // Assert
            _appTracker.Verify(a => a.StopTracking(), Times.Never);
            _deviceTracker.Verify(d => d.StopTracking(), Times.Never);
        }
    }
}