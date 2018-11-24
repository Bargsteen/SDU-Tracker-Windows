using Tracker.Implementations;
using TrackerLib.Interfaces;
using Xunit;

namespace TrackerTests
{
    public class SettingsTests
    {
        private readonly ISettings _settings;

        public SettingsTests()
        {
            _settings = new Settings();
        }

        [Fact]
        public void AppHasBeenSetup__WasFalse_SetToTrue__ReturnsTrue()
        {
            // Arrange
            _settings.AppHasBeenSetup = false;

            // Act
            _settings.AppHasBeenSetup = true;

            // Assert
            var result = _settings.AppHasBeenSetup;
            Assert.True(result);
        }

        [Fact]
        public void ParticipantIdentifier__UserIdIsSet_CurrentUserIsSet__ReturnsCorrect()
        {
            // Arrange
            const string userId = "UserId";
            const string currentUser = "CurrentUser";
            _settings.UserId = userId;
            _settings.CurrentUser = currentUser;

            // Act
            string result = _settings.ParticipantIdentifier;

            // Assert
            string expectedResult = $"{userId}:{currentUser}";

            Assert.Equal(expectedResult, result);
        }
    }
}