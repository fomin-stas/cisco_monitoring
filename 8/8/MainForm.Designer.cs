namespace WaterGate
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.topMenuStrip = new System.Windows.Forms.MenuStrip();
            this.настройкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.добавитьCiscoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.назначитьПортыJDSUCiscoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сконфигурироватьJDSUToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.списокАварийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.управлениеУчетнымиЗаписямиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jdsuIsActiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.jdsuIsActiveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.JDSUport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.switchON = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.topMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // topMenuStrip
            // 
            this.topMenuStrip.AllowItemReorder = true;
            this.topMenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.topMenuStrip.AutoSize = false;
            this.topMenuStrip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.topMenuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.topMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.настройкаToolStripMenuItem,
            this.справкаToolStripMenuItem,
            this.jdsuIsActiveToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.jdsuIsActiveToolStripMenuItem1});
            this.topMenuStrip.Location = new System.Drawing.Point(0, 60);
            this.topMenuStrip.Name = "topMenuStrip";
            this.topMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.topMenuStrip.Size = new System.Drawing.Size(525, 24);
            this.topMenuStrip.TabIndex = 0;
            this.topMenuStrip.Text = "menuStrip1";
            // 
            // настройкаToolStripMenuItem
            // 
            this.настройкаToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.настройкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьCiscoToolStripMenuItem,
            this.назначитьПортыJDSUCiscoToolStripMenuItem,
            this.сконфигурироватьJDSUToolStripMenuItem,
            this.списокАварийToolStripMenuItem,
            this.управлениеУчетнымиЗаписямиToolStripMenuItem});
            this.настройкаToolStripMenuItem.Name = "настройкаToolStripMenuItem";
            this.настройкаToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.настройкаToolStripMenuItem.Text = "Настройка";
            // 
            // добавитьCiscoToolStripMenuItem
            // 
            this.добавитьCiscoToolStripMenuItem.Name = "добавитьCiscoToolStripMenuItem";
            this.добавитьCiscoToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.добавитьCiscoToolStripMenuItem.Text = "Добавить/удалить Cisco";
            this.добавитьCiscoToolStripMenuItem.Click += new System.EventHandler(this.добавитьCiscoToolStripMenuItem_Click);
            // 
            // назначитьПортыJDSUCiscoToolStripMenuItem
            // 
            this.назначитьПортыJDSUCiscoToolStripMenuItem.Name = "назначитьПортыJDSUCiscoToolStripMenuItem";
            this.назначитьПортыJDSUCiscoToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.назначитьПортыJDSUCiscoToolStripMenuItem.Text = "Назначить порты JDSU/Cisco";
            this.назначитьПортыJDSUCiscoToolStripMenuItem.Click += new System.EventHandler(this.назначитьПортыJDSUCiscoToolStripMenuItem_Click);
            // 
            // сконфигурироватьJDSUToolStripMenuItem
            // 
            this.сконфигурироватьJDSUToolStripMenuItem.Name = "сконфигурироватьJDSUToolStripMenuItem";
            this.сконфигурироватьJDSUToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.сконфигурироватьJDSUToolStripMenuItem.Text = "Сконфигурировать JDSU";
            this.сконфигурироватьJDSUToolStripMenuItem.Click += new System.EventHandler(this.сконфигурироватьJDSUToolStripMenuItem_Click);
            // 
            // списокАварийToolStripMenuItem
            // 
            this.списокАварийToolStripMenuItem.Name = "списокАварийToolStripMenuItem";
            this.списокАварийToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.списокАварийToolStripMenuItem.Text = "Список аварий";
            this.списокАварийToolStripMenuItem.Click += new System.EventHandler(this.списокАварийToolStripMenuItem_Click);
            // 
            // управлениеУчетнымиЗаписямиToolStripMenuItem
            // 
            this.управлениеУчетнымиЗаписямиToolStripMenuItem.Name = "управлениеУчетнымиЗаписямиToolStripMenuItem";
            this.управлениеУчетнымиЗаписямиToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.управлениеУчетнымиЗаписямиToolStripMenuItem.Text = "Управление учетными записями";
            this.управлениеУчетнымиЗаписямиToolStripMenuItem.Click += new System.EventHandler(this.управлениеУчетнымиЗаписямиToolStripMenuItem_Click);
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLight;
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // jdsuIsActiveToolStripMenuItem
            // 
            this.jdsuIsActiveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.jdsuIsActiveToolStripMenuItem.Name = "jdsuIsActiveToolStripMenuItem";
            this.jdsuIsActiveToolStripMenuItem.Size = new System.Drawing.Size(22, 20);
            this.jdsuIsActiveToolStripMenuItem.Text = " ";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem1.Text = " ";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem2.Text = " ";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem3.Text = " ";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem4.Text = " ";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem5.Text = " ";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem6.Text = " ";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(22, 20);
            this.toolStripMenuItem7.Text = " ";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(31, 20);
            this.toolStripMenuItem8.Text = "    ";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(25, 20);
            this.toolStripMenuItem9.Text = "  ";
            // 
            // jdsuIsActiveToolStripMenuItem1
            // 
            this.jdsuIsActiveToolStripMenuItem1.BackColor = System.Drawing.Color.DarkGreen;
            this.jdsuIsActiveToolStripMenuItem1.Name = "jdsuIsActiveToolStripMenuItem1";
            this.jdsuIsActiveToolStripMenuItem1.Size = new System.Drawing.Size(87, 20);
            this.jdsuIsActiveToolStripMenuItem1.Text = "Jdsu is active";
            // 
            // mainDataGridView
            // 
            this.mainDataGridView.AllowUserToAddRows = false;
            this.mainDataGridView.AllowUserToDeleteRows = false;
            this.mainDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mainDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.mainDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.mainDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JDSUport,
            this.CiscoIP,
            this.CiscoPort,
            this.switchON});
            this.mainDataGridView.Location = new System.Drawing.Point(0, 99);
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.ReadOnly = true;
            this.mainDataGridView.Size = new System.Drawing.Size(525, 409);
            this.mainDataGridView.TabIndex = 1;
            this.mainDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // JDSUport
            // 
            this.JDSUport.HeaderText = "JDSU порт";
            this.JDSUport.Name = "JDSUport";
            this.JDSUport.ReadOnly = true;
            // 
            // CiscoIP
            // 
            this.CiscoIP.HeaderText = "Cisco IP";
            this.CiscoIP.Name = "CiscoIP";
            this.CiscoIP.ReadOnly = true;
            // 
            // CiscoPort
            // 
            this.CiscoPort.HeaderText = "Cisco порт";
            this.CiscoPort.Name = "CiscoPort";
            this.CiscoPort.ReadOnly = true;
            // 
            // switchON
            // 
            this.switchON.HeaderText = "Принудительно включить порт";
            this.switchON.Name = "switchON";
            this.switchON.ReadOnly = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WaterGate.Properties.Resources.voip_gateway_256;
            this.pictureBox1.Location = new System.Drawing.Point(154, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 37);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(525, 531);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mainDataGridView);
            this.Controls.Add(this.topMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.topMenuStrip;
            this.Name = "MainForm";
            this.Text = "Water Gate";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.topMenuStrip.ResumeLayout(false);
            this.topMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion





        private System.Windows.Forms.MenuStrip topMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem настройкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem добавитьCiscoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem назначитьПортыJDSUCiscoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сконфигурироватьJDSUToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem списокАварийToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem управлениеУчетнымиЗаписямиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jdsuIsActiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem jdsuIsActiveToolStripMenuItem1;
        private System.Windows.Forms.DataGridView mainDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn JDSUport;
        private System.Windows.Forms.DataGridViewTextBoxColumn CiscoIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CiscoPort;
        private System.Windows.Forms.DataGridViewButtonColumn switchON;
        private System.Windows.Forms.PictureBox pictureBox1;
     

    }
}

