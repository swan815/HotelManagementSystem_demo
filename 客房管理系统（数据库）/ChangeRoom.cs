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
    public partial class ChangeRoom : Form
    {
        string con;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;

        public ChangeRoom()
        {
            InitializeComponent();
        }

        private void comboBoxSelectedChanged(object sender, EventArgs e)
        {
            string roomNum = comboBox1.SelectedItem.ToString();
            textBox2.Text = roomNum;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string type;
            con = Authority.con;
            mycon = new SqlConnection(con);
            SqlConnection mycon1 = new SqlConnection(con);
            mycon.Open();
            mycon1.Open();

            SqlCommand cmd = mycon.CreateCommand();

            cmd.CommandText = "select * from Room where RoomNum='" + textBox1.Text + "'";
            sdr = cmd.ExecuteReader();
            sdr.Read();
            type = sdr["RoomType"].ToString();
            sdr.Close();
            cmd.CommandText = "select * from Room where RoomType='" + type + "'";
            sdr = cmd.ExecuteReader();
            sdr.Read();
            while (sdr.Read())
            {
                string roomNum = sdr["RoomNum"].ToString();
                if (roomNum != textBox1.Text)
                {
                    comboBox1.Items.Add(roomNum);
                }
            }
            sdr.Close();
            mycon.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            SqlConnection mycon1 = new SqlConnection(con);
            mycon.Open();

            SqlCommand cmd = mycon.CreateCommand();

            try
            {
                string currentCustomerNum = null;
                cmd.CommandText = "select * from Room where RoomNum='" + textBox1.Text + "'";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    currentCustomerNum = sdr["CurrentCustomerNum"].ToString();
                }
                sdr.Close();
                cmd.CommandText = "update Room set Condition='入住',CurrentCustomerNum='" + currentCustomerNum + "' where RoomNum='" + textBox2.Text + "' update Room set Condition='可用',CurrentCustomerNum=NULL where RoomNum='" + textBox1.Text + "' " +
                    "update _Order set RoomNum='" + textBox2.Text + "' where RoomNum='" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();
                MessageBox.Show("更换成功!");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            mycon.Close();
        }
    }
}
