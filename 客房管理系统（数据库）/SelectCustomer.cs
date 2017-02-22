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

    public partial class SelectCustomer : Form
    {
        string con, sql;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        SqlDataAdapter da = null;
        string selectedItem;
        DataSet ds = new DataSet();

        public SelectCustomer()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds.Clear();
            con = Authority.con;
            mycon = new SqlConnection(con);
            if (listBox1.SelectedIndex > 0)
            {
                mycon.Open();

                sql = "select Customer.CustomerName,OrderNum from Customer,_Order where Customer.CustomerNum='" + listBox1.SelectedItem.ToString() + "' and Customer.CustomerNum=_Order.CustomerNum";

                SqlCommand cmd = mycon.CreateCommand();
                cmd.CommandText = sql;
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    textBox1.Text = listBox1.SelectedItem.ToString();
                    textBox2.Text = sdr.GetString(0);
                    textBox3.Text = sdr.GetString(1);
                    
                }
                sdr.Close();


                cmd.CommandText = "select CustomerNum,CustomerID,CustomerGender,PhoneNum from Customer where CustomerNum='" + textBox1.Text + "'";
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Customer");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Customer";
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].HeaderText = "身份证号";
                dataGridView1.Columns[1].Width = 120;
                dataGridView1.Columns[2].HeaderText = "性别";
                dataGridView1.Columns[2].Width = 40;
                dataGridView1.Columns[3].HeaderText = "手机号码";
                dataGridView1.Columns[3].Width = 85;

                mycon.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();

            if (textBox1.Text == "")
            {
                if (textBox2.Text == "")
                {
                    if (textBox3.Text == "")
                    {
                        
                    }
                    else
                    {
                        sql = "select * from Customer,_Order where _Order.CustomerNum=Customer.CustomerNum and OrderNum='"+textBox3.Text+"'";
                    }
                }
                else
                {
                    if (textBox3.Text == "")
                    {
                        sql = "select * from Customer where CustomerName='" + textBox2.Text + "'";
                    }
                    else
                    {
                        sql = "select * from Customer,_Order where Customer.CustomerName='"+textBox2.Text+"' and OrderNum='"+textBox3.Text+"' and Customer.CustomerNum=_Order.CustomerNum";
                    }
                }
            }
            else
            {
                if (textBox2.Text == "")
                {
                    if (textBox3.Text == "")
                    {
                        sql = "select * from Customer where CustomerNum='" + textBox1.Text + "'";
                    }
                    else
                    {
                        sql = "select * from Customer,_Order where _Order.CustomerNum=Customer.CustomerNum and Customer.CustomerNum='"+textBox1.Text+"' and OrderNum='" + textBox3.Text + "'";
                    }
                }
                else
                {
                    if (textBox3.Text == "")
                    {
                        sql = "select * from Customer where CustomerNum='" + textBox1.Text + "' and CustomerName='" + textBox2.Text + "'";
                    }
                    else
                    {
                        sql = "select * from Customer,_Order where Customer.CustomerNum=_Order.CustomerNum and Customer.CustomerNum='" + textBox1.Text + "' and Customer.CustomerName='" + textBox2.Text + "' and OrderNum='" + textBox3.Text + "'";
                    }
                }
            }


            listBox1.Items.Clear();
            listBox1.Items.Add("顾客号");
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = sql;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listBox1.Items.Add(sdr["CustomerNum"].ToString());
                }
                sdr.Close();
            }
            mycon.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("顾客号");
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();

            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from Customer";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listBox1.Items.Add(sdr["CustomerNum"].ToString());
                }
                sdr.Close();
            }
            mycon.Close();
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
                string deleteSql = "delete from Customer where CustomerNum='" + listBox1.SelectedItem.ToString() + "'";
                SqlCommand cmd = new SqlCommand(deleteSql, mycon);
                cmd.ExecuteNonQuery();
                SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                da.Update(ds.Tables[0]);
                dataGridView1.DataSource = null;

                mycon.Close();
                MessageBox.Show("删除成功!");

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
           
            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            da.Update(ds.Tables[0]);
            MessageBox.Show("删除成功!");
            mycon.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            da.Update(ds.Tables[0]);
            MessageBox.Show("信息修改成功！");
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
    }
}
