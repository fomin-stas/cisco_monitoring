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
    public partial class UsersManage : Form
    {
        public UsersManage()
        {
            InitializeComponent();


            this.UserForm = new System.Windows.Forms.DataGridView();
            this.UserLogin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserRole = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.other = new System.Windows.Forms.DataGridViewTextBoxColumn();
           

            ((System.ComponentModel.ISupportInitialize)(this.UserForm)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1


            this.UserForm.AllowUserToOrderColumns = true;
            this.UserForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserForm.AllowUserToResizeColumns = true;
            this.UserForm.AllowUserToResizeRows = true;
            this.UserForm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.UserForm.ReadOnly = true;
            this.UserForm.AllowUserToAddRows = true;
           
            this.UserForm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserLogin,
            this.UserRole,
            this.other});

            this.UserForm.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top);
            this.UserForm.Size = new Size(445, 224);
            this.UserForm.Name = "UserForm";
            this.UserForm.Location = new System.Drawing.Point(0, 0);
            this.UserForm.TabIndex = 0;

            // 
            // UserLogin
            // 
            this.UserLogin.HeaderText = "Логин";
            this.UserLogin.Name = "UserLogin";
            this.UserLogin.Width = 120;
            // 
            // UserRole
            // 
            this.UserRole.HeaderText = "Группа";
            this.UserRole.Name = "UserRole";
            this.UserRole.Width = 120;
            // 
            // other
            // 
            this.other.HeaderText = "Описание";
            this.other.Name = "other";
            this.other.Width = 120;
            // 
          

            this.Controls.Add(this.UserForm);

            ((System.ComponentModel.ISupportInitialize)(this.UserForm)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            
        
        
        
        
        
        
        }



        private void UsersManage_Load(object sender, EventArgs e)
        {
       
        }
    }
}
