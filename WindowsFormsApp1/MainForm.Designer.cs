namespace WindowsFormsApp1
{
    partial class MainForm
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
            this.btnRegister = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnCall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCallNumber = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.incominNumber = new System.Windows.Forms.TextBox();
            this.recivedCallTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.couplePhone = new System.Windows.Forms.TextBox();
            this.Transferbut = new System.Windows.Forms.Button();
            this.BarcaPasslabel = new System.Windows.Forms.Label();
            this.BarcaUsernamelabel = new System.Windows.Forms.Label();
            this.BarcaPass = new System.Windows.Forms.TextBox();
            this.BarcaUsername = new System.Windows.Forms.TextBox();
            this.SettingButton = new System.Windows.Forms.Button();
            this.RegisterRadioButton = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(300, 396);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "REGISTER";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Visible = false;
            this.btnRegister.Click += new System.EventHandler(this.RegisterAccount);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(394, 334);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 20);
            this.txtUsername.TabIndex = 1;
            this.txtUsername.Text = "319";
            this.txtUsername.Visible = false;
            // 
            // txtPassword
            // 
            this.txtPassword.AccessibleDescription = "DFGD";
            this.txtPassword.Location = new System.Drawing.Point(394, 368);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "319319";
            this.txtPassword.Visible = false;
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(130, 136);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(105, 30);
            this.btnCall.TabIndex = 4;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.CreateCall);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(297, 337);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "SIP USERNAME";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(297, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "SIP PASS";
            this.label2.Visible = false;
            // 
            // txtCallNumber
            // 
            this.txtCallNumber.Location = new System.Drawing.Point(275, 97);
            this.txtCallNumber.Name = "txtCallNumber";
            this.txtCallNumber.Size = new System.Drawing.Size(239, 20);
            this.txtCallNumber.TabIndex = 1;
            this.txtCallNumber.Text = "115";
            this.txtCallNumber.TextChanged += new System.EventHandler(this.txtCallNumber_TextChanged);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(37, 433);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(733, 214);
            this.txtLog.TabIndex = 1;
            // 
            // cmbProvider
            // 
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Items.AddRange(new object[] {
            "OzekiVoip"});
            this.cmbProvider.Location = new System.Drawing.Point(663, 18);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(121, 21);
            this.cmbProvider.TabIndex = 7;
            this.cmbProvider.Text = "OzekiVoip";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(595, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Provider";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(130, 183);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 29);
            this.button3.TabIndex = 4;
            this.button3.Text = "Drop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DropCall);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(61, 343);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Regect Or Drop";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.RejectCall);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(61, 311);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Answer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.AnswerCall);
            // 
            // incominNumber
            // 
            this.incominNumber.Location = new System.Drawing.Point(194, 313);
            this.incominNumber.Name = "incominNumber";
            this.incominNumber.Size = new System.Drawing.Size(100, 20);
            this.incominNumber.TabIndex = 10;
            this.incominNumber.Visible = false;
            // 
            // recivedCallTime
            // 
            this.recivedCallTime.AutoSize = true;
            this.recivedCallTime.Location = new System.Drawing.Point(34, 404);
            this.recivedCallTime.Name = "recivedCallTime";
            this.recivedCallTime.Size = new System.Drawing.Size(33, 13);
            this.recivedCallTime.TabIndex = 11;
            this.recivedCallTime.Text = "Timer";
            this.recivedCallTime.Visible = false;
            this.recivedCallTime.Click += new System.EventHandler(this.recivedCallTime_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(518, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Enter Coupled Phone";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // couplePhone
            // 
            this.couplePhone.Location = new System.Drawing.Point(648, 334);
            this.couplePhone.Name = "couplePhone";
            this.couplePhone.Size = new System.Drawing.Size(100, 20);
            this.couplePhone.TabIndex = 12;
            this.couplePhone.Text = "143";
            this.couplePhone.Visible = false;
            this.couplePhone.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Transferbut
            // 
            this.Transferbut.Location = new System.Drawing.Point(648, 360);
            this.Transferbut.Name = "Transferbut";
            this.Transferbut.Size = new System.Drawing.Size(100, 23);
            this.Transferbut.TabIndex = 14;
            this.Transferbut.Text = "Transfer";
            this.Transferbut.UseVisualStyleBackColor = true;
            this.Transferbut.Visible = false;
            this.Transferbut.Click += new System.EventHandler(this.button4_Click);
            // 
            // BarcaPasslabel
            // 
            this.BarcaPasslabel.AutoSize = true;
            this.BarcaPasslabel.Location = new System.Drawing.Point(75, 405);
            this.BarcaPasslabel.Name = "BarcaPasslabel";
            this.BarcaPasslabel.Size = new System.Drawing.Size(66, 13);
            this.BarcaPasslabel.TabIndex = 18;
            this.BarcaPasslabel.Text = "Barca PASS";
            this.BarcaPasslabel.Visible = false;
            // 
            // BarcaUsernamelabel
            // 
            this.BarcaUsernamelabel.AutoSize = true;
            this.BarcaUsernamelabel.Location = new System.Drawing.Point(75, 371);
            this.BarcaUsernamelabel.Name = "BarcaUsernamelabel";
            this.BarcaUsernamelabel.Size = new System.Drawing.Size(99, 13);
            this.BarcaUsernamelabel.TabIndex = 17;
            this.BarcaUsernamelabel.Text = "Barca USERNAME";
            this.BarcaUsernamelabel.Visible = false;
            // 
            // BarcaPass
            // 
            this.BarcaPass.AccessibleDescription = "DFGD";
            this.BarcaPass.Location = new System.Drawing.Point(180, 402);
            this.BarcaPass.Name = "BarcaPass";
            this.BarcaPass.Size = new System.Drawing.Size(100, 20);
            this.BarcaPass.TabIndex = 16;
            this.BarcaPass.Text = "123";
            this.BarcaPass.Visible = false;
            this.BarcaPass.TextChanged += new System.EventHandler(this.BarcaPass_TextChanged);
            // 
            // BarcaUsername
            // 
            this.BarcaUsername.Location = new System.Drawing.Point(180, 368);
            this.BarcaUsername.Name = "BarcaUsername";
            this.BarcaUsername.Size = new System.Drawing.Size(100, 20);
            this.BarcaUsername.TabIndex = 15;
            this.BarcaUsername.Text = "راهبر";
            this.BarcaUsername.Visible = false;
            // 
            // SettingButton
            // 
            this.SettingButton.Location = new System.Drawing.Point(598, 57);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(190, 23);
            this.SettingButton.TabIndex = 19;
            this.SettingButton.Text = "Setting";
            this.SettingButton.UseVisualStyleBackColor = true;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // RegisterRadioButton
            // 
            this.RegisterRadioButton.AutoSize = true;
            this.RegisterRadioButton.Enabled = false;
            this.RegisterRadioButton.Location = new System.Drawing.Point(37, 11);
            this.RegisterRadioButton.Name = "RegisterRadioButton";
            this.RegisterRadioButton.Size = new System.Drawing.Size(97, 17);
            this.RegisterRadioButton.TabIndex = 20;
            this.RegisterRadioButton.Text = "Register Status";
            this.RegisterRadioButton.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(277, 136);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "1";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(358, 136);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 22;
            this.button5.Text = "2";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(439, 136);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 23;
            this.button6.Text = "3";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(439, 174);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 26;
            this.button7.Text = "4";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(358, 174);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 25;
            this.button8.Text = "5";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(277, 174);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 24;
            this.button9.Text = "6";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(440, 213);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 29;
            this.button10.Text = "7";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(359, 213);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 28;
            this.button11.Text = "8";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(278, 213);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 27;
            this.button12.Text = "9";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(359, 251);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(75, 23);
            this.button13.TabIndex = 30;
            this.button13.Text = "0";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AccessibleDescription = "DFG";
            this.AccessibleName = "DFGD";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 670);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.RegisterRadioButton);
            this.Controls.Add(this.SettingButton);
            this.Controls.Add(this.BarcaPasslabel);
            this.Controls.Add(this.BarcaUsernamelabel);
            this.Controls.Add(this.BarcaPass);
            this.Controls.Add(this.BarcaUsername);
            this.Controls.Add(this.Transferbut);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.couplePhone);
            this.Controls.Add(this.recivedCallTime);
            this.Controls.Add(this.incominNumber);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbProvider);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtCallNumber);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnRegister);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnRegister;
        public System.Windows.Forms.TextBox txtUsername;
        public System.Windows.Forms.TextBox txtPassword;
        public System.Windows.Forms.Button btnCall;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtCallNumber;
        public System.Windows.Forms.TextBox txtLog;
        public System.Windows.Forms.ComboBox cmbProvider;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.TextBox incominNumber;
        public System.Windows.Forms.Label recivedCallTime;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox couplePhone;
        public System.Windows.Forms.Button Transferbut;
        public System.Windows.Forms.Label BarcaPasslabel;
        public System.Windows.Forms.Label BarcaUsernamelabel;
        public System.Windows.Forms.TextBox BarcaPass;
        public System.Windows.Forms.TextBox BarcaUsername;
        public System.Windows.Forms.Button SettingButton;
        public System.Windows.Forms.RadioButton RegisterRadioButton;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
    }
}

