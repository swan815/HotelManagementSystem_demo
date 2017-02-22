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
using HotelManager.Common;
using HotelManager.Model;
namespace 客房管理系统_数据库_
{
    public partial class FrmDish : Form
    {
        public FrmDish()
        {
            InitializeComponent();
            this.txtAddName.Enabled = false;
            this.txtPrice.Enabled = false;
            this.txtUnit.Enabled = false;
            this.btnAdd.Enabled = false;
            this.tsbtnCancel.Enabled = false;
        }
        private State state = State.add;

        //加载
        private void FrmDish_Load(object sender, EventArgs e)
        {
            try
            {
                this.dgvDishList.DataSource = DishListManager.GetList();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知异常：" + ex.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                this.dgvDishList.DataSource = DishListManager.FindList(this.txtName.Text);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知异常：" + ex.Message);
            }
        }

        private void tsmiAdd_Click_1(object sender, EventArgs e)
        {
            state = State.add;
            this.txtAddName.Enabled = true;
            this.txtPrice.Enabled = true;
            this.txtUnit.Enabled = true;
            this.btnAdd.Enabled = true;
            this.tsbtnCancel.Enabled = true;
            this.txtAddName.Text = string.Empty;
            this.txtPrice.Text = string.Empty;
            this.txtUnit.Text = string.Empty;
        }

        private void tsbtnUpdate_Click_1(object sender, EventArgs e)
        {
            state = State.update;
            this.txtAddName.Enabled = true;
            this.txtPrice.Enabled = true;
            this.txtUnit.Enabled = true;
            this.btnAdd.Enabled = true;
            this.tsbtnCancel.Enabled = true;
            this.txtAddName.Text = dgvDishList.CurrentRow.Cells["DishName"].Value.ToString();
            this.txtPrice.Text = dgvDishList.CurrentRow.Cells["Price"].Value.ToString();
            this.txtUnit.Text = dgvDishList.CurrentRow.Cells["Unit"].Value.ToString();
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            //非空验证
            if (string.IsNullOrEmpty(this.txtAddName.Text) || string.IsNullOrEmpty(this.txtPrice.Text) || string.IsNullOrEmpty(this.txtUnit.Text))
            {
                MessageBox.Show("请完善餐饮信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            DishList list = new DishList();
            list.DishName = this.txtAddName.Text;
            list.Price = Convert.ToDecimal(this.txtPrice.Text);
            list.Unit = this.txtUnit.Text;
            list.DishId = Convert.ToInt32(this.dgvDishList.CurrentRow.Cells["id"].Value);
            //更新
            if (state == State.update)
            {
                try
                {
                    if (DishListManager.UpdateInfo(list))
                    {
                        MessageBox.Show("更新成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //刷新
                        FrmDish_Load(null, null);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("数据库异常：" + ex.Message); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("未知异常：" + ex.Message);
                }
            }
            //新增：
            if (state == State.add)
            {
                try
                {
                    if (DishListManager.InsertInfo(list))
                    {
                        MessageBox.Show("新增成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //刷新
                        FrmDish_Load(null, null);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void tsmiDel_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                bool flag = DishListManager.DelDishInfo(Convert.ToInt32(this.dgvDishList.CurrentRow.Cells["id"].Value));
                if (flag)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //刷新
                    FrmDish_Load(null, null);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("数据库异常：" + ex.Message); ;
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知异常：" + ex.Message);
            }
        }

        private void tsbtnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbtnCancel_Click_1(object sender, EventArgs e)
        {
            state = State.add;
            this.txtAddName.Enabled = false;
            this.txtPrice.Enabled = false;
            this.txtUnit.Enabled = false;
            this.btnAdd.Enabled = false;
            this.tsbtnCancel.Enabled = false;
            this.txtAddName.Text = string.Empty;
            this.txtPrice.Text = string.Empty;
            this.txtUnit.Text = string.Empty;
        }

        private void dgvDishList_SelectionChanged_1(object sender, EventArgs e)
        {
            if (state == State.update)
            {
                this.txtAddName.Text = dgvDishList.CurrentRow.Cells["DishName"].Value.ToString();
                this.txtPrice.Text = dgvDishList.CurrentRow.Cells["Price"].Value.ToString();
                this.txtUnit.Text = dgvDishList.CurrentRow.Cells["Unit"].Value.ToString();
            }
        }
    }
}
