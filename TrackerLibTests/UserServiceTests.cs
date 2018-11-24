using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TrackerLib.Implementations;
using TrackerLib.Interfaces;
using Xunit;

namespace TrackerLibTests
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;

        private readonly Mock<IAlertService> _alertServiceMock;
        private readonly Mock<ISettings> _settingsMock;
        private readonly Mock<IUserWindow> _userWindowMock;

        private readonly List<string> _onUserSessionStartedUsers;
        private readonly List<string> _onUserSessionEndedUsers;

        private int OnUserSessionStartedInvokes => _onUserSessionStartedUsers.Count;
        private int OnUserSessionEndedInvokes => _onUserSessionEndedUsers.Count;

        public UserServiceTests()
        {
            _alertServiceMock = new Mock<IAlertService>();
            _settingsMock = new Mock<ISettings>();
            _userWindowMock = new Mock<IUserWindow>();

            _userService = new UserService(_alertServiceMock.Object, _settingsMock.Object, _userWindowMock.Object);

            _onUserSessionStartedUsers = new List<string>();
            _onUserSessionEndedUsers = new List<string>();

            // Collect the users from the events
            _userService.OnUserSessionStarted += (sender, args) => { _onUserSessionStartedUsers.Add(args.User); };
            _userService.OnUserSessionEnded += (sender, args) => { _onUserSessionEndedUsers.Add(args.User); };
        }

   
        ///////////////////////////////////////////////////////////////////////
        //                     CHECK IF USER HAS CHANGED                     //
        ///////////////////////////////////////////////////////////////////////

        [Fact]
        public void CheckIfUserHasChanged__SingleUser__NoAlertsShown_InvokesOnUserSessionStarted()
        {
            // Arrange
            const string user = "A";
            SettingsUserCount(1);
            SettingsCurrentUser(user);
            
            // Act
            _userService.CheckIfUserHasChanged();

            // Assert
            VerifyAlertServiceShowAlertReturnYesWasPressed(Times.Never());
            Assert.Equal(1, OnUserSessionStartedInvokes);
            Assert.Equal(0, OnUserSessionEndedInvokes);

            Assert.Equal(user, _onUserSessionStartedUsers.FirstOrDefault());
        }

        [Fact]
        public void CheckIfUserHasChanged__MultipleUsers__ChangeUserAlertShown()
        {
            // Arrange
            SettingsUserCount(2);

            // Act
            _userService.CheckIfUserHasChanged();

            // Assert
            VerifyAlertServiceShowAlertReturnYesWasPressed(Times.Once());
        }

        [Fact]
        public void CheckIfUserHasChanged__MultipleUsers_SameUser__InvokesOnUserSessionStarted()
        {
            // Arrange
            const string user = "A";
            SettingsUserCount(2);
            SettingsCurrentUser(user);
            SetupReturnForShowAlertReturnYesWasPressed(true);

            // Act
            _userService.CheckIfUserHasChanged();

            // Assert
            VerifyShowUserWindow(Times.Never());
            Assert.Equal(0, OnUserSessionEndedInvokes);
            Assert.Equal(1, OnUserSessionStartedInvokes);
            Assert.Equal(user, _onUserSessionStartedUsers.FirstOrDefault());
        }

        [Fact]
        public void CheckIfUserHasChanged__MultipleUsers_DifferentUser__ShowsUserWindow()
        {
            // Arrange
            SettingsUserCount(3);
            SetupReturnForShowAlertReturnYesWasPressed(false);

            // Act
            _userService.CheckIfUserHasChanged();

            // Assert
            VerifyShowUserWindow(Times.Once());
        }

        [Fact]
        public void CheckIfUserHasChanged__UserWindowReturnsSameUser_InvokesOnUserSessionStartedWithSameUser()
        {
            // Arrange
            const string user = "A";
            SettingsUserCount(3);
            SettingsCurrentUser(user);
            SetupReturnForShowAlertReturnYesWasPressed(false);
            ShowUserWindowReturns(user);

            // Act
            _userService.CheckIfUserHasChanged();

            // Assert
            Assert.Equal(1, OnUserSessionStartedInvokes);
            Assert.Equal(0, OnUserSessionEndedInvokes);

            Assert.Equal(user, _onUserSessionStartedUsers.FirstOrDefault());
        }

        [Fact]
        public void CheckIfUserHasChanged__UserWindowChangesCurrentUser__InvokesOnUserSessionStartedWithNewUser()
        {
            // Arrange
            const string previousUser = "A";
            const string newUser = "B";
            SettingsUserCount(2);
            _settingsMock.SetupProperty(s => s.CurrentUser, previousUser);
            SetupReturnForShowAlertReturnYesWasPressed(false);
            ShowUserWindowReturns(newUser);
            _userWindowMock.Setup(u => u.ShowWindow()).Returns(() =>
            {
                // This mimics that the user chooses the newUser
                _settingsMock.Object.CurrentUser = newUser;
                return newUser;
            });

            // Act
            _userService.CheckIfUserHasChanged();

            // Assert
            Assert.Equal(1, OnUserSessionStartedInvokes);
            Assert.Equal(0, OnUserSessionEndedInvokes);

            Assert.Equal(newUser, _onUserSessionStartedUsers.FirstOrDefault());
        }


        ///////////////////////////////////////////////////////////////////////
        //                          SHOW USER WINDOW                         //
        ///////////////////////////////////////////////////////////////////////

        [Fact]
        public void ShowUserWindow__NoConditions__ShowsUserWindow()
        {
            // Arrange

            // Act
            _userService.ShowUserWindow();

            // Assert
            VerifyShowUserWindow(Times.Once());
        }

        [Fact]
        public void ShowUserWindow__UserWindowReturnsSameUser__NothingHappens()
        {
            // Arrange
            const string user = "A";
            SettingsCurrentUser(user);
            ShowUserWindowReturns(user);

            // Act
            _userService.ShowUserWindow();

            // Assert
            Assert.Equal(0, OnUserSessionStartedInvokes);
            Assert.Equal(0, OnUserSessionEndedInvokes);
        }

        [Fact]
        public void ShowUserWindow__UserWindowReturnsDifferentUser__InvokesCorrectEndedAndStarted()
        {
            // Arrange
            const string previousUser = "A";
            const string newUser = "B";
            SettingsCurrentUser(previousUser);
            ShowUserWindowReturns(newUser);

            // Act
            _userService.ShowUserWindow();

            // Assert
            Assert.Equal(1, OnUserSessionEndedInvokes);
            Assert.Equal(1, OnUserSessionStartedInvokes);

            Assert.Equal(previousUser, _onUserSessionEndedUsers.FirstOrDefault());
            Assert.Equal(newUser, _onUserSessionStartedUsers.FirstOrDefault());
        }


        ///////////////////////////////////////////////////////////////////////
        //                              HELPERS                              //
        ///////////////////////////////////////////////////////////////////////

        private void VerifyAlertServiceShowAlertReturnYesWasPressed(Times times)
        {
            _alertServiceMock.Verify(a => a.ShowAlertReturnYesWasPressed(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                times);
        }

        private void SettingsUserCount(int userCount)
        {
            _settingsMock.Setup(s => s.UserCount).Returns(userCount);
        }

        private void SettingsCurrentUser(string user)
        {
            _settingsMock.Setup(s => s.CurrentUser).Returns(user);
        }

        private void SetupReturnForShowAlertReturnYesWasPressed(bool returnValue)
        {
            // True means continue with current user. False means change is needed.
            _alertServiceMock.Setup(a => a.ShowAlertReturnYesWasPressed(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(returnValue);
        }

        private void VerifyShowUserWindow(Times times)
        {
            _userWindowMock.Verify(u => u.ShowWindow(), times);
        }

        private void ShowUserWindowReturns(string user)
        {
            _userWindowMock.Setup(u => u.ShowWindow()).Returns(user);
        }
    }
}