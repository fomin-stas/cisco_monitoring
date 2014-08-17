using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WaterGate
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        public string InputBoxText
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        private void InputBox_Load(object sender, EventArgs e)
        {

            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            
            button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

     

    }
}
