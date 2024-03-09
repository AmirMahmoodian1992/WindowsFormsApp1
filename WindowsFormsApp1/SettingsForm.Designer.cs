namespace WindowsFormsApp1
{
    partial class SettingsForm
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
            this.BarsaPasslabel = new System.Windows.Forms.Label();
            this.BarsaUsernamelabel = new System.Windows.Forms.Label();
            this.BarcaPass = new System.Windows.Forms.TextBox();
            this.BarcaUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.couplePhone = new System.Windows.Forms.TextBox();
            this.TransferphoneCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // BarsaPasslabel
            // 
            this.BarsaPasslabel.AutoSize = true;
            this.BarsaPasslabel.Location = new System.Drawing.Point(31, 57);
            this.BarsaPasslabel.Name = "BarsaPasslabel";
            this.BarsaPasslabel.Size = new System.Drawing.Size(66, 13);
            this.BarsaPasslabel.TabIndex = 27;
            this.BarsaPasslabel.Text = "Barca PASS";
            this.BarsaPasslabel.Click += new System.EventHandler(this.BarsaPasslabel_Click);
            // 
            // BarsaUsernamelabel
            // 
            this.BarsaUsernamelabel.AutoSize = true;
            this.BarsaUsernamelabel.Location = new System.Drawing.Point(31, 23);
            this.BarsaUsernamelabel.Name = "BarsaUsernamelabel";
            this.BarsaUsernamelabel.Size = new System.Drawing.Size(98, 13);
            this.BarsaUsernamelabel.TabIndex = 26;
            this.BarsaUsernamelabel.Text = "Barsa USERNAME";
            this.BarsaUsernamelabel.Click += new System.EventHandler(this.BarsaUsernamelabel_Click);
            // 
            // BarcaPass
            // 
            this.BarcaPass.AccessibleDescription = "DFGD";
            this.BarcaPass.Location = new System.Drawing.Point(136, 54);
            this.BarcaPass.Name = "BarcaPass";
            this.BarcaPass.Size = new System.Drawing.Size(100, 20);
            this.BarcaPass.TabIndex = 25;
            this.BarcaPass.Text = "123";
            this.BarcaPass.TextChanged += new System.EventHandler(this.BarcaPass_TextChanged);
            // 
            // BarcaUsername
            // 
            this.BarcaUsername.Location = new System.Drawing.Point(136, 20);
            this.BarcaUsername.Name = "BarcaUsername";
            this.BarcaUsername.Size = new System.Drawing.Size(100, 20);
            this.BarcaUsername.TabIndex = 24;
            this.BarcaUsername.Text = "راهبر";
            this.BarcaUsername.TextChanged += new System.EventHandler(this.BarcaUsername_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "SIP PASS";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "SIP USERNAME";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.AccessibleDescription = "DFGD";
            this.txtPassword.Location = new System.Drawing.Point(136, 127);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 21;
            this.txtPassword.Text = "319319";
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(136, 93);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 20;
            this.txtUsername.Text = "319";
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(34, 272);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 19;
            this.btnRegister.Text = "REGISTER";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Coupled Phone";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // couplePhone
            // 
            this.couplePhone.Location = new System.Drawing.Point(136, 162);
            this.couplePhone.Name = "couplePhone";
            this.couplePhone.Size = new System.Drawing.Size(100, 20);
            this.couplePhone.TabIndex = 28;
            this.couplePhone.Text = "143";
            this.couplePhone.TextChanged += new System.EventHandler(this.couplePhone_TextChanged);
            // 
            // TransferphoneCheckBox
            // 
            this.TransferphoneCheckBox.AutoSize = true;
            this.TransferphoneCheckBox.Location = new System.Drawing.Point(259, 165);
            this.TransferphoneCheckBox.Name = "TransferphoneCheckBox";
            this.TransferphoneCheckBox.Size = new System.Drawing.Size(131, 17);
            this.TransferphoneCheckBox.TabIndex = 30;
            this.TransferphoneCheckBox.Text = "Transferphone Enable";
            this.TransferphoneCheckBox.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 321);
            this.Controls.Add(this.TransferphoneCheckBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.couplePhone);
            this.Controls.Add(this.BarsaPasslabel);
            this.Controls.Add(this.BarsaUsernamelabel);
            this.Controls.Add(this.BarcaPass);
            this.Controls.Add(this.BarcaUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnRegister);
            this.Name = "SettingsForm";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BarsaPasslabel;
        private System.Windows.Forms.Label BarsaUsernamelabel;
        public System.Windows.Forms.TextBox BarcaPass;
        public System.Windows.Forms.TextBox BarcaUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox couplePhone;
        private System.Windows.Forms.CheckBox TransferphoneCheckBox;
    }
}