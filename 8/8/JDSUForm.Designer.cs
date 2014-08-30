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
            this.CheckDelayLabel = new System.Windows.Forms.Label();
            this.CheckDelayButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonIP
            // 
            this.buttonIP.Location = new System.Drawing.Point(176, 75);
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
            this.lIP.Location = new System.Drawing.Point(23, 75);
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
            this.lCommunity.Location = new System.Drawing.Point(23, 104);
            this.lCommunity.Name = "lCommunity";
            this.lCommunity.Size = new System.Drawing.Size(118, 23);
            this.lCommunity.TabIndex = 3;
            this.lCommunity.Text = "label2";
            this.lCommunity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCom
            // 
            this.buttonCom.Location = new System.Drawing.Point(176, 104);
            this.buttonCom.Name = "buttonCom";
            this.buttonCom.Size = new System.Drawing.Size(173, 23);
            this.buttonCom.TabIndex = 4;
            this.buttonCom.Text = "Изменить Community JDSU";
            this.buttonCom.UseVisualStyleBackColor = true;
            this.buttonCom.Click += new System.EventHandler(this.buttonCom_Click);
            // 
            // CheckDelayLabel
            // 
            this.CheckDelayLabel.BackColor = System.Drawing.Color.LavenderBlush;
            this.CheckDelayLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CheckDelayLabel.Location = new System.Drawing.Point(23, 133);
            this.CheckDelayLabel.Name = "CheckDelayLabel";
            this.CheckDelayLabel.Size = new System.Drawing.Size(118, 23);
            this.CheckDelayLabel.TabIndex = 5;
            this.CheckDelayLabel.Text = "CheckDelay";
            this.CheckDelayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CheckDelayButton
            // 
            this.CheckDelayButton.Location = new System.Drawing.Point(176, 133);
            this.CheckDelayButton.Name = "CheckDelayButton";
            this.CheckDelayButton.Size = new System.Drawing.Size(173, 23);
            this.CheckDelayButton.TabIndex = 6;
            this.CheckDelayButton.Text = "Изменить интервал опроса";
            this.CheckDelayButton.UseVisualStyleBackColor = true;
            this.CheckDelayButton.Click += new System.EventHandler(this.CheckDelayButton_Click);
            // 
            // JDSUForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 179);
            this.Controls.Add(this.CheckDelayButton);
            this.Controls.Add(this.CheckDelayLabel);
            this.Controls.Add(this.buttonCom);
            this.Controls.Add(this.lCommunity);
            this.Controls.Add(this.lIP);
            this.Controls.Add(this.buttonIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "JDSUForm";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Сконфигурировать JDSU";
            this.Load += new System.EventHandler(this.JDSUForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonIP;
        private System.Windows.Forms.Label lIP;
        private System.Windows.Forms.Label lCommunity;
        private System.Windows.Forms.Button buttonCom;
        private System.Windows.Forms.Label CheckDelayLabel;
        private System.Windows.Forms.Button CheckDelayButton;

    }
}