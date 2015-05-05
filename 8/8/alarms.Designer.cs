namespace WaterGate
{
    partial class alarms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(alarms));
            this.AlarmDataGridView = new System.Windows.Forms.DataGridView();
            this.NameOfAlarm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Execute = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.leveldataGridView = new System.Windows.Forms.DataGridView();
            this.levelAlarm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExecuteLevel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.AlarmDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.leveldataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AlarmDataGridView
            // 
            this.AlarmDataGridView.AllowUserToAddRows = false;
            this.AlarmDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.AlarmDataGridView.ColumnHeadersHeight = 50;
            this.AlarmDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AlarmDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameOfAlarm,
            this.Execute});
            this.AlarmDataGridView.Location = new System.Drawing.Point(12, 63);
            this.AlarmDataGridView.Name = "AlarmDataGridView";
            this.AlarmDataGridView.RowHeadersVisible = false;
            this.AlarmDataGridView.Size = new System.Drawing.Size(341, 254);
            this.AlarmDataGridView.TabIndex = 2;
            // 
            // NameOfAlarm
            // 
            this.NameOfAlarm.Frozen = true;
            this.NameOfAlarm.HeaderText = "Name Of Alarm";
            this.NameOfAlarm.Name = "NameOfAlarm";
            this.NameOfAlarm.ReadOnly = true;
            this.NameOfAlarm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NameOfAlarm.Width = 270;
            // 
            // Execute
            // 
            this.Execute.Frozen = true;
            this.Execute.HeaderText = "Execute";
            this.Execute.Name = "Execute";
            this.Execute.ReadOnly = true;
            this.Execute.Width = 70;
            // 
            // leveldataGridView
            // 
            this.leveldataGridView.AllowUserToAddRows = false;
            this.leveldataGridView.BackgroundColor = System.Drawing.Color.White;
            this.leveldataGridView.ColumnHeadersHeight = 50;
            this.leveldataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.leveldataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.levelAlarm,
            this.ExecuteLevel});
            this.leveldataGridView.Location = new System.Drawing.Point(381, 63);
            this.leveldataGridView.Name = "leveldataGridView";
            this.leveldataGridView.RowHeadersVisible = false;
            this.leveldataGridView.Size = new System.Drawing.Size(169, 254);
            this.leveldataGridView.TabIndex = 3;
            // 
            // levelAlarm
            // 
            this.levelAlarm.Frozen = true;
            this.levelAlarm.HeaderText = "Уровень аварии";
            this.levelAlarm.Name = "levelAlarm";
            this.levelAlarm.ReadOnly = true;
            this.levelAlarm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExecuteLevel
            // 
            this.ExecuteLevel.Frozen = true;
            this.ExecuteLevel.HeaderText = "Execute";
            this.ExecuteLevel.Name = "ExecuteLevel";
            this.ExecuteLevel.ReadOnly = true;
            this.ExecuteLevel.Width = 70;
            // 
            // alarms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 317);
            this.Controls.Add(this.leveldataGridView);
            this.Controls.Add(this.AlarmDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "alarms";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "alarms";
            this.Load += new System.EventHandler(this.alarms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AlarmDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.leveldataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView AlarmDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameOfAlarm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Execute;
        private System.Windows.Forms.DataGridView leveldataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelAlarm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ExecuteLevel;

    }
}