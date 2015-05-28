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
            ((System.ComponentModel.ISupportInitialize)(this.AlarmDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // AlarmDataGridView
            // 
            this.AlarmDataGridView.AllowUserToAddRows = false;
            this.AlarmDataGridView.AllowUserToResizeRows = false;
            this.AlarmDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.AlarmDataGridView.ColumnHeadersHeight = 50;
            this.AlarmDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.AlarmDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameOfAlarm,
            this.Execute});
            this.AlarmDataGridView.Location = new System.Drawing.Point(12, 63);
            this.AlarmDataGridView.Name = "AlarmDataGridView";
            this.AlarmDataGridView.ReadOnly = true;
            this.AlarmDataGridView.RowHeadersVisible = false;
            this.AlarmDataGridView.Size = new System.Drawing.Size(341, 254);
            this.AlarmDataGridView.TabIndex = 2;
            this.AlarmDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.AlarmDataGridView_CellContentClick_1);
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
            // alarms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 317);
            this.Controls.Add(this.AlarmDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "alarms";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "alarms";
            this.Load += new System.EventHandler(this.alarms_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AlarmDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        private void AlarmDataGridView_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.DataGridView AlarmDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameOfAlarm;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Execute;

    }
}