namespace WaterGate
{
    partial class AuthorizationDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorizationDialog));
            this.lLogin = new System.Windows.Forms.Label();
            this.lPas = new System.Windows.Forms.Label();
            this.LoginTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LoginButton = new System.Windows.Forms.Button();
            this.ServerAddressLabel = new System.Windows.Forms.Label();
            this.ServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lLogin
            // 
            resources.ApplyResources(this.lLogin, "lLogin");
            this.lLogin.Name = "lLogin";
            // 
            // lPas
            // 
            resources.ApplyResources(this.lPas, "lPas");
            this.lPas.Name = "lPas";
            // 
            // LoginTextBox
            // 
            resources.ApplyResources(this.LoginTextBox, "LoginTextBox");
            this.LoginTextBox.Name = "LoginTextBox";
            this.LoginTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AuthorizationDialog_KeyUp);
            // 
            // PasswordTextBox
            // 
            resources.ApplyResources(this.PasswordTextBox, "PasswordTextBox");
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AuthorizationDialog_KeyUp);
            // 
            // LoginButton
            // 
            resources.ApplyResources(this.LoginButton, "LoginButton");
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // ServerAddressLabel
            // 
            resources.ApplyResources(this.ServerAddressLabel, "ServerAddressLabel");
            this.ServerAddressLabel.Name = "ServerAddressLabel";
            // 
            // ServerAddressTextBox
            // 
            resources.ApplyResources(this.ServerAddressTextBox, "ServerAddressTextBox");
            this.ServerAddressTextBox.Name = "ServerAddressTextBox";
            this.ServerAddressTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AuthorizationDialog_KeyUp);
            // 
            // AuthorizationDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ServerAddressTextBox);
            this.Controls.Add(this.ServerAddressLabel);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.LoginTextBox);
            this.Controls.Add(this.lPas);
            this.Controls.Add(this.lLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthorizationDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lLogin;
        private System.Windows.Forms.Label lPas;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label ServerAddressLabel;
        private System.Windows.Forms.TextBox ServerAddressTextBox;
    }
}