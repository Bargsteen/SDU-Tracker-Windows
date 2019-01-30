using Tracker.Implementations;
using Xunit;

namespace TrackerTests
{
    public class ActiveWindowServiceTests
    {
        //Tested MakeIdentifier by making it public. 
        /*[Theory]
        [InlineData("ApplicationFrameHost", "Google and 1 more page ?- Microsoft Edge", "ApplicationFrameHost - Microsoft Edge")]
        [InlineData("ApplicationFrameHost", "New tab and 1 more page ?- Microsoft Edge", "ApplicationFrameHost - Microsoft Edge")]
        [InlineData("Git Gui", "", "Git Gui")]
        [InlineData("devenv", "Tracker (Running) - Microsoft Visual Studio", "devenv - Microsoft Visual Studio")]
        [InlineData("chrome", "Untitled - Google Chrome", "chrome - Google Chrome")]
        [InlineData("chrome", "Notice Wide dash – Google Chrome", "chrome - Google Chrome")]
        [InlineData("chrome", "Regular dash - and wide – Google Chrome", "chrome - Google Chrome")]
        public void MakeIdentifier__VariousInputs__ExpectedResult(string processName, string windowName, string expected)
        {
            // Act
            string actual = ActiveWindowService.MakeIdentifier(processName, windowName);

            // Assert
            Assert.Equal(expected, actual);
        }*/
    }
}