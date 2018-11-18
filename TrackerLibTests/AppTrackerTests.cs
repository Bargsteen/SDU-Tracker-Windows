using System;
using System.Threading;
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
        private readonly Mock<IActiveWindowService> _activeWindowHandler;
        private readonly Mock<ISendOrSaveService> _sendOrSaveHandler;

        public AppTrackerTests()
        {
            _activeWindowHandler = new Mock<IActiveWindowService>();
            _sendOrSaveHandler = new Mock<ISendOrSaveService>();
            var sleepHandler = new Mock<ISleepService>();
            var usageBuilder = new Mock<IUsageBuilder>();

            _appTracker = new AppTracker(_activeWindowHandler.Object, _sendOrSaveHandler.Object, sleepHandler.Object,
                usageBuilder.Object);
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

            // Wait for the thread to start. Could be solved differently using a wrapper around the Thread.
            // Similarly to how sleep is handled with the SleepService.
            Thread.Sleep(100); 

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
            _sendOrSaveHandler.Verify(s => s.SendOrSaveUsage(It.IsAny<AppUsage>(), false), Times.Never);
        }
    }
}
