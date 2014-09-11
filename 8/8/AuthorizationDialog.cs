using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using StaticValuesDll;
using WaterGate.Models;

namespace WaterGate
{
    public partial class AuthorizationDialog : MetroForm
    {
        public AuthorizationDialog()
        {
            InitializeComponent();

            base.Select();
            LoadSettings();
        }

        private bool IsInputDataValid()
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text) || LoginTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Логин не может быть пустым.", "Введите логин", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrEmpty(PasswordTextBox.Text) || PasswordTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Пароль не может быть пустым.", "Введите пароль", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (string.IsNullOrEmpty(ServerAddressTextBox.Text) || ServerAddressTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Адрес сервера не может быть пустым.", "Введите адрес сервера", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            if (string.IsNullOrEmpty(PortTextBox.Text) || PortTextBox.Text.Trim().Length == 0)
            {
                MessageBox.Show("Порт не может быть пустым.", "Введите порт", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (!IsInputDataValid())
                return;
            LoginButton.Enabled = false;
            Cursor = Cursors.AppStarting;

            Settings.Initialize(ServerAddressTextBox.Text.Trim(), PortTextBox.Text.Trim());
            var serviceContext = new WaterGateServiceContext();

            const string errorText = "Отсутствует соединение к серверу. Проверьте правильно адреса сервера.";

            try
            {
                serviceContext.SignInAsync(LoginTextBox.Text.Trim(), PasswordTextBox.Text.Trim(), (result, error) =>
                {
                    if (error != null)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show(errorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            LoginButton.Enabled = true;
                            Cursor = Cursors.Arrow;
                        }));
                        return;
                    }

                    if (result.User.Permissions == Permissions.None)
                    {
                        Invoke(new Action(() =>
                        {
                            MessageBox.Show("Пользователя с данным логином и паролем не существует.", "В доступе отказано", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoginButton.Enabled = true;
                            Cursor = Cursors.Arrow;
                        }));
                        return;
                    }

                    _authorizationToken = result;

                    SaveSettings();

                    Invoke(new Action(() =>
                    {
                        DialogResult = DialogResult.OK;
                    }));
                });
            }
            catch (Exception)
            {
                MessageBox.Show(errorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoginButton.Enabled = true;
                Cursor = Cursors.Arrow;
            }
        }

        private AuthorizationToken _authorizationToken;

        public AuthorizationToken ShowAuthorizationDialog()
        {
            return ShowDialog() == DialogResult.OK ? _authorizationToken : new AuthorizationToken(new User(){Permissions = Permissions.None}, null);
        }

        private void AuthorizationDialog_KeyUp(object sender, KeyEventArgs e)
        {
           if (e.KeyCode == Keys.Return)
            {
                LoginButton_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void PortTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
        && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LoadSettings()
        {
            LoginTextBox.Text = Properties.Settings.Default.Login;
            ServerAddressTextBox.Text = Properties.Settings.Default.Server;
            PortTextBox.Text = Properties.Settings.Default.Port;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Login = LoginTextBox.Text.Trim();
            Properties.Settings.Default.Server = ServerAddressTextBox.Text.Trim();
            Properties.Settings.Default.Port = PortTextBox.Text.Trim();

            Properties.Settings.Default.Save();
        }

        
    }
}
