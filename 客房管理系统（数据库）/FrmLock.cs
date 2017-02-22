using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelManager.Common;

namespace 客房管理系统_数据库_
{
    public partial class FrmLock : Form
    {
        public FrmLock()
        {
            InitializeComponent();
        }

        private void btnUnLock_Click(object sender, EventArgs e)
        {
            if (this.txtPassWord.Text == Variable.PassWord)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("密码输入错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
