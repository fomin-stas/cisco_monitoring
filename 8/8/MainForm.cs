using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Serialization;
using MetroFramework.Forms;
using Service.Services;
using SnmpSharpNet;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using StaticValuesDll;
using System.Threading;
using WaterGate.Models;
using WaterGate.Properties;

namespace WaterGate
{
    public partial class MainForm : MetroForm
    {
        static object locker = new object();
        
      
        public MainForm()
        {
            InitializeComponent();



            SwitchColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            SwitchColumn.Image = Properties.Resources.OnButton;
            SwitchColumn.Description = "OnButton";
            NotifyIcon.Visible=false;
            NotifyIcon.ContextMenu = new ContextMenu(new[]
{
                new MenuItem("Развернуть", (s,e1)=> ExpandForm()),
                new MenuItem("Выход", (s,e1)=>this.Close())
            });
            CheckPortColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            CheckPortColumn.Image = Properties.Resources.CheckPort;
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            var authorizationToken = (new AuthorizationDialog()).ShowAuthorizationDialog();
            if (authorizationToken.User.Permissions == Permissions.None)
            {
                Environment.Exit(0);
                return;
            }
            else if (authorizationToken.User.Permissions != Permissions.Administrator)
            {
                DescriptionColumn.ReadOnly = true;
                topMenuStrip.Visible = false;
            }
            NotifyIcon.Visible = true;
            StaticValues.CiscoList = authorizationToken.ConfigContainer.CiscoList;
            StaticValues.JDSUCiscoArray = authorizationToken.ConfigContainer.JDSUCiscoArray;
            StaticValues.JDSUIP = authorizationToken.ConfigContainer.JDSUIP;
            StaticValues.CheckDelay = authorizationToken.ConfigContainer.CheckDelay;

            FillForm();
            base.Select();

            this.mainDataGridView.ClearSelection();

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
                var crashCount = 0;

                try
                {
                    foreach (IPCom cisco in StaticValues.CiscoList)
                    {
                        try
                        {
                            if (!IsCiscoActive(cisco))
                                crashCount++;
                        }

                        catch (Exception ex)
                        {

                            foreach (DataGridViewRow label in this.mainDataGridView.Rows)
                            {
                                if (label.Cells[1].Value.ToString() == cisco.IP)
                                {
                                    crashCount++;
                                    if (!IsPortDisabled(label))
                                        ShowToolTrayTooltip(cisco.IP + " " + cisco.Com);
                                    
                                    Invoke(
                                        new Action(() => MarkPortCellAsDisabled((DataGridViewImageCell)label.Cells[6])));
                                }
                            }
                            Functions.AddTempLog(cisco.IP, ex.Message);
                        }
                    }

                    if (crashCount == 0)
                    {
                        Invoke(new Action(() => NotifyIcon.Icon = Resources.Icon));
                    }



                }
                catch (Exception ex)
                { Functions.AddTempLog(ex.ToString()); }
                Thread.Sleep((int)(StaticValues.CheckDelay * 60000));
            }
        }

        private bool IsCiscoActive(IPCom cisco)
        {
           
            foreach (DataGridViewRow label in this.mainDataGridView.Rows)
            {
                if (label.Cells[1].Value.ToString() == cisco.IP)
                {
                    
                    string host = cisco.IP;
                    string community = cisco.Com;
                    var snmp = new SimpleSnmp(host, community);
                    

                    Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver1, new[]
                    {
                        ".1.3.6.1.2.1.2.2.1.7" +
                        StaticValues.JDSUCiscoArray[label.Index].CiscoPort.PortID
                    });

                    if (result == null)
                    {

                        Invoke(new Action(() =>
                        {
                            if (!IsPortDisabled(label))
                                ShowToolTrayTooltip(cisco.IP + " " + cisco.Com);
                            MarkPortCellAsDisabled((DataGridViewImageCell)label.Cells[6]);
                        }));
                        paintCiscoIP(label, System.Drawing.Color.Yellow);
                        paintCiscoPort(label, System.Drawing.Color.Red);

                    }

                    else
                    {
                        foreach (var kvp in result)
                        {
                            if (kvp.Value.ToString() == "1")
                            {

                                // MessageBox.Show("Порт " + lCiscoPort[u].Text + " на Cisco c IP адресом " + lCisco[u].Text + " активен");
                                paintCiscoIP(label, System.Drawing.Color.Green);
                                paintCiscoPort(label, System.Drawing.Color.Green);

                                Invoke(
                                    new Action(
                                        () => MarkPortCellAsEnabled((DataGridViewImageCell)label.Cells[6])));
                            }
                            else
                            {
                                //   MessageBox.Show("Порт " + lCiscoPort[u].Text + " на Cisco c IP адресом " + lCisco[u].Text + " не активен");


                                Invoke(new Action(() =>
                                {
                                    if (!IsPortDisabled(label))
                                        ShowToolTrayTooltip(cisco.IP + " " + cisco.Com);
                                    MarkPortCellAsDisabled((DataGridViewImageCell)label.Cells[6]);
                                }));

                                paintCiscoIP(label, System.Drawing.Color.Green);
                                paintCiscoPort(label, System.Drawing.Color.Red);


                            }

                        }
                    }


                }
            }
            return true;
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
        
        private bool IsPortDisabled(DataGridViewRow row)
        {
            return row.Cells[1].Style.BackColor == Color.Red || row.Cells[2].Style.BackColor == Color.Red;
        }
        
        private void MarkPortCellAsEnabled(DataGridViewImageCell cell)
        {
            cell.Value = Properties.Resources.OnButton;
            cell.Description = "OnButton";
        }

        private void MarkPortCellAsDisabled(DataGridViewImageCell cell)
        {
            cell.Value = Properties.Resources.OffButton;
            cell.Description = "OffButton";
        }


        private void FillForm()
        {

            for (int i = 0; i < StaticValues.JDSUCiscoArray.Count; i++)
            {
                mainDataGridView.Rows.Insert(i, StaticValues.JDSUCiscoArray[i].JDSUPort, StaticValues.JDSUCiscoArray[i].CiscoIPCom.IP, StaticValues.JDSUCiscoArray[i].CiscoPort.PortName, StaticValues.JDSUCiscoArray[i].Description, StaticValues.JDSUCiscoArray[i].Note);
              
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
           PortsForm f = new PortsForm(this);
           //f.Owner = this;  // Removed. Crash on close, when using MetroForm
           f.ShowDialog();
       }

       private void сконфигурироватьJDSUToolStripMenuItem_Click(object sender, EventArgs e)
       {
           JDSUForm f = new JDSUForm();
           //f.Owner = this;
           f.ShowDialog();
       }

       private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
       {
           About f = new About();
           //f.Owner = this;
           f.ShowDialog();
       }

      

       private void списокАварийToolStripMenuItem_Click(object sender, EventArgs e)
       {
           alarms f = new alarms();
           //f.Owner = this;
           f.ShowDialog();
       }



       private void управлениеУчетнымиЗаписямиToolStripMenuItem_Click(object sender, EventArgs e)
       {
           UsersManage f = new UsersManage();
           //f.Owner = this;
           f.ShowDialog();
       }



        private JDSUCiscoClass GetJdsuCiscoClass(string jdsuPortName)
        {
            return StaticValues.JDSUCiscoArray.FirstOrDefault(item => item.JDSUPort == jdsuPortName);
        }

        public void UpdateCell(string jdsuPortName, int columnIndex, object value)
        {
            var row = mainDataGridView.Rows.Cast<DataGridViewRow>().FirstOrDefault(item=>item.Cells[0].Value.ToString() == jdsuPortName);
            if (row == null)
                return;

            row.Cells[columnIndex].Value = value;
        }

        public void AddRow(JDSUCiscoClass jdsuCisco)
        {
            mainDataGridView.Rows.Add(jdsuCisco.JDSUPort, jdsuCisco.CiscoIPCom.IP, jdsuCisco.CiscoPort.PortName);
        }

        public void RemoveRowAt(int index)
        {
            mainDataGridView.Rows.RemoveAt(index);
        }

        private void mainDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.mainDataGridView.Rows[e.RowIndex].Selected = false;
            
            switch (e.ColumnIndex)
            {
                    //включить порт
                case 6:
                {
                    if (e.RowIndex == -1)
                        break;
                    var jdsuCisco = GetJdsuCiscoClass(mainDataGridView[0, e.RowIndex].Value.ToString());

                    var serviceContext = new WaterGateServiceContext();
                    serviceContext.LogUnlockingPortAsync(jdsuCisco, UnlockingPortStatus.Starting);

                    string host = jdsuCisco.CiscoIPCom.IP;
                    string community = jdsuCisco.CiscoIPCom.Com;
                    var port = jdsuCisco.CiscoPort;
                    var portCell = this.mainDataGridView.Rows[e.RowIndex].Cells[2];
                    var ipCell = this.mainDataGridView.Rows[e.RowIndex].Cells[1];


                    var imageCell = (DataGridViewImageCell)mainDataGridView[e.ColumnIndex, e.RowIndex];
                    if (imageCell.Description == "OnButton")
                    {
                        ShowToolTrayTooltipActive(port + " " + host);
                        return;
                    }
                    
                    

                    mainDataGridView.Cursor = Cursors.AppStarting;
                    var asyncAction = new Action(() =>
                    {
                        SimpleSnmp snmp = new SimpleSnmp(host, community);
                        if (!snmp.Valid)
                        {
                            Invoke(new Action(() => mainDataGridView.Cursor = Cursors.Arrow));

                            serviceContext.LogUnlockingPortAsync(jdsuCisco, UnlockingPortStatus.InvalidSmnp);
                            MessageBox.Show("Snmp isn't valid");
                            return;
                        }

                        Pdu pdu = new Pdu(PduType.Set);
                        pdu.VbList.Add(new Oid(".1.3.6.1.2.1.2.2.1.7" + port.PortID), new Integer32(1));
                        snmp.Set(SnmpVersion.Ver2, pdu);

                        Dictionary<Oid, AsnType> result = snmp.Get(SnmpVersion.Ver2, new[]
                   {
                       ".1.3.6.1.2.1.2.2.1.7" + port.PortID
                   });


                        if (result == null)
                        {
                            Invoke(new Action(() => mainDataGridView.Cursor = Cursors.Arrow));

                            serviceContext.LogUnlockingPortAsync(jdsuCisco, UnlockingPortStatus.NoResponse);
                            MessageBox.Show("Нет ответа от " + host + " / возможно указанный IP адрес не является IP адресом коммутационного оборудования Cisco");

                            return;
                        }

                        Invoke(new Action(() =>
                        {
                            mainDataGridView.Cursor = Cursors.Arrow;
                            foreach (var kvp in result)
                            {
                                if (kvp.Value.ToString() == "1")
                                {
                                    serviceContext.LogUnlockingPortAsync(jdsuCisco, UnlockingPortStatus.Active);

                                    MarkPortCellAsEnabled(imageCell);
                                    ShowToolTrayTooltipActive(portCell.Value + " " + ipCell.Value);
                                }
                                else
                                {
                                    serviceContext.LogUnlockingPortAsync(jdsuCisco, UnlockingPortStatus.NotActive);

                                    MarkPortCellAsDisabled(imageCell);
                                    ShowToolTrayTooltip(portCell.Value + " " + ipCell.Value);
                                }
                            }
                        }));
                    });

                    asyncAction.BeginInvoke(null, null);

                    break;
                }
                    //проверить порт
                case 5:
                {
                    if (e.RowIndex == -1)
                        break;
                    var jdsuCisco = GetJdsuCiscoClass(mainDataGridView[0, e.RowIndex].Value.ToString());
                    var cisco = jdsuCisco.CiscoIPCom;

                    //var cisco = GetJdsuCiscoClass(mainDataGridView[0, e.RowIndex].Value.ToString()).CiscoIPCom;
                    if (cisco == null)
                        return;

                  

                    mainDataGridView.Cursor = Cursors.AppStarting;

                    var asyncAction = new Action(() =>
                    {
                        try
                        {
                            if(IsCiscoActive(cisco)) ShowToolTrayTooltipActive(jdsuCisco.CiscoPort.PortName + "" + jdsuCisco.CiscoIPCom.IP);
                        }

                        catch (Exception ex)
                        {

                            foreach (DataGridViewRow label in this.mainDataGridView.Rows)
                            {
                                if (label.Cells[1].Value.ToString() == cisco.IP)
                                {
                                    if (!IsPortDisabled(label))
                                        ShowToolTrayTooltip(cisco.IP + " " + cisco.Com);
                                    paintCiscoIP(label, System.Drawing.Color.Red);
                                    Invoke(
                                        new Action(() => MarkPortCellAsDisabled((DataGridViewImageCell) label.Cells[6])));
                                }
                            }
                            Functions.AddTempLog(cisco.IP, ex.Message);
                        }
                        finally
                        {
                            Invoke(new Action(() => mainDataGridView.Cursor = Cursors.Arrow));
                        }
                    });

                    asyncAction.BeginInvoke(null, null);

                    break;
                }
            }
           
        }

        private void ExpandForm()
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void ShowToolTrayTooltip(string portName)
        {
            NotifyIcon.Icon = Resources.FailIcon;
            NotifyIcon.ShowBalloonTip(10000, "Порт заблокирован", "Порт " + portName + " заблокирован.", ToolTipIcon.Warning);
            System.Media.SystemSounds.Asterisk.Play();
        }

        private void ShowToolTrayTooltipActive(string portName)
        {
            NotifyIcon.Icon = Resources.FailIcon;
            NotifyIcon.ShowBalloonTip(10000, "Порт активен", "Порт " + portName + " активен.", ToolTipIcon.Warning);
            System.Media.SystemSounds.Asterisk.Play();
        }

        private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ExpandForm();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            ExpandForm();
        }

        private void mainDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (e.ColumnIndex)
            {
                case 3:
                {
                    var objectValue = mainDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    var value = objectValue == null ? null : objectValue.ToString();
                    var jdsuCisco = GetJdsuCiscoClass(mainDataGridView[0, e.RowIndex].Value.ToString());
                    jdsuCisco.Description = value;

                    mainDataGridView.Cursor = Cursors.AppStarting;

                    var serviceContext = new WaterGateServiceContext();
                    serviceContext.UpdatePortDescriptionAsync(jdsuCisco, (error) =>
                    {
                        if (error != null)
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show("Ошибка при соединении с сервисом. Проверьте подключение к сети.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));
                        }

                        Invoke(new Action(() =>
                        {
                            mainDataGridView.Cursor = Cursors.Arrow;
                        }));
                    });

                    break;
                }

                case 4:
                {
                    var objectValue = mainDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    var value = objectValue == null ? null : objectValue.ToString();
                    var jdsuCisco = GetJdsuCiscoClass(mainDataGridView[0, e.RowIndex].Value.ToString());
                    jdsuCisco.Note = value;

                    mainDataGridView.Cursor = Cursors.AppStarting;

                    var serviceContext = new WaterGateServiceContext();
                    serviceContext.UpdatePortNoteAsync(jdsuCisco, (error) =>
                    {
                        if (error != null)
                        {
                            Invoke(new Action(() =>
                            {
                                MessageBox.Show("Ошибка при соединении с сервисом. Проверьте подключение к сети.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }));
                        }

                        Invoke(new Action(() =>
                        {
                            mainDataGridView.Cursor = Cursors.Arrow;
                        }));
                    });
                    break;
                }
            }
           

        }

        private void загрузитьXmlФайлToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void сохранитьКонфигурациюToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "xml files (*.xml)|*.xml";
            saveFileDialog1.Title = "Сохранить конфигурацию";
            saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFileDialog1.FileName != "")
            {

               // File.Copy(Functions.pathConfig, saveFileDialog1.FileName, true);

               
                forSerialize ser = new forSerialize();

                ser.n = StaticValues.JDSUCiscoArray.Count();
                ser.JDSUIP = StaticValues.JDSUIP;
                ser.JDSUCiscoArray = StaticValues.JDSUCiscoArray.ToArray();

                FileStream myFileStream = new FileStream(saveFileDialog1.FileName, FileMode.OpenOrCreate);
                XmlSerializer serializer = new XmlSerializer(typeof(forSerialize));

                serializer.Serialize(myFileStream, ser);
            
                
                
                MessageBox.Show("Конфигурация сохранена");


            }
        }

       

   
    }
}
