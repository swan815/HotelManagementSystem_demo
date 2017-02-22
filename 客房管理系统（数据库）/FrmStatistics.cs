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
using HotelManager.BLL;

namespace 客房管理系统_数据库_
{
    public partial class FrmStatistics : Form
    {
        public FrmStatistics()
        {
            InitializeComponent();
        }
        //图标Y坐标
        private double[] totalMoney = null;
        private double[] dishPrice = null;

        //总收入
        private double sumTotalMoney = 0;
        private double sumDishMoney = 0;

        private void FrmStatistics_Load(object sender, EventArgs e)
        {
            GetMoney(Convert.ToInt32(this.dtpYear.Text));
            SumMoney(Convert.ToInt32(this.dtpYear.Text));
        }

        private void dtpYear_ValueChanged(object sender, EventArgs e)
        {
            this.chart.Titles.Clear();
            this.chart1.Titles.Clear();
            GetMoney(Convert.ToInt32(this.dtpYear.Text));
            SumMoney(Convert.ToInt32(this.dtpYear.Text));
        }

        private void chart_MouseHover(object sender, EventArgs e)
        {
            //显示当月房款收入
            for (int i = 0; i < this.chart.Series[0].Points.Count; i++)
            {
                this.chart.Series[0].Points[i].ToolTip = "￥" + totalMoney[i];
            }
            //显示当月餐饮收入
            for (int i = 0; i < this.chart.Series[1].Points.Count; i++)
            {
                this.chart.Series[1].Points[i].ToolTip = "￥" + dishPrice[i];
            }
            //显示总房款
            this.chart1.Series[0].Points[0].ToolTip = "￥" + sumTotalMoney;
            //显示总餐饮
            this.chart1.Series[0].Points[1].ToolTip = "￥" + sumDishMoney;
        }
        private void GetMoney(int year)
        {
            //y坐标收入
            try
            {
                totalMoney = StatisticsManager.GetMoney(year);
                dishPrice = StatisticsManager.GetDishPrice(year);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("其它异常：" + ex.Message);
            }
            //x坐标月数
            int[] x = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            //查询当年收入统计
            this.chart.Series[0].Points.DataBindXY(x, totalMoney);
            this.chart.Series[0].Points.DataBindXY(x, dishPrice);
            this.chart.Titles.Add(this.dtpYear.Text + "年度收入统计表");
            this.chart.Titles[0].Font = new Font("楷体", 20, FontStyle.Regular);
        }
        private void SumMoney(int year)
        {
            try
            {
                sumTotalMoney = StatisticsManager.SumTotalMoney(year);
                sumDishMoney = StatisticsManager.SumDishMoney(year);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("其它异常：" + ex.Message);
            }
            this.chart1.Series[0].Points[0].YValues = new double[] { sumTotalMoney };
            this.chart1.Series[0].Points[1].YValues = new double[] { sumDishMoney };
            this.chart1.Titles.Add(this.dtpYear.Text + "年总收入");
            this.chart1.Titles[0].Font = new Font("楷体", 20, FontStyle.Regular);
        }


    }
}
