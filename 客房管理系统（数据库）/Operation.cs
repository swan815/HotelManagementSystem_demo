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
    public partial class Operation : Form
    {
        string con, sql;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        string currentRoomNum = null;
        string employeeNum;
        RoomLabel[] rl = new RoomLabel[100];
        Button changeRemark = new Button();
        TextBox remarkTextBox = new TextBox();
        public Operation(string EmployeeNum)
        {
            InitializeComponent();
            this.employeeNum = EmployeeNum;
            this.Load += Operation_Load;
            label8.Text = "欢     迎     使     用     客     房     管     理     系     统";
            
            panel2.Controls.Add(remarkTextBox);
            remarkTextBox.Multiline = true;
            remarkTextBox.Size = new Size(130, 210);
            remarkTextBox.Location = new Point(0, 350);
            
            panel2.Controls.Add(changeRemark);
            changeRemark.Location = new Point(25, 580);
            changeRemark.Text = "修改备注";
            changeRemark.Size = new Size(90, 40);
            changeRemark.Font = new Font("幼圆", changeRemark.Font.Size, changeRemark.Font.Style|FontStyle.Bold);
            changeRemark.Click += ChangeRemark_Click;
        }

        private void ChangeRemark_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            if (currentRoomNum!=null)
            {
                SqlCommand cmd = mycon.CreateCommand();
                cmd.CommandText = "update Room set Remark='" + remarkTextBox.Text + "' where RoomNum='" + currentRoomNum + "'";
                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("请先选择房间！");
            }            
        }

        private void showRemark_Click(object sender, EventArgs e)
        {
            remarkTextBox.Clear();
            RoomLabel roomLabel = sender as RoomLabel;
            currentRoomNum = roomLabel.label1.Text;
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            cmd.CommandText = "select * from Room where RoomNum='" + roomLabel.label1.Text + "'";
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                remarkTextBox.Text = sdr["Remark"].ToString();
            }
            sdr.Close();
            mycon.Close();
        }

        private void Operation_Load(object sender, EventArgs e)
        {
            //this.employeeNum = EmployeeNum;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //string strWelcome = System.Windows.Forms.Application.StartupPath + "\\草原.jpg";
            //if (File.Exists(strWelcome))
            //{
            //    Bitmap bm = new Bitmap(Image.FromFile(strWelcome), pictureBox1.Width, pictureBox1.Height);
            //    pictureBox1.Image = bm;
            //}
            SqlCommand cmd = mycon.CreateCommand();
            cmd.CommandText = "select * from Employee where EmployeeNum='" + employeeNum + "'";
            label5.Text = Authority.LoginTime;
            sdr = cmd.ExecuteReader();
            label5.Text = System.DateTime.Now.ToString();
            if (sdr.Read())
            {
                label7.Text = sdr["EmployeeName"].ToString();
            }
            sdr.Close();

            roomLabel1.label1.Text = "";
            roomLabel1.label2.Text = "";
            roomLabel2.label1.Text = "";
            roomLabel2.label2.Text = "";
            roomLabel3.label1.Text = "";
            roomLabel3.label2.Text = "";

            int x = 3, y = 3, roomNum = 100;
            for (int i = 0; i < 100; i++)
            {
               
                if (roomNum != 150)
                {
                    roomNum++;
                }
                else
                {
                    roomNum = 201;
                }

                if (i % 12 == 0 && i != 0)
                {
                    x = 3;
                    y = y + 60;
                }

                rl[i] = new RoomLabel();
                rl[i].label2.Text = "";
                rl[i].label1.ForeColor = Color.White;
                rl[i].label2.ForeColor = Color.White;

                cmd.CommandText = "select * from Room,Customer where RoomNum=" + roomNum.ToString() + " and CurrentCustomerNum=CustomerNum";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    rl[i].label2.Text = sdr["CustomerName"].ToString();
                    //MessageBox.Show(sdr["CustomerName"].ToString());
                    rl[i].BackColor = Color.Green;
                }
                sdr.Close();

                cmd.CommandText = "select * from OrderDetail where Finished='未完成' and RoomNum='" + roomNum.ToString() + "'";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    rl[i].BackColor = Color.Teal;
                }
                sdr.Close();


                rl[i].Location = new System.Drawing.Point(x, y);
                x += 80;
                rl[i].Width = 80;
                rl[i].Height = 60;
                rl[i].label1.Text = roomNum.ToString();

                this.panel1.Controls.Add(rl[i]);
            }
            mycon.Close();
            for (int i = 0; i < 100; i++)
            {
                rl[i].Click += showRemark_Click;
            }
        }

        private void 查询房间信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SelectRoom().Show();
        }

        private void 添加订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddOrder().Show();
        }

        private void 查询订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SelectOrder().Show();
        }

        private void 入住处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CheckIn().Show();
        }

        private void 退房处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CheckOut().Show();
        }

        private void 换房处理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangeRoom().Show();
        }

        private void 查询顾客信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SelectCustomer().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            //this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //string strWelcome = System.Windows.Forms.Application.StartupPath + "\\草原.jpg";
            //if (File.Exists(strWelcome))
            //{
            //    Bitmap bm = new Bitmap(Image.FromFile(strWelcome), pictureBox1.Width, pictureBox1.Height);
            //    pictureBox1.Image = bm;
            //}
            SqlCommand cmd = mycon.CreateCommand();
            int roomNum = 100;
            for (int i = 0; i < 100; i++){
                if (roomNum != 150)
                {
                    roomNum++;
                }
                else
                {
                    roomNum = 201;
                }

                cmd.CommandText = "select * from Room,Customer where RoomNum=" + roomNum.ToString() + " and CurrentCustomerNum=CustomerNum";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    rl[i].label2.Text = sdr["CustomerName"].ToString();
                    rl[i].BackColor = Color.Green;
                }
                else
                {
                    rl[i].BackColor = Color.FromArgb(192, 192, 0);
                    rl[i].label2.Text = "";
                }
                sdr.Close();

                cmd.CommandText = "select * from OrderDetail where Finished='未完成' and RoomNum='" + roomNum.ToString() + "'";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    rl[i].BackColor = Color.Teal;
                }
                sdr.Close();
            }
        }

        private void 会员办理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddMember().Show();
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 员工查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Authority.Level == 2)
            {
                new SelectEmployee().Show();
            }
            else
            {
                MessageBox.Show("权限受限");
            }
        }

    }
}
