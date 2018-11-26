using Moq;
using Tracker.Implementations;
using Tracker.Interfaces;
using Xunit;

namespace TrackerTests
{
    public class ResendServiceTests
    {
        public ResendServiceTests()
        {
            _sendOrSaveService = new Mock<ISendOrSaveService>();
            _sleepService = new Mock<ISleepService>();

            _resendService = new ResendService(_sendOrSaveService.Object, _sleepService.Object);

        }

        private readonly IResendService _resendService;
        private readonly Mock<ISendOrSaveService> _sendOrSaveService;
        private readonly Mock<ISleepService> _sleepService;

        [Fact]
        public void StartPeriodicResendingOfSavedUsages__NoConditions__InvokesSendSomeUsagesFromPersistenceAndSleeps()
        {
            const int secondsToSleep = 0;
            const int limitOfEachUsage = 5;

            // Act
            _resendService.StartPeriodicResendingOfSavedUsages(secondsToSleep, limitOfEachUsage);

            // Assert
            _sendOrSaveService.Verify(s => s.SendSomeUsagesFromPersistence(limitOfEachUsage), Times.Once);
            _sleepService.Verify(s => s.SleepFor(secondsToSleep));
        }
    }
}