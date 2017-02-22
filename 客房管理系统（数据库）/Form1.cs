using SaleGoods;
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
using 客房管理系统_数据库_;

namespace kfglxt
{
    public partial class Form1 : Form
    {
        string con, sql;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
        string currentRoomNum = null;
        string employeeNum;
        RoomLabel[] rl = new RoomLabel[100];
        TextBox remarkTextBox = new TextBox();
        Sunisoft.IrisSkin.SkinEngine se = null;
        public Form1(string num)
        {
            InitializeComponent();
            this.employeeNum = num;
            this.Load += Operation_Load;
            se = new Sunisoft.IrisSkin.SkinEngine();
            se.SkinAllForm = true;//所有窗体均应用此皮肤
            se.SkinFile = "skin/DiamondBlue.ssk";
            skinPanel2.Controls.Add(remarkTextBox);
            remarkTextBox.Multiline = true;
            remarkTextBox.Size = new Size(150, 210);
            remarkTextBox.Location = new Point(35, 360);

        }


        private void Operation_Load(object sender, EventArgs e)
        {
            
            //this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Size = new Size(1060, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            SqlCommand cmd = mycon.CreateCommand();
            cmd.CommandText = "select * from Employee where EmployeeNum='" + employeeNum + "'";
            skinLabel5.Text = Authority.LoginTime;
            sdr = cmd.ExecuteReader();
            skinLabel5.Text = System.DateTime.Now.ToString();
            if (sdr.Read())
            {
                skinLabel7.Text = sdr["EmployeeName"].ToString();
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

                if (i % 10 == 0 && i != 0)
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

                this.skinPanel1.Controls.Add(rl[i]);
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

        private void 入住ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CheckIn().Show();
        }

        private void 添加订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddOrder().Show();
        }

        private void 查询订单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SelectOrder().Show();
        }

        private void 退房ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CheckOut().Show();
        }

        private void 换房ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangeRoom().Show();
        }

        private void 查询顾客信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SelectCustomer().Show();
        }

        private void 会员办理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddMember().Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            new SelectEmployee().Show();
        }

        private void update_Click(object sender, EventArgs e)
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

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 顾客订餐ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SellGoods().Show();
        }

        private void ChangeRemark_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            if (currentRoomNum != null)
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

        private void ToolStripMenuItemManageGood_Click(object sender, EventArgs e)
        {
            new FrmDish().Show();
        }

        private void 业绩查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmStatistics().Show();
        }
        private void 锁定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmLock().Show();
        }

        private void 锁定ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new FrmLock().Show();
        }
     
    }
}
