namespace WaterGate
{
    partial class PortsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PortsForm));
            this.SuspendLayout();
            // 
            // PortsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
           // this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(620, 600);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PortsForm";
            this.Text = "Назначить порты JDSU/Cisco";
            this.Load += new System.EventHandler(this.Ports_Load);
            this.ResumeLayout(false);

        }

        #endregion

     


        public System.Windows.Forms.DataGridView formJDSUPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn JDSUport;
        private System.Windows.Forms.DataGridViewComboBoxColumn CiscoIP;
        private System.Windows.Forms.DataGridViewComboBoxColumn CiscoPort;
        private System.Windows.Forms.DataGridViewButtonColumn SaveBut;
      
    
    }
       


}