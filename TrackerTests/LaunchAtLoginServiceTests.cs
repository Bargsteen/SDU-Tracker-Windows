using System;
using Tracker;
using Tracker.Implementations;
using TrackerLib.Interfaces;
using Xunit;

namespace TrackerTests
{
    public class LaunchAtLoginServiceTests : IDisposable
    {
        private ILaunchAtLoginService _launchAtLoginService;

        public LaunchAtLoginServiceTests()
        {
            _launchAtLoginService = new LaunchAtLoginService();
        }

        public void Dispose()
        {
            _launchAtLoginService = null;
        }

        [Fact]
        public void GetIsEnabled__HasBeenSetToTrue__ReturnsTrue()
        {
            // Arrange
            _launchAtLoginService.LaunchAtLoginIsEnabled = true;

            // Act
            bool result = _launchAtLoginService.LaunchAtLoginIsEnabled;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetIsEnabled__HasBeenSetToFalse__ReturnsFalse()
        {
            // Arrange
            _launchAtLoginService.LaunchAtLoginIsEnabled = false;

            // Act
            bool result = _launchAtLoginService.LaunchAtLoginIsEnabled;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SetIsEnabled__HasJustBeenSetToFalse__CanBeSetAgain()
        {
            // Arrange
            _launchAtLoginService.LaunchAtLoginIsEnabled = false;

            // Act
            _launchAtLoginService.LaunchAtLoginIsEnabled = false;

            // Assert
            // Success if no error was thrown
        }
    }
}