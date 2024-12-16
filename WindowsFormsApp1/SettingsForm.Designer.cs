using System.Drawing.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SIPWindowsAgent
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
            this.UserTokenTextBox = new System.Windows.Forms.TextBox();
            this.BarcaUsername = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CouplePhoneTextBox = new System.Windows.Forms.TextBox();
            this.TransferphoneCheckBox = new System.Windows.Forms.CheckBox();
            this.BarsaAddressLabel = new System.Windows.Forms.Label();
            this.BarsaAddressTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.generalSettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFormClosingInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.generalSettingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarsaPasslabel
            // 
            this.BarsaPasslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BarsaPasslabel.Location = new System.Drawing.Point(303, 105);
            this.BarsaPasslabel.Name = "BarsaPasslabel";
            this.BarsaPasslabel.Size = new System.Drawing.Size(63, 13);
            this.BarsaPasslabel.TabIndex = 27;
            this.BarsaPasslabel.Text = "کلید اتصال";
            // 
            // BarsaUsernamelabel
            // 
            this.BarsaUsernamelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BarsaUsernamelabel.Location = new System.Drawing.Point(279, 69);
            this.BarsaUsernamelabel.Name = "BarsaUsernamelabel";
            this.BarsaUsernamelabel.Size = new System.Drawing.Size(87, 13);
            this.BarsaUsernamelabel.TabIndex = 26;
            this.BarsaUsernamelabel.Text = "کاربر";
            // 
            // UserTokenTextBox
            // 
            this.UserTokenTextBox.AccessibleDescription = "DFGD";
            this.UserTokenTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserTokenTextBox.Location = new System.Drawing.Point(112, 102);
            this.UserTokenTextBox.Name = "UserTokenTextBox";
            this.UserTokenTextBox.ReadOnly = true;
            this.UserTokenTextBox.Size = new System.Drawing.Size(168, 21);
            this.UserTokenTextBox.TabIndex = 25;
            // 
            // BarcaUsername
            // 
            this.BarcaUsername.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BarcaUsername.Location = new System.Drawing.Point(112, 66);
            this.BarcaUsername.Name = "BarcaUsername";
            this.BarcaUsername.ReadOnly = true;
            this.BarcaUsername.Size = new System.Drawing.Size(168, 21);
            this.BarcaUsername.TabIndex = 24;
            // 
            // btnRegister
            // 
            this.btnRegister.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRegister.Location = new System.Drawing.Point(93, 378);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 28);
            this.btnRegister.TabIndex = 19;
            this.btnRegister.Text = "ذخیره";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(297, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "شماره کمکی";
            // 
            // CouplePhoneTextBox
            // 
            this.CouplePhoneTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CouplePhoneTextBox.Location = new System.Drawing.Point(112, 35);
            this.CouplePhoneTextBox.Name = "CouplePhoneTextBox";
            this.CouplePhoneTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CouplePhoneTextBox.Size = new System.Drawing.Size(168, 21);
            this.CouplePhoneTextBox.TabIndex = 28;
            // 
            // TransferphoneCheckBox
            // 
            this.TransferphoneCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TransferphoneCheckBox.AutoSize = true;
            this.TransferphoneCheckBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransferphoneCheckBox.Location = new System.Drawing.Point(257, 0);
            this.TransferphoneCheckBox.Name = "TransferphoneCheckBox";
            this.TransferphoneCheckBox.Size = new System.Drawing.Size(140, 17);
            this.TransferphoneCheckBox.TabIndex = 30;
            this.TransferphoneCheckBox.Text = "استفاده از گوشی کمکی";
            this.TransferphoneCheckBox.UseVisualStyleBackColor = true;
            this.TransferphoneCheckBox.CheckedChanged += new System.EventHandler(this.TransferphoneCheckBox_CheckedChanged);
            // 
            // BarsaAddressLabel
            // 
            this.BarsaAddressLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BarsaAddressLabel.Location = new System.Drawing.Point(291, 31);
            this.BarsaAddressLabel.Name = "BarsaAddressLabel";
            this.BarsaAddressLabel.Size = new System.Drawing.Size(75, 13);
            this.BarsaAddressLabel.TabIndex = 36;
            this.BarsaAddressLabel.Text = "آدرس سایت";
            // 
            // BarsaAddressTextBox
            // 
            this.BarsaAddressTextBox.AccessibleDescription = "DFGD";
            this.BarsaAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BarsaAddressTextBox.Location = new System.Drawing.Point(112, 28);
            this.BarsaAddressTextBox.Name = "BarsaAddressTextBox";
            this.BarsaAddressTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BarsaAddressTextBox.Size = new System.Drawing.Size(169, 21);
            this.BarsaAddressTextBox.TabIndex = 35;
            this.BarsaAddressTextBox.Text = "https://my.barsasoft.com/";
            this.BarsaAddressTextBox.TextChanged += new System.EventHandler(this.BarsaAddressTextBox_TextChanged);
            this.BarsaAddressTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.BarsaAddressTextBox_Validating);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.BarcaUsername);
            this.groupBox2.Controls.Add(this.BarsaAddressTextBox);
            this.groupBox2.Controls.Add(this.UserTokenTextBox);
            this.groupBox2.Controls.Add(this.BarsaUsernamelabel);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.BarsaAddressLabel);
            this.groupBox2.Controls.Add(this.BarsaPasslabel);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 144);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "تنظیمات اتصال";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(19, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 37;
            this.button1.Text = "اتصال کاربر";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.TransferphoneCheckBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.CouplePhoneTextBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 72);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " ";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(12, 378);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 19;
            this.btnCancel.Text = "انصراف";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // generalSettingsGroupBox
            // 
            this.generalSettingsGroupBox.Controls.Add(this.label2);
            this.generalSettingsGroupBox.Controls.Add(this.textBoxFormClosingInterval);
            this.generalSettingsGroupBox.Controls.Add(this.label1);
            this.generalSettingsGroupBox.Location = new System.Drawing.Point(12, 249);
            this.generalSettingsGroupBox.Name = "generalSettingsGroupBox";
            this.generalSettingsGroupBox.Size = new System.Drawing.Size(403, 100);
            this.generalSettingsGroupBox.TabIndex = 42;
            this.generalSettingsGroupBox.TabStop = false;
            this.generalSettingsGroupBox.Text = "تنظیمات عمومی";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ثانیه.";
            // 
            // textBoxFormClosingInterval
            // 
            this.textBoxFormClosingInterval.Location = new System.Drawing.Point(152, 35);
            this.textBoxFormClosingInterval.Name = "textBoxFormClosingInterval";
            this.textBoxFormClosingInterval.Size = new System.Drawing.Size(62, 21);
            this.textBoxFormClosingInterval.TabIndex = 1;
            this.textBoxFormClosingInterval.Text = "30";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(220, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "بسته شدن پنجره تماس بعد از ";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(432, 418);
            this.Controls.Add(this.generalSettingsGroupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRegister);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "تنظیمات برنامه ...";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.generalSettingsGroupBox.ResumeLayout(false);
            this.generalSettingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label BarsaPasslabel;
        private System.Windows.Forms.Label BarsaUsernamelabel;
        public System.Windows.Forms.TextBox UserTokenTextBox;
        public System.Windows.Forms.TextBox BarcaUsername;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox CouplePhoneTextBox;
        private System.Windows.Forms.CheckBox TransferphoneCheckBox;
        private System.Windows.Forms.Label BarsaAddressLabel;
        internal System.Windows.Forms.TextBox BarsaAddressTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox generalSettingsGroupBox;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBoxFormClosingInterval;
        private System.Windows.Forms.Label label1;
    }
}