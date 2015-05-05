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

                    this.AlarmDataGridView.Columns[1].ReadOnly = false;

                }));
            });
        }

       
    }
}
