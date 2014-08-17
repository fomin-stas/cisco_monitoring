﻿namespace WaterGate
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
            this.portsDataGridView = new System.Windows.Forms.DataGridView();
            this.SaveButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.RemoveButton = new System.Windows.Forms.Button();
            this.JDSUport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoIP = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CiscoPort = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.buttonON = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.portsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // portsDataGridView
            // 
            this.portsDataGridView.AllowUserToAddRows = false;
            this.portsDataGridView.AllowUserToDeleteRows = false;
            this.portsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.portsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.portsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JDSUport,
            this.CiscoIP,
            this.CiscoPort,
            this.buttonON});
            this.portsDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.portsDataGridView.Location = new System.Drawing.Point(0, 24);
            this.portsDataGridView.Name = "portsDataGridView";
            this.portsDataGridView.Size = new System.Drawing.Size(620, 527);
            this.portsDataGridView.TabIndex = 0;
            this.portsDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.formJDSUPort_CellContentClick);
            this.portsDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.formJDSUPort_CellEndEdit);
            this.portsDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.formJDSUPort_CurrentCellDirtyStateChanged);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(12, 557);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(119, 31);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Сохранить";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(395, 557);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(104, 31);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Добавить";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RemoveButton
            // 
            this.RemoveButton.Location = new System.Drawing.Point(505, 557);
            this.RemoveButton.Name = "RemoveButton";
            this.RemoveButton.Size = new System.Drawing.Size(103, 31);
            this.RemoveButton.TabIndex = 3;
            this.RemoveButton.Text = "Удалить";
            this.RemoveButton.UseVisualStyleBackColor = true;
            this.RemoveButton.Click += new System.EventHandler(this.RemoveButton_Click);
            // 
            // JDSUport
            // 
            this.JDSUport.HeaderText = "JDSU порт";
            this.JDSUport.Name = "JDSUport";
            // 
            // CiscoIP
            // 
            this.CiscoIP.HeaderText = "Cisco IP";
            this.CiscoIP.Name = "CiscoIP";
            this.CiscoIP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CiscoIP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // CiscoPort
            // 
            this.CiscoPort.HeaderText = "Cisco порт";
            this.CiscoPort.Name = "CiscoPort";
            this.CiscoPort.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CiscoPort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // buttonON
            // 
            this.buttonON.HeaderText = "Принудительно включить порт";
            this.buttonON.Name = "buttonON";
            this.buttonON.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.buttonON.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // PortsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 600);
            this.Controls.Add(this.RemoveButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.portsDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PortsForm";
            this.Text = "Назначить порты JDSU/Cisco";
            this.Load += new System.EventHandler(this.Ports_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView portsDataGridView;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button RemoveButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn JDSUport;
        private System.Windows.Forms.DataGridViewComboBoxColumn CiscoIP;
        private System.Windows.Forms.DataGridViewComboBoxColumn CiscoPort;
        private System.Windows.Forms.DataGridViewButtonColumn buttonON;



    }
       


}