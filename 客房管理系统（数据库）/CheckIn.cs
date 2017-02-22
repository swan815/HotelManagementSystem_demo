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
    public partial class CheckIn : Form
    {
        string con;
        int price = 0;
        int deposit = 0;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        string CustomerNum = null;

        public CheckIn()
        {
            InitializeComponent();
            timer1.Start();
        }

        public string createCustomerNum()
        {
            CustomerNum = null;
            Random ran = new Random();
            int randKey = ran.Next(10000, 99999);
            CustomerNum += randKey.ToString();
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from Customer";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (CustomerNum == sdr["CustomerNum"].ToString())
                    {
                        return createCustomerNum();
                    }
                }
            }
            sdr.Close();
            mycon.Close();
            return CustomerNum;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = System.DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            cmd.CommandText = "select Price*(DATEDIFF(day,inTime,OutTime)) as price from OrderDetail,Room where OrderDetail.RoomNum=Room.RoomNum and OrderNum='" + orderNumBox.Text + "'";
            sdr = cmd.ExecuteReader();
            sdr.Read();
            if (Convert.ToInt32(sdr["price"].ToString()) < 300)
            {
                price = 2 * Convert.ToInt32(sdr["price"].ToString());
                deposit = price;
            }
            else
            {
                price = 300 + Convert.ToInt32(sdr["price"].ToString());
                deposit = price;
            }
            MessageBox.Show("需付金额:" + "\r\n" + price.ToString() + "\r\n");
            sdr.Close();
            mycon.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string customerNum = null;
            customerNum = createCustomerNum();
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();

            try
            {

                cmd.CommandText = "select * from OrderDetail where OrderNum='" + orderNumBox.Text + "' and RoomNum='" + roomNumBox.Text + "'";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    if (sdr["Finished"].ToString() != "未完成")
                    {
                        MessageBox.Show("不符合入住条件！");
                        return;
                    }
                    else
                    {
                        sdr.Close();
                        cmd.CommandText = "insert into CheckIn values('" + orderNumBox.Text + "','" + roomNumBox.Text + "','" + System.DateTime.Now + "','" + deposit.ToString() + "') " +
                        "insert into Customer values('" + customerNum + "','" + IdBox.Text + "','" + nameBox.Text + "','" + genderBox.Text + "','" + phoneNumBox.Text + "') " +
                        "update Room set Condition='入住',CurrentCustomerNum='" + customerNum + "' where RoomNum='" + roomNumBox.Text + "' " + "update _Order set CustomerNum='" + customerNum + "' where OrderNum='" + orderNumBox.Text + "'" +
                        "update OrderDetail set Finished='处理中' where OrderNum='" + orderNumBox.Text + "' and RoomNum='" + roomNumBox.Text + "'";
                        cmd.ExecuteNonQuery();
                        mycon.Close();
                        MessageBox.Show("入住成功!" + "\r\n" + "顾客号:" + customerNum);
                    }
                }

            }
            catch (Exception e1)
            {

                MessageBox.Show(e1.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            cmd.CommandText = "select * from OrderDetail,_Order where _Order.OrderNum=OrderDetail.OrderNum and OrderDetail.OrderNum='" + orderNumBox.Text + "' and RoomNum='" + roomNumBox.Text + "'";
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                nameBox.Text = sdr["CustomerName"].ToString();
                phoneNumBox.Text = sdr["PhoneNumber"].ToString();
            }
            sdr.Close();
            mycon.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string customerNum = null;
            customerNum = createCustomerNum();
            string orderNum = createOrderNum();
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            cmd.CommandText = "select Price from Room where RoomNum='" + roomNumBox.Text + "'";
            sdr = cmd.ExecuteReader();
            sdr.Read();
            price = Convert.ToInt32(sdr["Price"]) * (dateTimePicker1.Value.Day - DateTime.Now.Day);
            if (price < 300)
            {
                price = 2 * Convert.ToInt32(sdr["price"].ToString());
                deposit = price;
            }
            else
            {
                price = 300 + Convert.ToInt32(sdr["price"].ToString());
                deposit = price;
            }
            sdr.Close();
            string EasyCheckIn = "insert into CheckIn values('" + orderNum + "','" + roomNumBox.Text + "','" + System.DateTime.Now + "','" + deposit.ToString() + "')"
                + "update Room set Condition='入住', CurrentCustomerNum='" + customerNum + "' where RoomNum='" + roomNumBox.Text + "'";
            SqlCommand comm1 = new SqlCommand(EasyCheckIn, mycon);
            string EasyAddCustomer = "insert into Customer values('" + customerNum + "','" + IdBox.Text + "','" + nameBox.Text + "','" + genderBox.Text + "','" + phoneNumBox.Text + "') ";
            SqlCommand comm2 = new SqlCommand(EasyAddCustomer, mycon);
            string EasyAddOrder = "insert into _Order values('" + orderNum + "','" + nameBox.Text + "','" + phoneNumBox.Text + "','" + customerNum + "')" +
                "insert into OrderDetail values('" + orderNum + "','" + roomNumBox.Text + "','" + System.DateTime.Now + "','" + dateTimePicker1.Value + "','处理中')";
            SqlCommand comm3 = new SqlCommand(EasyAddOrder, mycon);
            int x = comm3.ExecuteNonQuery();
            int y = comm2.ExecuteNonQuery();
            int z = comm1.ExecuteNonQuery();
            if (x > 0 && y > 0 && z > 0)
            {
                MessageBox.Show("入住成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("入住失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            mycon.Close();
        }
        public string createOrderNum()
        {
            string OrderNum = null;
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
            using (SqlCommand cmd = mycon.CreateCommand())
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
            sdr.Close();
            return OrderNum;
        }
    }
}
