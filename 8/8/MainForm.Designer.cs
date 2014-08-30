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
            this.components = new System.ComponentModel.Container();
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
            this.mainDataGridView = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.JDSUStatusLabel = new System.Windows.Forms.Label();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.JDSUport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoIP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CiscoPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CheckPortColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.SwitchColumn = new System.Windows.Forms.DataGridViewImageColumn();
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
            this.справкаToolStripMenuItem});
            this.topMenuStrip.Location = new System.Drawing.Point(0, 60);
            this.topMenuStrip.Name = "topMenuStrip";
            this.topMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.topMenuStrip.Size = new System.Drawing.Size(737, 24);
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
            this.DescriptionColumn,
            this.CheckPortColumn,
            this.SwitchColumn});
            this.mainDataGridView.Location = new System.Drawing.Point(0, 99);
            this.mainDataGridView.Name = "mainDataGridView";
            this.mainDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mainDataGridView.Size = new System.Drawing.Size(737, 410);
            this.mainDataGridView.TabIndex = 1;
            this.mainDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDataGridView_CellClick);
            this.mainDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            this.mainDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.mainDataGridView_CellEndEdit);
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
            // JDSUStatusLabel
            // 
            this.JDSUStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JDSUStatusLabel.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.JDSUStatusLabel.ForeColor = System.Drawing.Color.White;
            this.JDSUStatusLabel.Location = new System.Drawing.Point(659, 60);
            this.JDSUStatusLabel.Name = "JDSUStatusLabel";
            this.JDSUStatusLabel.Size = new System.Drawing.Size(78, 24);
            this.JDSUStatusLabel.TabIndex = 3;
            this.JDSUStatusLabel.Text = "JDSU Active";
            this.JDSUStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "WaterGate";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.BalloonTipClicked += new System.EventHandler(this.NotifyIcon_BalloonTipClicked);
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // JDSUport
            // 
            this.JDSUport.HeaderText = "JDSU порт";
            this.JDSUport.Name = "JDSUport";
            this.JDSUport.ReadOnly = true;
            this.JDSUport.Resizable = System.Windows.Forms.DataGridViewTriState.True;
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
            // DescriptionColumn
            // 
            this.DescriptionColumn.HeaderText = "Описание";
            this.DescriptionColumn.Name = "DescriptionColumn";
            // 
            // CheckPortColumn
            // 
            this.CheckPortColumn.HeaderText = "Проверить работоспособность";
            this.CheckPortColumn.Name = "CheckPortColumn";
            // 
            // SwitchColumn
            // 
            this.SwitchColumn.HeaderText = "Принудительно включить порт";
            this.SwitchColumn.Name = "SwitchColumn";
            this.SwitchColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(737, 532);
            this.Controls.Add(this.JDSUStatusLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mainDataGridView);
            this.Controls.Add(this.topMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.topMenuStrip;
            this.Name = "MainForm";
            this.Text = "Water Gate";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
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
        private System.Windows.Forms.DataGridView mainDataGridView;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label JDSUStatusLabel;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn JDSUport;
        private System.Windows.Forms.DataGridViewTextBoxColumn CiscoIP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CiscoPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
        private System.Windows.Forms.DataGridViewImageColumn CheckPortColumn;
        private System.Windows.Forms.DataGridViewImageColumn SwitchColumn;
     

    }
}

