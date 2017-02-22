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
    public partial class ChangePwd : Form
    {

        string con, sql;
        SqlConnection mycon = null;
        SqlDataAdapter myda = null;
        SqlCommand changePwd = null;

        private void button1_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            bool originalPwdWrong = false;
            bool newPwdNotEqual = false;
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                //查询命令为:查询UserName等于输入的用户名   
                cmd.CommandText = "select * from Employee where EmployeeNum='" + textBox1.Text + "'";

                //将查询到的数据保存在reader这个变量里   
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //如果reader.Read()的结果不为空, 则说明输入的用户名存在   
                    if (reader.Read())
                    {
                        /*从数据库里查询出和用户相对应的PassWord的值 
                         *reader.GetOrdinal("PassWord")的作用是得到PassWord的为这行数据中的第几列,返回回值是int 
                         *reader.GetString()的作用是得到第几列的值,返回类型为String. 
                         */
                        string dbpassword = reader.GetString(reader.GetOrdinal("Pwd"));

                        //比较用户输入的密码与从数据库中查询到的密码是否一至  
                        if(textBox2.Text!=dbpassword)
                        {
                            originalPwdWrong = true;
                            MessageBox.Show("密码输入错误!");
                        }
                        else if (textBox3.Text != textBox4.Text)
                        {
                            newPwdNotEqual = true;
                            MessageBox.Show("两次输入密码不相同!");
                        }

                        if (originalPwdWrong == false && newPwdNotEqual == false)
                        {
                            sql = "update Employee set Pwd='"+textBox3.Text+"'"+" where EmployeeNum='"+textBox1.Text+"'";
                            changePwd = mycon.CreateCommand();
                            changePwd.CommandText = sql;
                            reader.Close();
                            changePwd.ExecuteNonQuery();
                            MessageBox.Show("密码修改成功！");
                            this.Close();
                        }                       
                    }
                    else
                    {
                        //说明输入的用户名不存在   
                        MessageBox.Show("用户名不存在!");
                    }
                }
            }
        }

        public ChangePwd()
        {
            
            InitializeComponent();
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            label4.BackColor = Color.Transparent;
        }
    }
}
