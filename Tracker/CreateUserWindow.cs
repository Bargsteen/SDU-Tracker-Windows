using System;
using System.Windows.Forms;

namespace Tracker
{
    public partial class CreateUserWindow : Form
    {
        public string NameOfNewUser { get; set; }

        public CreateUserWindow()
        {
            InitializeComponent();
        }

        private void CreateUserWindowLoad(object sender, System.EventArgs e)
        {
            CenterToScreen();
            NameOfNewUser = "";
            NewUserTextField.Clear();
            CreateButton.Enabled = false;
        }

        private void CancelButtonClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void CreateButtonClicked(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            NameOfNewUser = NewUserTextField.Text;
            Close();
        }

        private void NewUserTextFieldChanged(object sender, EventArgs e)
        {
            CreateButton.Enabled = NewUserTextField.Text != "";
        }
    }
}
