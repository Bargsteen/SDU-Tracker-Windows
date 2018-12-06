using System;
using System.Linq;
using Moq;
using Tracker.Constants;
using Tracker.Implementations;
using Tracker.Interfaces;
using Xunit;

namespace TrackerTests
{
    public class SetupServiceTests
    {
        private readonly Mock<IAlertService> _alertServiceMock;
        private readonly Mock<ISettings> _settingsMock;

        private readonly ISetupService _setupService;

        public SetupServiceTests()
        {
            _alertServiceMock = new Mock<IAlertService>();
            var dateTimeServiceMock = new Mock<IDateTimeService>();
            _settingsMock = new Mock<ISettings>();
            _setupService = new SetupService(_alertServiceMock.Object, dateTimeServiceMock.Object, _settingsMock.Object);
        }

        [Fact]
        public void SetupAppByUri__NotValidUri__ShowsErrorAlert()
        {
            // Arrange
            const string notUri = "ok";

            // Act
            _setupService.SetupAppByUri(notUri);

            // Assert
            VerifyFailedAlertShown(Times.Once());
            VerifySuccessAlertShown(Times.Never());
        }

        [Theory]
        [InlineData("sdutracker://?data=eyAidXNlcl9pZCI6ICJUZXN0SWQiLCAidXNlcnMiOiAiW1Rlc3QxLCBUZXN0Ml0iLCAidHJhY2tpbmdfdHlwZSI6ICIxIiwgIm1lYXN1cmVtZW50X2RheXMiOiAiMzAiIH0=", "TestId", 30, "Test1", "Test2")]
        [InlineData("sdutracker://?data=eyAidXNlcl9pZCI6ICJUZXN0MklkIiwgInVzZXJzIjogIltUZXN0VXNlcl0iLCAidHJhY2tpbmdfdHlwZSI6ICIwIiwgIm1lYXN1cmVtZW50X2RheXMiOiAiMSIgfQ==", "Test2Id", 1, "TestUser")]
        public void SetupAppByUri__ValidUri__ChangesSettingsAndShowsSuccessAlert(string uri, string userId, int measurementDays, params string[] users)
        {
            // Arrange
            _settingsMock.SetupAllProperties();
            var testDate = DateTimeOffset.MinValue;

            
            // Act
            _setupService.SetupAppByUri(uri);

            // Assert
            VerifyFailedAlertShown(Times.Never());
            VerifySuccessAlertShown(Times.Once());

            _settingsMock.VerifySet(s => s.CurrentUser = users[0]);
            _settingsMock.VerifySet(s => s.AppHasBeenSetup = true);
            _settingsMock.VerifySet(s => s.StopTrackingDate = testDate.AddDays(measurementDays));
            _settingsMock.VerifySet(s => s.UserId = userId);
            _settingsMock.VerifySet(s => s.Users = users.ToList());
        }

        private void VerifyFailedAlertShown(Times times)
        {
            _alertServiceMock.Verify(a => a.ShowAlert(AlertConstants.SetupByUriErrorTitle, AlertConstants.SetupByUriErrorMessage), times);
        }

        private void VerifySuccessAlertShown(Times times)
        {
            _alertServiceMock.Verify(a => a.ShowAlert(AlertConstants.SetupByUriSuccessTitle, AlertConstants.SetupByUriSuccessMessage), times);
        }
    }
}