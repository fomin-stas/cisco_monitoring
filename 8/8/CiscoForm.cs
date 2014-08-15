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
using SnmpSharpNet;
using System.Xml.Linq;
using System.Xml;
using StaticValuesDll;

namespace WaterGate
{
    public partial class CiscoForm : Form
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
            this.ComboBoxIpAddressCisco.Location = new System.Drawing.Point(16, 55);
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
            this.TextBoxCommunity.Location = new System.Drawing.Point(163, 54);
            this.TextBoxCommunity.Name = "Community";
            this.TextBoxCommunity.Size = new System.Drawing.Size(121, 21);
            this.TextBoxCommunity.TabIndex = 8;
            
            
                


            this.Controls.Add(this.TextBoxCommunity);
            this.Controls.Add(this.ComboBoxIpAddressCisco);
        
        }

        private void IP_verify(object sender, EventArgs e)
        {
            String ValidIpAddressRegex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
            if (!Regex.IsMatch(ComboBoxIpAddressCisco.Text, ValidIpAddressRegex))
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

            //добавляем изменения в xmlку
            XmlDocument doc = new XmlDocument();
            doc.Load(Functions.pathConfig);
            XmlNode Cisco = doc.DocumentElement.SelectSingleNode("Cisco");
           
            XmlElement elem = doc.CreateElement("IPCom");

            elem.SetAttribute("Com", TextBoxCommunity.Text);
            elem.SetAttribute("IP", ComboBoxIpAddressCisco.Text);
            Cisco.AppendChild(elem);
            doc.Save(Functions.pathConfig);
     

            //добавляем в наш массив
            StaticValues.CiscoList.Add(new IPCom(ComboBoxIpAddressCisco.Text, TextBoxCommunity.Text));
                
            //добавляем в Combobox
            this.ComboBoxIpAddressCisco.Items.Add(ComboBoxIpAddressCisco.Text);

            TextBoxCommunity.Clear();
            ComboBoxIpAddressCisco.ResetText();

            MessageBox.Show("Запись добавлена");
        }

        private void Delete_Click(object sender, EventArgs e)
        {
           
            //добавляем изменения в xmlку
            XmlDocument doc = new XmlDocument();
            doc.Load(Functions.pathConfig);
            XmlNode Cisco = doc.DocumentElement.SelectSingleNode("Cisco");

           List<XmlNode> nodes = doc.DocumentElement.SelectSingleNode("Cisco")
                .Cast<XmlNode>().Where(a => a.Attributes["IP"].Value.ToString() == ComboBoxIpAddressCisco.Text).ToList();

            foreach (var el in nodes)
            {
               // if (el.GetAttribute("IP") == ComboBoxIpAddressCisco.Text)
              //  {
                    Cisco.RemoveChild(el);
              //  }
            }

            doc.Save(Functions.pathConfig);




            //удаляем старый ip и community из combobox'а 
            StaticValues.CiscoList.Remove(StaticValues.CiscoList.Find(delegate(IPCom x)
            {
                return x.IP.Contains(ComboBoxIpAddressCisco.Text);
            }
            ));
            
            //удаляем из Combobox
            this.ComboBoxIpAddressCisco.Items.Remove(ComboBoxIpAddressCisco.Text);

            
            TextBoxCommunity.Clear();
            ComboBoxIpAddressCisco.SelectedIndex = -1;
            this.ComboBoxIpAddressCisco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;

            MessageBox.Show("Запись удалена");

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
          
            
            //добавляем изменения в xmlку
            XmlDocument doc = new XmlDocument();
            doc.Load(Functions.pathConfig);
            XmlNode Cisco = doc.DocumentElement.SelectSingleNode("Cisco");

            List<XmlNode> nodes = doc.DocumentElement.SelectSingleNode("Cisco")
                 .Cast<XmlNode>().Where(a => a.Attributes["IP"].Value.ToString() == ComboBoxIpAddressCisco.Text).ToList();

            foreach (var el in nodes)
            {
                // if (el.GetAttribute("IP") == ComboBoxIpAddressCisco.Text)
                //  {
                el.Attributes["Com"].Value = TextBoxCommunity.Text;
                //  }
            }

            doc.Save(Functions.pathConfig);




            //удаляем старый ip и community     
            StaticValues.CiscoList.Remove(StaticValues.CiscoList.Find(delegate(IPCom x)
            {
                return x.IP.Contains(ComboBoxIpAddressCisco.Text);
            
            }
            ));
           
            //и добавляем в наш массив новые данные и все должно работать =)
            StaticValues.CiscoList.Add(new IPCom(ComboBoxIpAddressCisco.Text, TextBoxCommunity.Text));


            

            MessageBox.Show("Community изменена");

          
            ComboBoxIpAddressCisco.SelectedIndex = -1;
            this.ComboBoxIpAddressCisco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            TextBoxCommunity.Clear();
        }

        

        
    }
}
