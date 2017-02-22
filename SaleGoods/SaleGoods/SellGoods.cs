using HotelManager.BLL;
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

namespace SaleGoods
{
    public partial class SellGoods : Form
    {
        private int roomId = 0;
        public SellGoods()
        {
            InitializeComponent();
            this.dgvRoom.ReadOnly = false;
            this.btnOk.Enabled = false;
            this.dgvRoom.Enabled = false;
            this.txtList.Enabled = false;
        }
        private decimal money = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.dgvRoom.DataSource = DishListManager.GetList();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("其它异常：" + ex.Message);
            }
            this.btnOk.Enabled = true;
            this.dgvRoom.Enabled = true;
           
        }

        private void dgvRoom_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvRoom.IsCurrentCellDirty)
            {
                dgvRoom.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvRoom_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.txtList.Enabled = true;
            
            if (e.RowIndex >= 0 && !dgvRoom.Rows[e.RowIndex].IsNewRow)
            {
                if (e.ColumnIndex != 5)
                {
                    money = 0;
                StringBuilder sb = new StringBuilder();
                    foreach (DataGridViewRow row in dgvRoom.Rows)
                    {
                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells["ch"];

                        bool flag = Convert.ToBoolean(cell.Value);
                        if (flag)
                        {
                            int num = Convert.ToInt32(row.Cells["num"].Value);
                            //单价
                            decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                            if (num != 0)
                            {
                                sb.AppendLine(row.Cells["DishName"].Value.ToString() + " " + num.ToString() + row.Cells["Unit"].Value.ToString() + " ￥" + price * num);
                                money += Convert.ToDecimal(row.Cells["Price"].Value) * num;
                                this.txtList.Text = sb.ToString();
                            }
                        }
                    }
                        sb.AppendLine("- - - - - - - - - - - - - - - -");
                        sb.AppendLine("总计：￥" + money);
                        this.txtList.Text = sb.ToString();

                    this.txtList.Focus();
                    this.txtList.Select(this.txtList.TextLength,0);
                    this.txtList.ScrollToCaret();
                }
            }
            //this.txtList.Enabled = true;
            //if (e.RowIndex >= 0 && !dgvRoom.Rows[e.RowIndex].IsNewRow)
            //{
            //    if (e.ColumnIndex == 5)
            //    {
            //        money = 0;
            //        StringBuilder sb = new StringBuilder();
            //        foreach (DataGridViewRow row in dgvRoom.Rows)
            //        {
            //            DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells["ch"];
            //            //选中状态
            //            bool flag = Convert.ToBoolean(cell.Value);
            //            if (flag)
            //            {
            //                //份数
            //                int num = Convert.ToInt32(row.Cells["num"].Value);
            //                //单价
            //                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
            //                if (num != 0)
            //                {
            //                    sb.AppendLine(row.Cells["DishName"].Value.ToString() + " " + num.ToString() + row.Cells["Unit"].Value.ToString() + " ￥" + price * num);
            //                    money += Convert.ToDecimal(row.Cells["Price"].Value) * num;
            //                    this.txtList.Text = sb.ToString();
            //                }

            //            }
            //        }
            //        sb.AppendLine("- - - - - - - - - - - - - - - -");
            //        sb.AppendLine("总计：￥" + money);
            //        this.txtList.Text = sb.ToString();
            //        //设置TextBox焦点总在最后
            //        this.txtList.Focus();
            //        this.txtList.Select(this.txtList.TextLength, 0);
            //        this.txtList.ScrollToCaret();
            //    }
            //}
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            try
            {
                DishListManager.InsertPrice(roomId, money);
                MessageBox.Show("点餐成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("其它异常：" + ex.Message);
            }
        }


      

    }
}
