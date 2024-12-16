namespace SIPWindowsAgent
{
    partial class OutgoingCallForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutgoingCallForm));
            this.button2 = new System.Windows.Forms.Button();
            this.btnDropCall = new System.Windows.Forms.Button();
            this.ctlCallInfoList = new SIPWindowsAgent.CallInfoListControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.callStateLable = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(5, 250);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "بستن";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnDropCall
            // 
            this.btnDropCall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDropCall.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDropCall.Location = new System.Drawing.Point(116, 250);
            this.btnDropCall.Name = "btnDropCall";
            this.btnDropCall.Size = new System.Drawing.Size(105, 23);
            this.btnDropCall.TabIndex = 37;
            this.btnDropCall.Text = "قطع تماس";
            this.btnDropCall.UseVisualStyleBackColor = true;
            this.btnDropCall.Click += new System.EventHandler(this.button3_Click);
            // 
            // ctlCallInfoList
            // 
            this.ctlCallInfoList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlCallInfoList.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctlCallInfoList.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ctlCallInfoList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlCallInfoList.Location = new System.Drawing.Point(5, 5);
            this.ctlCallInfoList.Name = "ctlCallInfoList";
            this.ctlCallInfoList.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctlCallInfoList.Size = new System.Drawing.Size(388, 238);
            this.ctlCallInfoList.TabIndex = 39;
            this.ctlCallInfoList.Load += new System.EventHandler(this.ctlCallInfoList_Load);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // callStateLable
            // 
            this.callStateLable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.callStateLable.AutoSize = true;
            this.callStateLable.Location = new System.Drawing.Point(238, 255);
            this.callStateLable.Name = "callStateLable";
            this.callStateLable.Size = new System.Drawing.Size(53, 13);
            this.callStateLable.TabIndex = 40;
            this.callStateLable.Text = "Call State";
            // 
            // OutgoingCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(398, 277);
            this.Controls.Add(this.callStateLable);
            this.Controls.Add(this.ctlCallInfoList);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnDropCall);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "OutgoingCallForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Text = "تماس خروجی ..";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OutGoingCallForm_FormClosed);
            this.Load += new System.EventHandler(this.OutGoingCallForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button btnDropCall;
        public CallInfoListControl ctlCallInfoList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label callStateLable;
    }
}