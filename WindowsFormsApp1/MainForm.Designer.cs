namespace SIPWindowsAgent
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnCall = new System.Windows.Forms.Button();
            this.txtCallNumber = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.recivedCallTime = new System.Windows.Forms.Label();
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
            this.MuteButton = new System.Windows.Forms.Button();
            this.UnmuteButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lstBarcaUsernames = new System.Windows.Forms.ListBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // btnCall
            // 
            this.btnCall.Location = new System.Drawing.Point(10, 162);
            this.btnCall.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(122, 45);
            this.btnCall.TabIndex = 4;
            this.btnCall.Text = "Call";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.CreateCall);
            // 
            // txtCallNumber
            // 
            this.txtCallNumber.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCallNumber.Location = new System.Drawing.Point(150, 126);
            this.txtCallNumber.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCallNumber.Multiline = true;
            this.txtCallNumber.Name = "txtCallNumber";
            this.txtCallNumber.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtCallNumber.Size = new System.Drawing.Size(276, 30);
            this.txtCallNumber.TabIndex = 1;
            this.txtCallNumber.Text = "115";
            this.txtCallNumber.TextChanged += new System.EventHandler(this.txtCallNumber_TextChanged);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(10, 347);
            this.txtLog.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(448, 142);
            this.txtLog.TabIndex = 1;
            // 
            // cmbProvider
            // 
            this.cmbProvider.FormattingEnabled = true;
            this.cmbProvider.Items.AddRange(new object[] {
            "OzekiVoip"});
            this.cmbProvider.Location = new System.Drawing.Point(321, 10);
            this.cmbProvider.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(140, 22);
            this.cmbProvider.TabIndex = 7;
            this.cmbProvider.Text = "OzekiVoip";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(234, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "Provider";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(10, 213);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 46);
            this.button3.TabIndex = 4;
            this.button3.Text = "Drop";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DropCall);
            // 
            // recivedCallTime
            // 
            this.recivedCallTime.AutoSize = true;
            this.recivedCallTime.Location = new System.Drawing.Point(53, 306);
            this.recivedCallTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.recivedCallTime.Name = "recivedCallTime";
            this.recivedCallTime.Size = new System.Drawing.Size(36, 14);
            this.recivedCallTime.TabIndex = 11;
            this.recivedCallTime.Text = "Timer";
            this.recivedCallTime.Click += new System.EventHandler(this.recivedCallTime_Click);
            // 
            // SettingButton
            // 
            this.SettingButton.Location = new System.Drawing.Point(239, 52);
            this.SettingButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.SettingButton.Name = "SettingButton";
            this.SettingButton.Size = new System.Drawing.Size(222, 25);
            this.SettingButton.TabIndex = 19;
            this.SettingButton.Text = "Setting";
            this.SettingButton.UseVisualStyleBackColor = true;
            this.SettingButton.Click += new System.EventHandler(this.SettingButton_Click);
            // 
            // RegisterRadioButton
            // 
            this.RegisterRadioButton.AutoSize = true;
            this.RegisterRadioButton.Enabled = false;
            this.RegisterRadioButton.Location = new System.Drawing.Point(10, 13);
            this.RegisterRadioButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.RegisterRadioButton.Name = "RegisterRadioButton";
            this.RegisterRadioButton.Size = new System.Drawing.Size(102, 18);
            this.RegisterRadioButton.TabIndex = 20;
            this.RegisterRadioButton.Text = "Register Status";
            this.RegisterRadioButton.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(150, 162);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(88, 34);
            this.button4.TabIndex = 21;
            this.button4.Text = "1";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(245, 162);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(88, 34);
            this.button5.TabIndex = 22;
            this.button5.Text = "2";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(340, 162);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(88, 34);
            this.button6.TabIndex = 23;
            this.button6.Text = "3";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(340, 202);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(88, 36);
            this.button7.TabIndex = 26;
            this.button7.Text = "6";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button8.Location = new System.Drawing.Point(245, 202);
            this.button8.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(88, 36);
            this.button8.TabIndex = 25;
            this.button8.Text = "5";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            this.button9.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Location = new System.Drawing.Point(150, 202);
            this.button9.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(88, 36);
            this.button9.TabIndex = 24;
            this.button9.Text = "4";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Location = new System.Drawing.Point(340, 246);
            this.button10.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(88, 34);
            this.button10.TabIndex = 29;
            this.button10.Text = "9";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            this.button11.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button11.Location = new System.Drawing.Point(246, 246);
            this.button11.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(88, 34);
            this.button11.TabIndex = 28;
            this.button11.Text = "8";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // button12
            // 
            this.button12.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button12.Location = new System.Drawing.Point(150, 246);
            this.button12.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(88, 34);
            this.button12.TabIndex = 27;
            this.button12.Text = "7";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            this.button13.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.Location = new System.Drawing.Point(245, 286);
            this.button13.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(88, 34);
            this.button13.TabIndex = 30;
            this.button13.Text = "0";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // MuteButton
            // 
            this.MuteButton.Location = new System.Drawing.Point(13, 265);
            this.MuteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MuteButton.Name = "MuteButton";
            this.MuteButton.Size = new System.Drawing.Size(54, 27);
            this.MuteButton.TabIndex = 31;
            this.MuteButton.Text = "Mute";
            this.MuteButton.UseVisualStyleBackColor = true;
            this.MuteButton.Click += new System.EventHandler(this.MuteButton_Click);
            // 
            // UnmuteButton
            // 
            this.UnmuteButton.Location = new System.Drawing.Point(75, 265);
            this.UnmuteButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.UnmuteButton.Name = "UnmuteButton";
            this.UnmuteButton.Size = new System.Drawing.Size(57, 27);
            this.UnmuteButton.TabIndex = 32;
            this.UnmuteButton.Text = "Unmute";
            this.UnmuteButton.UseVisualStyleBackColor = true;
            this.UnmuteButton.Click += new System.EventHandler(this.UnmuteButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(189, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(43, 25);
            this.button1.TabIndex = 33;
            this.button1.Text = "Add Account";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstBarcaUsernames
            // 
            this.lstBarcaUsernames.FormattingEnabled = true;
            this.lstBarcaUsernames.ItemHeight = 14;
            this.lstBarcaUsernames.Location = new System.Drawing.Point(13, 52);
            this.lstBarcaUsernames.Name = "lstBarcaUsernames";
            this.lstBarcaUsernames.Size = new System.Drawing.Size(120, 88);
            this.lstBarcaUsernames.TabIndex = 34;
            this.lstBarcaUsernames.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SIP Agent";
            this.notifyIcon1.Visible = true;
            // 
            // MainForm
            // 
            this.AccessibleDescription = "DFG";
            this.AccessibleName = "DFGD";
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 501);
            this.Controls.Add(this.lstBarcaUsernames);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.UnmuteButton);
            this.Controls.Add(this.MuteButton);
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
            this.Controls.Add(this.recivedCallTime);
            this.Controls.Add(this.cmbProvider);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnCall);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtCallNumber);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.Text = "SIP Agent";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnCall;
        public System.Windows.Forms.TextBox txtCallNumber;
        public System.Windows.Forms.TextBox txtLog;
        public System.Windows.Forms.ComboBox cmbProvider;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Label recivedCallTime;
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
        public System.Windows.Forms.Button MuteButton;
        public System.Windows.Forms.Button UnmuteButton;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ListBox lstBarcaUsernames;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

