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
    public partial class SelectRoom : Form
    {
        string con;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        string roomType, condition, price, employee;
        string selectedRoomNum;

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            skinPictureBox1.Load(Application.StartupPath + "\\封面.jpg");
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from Room";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    listBox1.Items.Add(sdr["RoomNum"].ToString());
                }
                sdr.Close();
            }
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!=roomType||textBox2.Text!=condition||textBox3.Text!=price||textBox4.Text != employee)
            {
                con = Authority.con;
                mycon = new SqlConnection(con);
                mycon.Open();
                using (SqlCommand cmd = mycon.CreateCommand())
                {
                    cmd.CommandText = "select * from Employee where EmployeeName='" + textBox4.Text + "'";
                    sdr = cmd.ExecuteReader();
                    sdr.Read();
                    string emp = sdr["EmployeeNum"].ToString();
                    
                    sdr.Close();
                    cmd.CommandText = "update Room set EmployeeNum='" + emp + "' where RoomNum='" + selectedRoomNum + "' " +
                        "update Room set RoomType='" + textBox1.Text + "' where RoomNum='" + selectedRoomNum + "' " +
                        "update Room set Condition='" + textBox2.Text + "' where RoomNum='" + selectedRoomNum + "' " +
                        "update Room set Price=" + Convert.ToInt32(textBox3.Text) + " where RoomNum='" + selectedRoomNum + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("修改成功！");
                }
                mycon.Close();
            }
        }

        private void selected_Changed(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            string roomNum;
            roomNum = listBox1.SelectedItem.ToString();
            selectedRoomNum = roomNum;
            using (SqlCommand cmd = mycon.CreateCommand())
            {
                cmd.CommandText = "select * from Room,Employee where Room.EmployeeNum = Employee.EmployeeNum and RoomNum = '" + roomNum + "'";
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    textBox1.Text = sdr["RoomType"].ToString();
                    roomType = textBox1.Text;
                    textBox2.Text = sdr["Condition"].ToString();
                    condition = textBox2.Text;
                    textBox3.Text = sdr["Price"].ToString();
                    price = textBox3.ToString();
                    textBox4.Text = sdr["EmployeeName"].ToString();
                    employee = textBox4.Text;
                    MemoryStream ms = new MemoryStream((byte[])sdr["ImageSource"]);
                    Image image = Image.FromStream(ms, true);
                    Bitmap bm = new Bitmap(image, skinPictureBox1.Width, skinPictureBox1.Height);
                    skinPictureBox1.Image = bm;
                    skinPictureBox1.Image = image;
                }
            }
            mycon.Close();
        }

        public SelectRoom()
        {
           
            InitializeComponent();
            if (Authority.Level == 2)
            {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
            }
        }

    }
}
