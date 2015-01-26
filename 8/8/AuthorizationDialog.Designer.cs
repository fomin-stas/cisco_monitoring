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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ProxyPortUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ProxyAddressTextBox = new System.Windows.Forms.TextBox();
            this.ProxyPasswordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ProxyLoginTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyPortUpDown)).BeginInit();
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PortTextBox);
            this.groupBox1.Controls.Add(this.portLabel);
            this.groupBox1.Controls.Add(this.ServerAddressLabel);
            this.groupBox1.Controls.Add(this.ServerAddressTextBox);
            this.groupBox1.Controls.Add(this.PasswordTextBox);
            this.groupBox1.Controls.Add(this.lLogin);
            this.groupBox1.Controls.Add(this.LoginTextBox);
            this.groupBox1.Controls.Add(this.lPas);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // PortTextBox
            // 
            resources.ApplyResources(this.PortTextBox, "PortTextBox");
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AuthorizationDialog_KeyUp);
            this.PortTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PortTextBox_KeyPress);
            // 
            // portLabel
            // 
            resources.ApplyResources(this.portLabel, "portLabel");
            this.portLabel.Name = "portLabel";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ProxyPortUpDown);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ProxyAddressTextBox);
            this.groupBox2.Controls.Add(this.ProxyPasswordTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.ProxyLoginTextBox);
            this.groupBox2.Controls.Add(this.label4);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // ProxyPortUpDown
            // 
            resources.ApplyResources(this.ProxyPortUpDown, "ProxyPortUpDown");
            this.ProxyPortUpDown.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.ProxyPortUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ProxyPortUpDown.Name = "ProxyPortUpDown";
            this.ProxyPortUpDown.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ProxyAddressTextBox
            // 
            resources.ApplyResources(this.ProxyAddressTextBox, "ProxyAddressTextBox");
            this.ProxyAddressTextBox.Name = "ProxyAddressTextBox";
            // 
            // ProxyPasswordTextBox
            // 
            resources.ApplyResources(this.ProxyPasswordTextBox, "ProxyPasswordTextBox");
            this.ProxyPasswordTextBox.Name = "ProxyPasswordTextBox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // ProxyLoginTextBox
            // 
            resources.ApplyResources(this.ProxyLoginTextBox, "ProxyLoginTextBox");
            this.ProxyLoginTextBox.Name = "ProxyLoginTextBox";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // AuthorizationDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LoginButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthorizationDialog";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProxyPortUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lLogin;
        private System.Windows.Forms.Label lPas;
        private System.Windows.Forms.TextBox LoginTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.Label ServerAddressLabel;
        private System.Windows.Forms.TextBox ServerAddressTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown ProxyPortUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ProxyAddressTextBox;
        private System.Windows.Forms.TextBox ProxyPasswordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ProxyLoginTextBox;
        private System.Windows.Forms.Label label4;
    }
}