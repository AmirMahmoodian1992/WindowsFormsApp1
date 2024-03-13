namespace SIPWindowsAgent
{
    partial class IncomingCallForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncomingCallForm));
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnDrop = new System.Windows.Forms.Button();
            this.ctlCallInfo = new SIPWindowsAgent.CallInfoControl();
            this.SuspendLayout();
            // 
            // btnAnswer
            // 
            resources.ApplyResources(this.btnAnswer, "btnAnswer");
            this.btnAnswer.Name = "btnAnswer";
            this.btnAnswer.UseVisualStyleBackColor = true;
            this.btnAnswer.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // btnDrop
            // 
            resources.ApplyResources(this.btnDrop, "btnDrop");
            this.btnDrop.Name = "btnDrop";
            this.btnDrop.UseVisualStyleBackColor = true;
            this.btnDrop.Click += new System.EventHandler(this.button3_Click);
            // 
            // ctlCallInfo
            // 
            this.ctlCallInfo.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            resources.ApplyResources(this.ctlCallInfo, "ctlCallInfo");
            this.ctlCallInfo.Name = "ctlCallInfo";
            // 
            // IncomingCallForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.ctlCallInfo);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.btnDrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "IncomingCallForm";
            this.Opacity = 0.9D;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.IncomingCallForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button btnAnswer;
        public System.Windows.Forms.Button btnDrop;
        public CallInfoControl ctlCallInfo;
    }
}