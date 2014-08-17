using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StaticValuesDll;
using WaterGate.Models;

namespace WaterGate
{
    public partial class UsersManage : Form
    {
        public UsersManage()
        {
            InitializeComponent();
        }



        private void UsersManage_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.AppStarting;
            var serviceContext = new WaterGateServiceContext();

            serviceContext.GetUsersAsync((result, error) =>
            {
                if (error != null)
                {
                    MessageBox.Show("Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                       "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Invoke(new Action(this.Close));
                    return;
                }

                Invoke(new Action(() =>
                {
                    foreach (var user in result)
                    {
                        usersDataGridView.Rows.Add(user.Login, ConvertPermissionsToString(user.Permissions));
                    }

                    Cursor = Cursors.Arrow;

                    AddButton.Enabled = true;
                    if(result.Length > 1)
                        RemoveButton.Enabled = true; 
                }));
            });
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var user = (new UserDialog()).GetUser();
            if (user == null)
                return;

            AddButton.Enabled = false;
            RemoveButton.Enabled = false;
            Cursor = Cursors.AppStarting;

            var serviceContext = new WaterGateServiceContext();
            serviceContext.AddUserAsync(user, (result, error) =>
            {
                if (error != null)
                {
                    Invoke(new Action(() => MessageBox.Show("Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                                      "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
                else if (result)
                {
                    Invoke(new Action(() => usersDataGridView.Rows.Add(user.Login, ConvertPermissionsToString(user.Permissions))));
                }
                else
                {
                    Invoke(new Action(() => MessageBox.Show("Пользователь с данным логином уже существует.",
                                       "Логин занят", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                }

                Invoke(new Action(() =>
                {
                    Cursor = Cursors.Arrow;

                    AddButton.Enabled = true;
                    if (usersDataGridView.RowCount > 1)
                        RemoveButton.Enabled = true;
                }));
            });
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Требуется выбрать строку для удаления.", "Выберите строку", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var index = usersDataGridView.SelectedCells[0].RowIndex;

            AddButton.Enabled = false;
            RemoveButton.Enabled = false;
            Cursor = Cursors.AppStarting;

            var serviceContext = new WaterGateServiceContext();
            serviceContext.RemoveUserAsync(usersDataGridView.Rows[index].Cells[0].Value as string, (result, error) =>
            {
                if (error != null)
                {
                    Invoke(new Action(() => MessageBox.Show( "Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)));

                }
                else if (result)
                {
                    Invoke(new Action(() => usersDataGridView.Rows.RemoveAt(index)));
                }
                else
                {
                    Invoke(new Action(() => MessageBox.Show("Должен остаться хотя бы один администратор.",
                                      "Нельзя удалить единственного администратора", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                }

                Invoke(new Action(() =>
                {
                    Cursor = Cursors.Arrow;

                    AddButton.Enabled = true;
                    if (usersDataGridView.RowCount > 1)
                        RemoveButton.Enabled = true;
                }));
            });
        }

        private string ConvertPermissionsToString(Permissions permissions)
        {
            switch (permissions)
            {
                case Permissions.Administrator:
                {
                    return "Администратор";
                }
                case Permissions.User:
                {
                    return "Пользователь";
                }
            }

            return "Неизвестно";
        }

        
    }
}
