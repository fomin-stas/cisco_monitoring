using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using WaterGate.Models;

namespace WaterGate
{
    public partial class alarms : MetroForm
    {
        public alarms()
        {
            InitializeComponent();
        }

        private void alarms_Load(object sender, EventArgs e)
        {
            var serviceContext = new WaterGateServiceContext();

      
            serviceContext.GetAlarmAsync((result, error) =>
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
                    foreach (var alarm in result)
                    {

                        if (alarm.Execute == 1)
                        {

                            this.AlarmDataGridView.Rows.Add(alarm.Name, true);

                        }
                        else

                            this.AlarmDataGridView.Rows.Add(alarm.Name, false);

                    }

                    Cursor = Cursors.Arrow;
                    this.AlarmDataGridView.ReadOnly = false;
                  

                }));
            });
        }

      
        private void leveldataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void AlarmDataGridView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 1 & this.AlarmDataGridView.Columns[1].ReadOnly == false)
            {      var alarm = new StaticValuesDll.AlarmList();
                    alarm.Name = this.AlarmDataGridView.Rows[e.RowIndex].Cells[0].ToString();
                    alarm.Execute = Convert.ToInt32(this.AlarmDataGridView.Rows[e.RowIndex].Cells[1].Value);
                
                   var serviceContext = new WaterGateServiceContext();
                serviceContext.ChangeAlarmAsync(alarm, (result, error) =>
                {
                    if (error != null)
                    {
                        Invoke(new Action(() => MessageBox.Show("Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                                          "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                    }
                    else if (result)
                    {
                        //Invoke(new Action(() => this.AlarmDataGridView.Rows[e.RowIndex].Cells[1].Value = result));
                    }
                    else
                    {
                        Invoke(new Action(() => MessageBox.Show("Что-то пошло не так.", "Что-то пошло не так.", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                    }

                    Invoke(new Action(() =>
                    {
                        Cursor = Cursors.Arrow;

                       
                    }));
                });
               
                
                MessageBox.Show(alarm.Execute.ToString());
               

            }
        }
       
    }
}
