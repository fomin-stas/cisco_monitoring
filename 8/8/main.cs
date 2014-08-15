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

namespace WaterGate
{
    public partial class main : Form
    {
        static object locker = new object();
      
        public main()
        {
            InitializeComponent();
            this.form = new System.Windows.Forms.DataGridView();
            this.JDSUport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.switchON = new System.Windows.Forms.DataGridViewButtonColumn();
            this.verify = new System.Windows.Forms.DataGridViewButtonColumn();

            ((System.ComponentModel.ISupportInitialize)(this.form)).BeginInit();
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

            this.form.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JDSUport,
            this.CiscoIP,
            this.CiscoPort,
            this.switchON,
            this.verify});

            this.form.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            this.form.CellClick += new DataGridViewCellEventHandler(dataGridView_CellContentClick);
            this.form.Size = new Size(620, 575);
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
            // buttonVerify
            // 
            this.verify.HeaderText = "Принудительно включить порт";
            this.verify.Name = "buttonON";
            // Form1
            // 

            FillForm();
            this.Controls.Add(this.form);

            ((System.ComponentModel.ISupportInitialize)(this.form)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread AlarmsThread = new Thread(TCPlistener);
            AlarmsThread.IsBackground = true;
            AlarmsThread.SetApartmentState(ApartmentState.STA);
            AlarmsThread.Start();

            Thread CheckCiscoThread = new System.Threading.Thread(new System.Threading.ThreadStart(CheckCisco));
            CheckCiscoThread.SetApartmentState(ApartmentState.STA);
            CheckCiscoThread.IsBackground = true;
            CheckCiscoThread.Start();

            for (int i = 0; i < 5; i++)
                {
                    LogPass _LogPass = new LogPass();
                    DialogResult dialogResult = _LogPass.ShowDialog();

                        

                    if (dialogResult == DialogResult.OK )
                        {

                             if (_LogPass.Log == "admin" && _LogPass.Pas == "password")
                               {
                                   return;
                               }
                               else
                             {
                                 MessageBox.Show("Логин/Пароль неверны. Попробуйте еще раз.");
                                 if (i == 4)
                                 {
                                     Application.Exit();
                                     return;
                                 }
                                
                             }
                        }
                            if (dialogResult == DialogResult.Cancel)
                            {
                                      Application.Exit();
                                      return;
                            }
                  }
          
 
        
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
                                paint(label, System.Drawing.Color.Green);
                            }
                        }    
                    
                    }

                    catch (Exception ex)
                    {
                        Functions.AddTempLog(cisco.IP, ex.Message);


                        foreach (DataGridViewRow label in this.form.Rows)
                        {
                            if (label.Cells[1].Value.ToString() == cisco.IP)
                            {
                                paint(label, System.Drawing.Color.Red);
                            }
                        }
                                             
                    }

                }

                Thread.Sleep(10000);
                
            }
        }
         

        private static void paint(DataGridViewRow label, System.Drawing.Color color)
        {
            lock (locker)
            {
                label.DefaultCellStyle.BackColor = color;
            }
        
        }

        private void FillForm()
        {

            for (int i = 0; i < StaticValues.n; i++)
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
                   for (int i = 0; i < StaticValues.n; i++)
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
           if (e.ColumnIndex == 3)
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
