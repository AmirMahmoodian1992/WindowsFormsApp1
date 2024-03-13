namespace SIPWindowsAgent
{
    partial class OutGoingCallForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutGoingCallForm));
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.BarcaCallerFormID = new System.Windows.Forms.TextBox();
            this.DescriptionBarcaCaller = new System.Windows.Forms.TextBox();
            this.declabel = new System.Windows.Forms.Label();
            this.TitleBarcaCaller = new System.Windows.Forms.TextBox();
            this.texllabel = new System.Windows.Forms.Label();
            this.BarcaCallerPicture = new System.Windows.Forms.PictureBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.ctlCallInfo = new SIPWindowsAgent.CallInfoControl();
            ((System.ComponentModel.ISupportInitialize)(this.BarcaCallerPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button2.Location = new System.Drawing.Point(208, 260);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 23);
            this.button2.TabIndex = 38;
            this.button2.Text = "Call";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button3.Location = new System.Drawing.Point(324, 260);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 23);
            this.button3.TabIndex = 37;
            this.button3.Text = "Dorp and Close";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(138, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 20);
            this.label1.TabIndex = 36;
            this.label1.Text = "New Out Going Call ...";
            // 
            // BarcaCallerFormID
            // 
            this.BarcaCallerFormID.Location = new System.Drawing.Point(12, 180);
            this.BarcaCallerFormID.Name = "BarcaCallerFormID";
            this.BarcaCallerFormID.Size = new System.Drawing.Size(100, 20);
            this.BarcaCallerFormID.TabIndex = 35;
            this.BarcaCallerFormID.Visible = false;
            // 
            // DescriptionBarcaCaller
            // 
            this.DescriptionBarcaCaller.Location = new System.Drawing.Point(172, 123);
            this.DescriptionBarcaCaller.Multiline = true;
            this.DescriptionBarcaCaller.Name = "DescriptionBarcaCaller";
            this.DescriptionBarcaCaller.Size = new System.Drawing.Size(257, 77);
            this.DescriptionBarcaCaller.TabIndex = 33;
            // 
            // declabel
            // 
            this.declabel.AutoSize = true;
            this.declabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.declabel.Location = new System.Drawing.Point(139, 98);
            this.declabel.Name = "declabel";
            this.declabel.Size = new System.Drawing.Size(60, 13);
            this.declabel.TabIndex = 32;
            this.declabel.Text = "Description";
            // 
            // TitleBarcaCaller
            // 
            this.TitleBarcaCaller.Location = new System.Drawing.Point(172, 53);
            this.TitleBarcaCaller.Name = "TitleBarcaCaller";
            this.TitleBarcaCaller.Size = new System.Drawing.Size(257, 20);
            this.TitleBarcaCaller.TabIndex = 31;
            // 
            // texllabel
            // 
            this.texllabel.AutoSize = true;
            this.texllabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.texllabel.Location = new System.Drawing.Point(139, 56);
            this.texllabel.Name = "texllabel";
            this.texllabel.Size = new System.Drawing.Size(27, 13);
            this.texllabel.TabIndex = 30;
            this.texllabel.Text = "Title";
            // 
            // BarcaCallerPicture
            // 
            this.BarcaCallerPicture.AccessibleName = "CallerPicture";
            this.BarcaCallerPicture.Image = ((System.Drawing.Image)(resources.GetObject("BarcaCallerPicture.Image")));
            this.BarcaCallerPicture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.BarcaCallerPicture.Location = new System.Drawing.Point(12, 53);
            this.BarcaCallerPicture.Name = "BarcaCallerPicture";
            this.BarcaCallerPicture.Size = new System.Drawing.Size(100, 115);
            this.BarcaCallerPicture.TabIndex = 29;
            this.BarcaCallerPicture.TabStop = false;
            this.BarcaCallerPicture.Tag = "Picture";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 206);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(417, 37);
            this.txtLog.TabIndex = 28;
            // 
            // ctlCallInfo
            // 
            this.ctlCallInfo.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctlCallInfo.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ctlCallInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctlCallInfo.Location = new System.Drawing.Point(12, 12);
            this.ctlCallInfo.Name = "ctlCallInfo";
            this.ctlCallInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ctlCallInfo.Size = new System.Drawing.Size(417, 234);
            this.ctlCallInfo.TabIndex = 39;
            // 
            // OutGoingCallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 297);
            this.Controls.Add(this.ctlCallInfo);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BarcaCallerFormID);
            this.Controls.Add(this.DescriptionBarcaCaller);
            this.Controls.Add(this.declabel);
            this.Controls.Add(this.TitleBarcaCaller);
            this.Controls.Add(this.texllabel);
            this.Controls.Add(this.BarcaCallerPicture);
            this.Controls.Add(this.txtLog);
            this.Name = "OutGoingCallForm";
            this.Text = "OutGoingCallForm";
            this.Load += new System.EventHandler(this.OutGoingCallForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BarcaCallerPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BarcaCallerFormID;
        private System.Windows.Forms.TextBox DescriptionBarcaCaller;
        private System.Windows.Forms.Label declabel;
        private System.Windows.Forms.TextBox TitleBarcaCaller;
        private System.Windows.Forms.Label texllabel;
        private System.Windows.Forms.PictureBox BarcaCallerPicture;
        private System.Windows.Forms.TextBox txtLog;
        public CallInfoControl ctlCallInfo;
    }
}