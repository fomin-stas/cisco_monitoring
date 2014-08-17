using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using SnmpSharpNet;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using StaticValuesDll;
using System.Threading;
using WaterGate.Models;

namespace WaterGate
{
    public partial class MainForm : Form
    {
        static object locker = new object();
      
        public MainForm()
        {
            InitializeComponent();
           
            this.form = new System.Windows.Forms.DataGridView();
            this.JDSUport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.switchON = new System.Windows.Forms.DataGridViewButtonColumn();
           

            this.form_1 = new System.Windows.Forms.DataGridView();
            this.JDSUport_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoIP_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoPort_1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.switchON_1 = new System.Windows.Forms.DataGridViewButtonColumn();


            ((System.ComponentModel.ISupportInitialize)(this.form)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form_1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1


            this.form.AllowUserToOrderColumns = true;
            this.form.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.form.AllowUserToResizeColumns = true;
            this.form.AllowUserToResizeRows = true;
            this.form.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.form.ReadOnly = true;
            this.form.AllowUserToAddRows = false;
         //   this.form.ScrollBars = System.Windows.Forms.ScrollBars.None;


            this.form.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JDSUport,
            this.CiscoIP,
            this.CiscoPort,
            this.switchON});

            this.form.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Top);
            this.form.CellClick += new DataGridViewCellEventHandler(dataGridView_CellContentClick);
            this.form.Size = new Size(500, 560);
            this.form.Name = "dataGridView1";
            this.form.Location = new System.Drawing.Point(0, 25);
            this.form.TabIndex = 0;

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
            // 
            // CiscoPort
            // 
            this.CiscoPort.HeaderText = "Cisco порт";
            this.CiscoPort.Name = "CiscoPort";
            this.CiscoPort.Width = 120;
            // 
            // buttonON
            // 
            this.switchON.HeaderText = "Принудительно включить порт";
            this.switchON.Name = "buttonON";


          

            //
            //datagridview2



            this.form_1.AllowUserToOrderColumns = true;
            this.form_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.form_1.AllowUserToResizeColumns = true;
            this.form_1.AllowUserToResizeRows = true;
            this.form_1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.form_1.ReadOnly = true;
            this.form_1.AllowUserToAddRows = false;
          //  this.form_1.ScrollBars = System.Windows.Forms.ScrollBars.None;

            this.form_1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JDSUport_1,
            this.CiscoIP_1,
            this.CiscoPort_1,
            this.switchON_1});

            this.form_1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right| AnchorStyles.Top);
            this.form_1.CellClick += new DataGridViewCellEventHandler(dataGridView_CellContentClick);
            this.form_1.Size = new Size(500, 560);
            this.form_1.Name = "dataGridView1";
            this.form_1.Location = new System.Drawing.Point(520, 25);
            this.form_1.TabIndex = 1;

            // 
            // JDSUport
            // 
            this.JDSUport_1.HeaderText = "JDSU порт";
            this.JDSUport_1.Name = "JDSUport";
            this.JDSUport_1.Width = 120;
            // 
            // CiscoIP
            // 
            this.CiscoIP_1.HeaderText = "Cisco IP";
            this.CiscoIP_1.Name = "CiscoIP";
            this.CiscoIP_1.Width = 120;
            // 
            // CiscoPort
            // 
            this.CiscoPort_1.HeaderText = "Cisco порт";
            this.CiscoPort_1.Name = "CiscoPort";
            this.CiscoPort_1.Width = 120;
            // 
            // buttonON
            // 
            this.switchON_1.HeaderText = "Принудительно включить порт";
            this.switchON_1.Name = "buttonON";
     

            
            this.Controls.Add(this.form);
            this.Controls.Add(this.form_1);

            ((System.ComponentModel.ISupportInitialize)(this.form)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form_1)).EndInit();

            // 
            // Form1
            // 

            this.ResumeLayout(false);
            this.PerformLayout();
    
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            var authorizationToken = (new AuthorizationDialog()).ShowAuthorizationDialog();
            if (authorizationToken.Permissions == Permissions.None)
            {
                this.Close();
                return;
            }
            else if (authorizationToken.Permissions != Permissions.Administrator)
            {
                topMenuStrip.Visible = false;
            }

            StaticValues.CiscoList = authorizationToken.ConfigContainer.CiscoList;
            StaticValues.JDSUCiscoArray = authorizationToken.ConfigContainer.JDSUCiscoArray;
            StaticValues.JDSUIP = authorizationToken.ConfigContainer.JDSUIP;

            FillForm();


           

            Thread AlarmsThread = new Thread(TCPlistener);
            AlarmsThread.IsBackground = true;
            AlarmsThread.Start();

            Thread CheckCiscoThread = new System.Threading.Thread(new System.Threading.ThreadStart(CheckCisco));
            CheckCiscoThread.IsBackground = true;
            CheckCiscoThread.Start();
        }

      

        private void TCPlistener()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 32158);
            EndPoint ep = (EndPoint)ipep;
            socket.Bind(ep);
            // Disable timeout processing. Just block until packet is received 
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 0);

            while (true)
            {
                byte[] indata = new byte[16 * 1024];
                // 16KB receive buffer 
                int inlen = 0;
                IPEndPoint peer = new IPEndPoint(IPAddress.Any, 0);
                EndPoint inep = (EndPoint)peer;
                try
                {
                    inlen = socket.ReceiveFrom(indata, ref inep);
                }
                catch (Exception ex)
                {
                    Functions.AddTempLog(ex.Message, ex.ToString());

                    inlen = -1;
                }
                if (inlen > 0)
                {
                    /*try
                    {
                       /* StaticValuesDll.JDSUCiscoClass ser = new StaticValuesDll.JDSUCiscoClass();
                        ser = null;

                        var formatter = new BinaryFormatter();
                        using (var ms = new MemoryStream(indata))
                        {
                            using (var ds = new DeflateStream(ms, CompressionMode.Decompress, true))
                            {
                                ser = (StaticValuesDll.JDSUCiscoClass)formatter.Deserialize(ds);
                            }
                        }

                        Functions.AddTempLog(ser.JDSUPort.ToString());
              
                    }
                    catch (Exception ex)
                    {
                        Functions.AddTempLog(ex.Message, ex.ToString());
                    }

            */
                  Functions.AddTempLog(Encoding.ASCII.GetString(indata));


                }
            }


        }


        private void CheckCisco()
        {
            while (true)
            {
                foreach (IPCom cisco in StaticValues.CiscoList)
                {

                    try
                    {

                        UdpTarget target = new UdpTarget((IPAddress)new IpAddress(cisco.IP), 161, 500, 0);
                        Pdu pdu = new Pdu(PduType.Get);
                        pdu.VbList.Add(".1.3.6.1.2.1.1.6.0");
                        AgentParameters aparam = new AgentParameters(SnmpVersion.Ver2, new OctetString(cisco.Com));
                        SnmpV2Packet response;

                        response = target.Request(pdu, aparam) as SnmpV2Packet;
                        target.Close();
             
                        foreach (DataGridViewRow label in this.form.Rows)
                        {
                            if (label.Cells[1].Value.ToString() == cisco.IP)
                            {
                                paintCiscoIP(label, System.Drawing.Color.Green);
                                string host = cisco.IP;
                                string community = cisco.Com;
                                var snmp = new SimpleSnmp(host, community);
                                if (!snmp.Valid)
                                {
                                    paintCiscoIP(label, System.Drawing.Color.Yellow);
                                    return;
                                }



                                Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver1, new[]
                                    {
                                        ".1.3.6.1.2.1.2.2.1.7" + StaticValues.JDSUCiscoArray[label.Index].CiscoPort.PortID 
                                    });

                                if (result == null)
                                {
                                     
                                    paintCiscoPort(label, System.Drawing.Color.Red);
                                    return;
                                }


                                foreach (var kvp in result)
                                {
                                    if (kvp.Value.ToString() == "1")
                                    {

                                       // MessageBox.Show("Порт " + lCiscoPort[u].Text + " на Cisco c IP адресом " + lCisco[u].Text + " активен");
                                        paintCiscoIP(label, System.Drawing.Color.Green);
                                        paintCiscoPort(label, System.Drawing.Color.Green);
                                    }
                                    else
                                    {
                                     //   MessageBox.Show("Порт " + lCiscoPort[u].Text + " на Cisco c IP адресом " + lCisco[u].Text + " не активен");
                                        paintCiscoIP(label, System.Drawing.Color.Green);
                                        paintCiscoPort(label, System.Drawing.Color.Red);
                                    }

                                }


                            }
                        }    
                    
                    }

                    catch (Exception ex)
                    {


                        foreach (DataGridViewRow label in this.form.Rows)
                        {
                            if (label.Cells[1].Value.ToString() == cisco.IP)
                            {
                                paintCiscoIP(label, System.Drawing.Color.Red);
                            }
                        }
                        Functions.AddTempLog(cisco.IP, ex.Message);              
                    }

                }

                Thread.Sleep(10000);
                
            }
        }
         

        private static void paintCiscoIP(DataGridViewRow label, System.Drawing.Color color)
        {
            lock (locker)
            {
                label.Cells[1].Style.BackColor = color;
            }
        
        }
        private static void paintCiscoPort(DataGridViewRow label, System.Drawing.Color color)
        {
            lock (locker)
            {
                label.Cells[2].Style.BackColor = color;
            }

        }
        private void FillForm()
        {

            for (int i = 0; i < StaticValues.JDSUCiscoArray.Count; i++)
            {
                form.Rows.Insert(i, StaticValues.JDSUCiscoArray[i].JDSUPort, StaticValues.JDSUCiscoArray[i].CiscoIPCom.IP, StaticValues.JDSUCiscoArray[i].CiscoPort.PortName);
              
            }

        }



       private void загрузитьКонфигурациюToolStripMenuItem_Click(object sender, EventArgs e)
       {
         
           OpenFileDialog openFileDialog1 = new OpenFileDialog();

           openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
           openFileDialog1.Filter = "xml files (*.xml)|*.xml";
           openFileDialog1.FilterIndex = 2;
           openFileDialog1.RestoreDirectory = true;

           if (openFileDialog1.ShowDialog() == DialogResult.OK)
           {
               try
               {
                
                   Functions.SerializeConfig(openFileDialog1.FileName);
                   File.Copy(openFileDialog1.FileName, Functions.pathConfig, true);
                   for (int i = 0; i < StaticValues.JDSUCiscoArray.Count; i++)
                   {
                       FillForm();
                   }
                       MessageBox.Show("конфигурация загружена");
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message);
               }
           }
       }

       private void добавитьCiscoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           CiscoForm f = new CiscoForm();
           //  f.Owner = this;
           f.ShowDialog();
       }

       private void назначитьПортыJDSUCiscoToolStripMenuItem_Click(object sender, EventArgs e)
       {
           PortsForm f = new PortsForm();
           f.Owner = this;
           f.ShowDialog();
       }

       private void сконфигурироватьJDSUToolStripMenuItem_Click(object sender, EventArgs e)
       {
           JDSUForm f = new JDSUForm();
           f.Owner = this;
           f.ShowDialog();
       }

       private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
       {
           About f = new About();
           f.Owner = this;
           f.ShowDialog();
       }

       private void сохранитьКонфигурациюToolStripMenuItem_Click(object sender, EventArgs e)
       {
           SaveFileDialog saveFileDialog1 = new SaveFileDialog();
           saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
           saveFileDialog1.Title = "Сохранить конфигурацию";
           saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
   
           if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog1.FileName != "")
           {

               File.Copy(Functions.pathConfig, saveFileDialog1.FileName, true);
               MessageBox.Show("Конфигурация сохранена");

           
           }
       }

       private void списокАварийToolStripMenuItem_Click(object sender, EventArgs e)
       {
           alarms f = new alarms();
           f.Owner = this;
           f.ShowDialog();
       }



       private void управлениеУчетнымиЗаписямиToolStripMenuItem_Click(object sender, EventArgs e)
       {
           UsersManage f = new UsersManage();
           f.Owner = this;
           f.ShowDialog();
       }



       private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
       {
           //switchON
           if (e.ColumnIndex == 3 && (int)(e.RowIndex) != -1)
           {
              
               int u = (int)(e.RowIndex);
               string host = StaticValues.JDSUCiscoArray[u].CiscoIPCom.IP;
               string community = StaticValues.JDSUCiscoArray[u].CiscoIPCom.Com;


               SimpleSnmp snmp = new SimpleSnmp(host, community);
               if (!snmp.Valid)
               {
                   MessageBox.Show("Snmp isn't valid");
                   return;
               }

               Pdu pdu = new Pdu(PduType.Set);
               pdu.VbList.Add(new Oid(".1.3.6.1.2.1.2.2.1.7" + StaticValues.JDSUCiscoArray[u].CiscoPort.PortID), new Integer32(1));
               snmp.Set(SnmpVersion.Ver2, pdu);

               Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver1, new[]
                {
                    ".1.3.6.1.2.1.2.2.1.7" + StaticValues.JDSUCiscoArray[u].CiscoPort.PortID 
                });
               
               if (result == null)
               {
                   MessageBox.Show("net otveta / возможно указанный IP адрес не является IP адресом коммутационного оборудования Cisco");
                   return;
               }


               foreach (var kvp in result)
               {
                   if (kvp.Value.ToString() == "1")
                   {

                       MessageBox.Show("Порт " + this.form.Rows[u].Cells[3].Value + " на Cisco c IP адресом " + this.form.Rows[u].Cells[2].Value + " активен");
                   }
                   else
                   {
                       MessageBox.Show("Порт " + this.form.Rows[u].Cells[3].Value + " на Cisco c IP адресом " + this.form.Rows[u].Cells[2].Value + " не активен");
                   }

               }
          
           
           
           }

           if (e.ColumnIndex == 4)
           {
             

               int u = (int)(e.RowIndex);

               string host = StaticValues.JDSUCiscoArray[u].CiscoIPCom.IP;
               string community = StaticValues.JDSUCiscoArray[u].CiscoIPCom.Com;
               var snmp = new SimpleSnmp(host, community);
               if (!snmp.Valid)
               {
                   MessageBox.Show("net soedinenia");
                   return;
               }


               Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver1, new[]
                {
                    ".1.3.6.1.2.1.2.2.1.7" + StaticValues.JDSUCiscoArray[u].CiscoPort.PortID 
                });

               if (result == null)
               {
                   MessageBox.Show("net otveta / возможно указанный IP адрес не является IP адресом коммутационного оборудования Cisco");
                   return;
               }


               foreach (var kvp in result)
               {
                   if (kvp.Value.ToString() == "1")
                   {

                       MessageBox.Show("Порт " + this.form.Rows[u].Cells[3].Value + " на Cisco c IP адресом " + this.form.Rows[u].Cells[2].Value + " активен");
                   }
                   else
                   {
                       MessageBox.Show("Порт " + this.form.Rows[u].Cells[3].Value + " на Cisco c IP адресом " + this.form.Rows[u].Cells[2].Value + " не активен");
                   }

               }

           
           }

       }
    
      
        

        
    }
}
