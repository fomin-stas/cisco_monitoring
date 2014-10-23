using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MetroFramework.Forms;
using SnmpSharpNet;
using StaticValuesDll;
using WaterGate.Models;

namespace WaterGate
{
    public partial class PortsForm : MetroForm
    {
        private MainForm _mainForm;
        private bool _isIpCellEditing;
        private string _currentEditingPortName;

        public PortsForm(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;
            CiscoPort.DisplayMember = "PortName";
            CiscoIP.DisplayMember = "IP";

            this.CiscoIP.Items.Add("not set");

            foreach (IPCom key in StaticValues.CiscoList)
            {
                if (!string.IsNullOrEmpty(key.IP))
                {
                    
                    this.CiscoIP.Items.Add(key.IP);
                }
            }

            for (int i = 0; i < StaticValues.JDSUCiscoArray.Count; i++)
            {
                portsDataGridView.Rows.Add(StaticValues.JDSUCiscoArray[i].JDSUPort);
                int f = (portsDataGridView[1, i] as DataGridViewComboBoxCell).Items.IndexOf(StaticValues.JDSUCiscoArray[i].CiscoIPCom.IP);
               
                (portsDataGridView[1, i] as DataGridViewComboBoxCell).Value = (portsDataGridView[1, i] as DataGridViewComboBoxCell).Items[f];
               
                (portsDataGridView[2, i] as DataGridViewComboBoxCell).Items.Add(StaticValues.JDSUCiscoArray[i].CiscoPort);
                portsDataGridView.Rows[i].Cells[2].Value = (portsDataGridView[2, i] as DataGridViewComboBoxCell).Items[0];

            }
        }

       
        void formJDSUPort_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 0:
                {
                    UpdateCell(e.RowIndex, e.ColumnIndex);
                    break;
                }
                case 1:
                {
                    _isIpCellEditing = false;
                    break;
                }
                case 2:
                {
                    UpdateCell(e.RowIndex, e.ColumnIndex);
                    break;
                }
            }

        }

        private void UpdateCell(int row, int column)
        {           
            switch (column)
            {
                case 0:
                {
                    var objectValue = this.portsDataGridView[0, row].Value;
                    var value = objectValue == null ? string.Empty : objectValue.ToString();
                    
                    var ipCom = StaticValues.JDSUCiscoArray[row].CiscoIPCom;

                    if (StaticValues.JDSUCiscoArray.Count(item => item.JDSUPort.Equals(ipCom.IP)) > 1)
                    {
                        portsDataGridView[0, row].Value = "not set";
                        MessageBox.Show("Порт с данными параметрами уже отслеживается.", "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    StaticValues.JDSUCiscoArray[row].JDSUPort = value;
                    if (_mainForm != null)
                    {
                        _mainForm.UpdateCell(_currentEditingPortName, 0, value);
                    }
                    break;
                }
                case 1:
                {
                    var value = Convert.ToString((this.portsDataGridView[1, row] as DataGridViewComboBoxCell).FormattedValue);
                    if (value == "not set")
                    {
                        StaticValues.JDSUCiscoArray[row].CiscoIPCom = new IPCom("not set", "not set");
                    }
                    else
                    {
                        var name = Convert.ToString((this.portsDataGridView[1, row] as DataGridViewComboBoxCell).FormattedValue);
                        StaticValues.JDSUCiscoArray[row].CiscoIPCom = (IPCom)(this.portsDataGridView[1, row] as DataGridViewComboBoxCell).Items.Cast<IPCom>().First(item => item.IP.Equals(name));
                    }

                    if (_mainForm != null)
                    {
                        _mainForm.UpdateCell(this.portsDataGridView[0, row].Value.ToString(), 1, value);
                    }
                    break;
                }
                case 2:
                {
                    var value = Convert.ToString((this.portsDataGridView[2, row] as DataGridViewComboBoxCell).FormattedValue);
                    if (value == "not set")
                    {
                        StaticValues.JDSUCiscoArray[row].CiscoPort = new CiscoPort("not set", "not set");
                    }
                    else
                    {
                        var ciscoPort = (this.portsDataGridView[2, row] as DataGridViewComboBoxCell).Items.Cast<CiscoPort>().First(item=>item.PortName.Equals(value));

                        StaticValues.JDSUCiscoArray[row].CiscoPort = ciscoPort;
                    }

                    if (_mainForm != null)
                    {
                        _mainForm.UpdateCell(this.portsDataGridView[0, row].Value.ToString(), 2, value);
                    }
                    break;
                }
            }
        }

        void formJDSUPort_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (portsDataGridView.IsCurrentCellDirty)
            {
                portsDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

      /*  void formJDSUPort_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           if (e.ColumnIndex == 1 && e.RowIndex >= 0) //check if combobox column
            {
                object selectedValue = formJDSUPort.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
              
               StaticValues.PortList.Clear();
            
                (formJDSUPort.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Clear();
                (formJDSUPort.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Add("not set");
                formJDSUPort.Rows[e.RowIndex].Cells[2].Value = (formJDSUPort[2, e.RowIndex] as DataGridViewComboBoxCell).Items[0];
                if (Convert.ToString(formJDSUPort.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == "not set")
                {
                       return;
                }
               
               
             
                this.ComboBoxCiscoPort[u].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                this.ComboBoxCiscoPort[u].Items.Clear();
                this.ComboBoxCiscoPort[u].Items.Add("not set");
                this.ComboBoxCiscoPort[u].SelectedItem = "not set";
                if (Convert.ToString(this.ComboBoxCisco[u].SelectedItem) == "not set")
                {
                    this.ComboBoxCiscoPort[u].SelectedItem = "not set";
                    return;
                }

                String snmpAgent = Convert.ToString(this.ComboBoxCisco[u].SelectedItem);
                String snmpCom = StaticValues.CiscoList.Find(delegate(IPCom x)
                {
                    return x.IP.Contains(Convert.ToString(this.ComboBoxCisco[u].SelectedItem));
                }).Com;

                SimpleSnmp snmp = new SimpleSnmp(snmpAgent, snmpCom);
                Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.2.1.31.1.1.1.1");
                if (result == null)
                {
                    MessageBox.Show("result is null");

                }
                else
                {
                    foreach (KeyValuePair<Oid, AsnType> entry in result)
                    {
                        this.ComboBoxCiscoPort[u].Items.Add(entry.Value.ToString());
                        String a = entry.Key.ToString();
                        StaticValues.PortList.Add(new CiscoPort(entry.Value.ToString(), a.Substring(a.LastIndexOf(@"."))));

                    }
                }
            
            }

        }*/




        private void Ports_Load(object sender, EventArgs e)
        {
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (StaticValues.JDSUCiscoArray.Count(item => item.JDSUPort.Equals("not set")) > 0)
            {
                MessageBox.Show(
                    "Порт с именем 'not set' уже существует. Для добавления нового порта, присвойте всем портам уникальные имена.",
                    "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
            var jdsuCisco = new JDSUCiscoClass()
            {
                JDSUPort = "not set",
                CiscoIPCom = new IPCom("not set", "not set"),
                CiscoPort = new CiscoPort("not set", "not set")
            };


            StaticValues.JDSUCiscoArray.Add(jdsuCisco);
            


            portsDataGridView.Rows.Add(jdsuCisco.JDSUPort);
            var rowNumber = portsDataGridView.Rows.Count - 1;


            portsDataGridView.Rows[rowNumber].Cells[1].Value =
                (portsDataGridView[1, rowNumber] as DataGridViewComboBoxCell).Items.Cast<IPCom>()
                    .First(item => item.IP.Equals("not set"));



            (portsDataGridView[2, rowNumber] as DataGridViewComboBoxCell).Items.Add(jdsuCisco.CiscoPort);
            portsDataGridView.Rows[rowNumber].Cells[2].Value = (portsDataGridView[2, rowNumber] as DataGridViewComboBoxCell).Items[0];


            if (_mainForm != null)
            {
                _mainForm.AddRow(jdsuCisco);
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (portsDataGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Требуется выбрать строку для удаления.", "Выберите строку", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var index = portsDataGridView.SelectedCells[0].RowIndex;
            StaticValues.JDSUCiscoArray.RemoveAt(index);

            portsDataGridView.Rows.RemoveAt(index);

            if (_mainForm != null)
            {
                _mainForm.RemoveRowAt(index);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveButton.Enabled = false;
            AddButton.Enabled = false;
            RemoveButton.Enabled = false;

            Cursor = Cursors.AppStarting;

            var serviceContext = new WaterGateServiceContext();
            serviceContext.UpdatePorts(StaticValues.JDSUCiscoArray, (error) =>
            {
                if (error != null)
                {
                    Invoke(new Action(() =>
                        MessageBox.Show(
                            "Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
                else
                {
                    Invoke(new Action(() =>
                        MessageBox.Show(
                            "Настройки успешно сохранены.",
                            "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                }

                Invoke(new Action(() =>
                {
                    Cursor = Cursors.Arrow;
                    SaveButton.Enabled = true;
                    AddButton.Enabled = true;
                    RemoveButton.Enabled = true;
                }));
            });
        }

        private void portsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 1 || e.RowIndex == -1 || !_isIpCellEditing)
                return;

            (portsDataGridView.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Clear();
            (portsDataGridView.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Add(new CiscoPort("not set", "not set"));
            portsDataGridView.Rows[e.RowIndex].Cells[2].Value = (portsDataGridView[2, e.RowIndex] as DataGridViewComboBoxCell).Items[0];


            UpdateCell(e.RowIndex, e.ColumnIndex);
            // UpdateCell(e.RowIndex, e.ColumnIndex + 1);

            if (Convert.ToString(portsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue) == "not set")
            {
                return;
            }

            var stringValue = portsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue;
            var value = (portsDataGridView[e.ColumnIndex, e.RowIndex] as DataGridViewComboBoxCell).Items.Cast<IPCom>().First(item => item.IP.Equals(stringValue));

            portsDataGridView.Cursor = Cursors.AppStarting;
            var asyncAction = new Action(() =>
            {
                String snmpAgent = value.IP;
                String snmpCom = value.Com;

                SimpleSnmp snmp = new SimpleSnmp(snmpAgent, snmpCom);
                Dictionary<Oid, AsnType> result = snmp.Walk(SnmpVersion.Ver2, ".1.3.6.1.2.1.31.1.1.1.1");
                if (result == null)
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("Result is null on IP " + value.IP);
                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        foreach (KeyValuePair<Oid, AsnType> entry in result)
                        {

                            String a = entry.Key.ToString();

                            (portsDataGridView.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Add(new CiscoPort(entry.Value.ToString(), a.Substring(a.LastIndexOf(@"."))));
                        }

                    }));
                }


                Invoke(new Action(() =>
                {
                    UpdateCell(e.RowIndex, e.ColumnIndex + 1);
                    portsDataGridView.Cursor = Cursors.Arrow;
                }));
            });

            asyncAction.BeginInvoke(null, null);
        }

        private void portsDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            _isIpCellEditing = true;
            _currentEditingPortName = portsDataGridView[0, e.RowIndex].Value.ToString();
        }
      


      
        }

    }
