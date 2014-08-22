using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using StaticValuesDll;

namespace WaterGate
{
    public partial class UserDialog : MetroForm
    {
        private User _user;

        public UserDialog()
        {
            InitializeComponent();

            var permissionsContainers = new[]
            {
                new PermissionsContainer() {Name = "Администратор", Permissions = Permissions.Administrator},
                new PermissionsContainer() {Name = "Пользователь", Permissions = Permissions.User}
            };

            PermissionsComboBox.DataSource = permissionsContainers;
            PermissionsComboBox.DisplayMember = "Name";
            PermissionsComboBox.SelectedItem = permissionsContainers[0];
        }

        public User GetUser()
        {
            return ShowDialog() == DialogResult.OK ? _user : null;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (!IsInputDataValid())
            {
                return;
            }

            _user = new User()
            {
                Login = LoginTextBox.Text.Trim(),
                Password = PasswordTextBox.Text.Trim(),
                Permissions = ((PermissionsContainer)PermissionsComboBox.SelectedItem).Permissions
            };

            DialogResult = DialogResult.OK;
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

            return true;
        }

        public class PermissionsContainer
        {
            public string Name { get; set; }
            public Permissions Permissions { get; set; }
        }
    }
}
