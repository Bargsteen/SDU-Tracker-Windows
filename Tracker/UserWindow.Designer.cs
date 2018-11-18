namespace Tracker
{
    partial class UserWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SaveChangesButton = new System.Windows.Forms.Button();
            this.userListBox = new System.Windows.Forms.ListBox();
            this.CreateNewUserButton = new System.Windows.Forms.Button();
            this.DeleteChosenUserButton = new System.Windows.Forms.Button();
            this.ChooseUserLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.ClearChangesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SaveChangesButton
            // 
            this.SaveChangesButton.Location = new System.Drawing.Point(358, 152);
            this.SaveChangesButton.Name = "SaveChangesButton";
            this.SaveChangesButton.Size = new System.Drawing.Size(117, 23);
            this.SaveChangesButton.TabIndex = 0;
            this.SaveChangesButton.Text = "Gem ændringerne";
            this.SaveChangesButton.UseVisualStyleBackColor = true;
            this.SaveChangesButton.Click += new System.EventHandler(this.SaveChangesButtonClicked);
            // 
            // userListBox
            // 
            this.userListBox.FormattingEnabled = true;
            this.userListBox.Location = new System.Drawing.Point(12, 27);
            this.userListBox.Name = "userListBox";
            this.userListBox.Size = new System.Drawing.Size(463, 108);
            this.userListBox.TabIndex = 1;
            // 
            // CreateNewUserButton
            // 
            this.CreateNewUserButton.Location = new System.Drawing.Point(12, 152);
            this.CreateNewUserButton.Name = "CreateNewUserButton";
            this.CreateNewUserButton.Size = new System.Drawing.Size(75, 23);
            this.CreateNewUserButton.TabIndex = 2;
            this.CreateNewUserButton.Text = " Opret ny";
            this.CreateNewUserButton.UseVisualStyleBackColor = true;
            this.CreateNewUserButton.Click += new System.EventHandler(this.CreateNewUserButtonClicked);
            // 
            // DeleteChosenUserButton
            // 
            this.DeleteChosenUserButton.Location = new System.Drawing.Point(93, 152);
            this.DeleteChosenUserButton.Name = "DeleteChosenUserButton";
            this.DeleteChosenUserButton.Size = new System.Drawing.Size(75, 23);
            this.DeleteChosenUserButton.TabIndex = 3;
            this.DeleteChosenUserButton.Text = " Slet valgte";
            this.DeleteChosenUserButton.UseVisualStyleBackColor = true;
            this.DeleteChosenUserButton.Click += new System.EventHandler(this.DeleteChosenUserButtonClicked);
            // 
            // ChooseUserLabel
            // 
            this.ChooseUserLabel.AutoSize = true;
            this.ChooseUserLabel.Location = new System.Drawing.Point(9, 11);
            this.ChooseUserLabel.Name = "ChooseUserLabel";
            this.ChooseUserLabel.Size = new System.Drawing.Size(71, 13);
            this.ChooseUserLabel.TabIndex = 4;
            this.ChooseUserLabel.Text = " Vælg bruger:";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.ErrorLabel.Location = new System.Drawing.Point(321, 9);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(154, 13);
            this.ErrorLabel.TabIndex = 5;
            this.ErrorLabel.Text = "Du skal have mindst én bruger.";
            this.ErrorLabel.Visible = false;
            // 
            // ClearChangesButton
            // 
            this.ClearChangesButton.Location = new System.Drawing.Point(225, 152);
            this.ClearChangesButton.Name = "ClearChangesButton";
            this.ClearChangesButton.Size = new System.Drawing.Size(127, 23);
            this.ClearChangesButton.TabIndex = 6;
            this.ClearChangesButton.Text = " Ryd ændringer";
            this.ClearChangesButton.UseVisualStyleBackColor = true;
            this.ClearChangesButton.Click += new System.EventHandler(this.UserWindowLoaded);
            // 
            // UserWindow
            // 
            this.AcceptButton = this.SaveChangesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 181);
            this.Controls.Add(this.ClearChangesButton);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.ChooseUserLabel);
            this.Controls.Add(this.DeleteChosenUserButton);
            this.Controls.Add(this.CreateNewUserButton);
            this.Controls.Add(this.userListBox);
            this.Controls.Add(this.SaveChangesButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UserWindow";
            this.Text = "Brugere";
            this.Load += new System.EventHandler(this.UserWindowLoaded);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SaveChangesButton;
        private System.Windows.Forms.ListBox userListBox;
        private System.Windows.Forms.Button CreateNewUserButton;
        private System.Windows.Forms.Button DeleteChosenUserButton;
        private System.Windows.Forms.Label ChooseUserLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Button ClearChangesButton;
    }
}

