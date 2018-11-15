using System;
using Moq;
using Xunit;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
using TrackerLib.Models;

namespace TrackerLibTests
{
    public class AppTrackerTests : IDisposable
    {
        private readonly IAppTracker _appTracker;
        private readonly Mock<IActiveWindowHandler> _activeWindowHandler;
        private readonly Mock<DateTimeHandler> _dateTimeHandler;
        private readonly Mock<ISendOrSaveHandler> _sendOrSaveHandler;
        private readonly Mock<ISettings> _settings;
        private readonly Mock<ISleepHandler> _sleepHandler;

        public AppTrackerTests()
        {
            _activeWindowHandler = new Mock<IActiveWindowHandler>();
            _dateTimeHandler = new Mock<DateTimeHandler>();
            _sendOrSaveHandler = new Mock<ISendOrSaveHandler>();
            _settings = new Mock<ISettings>();
            _sleepHandler = new Mock<ISleepHandler>();

            _appTracker = new AppTracker(_activeWindowHandler.Object, _dateTimeHandler.Object, 
                _sendOrSaveHandler.Object, _settings.Object, _sleepHandler.Object);
        }

        public void Dispose()
        {

        }

        [Fact]
        public void StartTracking__ActiveWindowChanged__SendsAppUsage()
        {
            // Arrange
            _activeWindowHandler.Setup(a => a.MaybeGetLastActiveWindow())
                .Returns(new ActiveWindow("Test", DateTimeOffset.Now));

            // Act
            _appTracker.StartTracking();

            // Assert
            _sendOrSaveHandler.Verify(s => s.SendOrSaveUsage(It.IsAny<AppUsage>(), false));
        }

        [Fact]
        public void Initialized__ActiveWindowChanged__DoesNothing()
        {
            // Arrange
            _activeWindowHandler.Setup(a => a.MaybeGetLastActiveWindow())
                .Returns(new ActiveWindow("Test", DateTimeOffset.Now));

            // Act
            // Being Initialized

            // Assert
            _sendOrSaveHandler.Verify(s => s.SendOrSaveUsage(It.IsAny<AppUsage>(), false));
        }
    }
}
