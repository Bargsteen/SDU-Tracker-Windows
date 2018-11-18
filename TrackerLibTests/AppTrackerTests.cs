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
        private readonly Mock<IActiveWindowService> _activeWindowService;
        private readonly Mock<ISendOrSaveService> _sendOrSaveService;

        public AppTrackerTests()
        {
            _activeWindowService = new Mock<IActiveWindowService>();
            _sendOrSaveService = new Mock<ISendOrSaveService>();
            var sleepService = new Mock<ISleepService>();
            var usageBuilder = new Mock<IUsageBuilder>();

            _appTracker = new AppTracker(_activeWindowService.Object, _sendOrSaveService.Object, sleepService.Object,
                usageBuilder.Object);
        }

        public void Dispose()
        {

        }

        [Fact]
        public void StartTracking__ActiveWindowChanged__SendsAppUsage()
        {
            // Arrange
            _activeWindowService.Setup(a => a.MaybeGetLastActiveWindow())
                .Returns(new ActiveWindow("Test", DateTimeOffset.Now));

            // Act
            _appTracker.StartTracking();

            // Wait for the thread to start. Could be solved differently using a wrapper around the Thread.
            // Similarly to how sleep is handled with the SleepService.
            Thread.Sleep(100); 

            // Assert
            _sendOrSaveService.Verify(s => s.SendOrSaveUsage(It.IsAny<AppUsage>(), false));
        }

        [Fact]
        public void Initialized__ActiveWindowChanged__DoesNothing()
        {
            // Arrange
            _activeWindowService.Setup(a => a.MaybeGetLastActiveWindow())
                .Returns(new ActiveWindow("Test", DateTimeOffset.Now));

            // Act
            // Being Initialized

            // Assert
            _sendOrSaveService.Verify(s => s.SendOrSaveUsage(It.IsAny<AppUsage>(), false), Times.Never);
        }
    }
}
