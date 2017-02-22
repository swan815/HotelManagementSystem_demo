namespace 客房管理系统_数据库_
{
    partial class FrmDish
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDishList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DishName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.txtAddName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsmiAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnUpdate = new System.Windows.Forms.ToolStripButton();
            this.tsmiDel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDishList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDishList
            // 
            this.dgvDishList.AllowUserToAddRows = false;
            this.dgvDishList.AllowUserToDeleteRows = false;
            this.dgvDishList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDishList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDishList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.DishName,
            this.Unit,
            this.Price});
            this.dgvDishList.Location = new System.Drawing.Point(13, 92);
            this.dgvDishList.Name = "dgvDishList";
            this.dgvDishList.ReadOnly = true;
            this.dgvDishList.RowTemplate.Height = 23;
            this.dgvDishList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDishList.Size = new System.Drawing.Size(457, 150);
            this.dgvDishList.TabIndex = 10;
            this.dgvDishList.SelectionChanged += new System.EventHandler(this.dgvDishList_SelectionChanged_1);
            // 
            // id
            // 
            this.id.DataPropertyName = "Dishid";
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // DishName
            // 
            this.DishName.DataPropertyName = "DishName";
            this.DishName.HeaderText = "名称";
            this.DishName.Name = "DishName";
            this.DishName.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "单位";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            this.Price.HeaderText = "单价";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPrice);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtUnit);
            this.groupBox2.Controls.Add(this.txtAddName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 248);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(456, 105);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "维护";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(91, 65);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(64, 21);
            this.txtPrice.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "价格：";
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAdd.Location = new System.Drawing.Point(342, 62);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "确定";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(243, 65);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(60, 21);
            this.txtUnit.TabIndex = 2;
            // 
            // txtAddName
            // 
            this.txtAddName.Location = new System.Drawing.Point(91, 28);
            this.txtAddName.Name = "txtAddName";
            this.txtAddName.Size = new System.Drawing.Size(212, 21);
            this.txtAddName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "单位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "名称：";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsbtnUpdate,
            this.tsmiDel,
            this.tsbtnCancel,
            this.tsbtnExit});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(482, 25);
            this.toolStrip.TabIndex = 12;
            this.toolStrip.Text = "toolStrip1";
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(52, 22);
            this.tsmiAdd.Text = "新增(&A)";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click_1);
            // 
            // tsbtnUpdate
            // 
            this.tsbtnUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnUpdate.Name = "tsbtnUpdate";
            this.tsbtnUpdate.Size = new System.Drawing.Size(53, 22);
            this.tsbtnUpdate.Text = "修改(&U)";
            this.tsbtnUpdate.Click += new System.EventHandler(this.tsbtnUpdate_Click_1);
            // 
            // tsmiDel
            // 
            this.tsmiDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsmiDel.Name = "tsmiDel";
            this.tsmiDel.Size = new System.Drawing.Size(53, 22);
            this.tsmiDel.Text = "删除(&D)";
            this.tsmiDel.Click += new System.EventHandler(this.tsmiDel_Click_1);
            // 
            // tsbtnCancel
            // 
            this.tsbtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnCancel.Name = "tsbtnCancel";
            this.tsbtnCancel.Size = new System.Drawing.Size(52, 22);
            this.tsbtnCancel.Text = "取消(&C)";
            this.tsbtnCancel.Click += new System.EventHandler(this.tsbtnCancel_Click_1);
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(51, 22);
            this.tsbtnExit.Text = "退出(&E)";
            this.tsbtnExit.Click += new System.EventHandler(this.tsbtnExit_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 61);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // btnFind
            // 
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFind.Location = new System.Drawing.Point(342, 16);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(148, 21);
            this.txtName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // FrmDish
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 365);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.dgvDishList);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmDish";
            this.Text = "FrmDish";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDishList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDishList;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn DishName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.TextBox txtAddName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsmiAdd;
        private System.Windows.Forms.ToolStripButton tsbtnUpdate;
        private System.Windows.Forms.ToolStripButton tsmiDel;
        private System.Windows.Forms.ToolStripButton tsbtnCancel;
        private System.Windows.Forms.ToolStripButton tsbtnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
    }
}