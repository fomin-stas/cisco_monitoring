namespace WaterGate
{
    partial class JDSUForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JDSUForm));
            this.buttonIP = new System.Windows.Forms.Button();
            this.lIP = new System.Windows.Forms.Label();
            this.lCommunity = new System.Windows.Forms.Label();
            this.buttonCom = new System.Windows.Forms.Button();
            this.buttonn = new System.Windows.Forms.Button();
            this.ln = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonIP
            // 
            this.buttonIP.Location = new System.Drawing.Point(188, 34);
            this.buttonIP.Name = "buttonIP";
            this.buttonIP.Size = new System.Drawing.Size(173, 23);
            this.buttonIP.TabIndex = 0;
            this.buttonIP.Text = "Изменить IP адрес JDSU";
            this.buttonIP.UseVisualStyleBackColor = true;
            this.buttonIP.Click += new System.EventHandler(this.buttonIP_Click);
            // 
            // lIP
            // 
            this.lIP.BackColor = System.Drawing.Color.LavenderBlush;
            this.lIP.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lIP.Location = new System.Drawing.Point(35, 34);
            this.lIP.Name = "lIP";
            this.lIP.Size = new System.Drawing.Size(118, 23);
            this.lIP.TabIndex = 2;
            this.lIP.Text = "label1";
            this.lIP.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lCommunity
            // 
            this.lCommunity.BackColor = System.Drawing.Color.LavenderBlush;
            this.lCommunity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lCommunity.Location = new System.Drawing.Point(35, 63);
            this.lCommunity.Name = "lCommunity";
            this.lCommunity.Size = new System.Drawing.Size(118, 23);
            this.lCommunity.TabIndex = 3;
            this.lCommunity.Text = "label2";
            this.lCommunity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCom
            // 
            this.buttonCom.Location = new System.Drawing.Point(188, 63);
            this.buttonCom.Name = "buttonCom";
            this.buttonCom.Size = new System.Drawing.Size(173, 23);
            this.buttonCom.TabIndex = 4;
            this.buttonCom.Text = "Изменить Community JDSU";
            this.buttonCom.UseVisualStyleBackColor = true;
            this.buttonCom.Click += new System.EventHandler(this.buttonCom_Click);
            // 
            // buttonn
            // 
            this.buttonn.Location = new System.Drawing.Point(188, 92);
            this.buttonn.Name = "buttonn";
            this.buttonn.Size = new System.Drawing.Size(173, 23);
            this.buttonn.TabIndex = 5;
            this.buttonn.Text = "Изменить Community JDSU";
            this.buttonn.UseVisualStyleBackColor = true;
            this.buttonn.Click += new System.EventHandler(this.buttonn_Click);
            // 
            // ln
            // 
            this.ln.BackColor = System.Drawing.Color.LavenderBlush;
            this.ln.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ln.Location = new System.Drawing.Point(35, 92);
            this.ln.Name = "ln";
            this.ln.Size = new System.Drawing.Size(118, 23);
            this.ln.TabIndex = 6;
            this.ln.Text = "label2";
            this.ln.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JDSUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 145);
            this.Controls.Add(this.ln);
            this.Controls.Add(this.buttonn);
            this.Controls.Add(this.buttonCom);
            this.Controls.Add(this.lCommunity);
            this.Controls.Add(this.lIP);
            this.Controls.Add(this.buttonIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "JDSUForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сконфигурировать JDSU";
            this.Load += new System.EventHandler(this.JDSUForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonIP;
        private System.Windows.Forms.Label lIP;
        private System.Windows.Forms.Label lCommunity;
        private System.Windows.Forms.Button buttonCom;
        private System.Windows.Forms.Button buttonn;
        private System.Windows.Forms.Label ln;

    }
}