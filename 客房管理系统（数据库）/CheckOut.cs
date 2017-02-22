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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using System.Diagnostics;

namespace 客房管理系统_数据库_
{
    public partial class CheckOut : Form
    {
        string con, sql, customerID;
        int dep, bill, realPrice;
        bool isMember = false;
        string orderNum, customerName, roomNum, inTime, outTime, roomPrice, deposit, employeeName;
        SqlConnection mycon = null;
        SqlDataReader sdr = null;
   
        public CheckOut()
        {
            InitializeComponent();            
            label3.Text = System.DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double discount = 1;
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            SqlCommand selectOrderNumCommand = mycon.CreateCommand();
            SqlCommand selectEmployeeName = mycon.CreateCommand();
            if (textBox1.Text != "")
            {
                cmd.CommandText = "select * from Room where RoomNum='" + textBox1.Text + "'";
                sdr = cmd.ExecuteReader();
                sdr.Read();
                string condition = sdr["Condition"].ToString();
                sdr.Close();

                cmd.CommandText = "select * from Member,Room,Customer where RoomNum='" + textBox1.Text + "' and Customer.CustomerNum=Room.CurrentCustomerNum and Member.CustomerID=Customer.CustomerID";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    discount = 0.8;
                    isMember = true;                  
                }
                sdr.Close();

                if (condition == "入住")
                {
                    selectOrderNumCommand.CommandText = "select * from OrderDetail,_Order,CheckIn,Room where OrderDetail.OrderNum=_Order.OrderNum and OrderDetail.RoomNum=Room.RoomNum and OrderDetail.OrderNum=CheckIn.OrderNum and OrderDetail.RoomNum='" + textBox1.Text + "'";
                    
                    sdr = selectOrderNumCommand.ExecuteReader();
                    sdr.Read();
                    orderNum = sdr["OrderNum"].ToString();
                    roomNum = textBox1.Text;
                    outTime = System.DateTime.Now.ToString();
                    deposit = sdr["Deposit"].ToString();
                    MessageBox.Show("已收押金："+deposit);
                    inTime = sdr["CITime"].ToString();
                    roomPrice = sdr["Price"].ToString();
                    customerName = sdr["CustomerName"].ToString();
                    sdr.Close();
                    selectEmployeeName.CommandText = "select * from Employee where EmployeeNum='" + Authority.EmployeeNum + "'";
                    sdr = selectEmployeeName.ExecuteReader();
                    sdr.Read();
                    employeeName = sdr["EmployeeName"].ToString();
                    sdr.Close();
                    cmd.CommandText = "update Room set Condition='可用' where RoomNum='" + textBox1.Text + "' " + "insert into CheckOut values('" + orderNum + "','" + textBox1.Text + "','" + System.DateTime.Now + "',0)" +
                        "update OrderDetail set Finished='已完成' where OrderNum='" + orderNum + "' and RoomNum='" + textBox1.Text + "'" +
                        "update Room set CurrentCustomerNum=null where RoomNum='" + textBox1.Text + "'";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("退房成功！"+orderNum);
                }
                else
                {
                    MessageBox.Show(textBox1.Text + "房间不符合退房标准");
                }
            }
            else
            {
                MessageBox.Show("请输入房间号");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string yesOrno = "否";
            if (isMember == true)
            {
                yesOrno = "是";
            }
            iTextSharp.text.Document doc = new Document(PageSize.A4.Rotate());
            string inputPath = @"C:\Users\Administrator\Desktop\" + orderNum + "-" + textBox1.Text + ".pdf";
            try
            {
                PdfWriter.GetInstance(doc, new FileStream(inputPath, FileMode.Create));
                doc.Open();
                BaseFont baseFT = BaseFont.CreateFont(@"c:\windows\fonts\SIMHEI.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                iTextSharp.text.Font font = new iTextSharp.text.Font(baseFT);
                font.Size = 20;
                doc.Add(new Paragraph("顾客姓名："+customerName+"                            房间号："+roomNum, font));
                doc.Add(new Paragraph("抵店日期：" + inTime + "               离店日期：" + outTime, font));
                doc.Add(new Paragraph("房间单价：" + roomPrice+"                              是否会员："+yesOrno, font));
                doc.Add(new Paragraph("-------------------------------------------------------------------------", font));
                doc.Add(new Paragraph("     项目                                                  金额", font));
                doc.Add(new Paragraph("-------------------------------------------------------------------------", font));
                doc.Add(new Paragraph("     押金                                                  " + deposit, font));
                doc.Add(new Paragraph("     房租                                                  "+realPrice.ToString(), font));
                doc.Add(new Paragraph("     结账退款                                              "+bill.ToString(), font));
                doc.Add(new Paragraph("-------------------------------------------------------------------------", font));
                doc.Add(new Paragraph("                                                           合计:"+realPrice.ToString(), font));
                doc.Add(new Paragraph("", font));
                doc.Add(new Paragraph("打印人:"+employeeName+"                                    打印时间:"+System.DateTime.Now.ToString(), font));   
                doc.Close();
                MessageBox.Show("账单生成成功！");


            }
            catch (DocumentException de) { Console.WriteLine(de.Message); }
            catch (IOException io) { Console.WriteLine(io.Message); }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            con = Authority.con;
            mycon = new SqlConnection(con);
            mycon.Open();
            SqlCommand cmd = mycon.CreateCommand();
            SqlCommand getDeposit = mycon.CreateCommand();
            cmd.CommandText = "select Price*(DATEDIFF(day,inTime,getdate())) as price from OrderDetail,Room where OrderDetail.RoomNum=Room.RoomNum and OrderNum='" + orderNum + "'";
            sdr = cmd.ExecuteReader();
            sdr.Read();
            realPrice = Convert.ToInt32(sdr["price"].ToString());
            if (isMember == true)
            {
                realPrice = (int)(realPrice * 0.8);
            }
            MessageBox.Show("消费金额:" + realPrice.ToString());
            sdr.Close();
            getDeposit.CommandText = "select Deposit as dep from CheckIn where OrderNum='" + orderNum + "' and RoomNum='" + textBox1.Text + "'";
            sdr = getDeposit.ExecuteReader();
            sdr.Read();
            int depo = Convert.ToInt32(sdr["dep"].ToString());
            dep = Convert.ToInt32(depo);
            sdr.Close();
            bill = dep - realPrice;
            MessageBox.Show("返还金额：" + bill.ToString());
            mycon.Close();
        }
    }
}
