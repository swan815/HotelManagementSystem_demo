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
    public partial class SelectEmployee : Form
    {
        string con, sql;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        SqlDataAdapter da = null;
        DataSet ds = new DataSet();

        public SelectEmployee()
        {
            InitializeComponent();
            dataGridView1.RowTemplate.Height = 40;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds.Clear();
            con = Authority.con;
            mycon = new SqlConnection(con);
            if (listBox1.SelectedIndex > 0)
            {
                mycon.Open();

                sql = "select * from Employee where EmployeeNum='" + listBox1.SelectedItem.ToString() + "'";

                SqlCommand cmd = mycon.CreateCommand();
                cmd.CommandText = sql;
                sdr = cmd.ExecuteReader();
                sdr.Read();
                textBox1.Text = sdr["EmployeeName"].ToString();
                textBox2.Text = listBox1.SelectedItem.ToString();

                sdr.Close();
                cmd.CommandText = "select EmployeeName,EmployeeNum,EmployeeID,PhoneNum from Employee where EmployeeNum='" + listBox1.SelectedItem.ToString() + "'";
                da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Employee");
                dataGridView1.DataSource = ds;
                dataGridView1.DataMember = "Employee";
                dataGridView1.Columns[0].HeaderText = "姓名";
                dataGridView1.Columns[0].Width = 55;
                dataGridView1.Columns[1].HeaderText = "员工号";
                dataGridView1.Columns[1].Width = 60;
                dataGridView1.Columns[2].HeaderText = "身份证号";
                dataGridView1.Columns[2].Width = 120;
                dataGridView1.Columns[3].HeaderText = "手机号码";
                dataGridView1.Columns[3].Width = 85;

                mycon.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("员工号");
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();

            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from Employee";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listBox1.Items.Add(sdr["EmployeeNum"].ToString());
                }
                sdr.Close();
            }
            mycon.Close();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            StringFormat strFmt = new System.Drawing.StringFormat();
            strFmt.Alignment = StringAlignment.Center; //文本垂直居中
            strFmt.LineAlignment = StringAlignment.Center; //文本水平居中
            
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.Black)), e.Bounds);
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds, strFmt);
            e.DrawFocusRectangle();//焦点框 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            da.Update(ds.Tables[0]);
            MessageBox.Show("信息修改成功！");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con = "Data Source=C3CF\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True";
            mycon = new SqlConnection(con);
            mycon.Open();
            if (MessageBox.Show("确认删除?", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                SqlCommandBuilder cmb = new SqlCommandBuilder(da);
                da.Update(ds.Tables[0]);
                MessageBox.Show("删除成功!");
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

                }
                else
                {
                    sql = "select * from Employee where EmployeeNum='" + textBox2.Text + "'";
                }
            }
            else
            {
                if (textBox2.Text == "")
                {
                    sql = "select * from Employee where EmployeeName='" + textBox1.Text + "'";
                }
                else
                {
                    sql = "select * from Employee where EmployeeNum='" + textBox2.Text + "' and EmployeeName='" + textBox1.Text + "'";
                }
            }

            listBox1.Items.Clear();
            listBox1.Items.Add("员工号");
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = sql;
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listBox1.Items.Add(sdr["EmployeeNum"].ToString());
                }
                sdr.Close();
            }
            mycon.Close();

        }
    }
}
