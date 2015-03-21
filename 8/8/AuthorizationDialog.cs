using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using StaticValuesDll;
using WaterGate.Models;
using System.IO;

namespace WaterGate
{
    public partial class AuthorizationDialog : MetroForm
    {
        public AuthorizationDialog()
        {
            InitializeComponent();

            CheckIntegrity();
            base.Select();
            LoadSettings();
        }

        private void CheckIntegrity()
        {
            var path = string.Format("{0}.exe", Application.ProductName);

            var checkSum = string.Empty;
            using (var md5 = new MD5CryptoServiceProvider())
            {
                byte[] checkBytes = md5.ComputeHash(File.ReadAllBytes(path));
                checkSum = BitConverter.ToString(checkBytes).Replace("-", String.Empty);
            }

            string integrity = File.ReadAllText("integrity.txt");
            if (Equals(checkSum, integrity))
            {
                MessageBox.Show("Проверка на целостность прошла успешно.", "Проверка целостности", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Проверка на целостность не пройдена. Приложение будет закрыто.", "Проверка целостности", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
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

            if (!Uri.IsWellFormedUriString(ProxyAddressTextBox.Text.Trim(), UriKind.RelativeOrAbsolute))
            {
                MessageBox.Show("Прокси-сервер неверно задан.", "Введите корректный прокси-сервер", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            if (!string.IsNullOrEmpty(ProxyAddressTextBox.Text.Trim()))
                SetManualSettingProxyServer();
            else
                DisableProxyServer();

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

            ProxyAddressTextBox.Text = Properties.Settings.Default.ProxyServer;
            ProxyPortUpDown.Value = Properties.Settings.Default.ProxyPort;
            ProxyLoginTextBox.Text = Properties.Settings.Default.ProxyLogin;
            ProxyPasswordTextBox.Text = Properties.Settings.Default.ProxyPassword;
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Login = LoginTextBox.Text.Trim();
            Properties.Settings.Default.Server = ServerAddressTextBox.Text.Trim();
            Properties.Settings.Default.Port = PortTextBox.Text.Trim();

            Properties.Settings.Default.ProxyServer = ProxyAddressTextBox.Text.Trim();
            Properties.Settings.Default.ProxyPort = (ushort)ProxyPortUpDown.Value;
            Properties.Settings.Default.ProxyLogin = ProxyLoginTextBox.Text.Trim();
            Properties.Settings.Default.ProxyPassword = ProxyPasswordTextBox.Text.Trim();

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Устанавливает ручные настройки прокси-сервера
        /// </summary>
        private void SetManualSettingProxyServer()
        {
            try
            {
                //ServicePointManager.Expect100Continue = false;
                var proxyServer = string.Format("{0}:{1}", ProxyAddressTextBox.Text.Trim(), ProxyPortUpDown.Value);

                var wproxy = new WebProxy(proxyServer, false)
                {
                    Credentials = new NetworkCredential(ProxyLoginTextBox.Text.Trim(), ProxyPasswordTextBox.Text.Trim())
                };
                WebRequest.DefaultWebProxy = wproxy;
            }
            catch (Exception err)
            {
                MessageBox.Show("Не удалось установить ручные настройки прокси-сервера.\n\n" + err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Устанавливает системные настройки прокси-сервера
        /// </summary>
        private static void DisableProxyServer()
        {
            try
            {
                WebRequest.DefaultWebProxy = null;
            }
            catch (Exception err)
            {
                MessageBox.Show("Не удалось отключить прокси-сервер.\n\n" + err.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
