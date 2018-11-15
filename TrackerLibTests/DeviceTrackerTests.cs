using Xunit;

namespace TrackerLibTests
{
    // Naming convention: MethodName_Conditions_ExpectedResult
    public class DeviceTrackerTests
    {
        [Fact]
        public void Initialized_TrackingStartedAndUserLogsOut_SendsDeviceEndedUsage()
        {

        }

        [Fact]
        public void Initialized_TrackingStartedAndUserLogsIn_SendsDeviceStartedUsage()
        {

        }

        [Fact]
        public void Initialized_TrackingStartedAndComputerSleeps_SendsDeviceEndedUsage()
        {

        }

        [Fact]
        public void Initialized_TrackingStartedAndComputerWakes_SendsDeviceStartedUsage()
        {

        }

        [Fact]
        public void Initialized_TrackingStartedAndCurrentUserChanges_SendsDeviceEndAndStart()
        {

        }

        [Fact]
        public void Initialized_TrackingStoppedAndUserLogsOut_DoesNothing()
        {
          
        }
    }
}
