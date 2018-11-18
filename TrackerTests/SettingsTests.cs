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

        [Fact]
        public void OnParticipantIdentifierChanged__ChangeCurrentUser__InvokesEventWithCorrectArgs()
        {
            // Arrange
            const string userId = "TestUserId";
            const string currentUser = "TestCurrentUser";
            _settings.UserId = userId;
            _settings.CurrentUser = currentUser;

            const string newCurrentUser = "TestNewCurrentUser";

            string actualPreviousParticipantIdentifier = "";
            string actualNewParticipantIdentifier = "";
            
            _settings.OnParticipantIdentifierChanged += (sender, args) =>
            {
                actualPreviousParticipantIdentifier = args.PreviousParticipantIdentifier;
                actualNewParticipantIdentifier = args.NewParticipantIdentifier;
            };


            // Act
            _settings.CurrentUser = newCurrentUser;

            // Assert
            string expectedPreviousParticipantIdentifier = $"{userId}:{currentUser}";
            string expectedNewParticipantIdentIdentifier = $"{userId}:{newCurrentUser}";

            Assert.Equal(expectedPreviousParticipantIdentifier, actualPreviousParticipantIdentifier);
            Assert.Equal(expectedNewParticipantIdentIdentifier, actualNewParticipantIdentifier);
        }
    }
}