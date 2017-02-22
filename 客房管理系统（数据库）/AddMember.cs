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
    public partial class AddMember : Form
    {

        string con;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;

        public AddMember()
        {
            InitializeComponent();
            textBox1.Text = "会员特权：\r\n" + "1、享有8.8的住房折扣\r\n" + "2、每次消费均累计积分，积分可兑换礼品或折扣券\r\n" + "3、更多特权，敬请期待";
        }

        public string createMemberNum()
        {
            Random ran = new Random();
            int randKey = ran.Next(10000, 99999);
            string memberNum = randKey.ToString();
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from Member";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    if (memberNum == sdr["MemberNum"].ToString())
                    {
                        return createMemberNum();
                    }
                }
            }
            return memberNum;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == String.Empty)
            {
                MessageBox.Show("请输入身份证号！");
            }
            else
            {
                con = Authority.con;
                mycon = new SqlConnection(con);
                mycon.Open();
                using (SqlCommand cmd = mycon.CreateCommand())
                {
                    cmd.CommandText = "select * from Customer where CustomerID='" + textBox2.Text + "'";
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        textBox3.Text = sdr["CustomerName"].ToString();
                        textBox4.Text = sdr["CustomerGender"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("该顾客暂未在本店消费！");
                    }
                    sdr.Close();
                }
                mycon.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            try
            {
                using (SqlCommand cmd = mycon.CreateCommand())
                {
                    string memberNum = createMemberNum();
                    cmd.CommandText = "insert into Member values('" + memberNum + "','" + textBox2.Text + "',0)";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("会员添加成功！\r\n会员号：" + memberNum);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
            mycon.Close();
        }
    }
}
