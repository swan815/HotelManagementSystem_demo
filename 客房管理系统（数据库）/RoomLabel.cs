using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using SaleGoods;
//using SaleGoods;

namespace 客房管理系统_数据库_
{
    public partial class RoomLabel : UserControl
    {
        public string remark = "";
        string con, sql;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        public RoomLabel()
        {
            InitializeComponent();
           
        }

        private void btn_Click(object sender, EventArgs e)
        {
            con = "Data Source=USER-UNUIK73QH2\\SQLEXPRESSNEW;Initial Catalog=MyDB;Integrated Security=True";
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            cmd.CommandText = "select * from Room where RoomNum='" + label1.Text + "'";
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                remark = sdr["Remark"].ToString();
            }
            Authority.Remark = remark;
            sdr.Close();
            mycon.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.label2.Text != "")
            {
                Authority.RoomNum = label1.Text;
                CheckOut co = new CheckOut();
                co.textBox1.Text = label1.Text;
                co.Show(); 
            }
            else
            {
                MessageBox.Show("不符合退房条件！");

            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            CheckIn ci = new CheckIn();
            ci.roomNumBox.Text = this.label1.Text;
            cmd.CommandText = "select * from _Order,OrderDetail where RoomNum='" + this.label1.Text
                + "' and OrderDetail.OrderNum=_Order.OrderNum and Finished='未完成'";
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ci.orderNumBox.Text = sdr["OrderNum"].ToString();
                ci.nameBox.Text = sdr["CustomerName"].ToString();
                ci.phoneNumBox.Text = sdr["PhoneNumber"].ToString();
            }
            sdr.Close();
            ci.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ChangeRoom cr = new ChangeRoom();
            cr.textBox1.Text = this.label1.Text;
            cr.Show();
        }

        private void 商品购买ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SellGoods().Show();
        }

        private void RoomLabel_MouseDown(object sender, MouseEventArgs e)
        {
            //按鼠标右键，弹出菜单   
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                contextMenuStrip1.Show(PointToScreen(e.Location));
            }
        }
    }
}
