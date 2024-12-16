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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IncomingCallForm));
            this.btnAnswer = new System.Windows.Forms.Button();
            this.btnDrop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.callStateLable = new System.Windows.Forms.Label();
            this.ctlCallInfoList = new SIPWindowsAgent.CallInfoListControl();
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
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // callStateLable
            // 
            resources.ApplyResources(this.callStateLable, "callStateLable");
            this.callStateLable.Name = "callStateLable";
            // 
            // ctlCallInfoList
            // 
            resources.ApplyResources(this.ctlCallInfoList, "ctlCallInfoList");
            this.ctlCallInfoList.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctlCallInfoList.Name = "ctlCallInfoList";
            // 
            // IncomingCallForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(225)))), ((int)(((byte)(200)))));
            this.Controls.Add(this.callStateLable);
            this.Controls.Add(this.ctlCallInfoList);
            this.Controls.Add(this.btnAnswer);
            this.Controls.Add(this.btnDrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "IncomingCallForm";
            this.Opacity = 0.9D;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IncomingCallForm_FormClosed);
            this.Load += new System.EventHandler(this.IncomingCallForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button btnAnswer;
        public System.Windows.Forms.Button btnDrop;
        public CallInfoListControl ctlCallInfoList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label callStateLable;
    }
}