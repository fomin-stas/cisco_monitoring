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
using WaterGate.Models;


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
        }
     
        private void buttonIP_Click(object sender, EventArgs e)
        {
            InputBox _InputBox = new InputBox();
            //Interaction.InputBox("Введите новый IP адрес");
            DialogResult dialogResult = _InputBox.ShowDialog(this);
        
            if (dialogResult == DialogResult.OK)
            {
                UpdateJDSUIP(new IPCom(_InputBox.InputBoxText.Trim(), lCommunity.Text.Trim()), () =>
                {
                    lIP.Text = _InputBox.InputBoxText.Trim();
                    StaticValues.JDSUIP.IP = lIP.Text;
                });  
            
            }    
        }

        private void buttonCom_Click(object sender, EventArgs e)
        {
            InputBox _InputBox = new InputBox();
            //Interaction.InputBox("Введите новый IP адрес");
            DialogResult dialogResult = _InputBox.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                UpdateJDSUIP(new IPCom(lIP.Text.Trim(), _InputBox.InputBoxText.Trim()), () =>
                {
                    lCommunity.Text = _InputBox.InputBoxText.Trim();
                    StaticValues.JDSUIP.Com = lCommunity.Text;
                });  
            }   
        }

        private void UpdateJDSUIP(IPCom jdsuIP, Action continueWith)
        {
            buttonIP.Enabled = false;
            buttonIP.Enabled = false;

            Cursor = Cursors.AppStarting;

            
            var serviceContext = new WaterGateServiceContext();
            serviceContext.UpdateJDSUIP(jdsuIP, (error) =>
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

                    continueWith();
                }));


                Invoke(new Action(() =>
                {
                    buttonIP.Enabled = true;
                    buttonIP.Enabled = true;
                    Cursor = Cursors.Arrow;
                }));
            });
        }
       
        
    }
}
