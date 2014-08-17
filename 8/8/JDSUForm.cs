using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using StaticValuesDll;


namespace WaterGate
{
    
    public partial class JDSUForm : Form
    {
        public JDSUForm()
        {
            InitializeComponent();
        }
    

        private void JDSUForm_Load(object sender, EventArgs e)
        {
            lIP.Text = StaticValues.JDSUIP.IP;
            lCommunity.Text = StaticValues.JDSUIP.Com;
            ln.Text = Convert.ToString(StaticValues.JDSUCiscoArray.Count);
        }
     
        private void buttonIP_Click(object sender, EventArgs e)
        {
            InputBox _InputBox = new InputBox();
            //Interaction.InputBox("Введите новый IP адрес");
            DialogResult dialogResult = _InputBox.ShowDialog(this);
        
            if (dialogResult == DialogResult.OK)
            {
               

                lIP.Text = _InputBox.InputBoxText;

                StaticValues.JDSUIP.IP =_InputBox.InputBoxText;


                //добавляем изменения в xmlку
                XmlDocument doc = new XmlDocument();
                doc.Load(Functions.pathConfig);


                XmlNode node = doc.DocumentElement.SelectSingleNode("JDSUIP");

                node.Attributes["IP"].Value = _InputBox.InputBoxText;


                doc.Save(Functions.pathConfig);



            
            }

        
        
        }

        private void buttonCom_Click(object sender, EventArgs e)
        {
            InputBox _InputBox = new InputBox();
            //Interaction.InputBox("Введите новый IP адрес");
            DialogResult dialogResult = _InputBox.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                lCommunity.Text = _InputBox.InputBoxText;
                StaticValues.JDSUIP.Com = _InputBox.InputBoxText;
                //добавляем изменения в xmlку
                XmlDocument doc = new XmlDocument();
                doc.Load(Functions.pathConfig);


                XmlNode node = doc.DocumentElement.SelectSingleNode("JDSUIP");

                node.Attributes["Com"].Value = _InputBox.InputBoxText;


                doc.Save(Functions.pathConfig);


            
            
            
            }   
        }

        private void buttonn_Click(object sender, EventArgs e)
        {
            InputBox _InputBox = new InputBox();
            //Interaction.InputBox("Введите новый IP адрес");
            DialogResult dialogResult = _InputBox.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    

                   


                    ln.Text = _InputBox.InputBoxText;
                    
                }
                catch
                {
                    MessageBox.Show("ВВедены некорректные данные");
                
                }
            }   
        }

        
    }
}
