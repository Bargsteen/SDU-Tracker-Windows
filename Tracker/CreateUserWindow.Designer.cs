namespace Tracker
{
    partial class CreateUserWindow
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
            this.CreateButton = new System.Windows.Forms.Button();
            this.NewUserTextField = new System.Windows.Forms.TextBox();
            this.EnterNameLabel = new System.Windows.Forms.Label();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(170, 60);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(75, 23);
            this.CreateButton.TabIndex = 0;
            this.CreateButton.Text = " Opret";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButtonClicked);
            // 
            // NewUserTextField
            // 
            this.NewUserTextField.Location = new System.Drawing.Point(12, 25);
            this.NewUserTextField.Name = "NewUserTextField";
            this.NewUserTextField.Size = new System.Drawing.Size(233, 20);
            this.NewUserTextField.TabIndex = 1;
            this.NewUserTextField.TextChanged += new System.EventHandler(this.NewUserTextFieldChanged);
            // 
            // EnterNameLabel
            // 
            this.EnterNameLabel.AutoSize = true;
            this.EnterNameLabel.Location = new System.Drawing.Point(12, 9);
            this.EnterNameLabel.Name = "EnterNameLabel";
            this.EnterNameLabel.Size = new System.Drawing.Size(69, 13);
            this.EnterNameLabel.TabIndex = 2;
            this.EnterNameLabel.Text = "Indtast navn:";
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(89, 60);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = " Annuller";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButtonClicked);
            // 
            // CreateUserWindow
            // 
            this.AcceptButton = this.CreateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(260, 95);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.EnterNameLabel);
            this.Controls.Add(this.NewUserTextField);
            this.Controls.Add(this.CreateButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CreateUserWindow";
            this.Text = "Opret bruger";
            this.Load += new System.EventHandler(this.CreateUserWindowLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.TextBox NewUserTextField;
        private System.Windows.Forms.Label EnterNameLabel;
        private System.Windows.Forms.Button CancelButton;
    }
}