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

        public PortsForm(MainForm mainForm)
        {
            InitializeComponent();

            _mainForm = mainForm;
            CiscoPort.DisplayMember = "PortName";
            CiscoIP.DisplayMember = "IP";

            this.CiscoIP.Items.Add(new IPCom("not set", "not set"));

            foreach (IPCom key in StaticValues.CiscoList)
            {
                if (!string.IsNullOrEmpty(key.IP))
                {
                    this.CiscoIP.Items.Add(key);
                }
            }

            for (int i = 0; i < StaticValues.JDSUCiscoArray.Count; i++)
            {
                portsDataGridView.Rows.Add(StaticValues.JDSUCiscoArray[i].JDSUPort);


                portsDataGridView.Rows[i].Cells[1].Value = (portsDataGridView[1, i] as DataGridViewComboBoxCell).Items[(portsDataGridView[1, i] as DataGridViewComboBoxCell).Items.IndexOf(StaticValues.JDSUCiscoArray[i].CiscoIPCom)]/*Contains(Convert.ToString(StaticValues.JDSUCiscoArray[i].CiscoIPCom.IP))*/;



                (portsDataGridView[2, i] as DataGridViewComboBoxCell).Items.Add(StaticValues.JDSUCiscoArray[i].CiscoPort);
                portsDataGridView.Rows[i].Cells[2].Value = (portsDataGridView[2, i] as DataGridViewComboBoxCell).Items[0];

            }
        }

        void formJDSUPort_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            { 
           
                //порядковый номер этого всего счастья
                var ser = StaticValues.JDSUCiscoArray[e.RowIndex];
                portsDataGridView.Cursor = Cursors.AppStarting;
                var asyncAction = new Action(() =>
                {
                    //send this to our service
                    using (UdpClient client = new UdpClient())
                    {
                        try
                        {
                            client.Connect(Settings.ServiceAddress, 32157); //was 32156
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            Invoke(new Action(() => portsDataGridView.Cursor = Cursors.Arrow));
                            return;
                        }

                        Socket Sock = client.Client;


                        byte[] content = null;
                        try
                        {
                            var formatter = new BinaryFormatter();
                            using (var ms = new MemoryStream())
                            {
                                using (var ds = new DeflateStream(ms, CompressionMode.Compress, true))
                                {
                                    formatter.Serialize(ds, ser);
                                }
                                ms.Position = 0;
                                content = ms.GetBuffer();

                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }

                        Sock.Send(content);
                        Sock.Close();

                        Invoke(new Action(() => portsDataGridView.Cursor = Cursors.Arrow));
                        MessageBox.Show("Изменения сохранены");
                    }

                });
                asyncAction.BeginInvoke(null, null);
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
                    (portsDataGridView.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Clear();
                    (portsDataGridView.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Add(new CiscoPort("not set", "not set"));
                    portsDataGridView.Rows[e.RowIndex].Cells[2].Value = (portsDataGridView[2, e.RowIndex] as DataGridViewComboBoxCell).Items[0];

              
                    UpdateCell(e.RowIndex, e.ColumnIndex);
                    UpdateCell(e.RowIndex, e.ColumnIndex + 1);

                    if (Convert.ToString(portsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue) == "not set")
                    {
                        return;
                    }

                    var stringValue = portsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].FormattedValue;
                    var value = (portsDataGridView[e.ColumnIndex, e.RowIndex] as DataGridViewComboBoxCell).Items.Cast<IPCom>().First(item=>item.IP.Equals(stringValue));

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

                        Invoke(new Action(() => portsDataGridView.Cursor = Cursors.Arrow));
                    });

                    asyncAction.BeginInvoke(null, null);
                   
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
                    var value = this.portsDataGridView[0, row].Value.ToString();
                    StaticValues.JDSUCiscoArray[row].JDSUPort = value;
                    if (_mainForm != null)
                    {
                        _mainForm.UpdateCell(row, 0, value);
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
                        _mainForm.UpdateCell(row, 1, value);
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
                        StaticValues.JDSUCiscoArray[row].CiscoPort = (CiscoPort)(this.portsDataGridView[2, row] as DataGridViewComboBoxCell).Items.Cast<CiscoPort>().First(item=>item.PortName.Equals(value));
                    }

                    if (_mainForm != null)
                    {
                        _mainForm.UpdateCell(row, 2, value);
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
      


      
        }

    }
