using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using StaticValuesDll;
using WaterGate.Models;

namespace WaterGate
{
    public partial class AuthorizationDialog : Form
    {
        public AuthorizationDialog()
        {
            InitializeComponent();
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

            return true;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            if (!IsInputDataValid())
                return;
            LoginButton.Enabled = false;
            Cursor = Cursors.AppStarting;

            Settings.Initialize(ServerAddressTextBox.Text.Trim());
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

                    if (result.Permissions == Permissions.None)
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
            return ShowDialog() == DialogResult.OK ? _authorizationToken : new AuthorizationToken(Permissions.None, null);
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

        
    }
}
