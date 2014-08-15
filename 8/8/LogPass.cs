using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterGate
{
    public partial class LogPass : Form
    {
        public LogPass()
        {
            InitializeComponent();
        }
       
        public string Log
        {
            get { return textBoxLog.Text; }
        }


        public string Pas
        {
            get { return textBoxPas.Text; }
        }


        private void LogPass_Load(object sender, EventArgs e)
        {
            bOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            bCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }



    }
}
