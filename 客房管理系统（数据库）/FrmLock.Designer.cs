namespace 客房管理系统_数据库_
{
    partial class FrmLock
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.btnUnLock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(64, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "已锁定！请输入密码解锁！！！";
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(27, 110);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(169, 21);
            this.txtPassWord.TabIndex = 5;
            // 
            // btnUnLock
            // 
            this.btnUnLock.Location = new System.Drawing.Point(237, 108);
            this.btnUnLock.Name = "btnUnLock";
            this.btnUnLock.Size = new System.Drawing.Size(75, 23);
            this.btnUnLock.TabIndex = 6;
            this.btnUnLock.Text = "解锁";
            this.btnUnLock.UseVisualStyleBackColor = true;
            this.btnUnLock.Click += new System.EventHandler(this.btnUnLock_Click);
            // 
            // FrmLock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 261);
            this.Controls.Add(this.btnUnLock);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.label1);
            this.Name = "FrmLock";
            this.Text = "FrmLock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Button btnUnLock;
    }
}