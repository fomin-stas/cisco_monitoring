using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.NetworkInformation;
using MetroFramework.Forms;
using SnmpSharpNet;
using System.Xml.Linq;
using System.Xml;
using StaticValuesDll;
using WaterGate.Models;

namespace WaterGate
{
    public partial class CiscoForm : MetroForm
    {
        public CiscoForm()
        {
            InitializeComponent();
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            // IpAddressCisco
            // 
            this.ComboBoxIpAddressCisco = new System.Windows.Forms.ComboBox();
            this.ComboBoxIpAddressCisco.FormattingEnabled = true;
            this.ComboBoxIpAddressCisco.Location = new System.Drawing.Point(16, 95);
            this.ComboBoxIpAddressCisco.Name = "ComboBoxIpAddressCisco";
            this.ComboBoxIpAddressCisco.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxIpAddressCisco.TabIndex = 7;
            this.ComboBoxIpAddressCisco.SelectedIndexChanged += new System.EventHandler(this.IP_selected);
            this.ComboBoxIpAddressCisco.Leave += new System.EventHandler(this.IP_verify);
          
            foreach (IPCom key in StaticValues.CiscoList)
                {
                    if (key.IP != "" && key.IP != null)
                    {
                        this.ComboBoxIpAddressCisco.Items.Add(key.IP);
                    }
                }
            
            
            // 
            // Community
            // 
            this.TextBoxCommunity = new System.Windows.Forms.TextBox();
            this.TextBoxCommunity.Location = new System.Drawing.Point(163, 95);
            this.TextBoxCommunity.Name = "Community";
            this.TextBoxCommunity.Size = new System.Drawing.Size(121, 21);
            this.TextBoxCommunity.TabIndex = 8;
            
            
                


            this.Controls.Add(this.TextBoxCommunity);
            this.Controls.Add(this.ComboBoxIpAddressCisco);
        
        }

        private void IP_verify(object sender, EventArgs e)
        {
            String ValidIpAddressRegex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
            if (this.ComboBoxIpAddressCisco.SelectedValue != null & !Regex.IsMatch(ComboBoxIpAddressCisco.Text, ValidIpAddressRegex))
            {
                MessageBox.Show("Введите корректно IP адрес");
                return;
            }
           
        }

        private void IP_selected(object sender, EventArgs e)
        {
            IPCom key = StaticValues.CiscoList.Find(delegate(IPCom x) 
            {
                return x.IP.Contains(ComboBoxIpAddressCisco.Text); 
            });
            TextBoxCommunity.Text = key.Com;
            this.ComboBoxIpAddressCisco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;


        }

        private void Save_Click(object sender, EventArgs e)
        {
            IPAddress address;
             if (!IPAddress.TryParse(ComboBoxIpAddressCisco.Text, out address))
             {
                 MessageBox.Show("Введите корректно IP адрес");
                 return;
             }

             //Проверяем существует ли такой ip адрес          
             if (ComboBoxIpAddressCisco.FindString(ComboBoxIpAddressCisco.Text) != -1)
             {
                 MessageBox.Show("Cisco с таким IPадресом уже добавлена");
                 return;
             }

            //идет ли пинг 
             try
             {
                 Ping pingSender = new Ping();
                 PingReply reply = pingSender.Send(ComboBoxIpAddressCisco.Text);
             }
            
            catch
             {
                    MessageBox.Show("IP адрес не доступен");
                    return;
             }

            //правильно ли указан community, то бишь может ли она получить доступ по SNMP
            UdpTarget target = new UdpTarget((IPAddress)new IpAddress(ComboBoxIpAddressCisco.Text), 161, 500, 0);
            Pdu pdu = new Pdu(PduType.Get);
            pdu.VbList.Add(".1.3.6.1.2.1.1.6.0");
            AgentParameters aparam = new AgentParameters(SnmpVersion.Ver2, new OctetString(TextBoxCommunity.Text));
            SnmpV2Packet response;
            try
            {
                response = target.Request(pdu, aparam) as SnmpV2Packet;
            }
            
            catch (Exception ex)
            {
                MessageBox.Show("connection failed");
                MessageBox.Show(ex.Message);
                target.Close();
                return;
            }


            var value = new IPCom(ComboBoxIpAddressCisco.Text, TextBoxCommunity.Text);

            var serviceContext = new WaterGateServiceContext();
            var list = StaticValues.CiscoList.ToList();
            list.Add(value);

            Save.Enabled = false;
            Delete.Enabled = false;
            Change.Enabled = false;
            Cursor = Cursors.AppStarting;
            serviceContext.UpdateCiscoRouters(list, (error) =>
            {
                if (error != null)
                {
                    MessageBox.Show( "Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("Запись успешно добавлена.",
                            "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //добавляем в наш массив
                        StaticValues.CiscoList.Add(value);

                        //добавляем в Combobox
                        this.ComboBoxIpAddressCisco.Items.Add(ComboBoxIpAddressCisco.Text);

                        TextBoxCommunity.Clear();
                        ComboBoxIpAddressCisco.ResetText();
                    }));
                }


                Invoke(new Action(() =>
                {
                    

                    Save.Enabled = true;
                    Delete.Enabled = true;
                    Change.Enabled = true;
                    Cursor = Cursors.Arrow;
                }));

            });
           
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (StaticValues.JDSUCiscoArray.Any(exampl => exampl.CiscoIPCom.IP == ComboBoxIpAddressCisco.Text))
            {
                // MessageBox.Show(StaticValues.JDSUCiscoArray.First(exampl => exampl.CiscoIPCom.IP == ComboBoxIpAddressCisco.Text).JDSUPort.ToString());
                MessageBox.Show("элемент коммутационного оборудования используется в конфигурации. Привязан к порту:",
                    StaticValues.JDSUCiscoArray.First(exampl => exampl.CiscoIPCom.IP == ComboBoxIpAddressCisco.Text)
                        .JDSUPort.ToString());
                return;
            }
            else
            {
                var value = StaticValues.CiscoList.FirstOrDefault(item => item.IP.Contains(ComboBoxIpAddressCisco.Text));
                if (value == null)
                    return;

                var serviceContext = new WaterGateServiceContext();
                var list = StaticValues.CiscoList.ToList();

                list.Remove(value);

                Save.Enabled = false;
                Delete.Enabled = false;
                Change.Enabled = false;
                Cursor = Cursors.AppStarting;
                serviceContext.UpdateCiscoRouters(list, (error) =>
                {
                    if (error != null)
                    {
                        MessageBox.Show("Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("Запись успешно удалена.",
                            "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //удаляем старый ip и community из combobox'а 
                        StaticValues.CiscoList.Remove(value);

                        //удаляем из Combobox
                        this.ComboBoxIpAddressCisco.Items.Remove(ComboBoxIpAddressCisco.Text);


                        TextBoxCommunity.Clear();
                        ComboBoxIpAddressCisco.SelectedIndex = -1;
                        this.ComboBoxIpAddressCisco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    }));


                    Invoke(new Action(() =>
                    {
                        Save.Enabled = true;
                        Delete.Enabled = true;
                        Change.Enabled = true;
                        Cursor = Cursors.Arrow;
                    }));
                });

            }
        }

        private void Change_Click(object sender, EventArgs e)
        {
            //правильно ли указан community, то бишь может ли она получить доступ по SNMP
            UdpTarget target = new UdpTarget((IPAddress)new IpAddress(ComboBoxIpAddressCisco.Text), 161, 500, 0);
            Pdu pdu = new Pdu(PduType.Get);
            pdu.VbList.Add(".1.3.6.1.2.1.1.6.0");
            AgentParameters aparam = new AgentParameters(SnmpVersion.Ver2, new OctetString(TextBoxCommunity.Text));
            SnmpV2Packet response;
            try
            {
                response = target.Request(pdu, aparam) as SnmpV2Packet;

            }

            catch (Exception ex)
            {
                MessageBox.Show("Скорее всего введен неверный Community");
                MessageBox.Show(ex.Message);
                target.Close();
                this.ComboBoxIpAddressCisco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                return;
            }



            var oldValue = StaticValues.CiscoList.FirstOrDefault(item => item.IP.Contains(ComboBoxIpAddressCisco.Text));
            if (oldValue == null)
                return;

            var newValue = new IPCom(ComboBoxIpAddressCisco.Text, TextBoxCommunity.Text);


            var serviceContext = new WaterGateServiceContext();
            var list = StaticValues.CiscoList.ToList();

            list.Remove(oldValue);
            list.Add(newValue);

            Save.Enabled = false;
            Delete.Enabled = false;
            Change.Enabled = false;
            Cursor = Cursors.AppStarting;



            foreach (var item in StaticValues.JDSUCiscoArray)
            {
                if (item.CiscoIPCom.IP == ComboBoxIpAddressCisco.Text)
                    item.CiscoIPCom.Com = TextBoxCommunity.Text;
            }

            //   var serviceContext = new WaterGateServiceContext();
            serviceContext.UpdatePorts(StaticValues.JDSUCiscoArray, (error) =>
            {
                if (error != null)
                {
                    Invoke(new Action(() =>
                        MessageBox.Show(
                            "Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                }
              
            });



            serviceContext.UpdateCiscoRouters(list, (error) =>
            {
                if (error != null)
                {
                    MessageBox.Show("Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Invoke(new Action(() =>
                {
                    MessageBox.Show("Запись успешно изменена.",
                        "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //удаляем старый ip и community     
                    StaticValues.CiscoList.Remove(oldValue);

                    //и добавляем в наш массив новые данные и все должно работать =)
                    StaticValues.CiscoList.Add(newValue);


                    ComboBoxIpAddressCisco.SelectedIndex = -1;
                    this.ComboBoxIpAddressCisco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
                    TextBoxCommunity.Clear();
                }));

                Invoke(new Action(() =>
                {
                    Save.Enabled = true;
                    Delete.Enabled = true;
                    Change.Enabled = true;
                    Cursor = Cursors.Arrow;
                }));
                
            });



        }

        

        
    }
}
