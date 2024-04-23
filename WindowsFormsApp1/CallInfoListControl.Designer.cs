using System.Windows.Forms;

namespace SIPWindowsAgent
{
    partial class CallInfoListControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CallInfoListControl));
            this.ctlLayout = new Panel();
            this.txtCallerNumber = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ctlLayout
            // 
            resources.ApplyResources(this.ctlLayout, "ctlLayout");
            this.ctlLayout.Name = "ctlLayout";
            // 
            // txtCallerNumber
            // 
            resources.ApplyResources(this.txtCallerNumber, "txtCallerNumber");
            this.txtCallerNumber.ForeColor = System.Drawing.Color.Black;
            this.txtCallerNumber.Name = "txtCallerNumber";
            // 
            // lblInfo
            // 
            resources.ApplyResources(this.lblInfo, "lblInfo");
            this.lblInfo.ForeColor = System.Drawing.Color.Black;
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblInfo_MouseClick);
            // 
            // CallInfoListControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtCallerNumber);
            this.Controls.Add(this.ctlLayout);
            this.Name = "CallInfoListControl";
            this.ResumeLayout(false);

        }
        
        #endregion
        private Panel ctlLayout;
        private System.Windows.Forms.Label txtCallerNumber;
        private System.Windows.Forms.Label lblInfo;
    }
}