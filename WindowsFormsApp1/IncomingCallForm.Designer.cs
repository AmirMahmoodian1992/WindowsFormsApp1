namespace WindowsFormsApp1
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
            this.txtLog = new System.Windows.Forms.TextBox();
            this.BarcaCallerPicture = new System.Windows.Forms.PictureBox();
            this.texllabel = new System.Windows.Forms.Label();
            this.TitleBarcaCaller = new System.Windows.Forms.TextBox();
            this.declabel = new System.Windows.Forms.Label();
            this.DescriptionBarcaCaller = new System.Windows.Forms.TextBox();
            this.BarcaWebRedirect = new System.Windows.Forms.Button();
            this.BarcaCallerFormID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BarcaCallerPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            resources.ApplyResources(this.txtLog, "txtLog");
            this.txtLog.Name = "txtLog";
            // 
            // BarcaCallerPicture
            // 
            resources.ApplyResources(this.BarcaCallerPicture, "BarcaCallerPicture");
            this.BarcaCallerPicture.Name = "BarcaCallerPicture";
            this.BarcaCallerPicture.TabStop = false;
            this.BarcaCallerPicture.Tag = "Picture";
            // 
            // texllabel
            // 
            resources.ApplyResources(this.texllabel, "texllabel");
            this.texllabel.Name = "texllabel";
            // 
            // TitleBarcaCaller
            // 
            resources.ApplyResources(this.TitleBarcaCaller, "TitleBarcaCaller");
            this.TitleBarcaCaller.Name = "TitleBarcaCaller";
            // 
            // declabel
            // 
            resources.ApplyResources(this.declabel, "declabel");
            this.declabel.Name = "declabel";
            // 
            // DescriptionBarcaCaller
            // 
            resources.ApplyResources(this.DescriptionBarcaCaller, "DescriptionBarcaCaller");
            this.DescriptionBarcaCaller.Name = "DescriptionBarcaCaller";
            // 
            // BarcaWebRedirect
            // 
            this.BarcaWebRedirect.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.BarcaWebRedirect, "BarcaWebRedirect");
            this.BarcaWebRedirect.Name = "BarcaWebRedirect";
            this.BarcaWebRedirect.UseVisualStyleBackColor = false;
            this.BarcaWebRedirect.Click += new System.EventHandler(this.button2_Click);
            // 
            // BarcaCallerFormID
            // 
            resources.ApplyResources(this.BarcaCallerFormID, "BarcaCallerFormID");
            this.BarcaCallerFormID.Name = "BarcaCallerFormID";
            this.BarcaCallerFormID.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Name = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // IncomingCallForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BarcaCallerFormID);
            this.Controls.Add(this.BarcaWebRedirect);
            this.Controls.Add(this.DescriptionBarcaCaller);
            this.Controls.Add(this.declabel);
            this.Controls.Add(this.TitleBarcaCaller);
            this.Controls.Add(this.texllabel);
            this.Controls.Add(this.BarcaCallerPicture);
            this.Controls.Add(this.txtLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "IncomingCallForm";
            this.Opacity = 0.9D;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.IncomingCallForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BarcaCallerPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.PictureBox BarcaCallerPicture;
        private System.Windows.Forms.Label texllabel;
        private System.Windows.Forms.TextBox TitleBarcaCaller;
        private System.Windows.Forms.Label declabel;
        private System.Windows.Forms.TextBox DescriptionBarcaCaller;
        private System.Windows.Forms.Button BarcaWebRedirect;
        private System.Windows.Forms.TextBox BarcaCallerFormID;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button button2;
        public System.Windows.Forms.Button button3;
    }
}