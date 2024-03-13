﻿namespace SIPWindowsAgent
{
    partial class CallInfoControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallInfoControl));
            this.txtType = new System.Windows.Forms.Label();
            this.ctlLayout = new System.Windows.Forms.TableLayoutPanel();
            this.linkMain = new System.Windows.Forms.LinkLabel();
            this.txtCallerNumber = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.ctlImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ctlImage)).BeginInit();
            this.SuspendLayout();
            // 
            // txtType
            // 
            resources.ApplyResources(this.txtType, "txtType");
            this.txtType.Name = "txtType";
            // 
            // ctlLayout
            // 
            resources.ApplyResources(this.ctlLayout, "ctlLayout");
            this.ctlLayout.Name = "ctlLayout";
            // 
            // linkMain
            // 
            resources.ApplyResources(this.linkMain, "linkMain");
            this.linkMain.Name = "linkMain";
            this.linkMain.TabStop = true;
            // 
            // txtCallerNumber
            // 
            resources.ApplyResources(this.txtCallerNumber, "txtCallerNumber");
            this.txtCallerNumber.ForeColor = System.Drawing.Color.Black;
            this.txtCallerNumber.Name = "txtCallerNumber";
            this.txtCallerNumber.Click += new System.EventHandler(this.txtCallerNumber_Click);
            // 
            // txtLog
            // 
            resources.ApplyResources(this.txtLog, "txtLog");
            this.txtLog.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            // 
            // ctlImage
            // 
            resources.ApplyResources(this.ctlImage, "ctlImage");
            this.ctlImage.Name = "ctlImage";
            this.ctlImage.TabStop = false;
            this.ctlImage.Tag = "Picture";
            // 
            // CallInfoControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.linkMain);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.ctlLayout);
            this.Controls.Add(this.txtCallerNumber);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.ctlImage);
            this.Name = "CallInfoControl";
            ((System.ComponentModel.ISupportInitialize)(this.ctlImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox ctlImage;
        private System.Windows.Forms.Label txtType;
        private System.Windows.Forms.TableLayoutPanel ctlLayout;
        private System.Windows.Forms.LinkLabel linkMain;
        private System.Windows.Forms.Label txtCallerNumber;
        public System.Windows.Forms.TextBox txtLog;
    }
}