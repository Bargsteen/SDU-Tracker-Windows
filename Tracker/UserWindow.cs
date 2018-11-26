using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tracker.Interfaces;

namespace Tracker
{
    public partial class UserWindow : Form, IUserWindow
    {
        private List<string> _unsavedUserList;
        private readonly ISettings _settings;
        private readonly CreateUserWindow _createUserWindow;


        public UserWindow(ISettings settings)
        {
            InitializeComponent();
            _settings = settings;
            _unsavedUserList = new List<string>();
            _createUserWindow = new CreateUserWindow();

            userListBox.DataSource = _unsavedUserList;
        }

        private void UserWindowLoaded(object sender, EventArgs e)
        {
            CenterToScreen();

            _unsavedUserList = new List<string>(_settings.Users);

            UpdateView();
        }

        public string ShowWindow()
        {
            if (!Visible)
            {
                ShowDialog();
            }
            // The current user after interaction with the window
            return _settings.CurrentUser;
        }

        private void SaveChangesButtonClicked(object sender, EventArgs e)
        {
            int selectedIndex = userListBox.SelectedIndex;

            if(_unsavedUserList.ElementAtOrDefault(selectedIndex) is string chosenUser)
            {
                _settings.Users = new List<string>(_unsavedUserList);
                _settings.CurrentUser = chosenUser;
            }

            Close();
        }

        private void DeleteChosenUserButtonClicked(object sender, EventArgs e)
        {
            int selectedIndex = userListBox.SelectedIndex;

            if (_unsavedUserList.ElementAtOrDefault(selectedIndex) != null)
            {
                _unsavedUserList.RemoveAt(selectedIndex);
                UpdateView();
            }
        }

        private void CreateNewUserButtonClicked(object sender, EventArgs e)
        {
            var result = _createUserWindow.ShowDialog();
            if (result == DialogResult.OK) // A new name was entered and create was pressed
            {
                _unsavedUserList.Add(_createUserWindow.NameOfNewUser);
            }

            UpdateView();
        }

        private void UpdateView()
        {
            userListBox.DataSource = null;
            userListBox.DataSource = _unsavedUserList;

            if (_unsavedUserList.Count != 0)
            {
                userListBox.SelectedIndex = GetIndexOfCurrentUserOrZero();
            }

            if (userListBox.Items.Count != 0)
            {
                DeleteChosenUserButton.Enabled = true;
                SaveChangesButton.Enabled = true;
                ErrorLabel.Visible = false;
            }
            else
            {
                DeleteChosenUserButton.Enabled = false;
                SaveChangesButton.Enabled = false;
                ErrorLabel.Visible = true;
            }
        }

        private int GetIndexOfCurrentUserOrZero()
        {
            // Returns -1 if not found.
            int index = _unsavedUserList.IndexOf(_settings.CurrentUser);

            return index != -1 ? index : 0;
        }
    }
}
