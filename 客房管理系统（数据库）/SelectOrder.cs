using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 客房管理系统_数据库_
{
    public partial class SelectOrder : Form
    {

        string con, sql;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        SqlDataAdapter da = null;
        string selectedItem;
        DataSet ds = new DataSet();

        public SelectOrder()
        {
            InitializeComponent();
            listBox1.Items.Add("   订单号");

        }

        private void selected_Changed(object sender, EventArgs e)
        {
            ds.Clear();
            con = Authority.con;
            mycon = new SqlConnection(con);
            SqlConnection mycon1 = new SqlConnection(con);
            if (listBox1.SelectedIndex > 0)
            {
                mycon1.Open();
                mycon.Open();

                sql = "select * from OrderDetail where OrderNum='" + listBox1.SelectedItem.ToString() + "'";
                string returnCustomerName = "select CustomerName from _Order where OrderNum='" + listBox1.SelectedItem.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(returnCustomerName, mycon);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                sdr1.Read();
                textBox2.Text = sdr1.GetString(0);
                mycon1.Close();
                sdr1.Close();

                SqlCommand cmd = new SqlCommand(sql, mycon);
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "OrderDetail");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "OrderDetail";
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].HeaderText = "";
                dataGridView1.Columns[0].Width = 60;
                dataGridView1.Columns[1].HeaderText = "房间号";
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].HeaderText = "入住时间";
                dataGridView1.Columns[2].Width = 75;
                dataGridView1.Columns[3].HeaderText = "离开时间";
                dataGridView1.Columns[3].Width = 75;
                dataGridView1.Columns[4].Width = 50;
                dataGridView1.Columns[4].HeaderText = "状态";

                mycon.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();

            if (textBox1.Text == "")
            {
                if (textBox2.Text == "")
                {
                   
                }
                else
                {
                    sql = "select * from _Order where CustomerName='" + textBox2.Text + "'";
                }
            }
            else
            {
                if (textBox2.Text == "")
                {
                    sql = "select * from _Order where OrderNum='" + textBox1.Text + "'";
                }
                else
                {
                    sql = "select * from _Order where CustomerName='" + textBox2.Text + "' and OrderNum='" + textBox1.Text + "'";
                }
            }


            listBox1.Items.Clear();
            listBox1.Items.Add("   订单号");
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = sql;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listBox1.Items.Add(sdr["OrderNum"].ToString());
                }
                sdr.Close();
            }
            mycon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = "Data Source=C3CF\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True";
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            da.Update(ds.Tables[0]);
            MessageBox.Show("订单修改成功！");
        }

        private void button4_Click(object sender, EventArgs e)
        {

            
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();

            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "update Room set Condition='可用' where RoomNum='" + dataGridView1.CurrentRow.Cells["RoomNum"].Value.ToString() + "'";
                cmd.ExecuteNonQuery();
            }
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            da.Update(ds.Tables[0]);
            MessageBox.Show("删除成功!");
            mycon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("   订单号");
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();

            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from _Order";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listBox1.Items.Add(sdr["OrderNum"].ToString());
                }
                sdr.Close();
            }
            mycon.Close();
        }

        private void listBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int posindex = listBox1.IndexFromPoint(new Point(e.X, e.Y));
                listBox1.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < listBox1.Items.Count)
                {
                    listBox1.SelectedIndex = posindex;
                    contextMenuStrip1.Show(listBox1, new Point(e.X, e.Y));
                    
                }
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(listBox1.SelectedItem.ToString());
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectedItem = listBox1.SelectedItem.ToString();
            int index = listBox1.SelectedIndex;
            if (MessageBox.Show("确认删除?", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                
                //listBox1.Items.Remove(listBox1.SelectedItem);
                con = Authority.con;
                mycon = new SqlConnection(con);
                mycon.Open();
                string deleteSql = "delete from OrderDetail where OrderNum='" + listBox1.SelectedItem.ToString() + " '"+"  delete from _Order where OrderNum='" + listBox1.SelectedItem.ToString() + "'";
                SqlCommand cmd = new SqlCommand(deleteSql, mycon);
                cmd.ExecuteNonQuery();
                SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                da.Update(ds.Tables[0]);
                dataGridView1.DataSource = null;
                
                mycon.Close();
                MessageBox.Show("删除成功!");
                
            }
            else MessageBox.Show("删除操作已取消");
        }

    }
}
