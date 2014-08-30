using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using MetroFramework.Forms;
using StaticValuesDll;
using WaterGate.Models;


namespace WaterGate
{
    
    public partial class JDSUForm : MetroForm
    {
        public JDSUForm()
        {
            InitializeComponent();
        }
    

        private void JDSUForm_Load(object sender, EventArgs e)
        {
            lIP.Text = StaticValues.JDSUIP.IP;
            lCommunity.Text = StaticValues.JDSUIP.Com;
            CheckDelayLabel.Text = StaticValues.CheckDelay.ToString(CultureInfo.InvariantCulture);
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
            ChangeButtonsEnabledState(false);
            Cursor = Cursors.AppStarting;
    
            var serviceContext = new WaterGateServiceContext();
            serviceContext.UpdateJDSUIP(jdsuIP, (error) =>
            {
                if (error != null)
                {
                    MessageBox.Show("Произошла ошибка при соединении с сервером, проверьте наличие соединения.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    Invoke(new Action(() =>
                    {
                        MessageBox.Show("Запись успешно изменена.",
                            "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        continueWith();
                    }));


                Invoke(new Action(() =>
                {
                    ChangeButtonsEnabledState(true);
                    Cursor = Cursors.Arrow;
                }));
            });
        }

        private void CheckDelayButton_Click(object sender, EventArgs e)
        {
            var inputBox = new InputBox();

            var dialogResult = inputBox.ShowDialog(this);

            if (dialogResult == DialogResult.OK)
            {
                double value = 0;
                try
                {
                    value = double.Parse(inputBox.InputBoxText.Trim(), CultureInfo.InvariantCulture);
                    if(value < 0)
                        throw new ArgumentException();
                }
                catch
                {
                    MessageBox.Show("Значение должно быть положительным числом.", "Некорректное значение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

               
                ChangeButtonsEnabledState(false);
                Cursor = Cursors.AppStarting;

                var serviceContext = new WaterGateServiceContext();
                serviceContext.UpdateCheckDelayAsync(value, (error) =>
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

                        CheckDelayLabel.Text = value.ToString(CultureInfo.InvariantCulture);
                        StaticValues.CheckDelay = value;
                    }));

                    Invoke(new Action(() =>
                    {
                        ChangeButtonsEnabledState(true);
                        Cursor = Cursors.Arrow;
                    }));
                });
            }    
        }

        private void ChangeButtonsEnabledState(bool state)
        {
            buttonIP.Enabled = state;
            buttonCom.Enabled = state;
            CheckDelayButton.Enabled = state;
        }
       
        
    }
}
