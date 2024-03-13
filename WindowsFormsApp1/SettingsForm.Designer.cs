﻿using System.Drawing.Text;
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
            this.BarcaPass = new System.Windows.Forms.TextBox();
            this.BarcaUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.CouplePhoneTextBox = new System.Windows.Forms.TextBox();
            this.TransferphoneCheckBox = new System.Windows.Forms.CheckBox();
            this.SIPServerLable = new System.Windows.Forms.Label();
            this.SIPServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.SIPServerPortLabel = new System.Windows.Forms.Label();
            this.SIPServerPortTextBox = new System.Windows.Forms.TextBox();
            this.BarsaAddressLabel = new System.Windows.Forms.Label();
            this.BarsaAddressTextBox = new System.Windows.Forms.TextBox();
            this.ShowPassBarsaButton = new System.Windows.Forms.Button();
            this.ShowPassSip = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarsaPasslabel
            // 
            this.BarsaPasslabel.AutoSize = true;
            this.BarsaPasslabel.Location = new System.Drawing.Point(14, 65);
            this.BarsaPasslabel.Name = "BarsaPasslabel";
            this.BarsaPasslabel.Size = new System.Drawing.Size(59, 13);
            this.BarsaPasslabel.TabIndex = 27;
            this.BarsaPasslabel.Text = "Barsa Pass";
            this.BarsaPasslabel.Click += new System.EventHandler(this.BarsaPasslabel_Click);
            // 
            // BarsaUsernamelabel
            // 
            this.BarsaUsernamelabel.AutoSize = true;
            this.BarsaUsernamelabel.Location = new System.Drawing.Point(14, 29);
            this.BarsaUsernamelabel.Name = "BarsaUsernamelabel";
            this.BarsaUsernamelabel.Size = new System.Drawing.Size(86, 13);
            this.BarsaUsernamelabel.TabIndex = 26;
            this.BarsaUsernamelabel.Text = "Barsa UserName";
            this.BarsaUsernamelabel.Click += new System.EventHandler(this.BarsaUsernamelabel_Click);
            // 
            // BarcaPass
            // 
            this.BarcaPass.AccessibleDescription = "DFGD";
            this.BarcaPass.Location = new System.Drawing.Point(119, 62);
            this.BarcaPass.Name = "BarcaPass";
            this.BarcaPass.PasswordChar = '*';
            this.BarcaPass.Size = new System.Drawing.Size(100, 21);
            this.BarcaPass.TabIndex = 25;
            this.BarcaPass.TextChanged += new System.EventHandler(this.BarcaPass_TextChanged);
            // 
            // BarcaUsername
            // 
            this.BarcaUsername.Location = new System.Drawing.Point(119, 26);
            this.BarcaUsername.Name = "BarcaUsername";
            this.BarcaUsername.Size = new System.Drawing.Size(100, 21);
            this.BarcaUsername.TabIndex = 24;
            this.BarcaUsername.TextChanged += new System.EventHandler(this.BarcaUsername_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "UserName";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.AccessibleDescription = "DFGD";
            this.txtPassword.Location = new System.Drawing.Point(119, 59);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(100, 21);
            this.txtPassword.TabIndex = 21;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(119, 25);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 21);
            this.txtUsername.TabIndex = 20;
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(24, 418);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(75, 28);
            this.btnRegister.TabIndex = 19;
            this.btnRegister.Text = "OK";
            this.btnRegister.UseVisualStyleBackColor = true;
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 29;
            this.label4.Text = "Coupled Phone";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // CouplePhoneTextBox
            // 
            this.CouplePhoneTextBox.Location = new System.Drawing.Point(119, 30);
            this.CouplePhoneTextBox.Name = "CouplePhoneTextBox";
            this.CouplePhoneTextBox.Size = new System.Drawing.Size(100, 21);
            this.CouplePhoneTextBox.TabIndex = 28;
            this.CouplePhoneTextBox.TextChanged += new System.EventHandler(this.couplePhone_TextChanged);
            // 
            // TransferphoneCheckBox
            // 
            this.TransferphoneCheckBox.AutoSize = true;
            this.TransferphoneCheckBox.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TransferphoneCheckBox.Location = new System.Drawing.Point(6, 0);
            this.TransferphoneCheckBox.Name = "TransferphoneCheckBox";
            this.TransferphoneCheckBox.Size = new System.Drawing.Size(107, 18);
            this.TransferphoneCheckBox.TabIndex = 30;
            this.TransferphoneCheckBox.Text = "استفاده از گوشی کمکی";
            this.TransferphoneCheckBox.UseVisualStyleBackColor = true;
            this.TransferphoneCheckBox.CheckedChanged += new System.EventHandler(this.TransferphoneCheckBox_CheckedChanged);
            // 
            // SIPServerLable
            // 
            this.SIPServerLable.AutoSize = true;
            this.SIPServerLable.Location = new System.Drawing.Point(14, 95);
            this.SIPServerLable.Name = "SIPServerLable";
            this.SIPServerLable.Size = new System.Drawing.Size(100, 13);
            this.SIPServerLable.TabIndex = 32;
            this.SIPServerLable.Text = "SIP Server Address";
            // 
            // SIPServerAddressTextBox
            // 
            this.SIPServerAddressTextBox.AccessibleDescription = "DFGD";
            this.SIPServerAddressTextBox.Location = new System.Drawing.Point(119, 92);
            this.SIPServerAddressTextBox.Name = "SIPServerAddressTextBox";
            this.SIPServerAddressTextBox.Size = new System.Drawing.Size(100, 21);
            this.SIPServerAddressTextBox.TabIndex = 31;
            // 
            // SIPServerPortLabel
            // 
            this.SIPServerPortLabel.AutoSize = true;
            this.SIPServerPortLabel.Location = new System.Drawing.Point(14, 127);
            this.SIPServerPortLabel.Name = "SIPServerPortLabel";
            this.SIPServerPortLabel.Size = new System.Drawing.Size(81, 13);
            this.SIPServerPortLabel.TabIndex = 34;
            this.SIPServerPortLabel.Text = "SIP Server Port";
            // 
            // SIPServerPortTextBox
            // 
            this.SIPServerPortTextBox.AccessibleDescription = "DFGD";
            this.SIPServerPortTextBox.Location = new System.Drawing.Point(119, 124);
            this.SIPServerPortTextBox.Name = "SIPServerPortTextBox";
            this.SIPServerPortTextBox.Size = new System.Drawing.Size(100, 21);
            this.SIPServerPortTextBox.TabIndex = 33;
            this.SIPServerPortTextBox.TextChanged += new System.EventHandler(this.SIPServerPortTextBox_TextChanged);
            // 
            // BarsaAddressLabel
            // 
            this.BarsaAddressLabel.AutoSize = true;
            this.BarsaAddressLabel.Location = new System.Drawing.Point(17, 98);
            this.BarsaAddressLabel.Name = "BarsaAddressLabel";
            this.BarsaAddressLabel.Size = new System.Drawing.Size(76, 13);
            this.BarsaAddressLabel.TabIndex = 36;
            this.BarsaAddressLabel.Text = "Barsa Address";
            // 
            // BarsaAddressTextBox
            // 
            this.BarsaAddressTextBox.AccessibleDescription = "DFGD";
            this.BarsaAddressTextBox.Location = new System.Drawing.Point(119, 95);
            this.BarsaAddressTextBox.Name = "BarsaAddressTextBox";
            this.BarsaAddressTextBox.Size = new System.Drawing.Size(100, 21);
            this.BarsaAddressTextBox.TabIndex = 35;
            // 
            // ShowPassBarsaButton
            // 
            this.ShowPassBarsaButton.BackgroundImage = global::SIPWindowsAgent.Properties.Resources.hide;
            this.ShowPassBarsaButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ShowPassBarsaButton.Location = new System.Drawing.Point(225, 62);
            this.ShowPassBarsaButton.Name = "ShowPassBarsaButton";
            this.ShowPassBarsaButton.Size = new System.Drawing.Size(47, 21);
            this.ShowPassBarsaButton.TabIndex = 37;
            this.ShowPassBarsaButton.Text = "Show Pass";
            this.ShowPassBarsaButton.UseVisualStyleBackColor = true;
            this.ShowPassBarsaButton.Click += new System.EventHandler(this.ShowPass_Click);
            // 
            // ShowPassSip
            // 
            this.ShowPassSip.BackgroundImage = global::SIPWindowsAgent.Properties.Resources.hide;
            this.ShowPassSip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ShowPassSip.Location = new System.Drawing.Point(225, 59);
            this.ShowPassSip.Name = "ShowPassSip";
            this.ShowPassSip.Size = new System.Drawing.Size(47, 21);
            this.ShowPassSip.TabIndex = 38;
            this.ShowPassSip.Text = "Show Pass";
            this.ShowPassSip.UseVisualStyleBackColor = true;
            this.ShowPassSip.Click += new System.EventHandler(this.ShowPassSip_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtUsername);
            this.groupBox1.Controls.Add(this.ShowPassSip);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.SIPServerAddressTextBox);
            this.groupBox1.Controls.Add(this.SIPServerPortLabel);
            this.groupBox1.Controls.Add(this.SIPServerLable);
            this.groupBox1.Controls.Add(this.SIPServerPortTextBox);
            this.groupBox1.Location = new System.Drawing.Point(24, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 163);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SIP Account";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BarsaUsernamelabel);
            this.groupBox2.Controls.Add(this.BarcaUsername);
            this.groupBox2.Controls.Add(this.ShowPassBarsaButton);
            this.groupBox2.Controls.Add(this.BarcaPass);
            this.groupBox2.Controls.Add(this.BarsaAddressLabel);
            this.groupBox2.Controls.Add(this.BarsaPasslabel);
            this.groupBox2.Controls.Add(this.BarsaAddressTextBox);
            this.groupBox2.Location = new System.Drawing.Point(24, 259);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 144);
            this.groupBox2.TabIndex = 40;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Barsa Account";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.CouplePhoneTextBox);
            this.groupBox3.Controls.Add(this.TransferphoneCheckBox);
            this.groupBox3.Location = new System.Drawing.Point(24, 181);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(314, 72);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 459);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnRegister);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SettingsForm";
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

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
        public System.Windows.Forms.TextBox CouplePhoneTextBox;
        private System.Windows.Forms.CheckBox TransferphoneCheckBox;
        private System.Windows.Forms.Label SIPServerLable;
        private System.Windows.Forms.TextBox SIPServerAddressTextBox;
        private System.Windows.Forms.Label SIPServerPortLabel;
        private System.Windows.Forms.TextBox SIPServerPortTextBox;
        private System.Windows.Forms.Label BarsaAddressLabel;
        private System.Windows.Forms.TextBox BarsaAddressTextBox;
        private System.Windows.Forms.Button ShowPassBarsaButton;
        private System.Windows.Forms.Button ShowPassSip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}