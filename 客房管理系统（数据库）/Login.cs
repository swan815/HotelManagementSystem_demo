using kfglxt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 客房管理系统_数据库_
{
    public partial class Login : Form
    {

        string con;
        SqlConnection mycon = null;

        private void button1_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                //查询命令为:查询UserName等于输入的用户名   
                cmd.CommandText = "select * from Employee where EmployeeNum='" + textBox1.Text + "'";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {  
                    if (reader.Read())
                    {                       
                        string dbpassword = reader.GetString(reader.GetOrdinal("Pwd"));
                        if (textBox2.Text == dbpassword)
                        { 
                            MessageBox.Show("登录成功!");
                            this.Visible = false;
                            //Operation operationWindow = new Operation(textBox1.Text);
                            Form1 mainWindow = new Form1(textBox1.Text);
                            if (textBox1.Text == "001")
                                Authority.Level = 2;
                            mainWindow.Show();
                            reader.Close();
                        }
                        else
                        {
                            //如果不相等,说明密码不对   
                            MessageBox.Show("密码输入错误!");
                        }

                    }
                    else
                    {
                        //说明输入的用户名不存在   
                        MessageBox.Show("输入的用户名不存在!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ChangePwd().Show();
        }
        Sunisoft.IrisSkin.SkinEngine se = null;
        public Login()
        {
            InitializeComponent();
            se = new Sunisoft.IrisSkin.SkinEngine();
            se.SkinAllForm = true;//所有窗体均应用此皮肤
            se.SkinFile = "skin/DiamondBlue.ssk";
            label1.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;
            label3.BackColor = System.Drawing.Color.Transparent;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            OperationButton op = new OperationButton();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strPhotoPath = @"\\Mac\Home\Desktop\计算机课程\软件工程\客房管理系统(软件工程)\客房图片\套房.jpg";
            //读取图片
            FileStream fs = new System.IO.FileStream(strPhotoPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] photo = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            //存入
            con = "Data Source=C3CF\\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True";
            mycon = new SqlConnection(con);
            mycon.Open();
            string strComm;
            strComm = "update Room set ImageSource=@photoBinary where RoomType='套房'";
            SqlCommand myComm = new SqlCommand(strComm, mycon);
            myComm.Parameters.Add("@photoBinary", SqlDbType.Binary, photo.Length);
            myComm.Parameters["@photoBinary"].Value = photo;
            myComm.ExecuteNonQuery();
            mycon.Close();
        }
    }
}
