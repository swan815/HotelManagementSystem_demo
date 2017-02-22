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
    public partial class AddOrder : Form
    {

        string con, sql;
        SqlConnection mycon = null;
        SqlConnection mycon1 = null;
        SqlDataReader sdr = null;
        string OrderNum = null;

        public AddOrder()
        {
            InitializeComponent();
            timer1.Start();
            dataGridView1.Font = new Font("幼圆", 12);
            dataGridView1.RowTemplate.Height = 23;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            roomNumBox.Text = dataGridView1.CurrentCell.Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = System.DateTime.Now.ToString();
        }

        public string createOrderNum()
        {
            OrderNum = null;
            OrderNum += System.DateTime.Now.Year.ToString();
            if (System.DateTime.Now.Month < 10)
            {
                OrderNum += "0";
            }
            OrderNum += System.DateTime.Now.Month.ToString();
            if (System.DateTime.Now.Day < 10)
            {
                OrderNum += "0";
            }
            OrderNum += System.DateTime.Now.Day.ToString();
            Random ran = new Random();
            int randKey = ran.Next(100, 999);
            OrderNum += randKey.ToString();
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            using(SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from _Order";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (OrderNum == sdr["OrderNum"].ToString())
                    {
                        return createOrderNum();
                    }
                }
            }
            return OrderNum;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
                        
            try
            {
                if (nameBox.Text != String.Empty && phoneNumBox.Text != String.Empty)
                {
                    using (SqlCommand cmd = mycon.CreateCommand())
                    {
                        cmd.CommandText = "insert into _Order values('" + createOrderNum() + "','" + nameBox.Text + "','" + phoneNumBox.Text + "','00000')";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("生成订单成功!\r\n" + "订单号:" + OrderNum);
                    }
                }
                else
                {
                    MessageBox.Show("信息输入不完整!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("订单生成错误!");
            }

            mycon.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();

            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "insert into OrderDetail values('" + OrderNum + "','" + roomNumBox.Text.ToString() + "','" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "','" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "','"+"未完成"+"') update Room set Condition='预定' where RoomNum='"+roomNumBox.Text.ToString()+"'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("订单号:" + OrderNum + "\r\n" + "房间号:" + roomNumBox.Text.ToString() +"\r\n"+ "入住时间:" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "\r\n" + "离开时间:" + dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string roomType = comboBox1.SelectedItem.ToString();
            con = Authority.con;
            mycon = new SqlConnection(con);
            SqlConnection mycon1 = new SqlConnection(con);
            mycon.Open();
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                mycon1.Open();
                cmd.CommandText = "select RoomNum from Room where Condition='可用' and RoomType='" + roomType + "'";
                sdr = cmd.ExecuteReader();
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd.CommandText, mycon1);
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

                sdr.Close();
            }
        }
    }
}
