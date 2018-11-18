using System;
using Tracker;
using TrackerLib.Interfaces;
using Xunit;

namespace TrackerTests
{
    public class LaunchAtLoginHandlerTests : IDisposable
    {
        private ILaunchAtLoginHandler _launchAtLoginHandler;

        public LaunchAtLoginHandlerTests()
        {
            _launchAtLoginHandler = new LaunchAtLoginHandler();
        }

        public void Dispose()
        {
            _launchAtLoginHandler = null;
        }

        [Fact]
        public void GetIsEnabled__HasBeenSetToTrue__ReturnsTrue()
        {
            // Arrange
            _launchAtLoginHandler.LaunchAtLoginIsEnabled = true;

            // Act
            bool result = _launchAtLoginHandler.LaunchAtLoginIsEnabled;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GetIsEnabled__HasBeenSetToFalse__ReturnsFalse()
        {
            // Arrange
            _launchAtLoginHandler.LaunchAtLoginIsEnabled = false;

            // Act
            bool result = _launchAtLoginHandler.LaunchAtLoginIsEnabled;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SetIsEnabled__HasJustBeenSetToFalse__CanBeSetAgain()
        {
            // Arrange
            _launchAtLoginHandler.LaunchAtLoginIsEnabled = false;

            // Act
            _launchAtLoginHandler.LaunchAtLoginIsEnabled = false;

            // Assert
            // Success if no error was thrown
        }
    }
}