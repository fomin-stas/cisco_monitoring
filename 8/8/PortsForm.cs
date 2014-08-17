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
using SnmpSharpNet;
using StaticValuesDll;

namespace WaterGate
{
    public partial class PortsForm : Form
    {
        public PortsForm()
        {
            InitializeComponent();

            this.formJDSUPort = new System.Windows.Forms.DataGridView();
            this.JDSUport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoIP = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CiscoPort = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.SaveBut = new System.Windows.Forms.DataGridViewButtonColumn();
            

            ((System.ComponentModel.ISupportInitialize)(this.formJDSUPort)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1


            this.formJDSUPort.AllowUserToOrderColumns = true;
            this.formJDSUPort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.formJDSUPort.AllowUserToResizeColumns = true;
            this.formJDSUPort.AllowUserToResizeRows = true;
            this.formJDSUPort.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.formJDSUPort.EditMode = DataGridViewEditMode.EditOnEnter;
            this.formJDSUPort.CellEndEdit += formJDSUPort_CellEndEdit;
            //this.formJDSUPort.CellValueChanged += new DataGridViewCellEventHandler(formJDSUPort_CellValueChanged);
            this.formJDSUPort.CurrentCellDirtyStateChanged += new EventHandler(formJDSUPort_CurrentCellDirtyStateChanged);
            this.formJDSUPort.CellContentClick += formJDSUPort_CellContentClick;
            this.formJDSUPort.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JDSUport,
            this.CiscoIP,
            this.CiscoPort,
            this.SaveBut});

            this.formJDSUPort.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
         
            this.formJDSUPort.Size = new Size(620, 575);
            this.formJDSUPort.Name = "dataGridView1";
            this.formJDSUPort.Location = new System.Drawing.Point(0, 25);
            this.formJDSUPort.TabIndex = 0;

            // 
            // JDSUport
            // 
            this.JDSUport.HeaderText = "JDSU порт";
            this.JDSUport.Name = "JDSUport";
            this.JDSUport.Width = 120;
            // 
            // CiscoIP
            // 
            this.CiscoIP.HeaderText = "Cisco IP";
            this.CiscoIP.Name = "CiscoIP";
            this.CiscoIP.Width = 120;

            this.CiscoIP.Items.Add("not set");

            foreach (IPCom key in StaticValues.CiscoList)
            {
                if (key.IP != "" && key.IP != null)
                {
                    this.CiscoIP.Items.Add(key.IP);
                }
            }


          //  this.CiscoIP.DisplayMember = "MyDiscription";

            // 
            // CiscoPort
            // 
            this.CiscoPort.HeaderText = "Cisco порт";
            this.CiscoPort.Name = "CiscoPort";
            this.CiscoPort.Width = 120;
            // 
            // Save button
            // 
            this.SaveBut.HeaderText = "Принудительно включить порт";
            this.SaveBut.Name = "buttonON";

            // 
          
            // Form1
            // 

           // FillForm();
            this.Controls.Add(this.formJDSUPort);

            for (int i = 0; i < StaticValues.JDSUCiscoArray.Count; i++)
            {
                formJDSUPort.Rows.Insert(i, StaticValues.JDSUCiscoArray[i].JDSUPort);

               
                formJDSUPort.Rows[i].Cells[1].Value = (formJDSUPort[1, i] as DataGridViewComboBoxCell).Items[(formJDSUPort[1, i] as DataGridViewComboBoxCell).Items.IndexOf(Convert.ToString(StaticValues.JDSUCiscoArray[i].CiscoIPCom.IP))]/*Contains(Convert.ToString(StaticValues.JDSUCiscoArray[i].CiscoIPCom.IP))*/;
               
                
                
                (formJDSUPort[2, i] as DataGridViewComboBoxCell).Items.Add(Convert.ToString(StaticValues.JDSUCiscoArray[i].CiscoPort.PortName));
                formJDSUPort.Rows[i].Cells[2].Value = (formJDSUPort[2, i] as DataGridViewComboBoxCell).Items[0];

            }

            ((System.ComponentModel.ISupportInitialize)(this.formJDSUPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void formJDSUPort_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            { 
           
                //порядковый номер этого всего счастья
                int u = e.RowIndex;


                StaticValues.PortList.Add(new CiscoPort("not set", ""));
                StaticValues.PortList.Add(StaticValues.JDSUCiscoArray[u].CiscoPort);
                //сохраняем порт JDSU

                StaticValues.JDSUCiscoArray[u].JDSUPort = this.formJDSUPort[0,e.RowIndex].Value.ToString();


                //сохраняем IP Cisco
                if (Convert.ToString((this.formJDSUPort[1,e.RowIndex] as DataGridViewComboBoxCell).Value) == "not set")
                    {
                        StaticValues.JDSUCiscoArray[u].CiscoIPCom = new IPCom("not set", "not set");
                    }
                else
                    {
                        StaticValues.JDSUCiscoArray[u].CiscoIPCom = StaticValues.CiscoList.Find(delegate(IPCom x)
                        {
                            return x.IP.Contains(Convert.ToString(Convert.ToString((this.formJDSUPort[1, e.RowIndex] as DataGridViewComboBoxCell).Value)));
                        });
                    }
           
                //сохраняем порт
                if (Convert.ToString((this.formJDSUPort[2, e.RowIndex] as DataGridViewComboBoxCell).Value) == "not set")
                    {
                        StaticValues.JDSUCiscoArray[u].CiscoPort = new CiscoPort("not set", "not set");
                    }
                else
                    {
                        StaticValues.JDSUCiscoArray[u].CiscoPort = StaticValues.PortList.Find(delegate(CiscoPort x)
                        {
                            return x.PortName.Contains(Convert.ToString((this.formJDSUPort[2, e.RowIndex] as DataGridViewComboBoxCell).Value));
                        });
                    }
           
                MainForm main = this.Owner as MainForm;
                    if (main != null)
                        {

                            main.form.Rows[u].Cells[0].Value = this.formJDSUPort[0,e.RowIndex].Value;
                            main.form.Rows[u].Cells[1].Value = Convert.ToString((this.formJDSUPort[1, e.RowIndex] as DataGridViewComboBoxCell).Value);
                            main.form.Rows[u].Cells[2].Value = Convert.ToString((this.formJDSUPort[2, e.RowIndex] as DataGridViewComboBoxCell).Value);
                        }


            //добавляем изменения в xmlку
            XmlDocument doc = new XmlDocument();
            doc.Load(Functions.pathConfig);
        

            List<XmlNode> nodes = doc.DocumentElement.SelectSingleNode("Ports")
                 .Cast<XmlNode>().Where(a => a.Attributes["n"].Value.ToString() == Convert.ToString(u)).ToList();

            foreach (var el in nodes)
            {
                el.SelectSingleNode("JDSUPort").InnerText = this.formJDSUPort[0, e.RowIndex].Value.ToString();
                el.SelectSingleNode("CiscoIPCom").Attributes["IP"].Value = StaticValues.JDSUCiscoArray[u].CiscoIPCom.IP;
                el.SelectSingleNode("CiscoIPCom").Attributes["Com"].Value = StaticValues.JDSUCiscoArray[u].CiscoIPCom.Com;
                el.SelectSingleNode("CiscoPort").Attributes["PortName"].Value = Convert.ToString((this.formJDSUPort[2, e.RowIndex] as DataGridViewComboBoxCell).Value);
               // try
               // {
                    el.SelectSingleNode("CiscoPort").Attributes["PortID"].Value = StaticValues.PortList.Find(delegate(CiscoPort x)
                    {
                        return x.PortName.Contains(Convert.ToString((this.formJDSUPort[2, e.RowIndex] as DataGridViewComboBoxCell).Value));
                    }).PortID;
             //   }
             //   catch (Exception ex)
            //    { 
                
            //    }
            }

            doc.Save(Functions.pathConfig);
            

            //send this to our service
            UdpClient client = new UdpClient();
            try
            {
                client.Connect("127.0.0.1", 32156);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
     
                Socket Sock = client.Client;
                JDSUCiscoClass ser = StaticValues.JDSUCiscoArray[u];

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
     
         
                client.Close();

            
            
            MessageBox.Show("Изменения сохранены");
            
        
            
            }
        }

        void formJDSUPort_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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

                
            //    this.ComboBoxCiscoPort[u].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

                String snmpAgent = Convert.ToString(this.formJDSUPort.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                String snmpCom = StaticValues.CiscoList.Find(delegate(IPCom x)
                {
                    return x.IP.Contains(Convert.ToString(formJDSUPort.Rows[e.RowIndex].Cells[e.ColumnIndex].Value));
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
                       (formJDSUPort.Rows[e.RowIndex].Cells[2] as DataGridViewComboBoxCell).Items.Add(entry.Value.ToString());
                        String a = entry.Key.ToString();
                        StaticValues.PortList.Add(new CiscoPort(entry.Value.ToString(), a.Substring(a.LastIndexOf(@"."))));

                    }
                }
            
            
            }
        }

        void formJDSUPort_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (formJDSUPort.IsCurrentCellDirty)
            {
                formJDSUPort.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
            MainForm main = this.Owner as MainForm;
            if (main != null)
            {
               


            }
        }
      

      
        }

    }
