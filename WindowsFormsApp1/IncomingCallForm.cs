using sipservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class IncomingCallForm : Form
    {
        public event EventHandler AcceptButtonClicked;
        public event EventHandler RejectButtonClicked;
        private SIPService sipService;

        public IncomingCallForm()
        {
            InitializeComponent();
        }
        public IncomingCallForm(string callerInfo)
        {

            InitializeComponent();
            // Set the caller information in the form
            txtLog.AppendText($"Incoming Call from: {callerInfo}" + Environment.NewLine);  
        }
        public IncomingCallForm(CallerResponse callerInfo, SIPService sipService)
        {

            InitializeComponent();
            this.sipService = sipService;
            // Set the caller information in the form
            txtLog.AppendText($"Incoming Call from: {callerInfo.CallerMO[3]}" + Environment.NewLine);
            TitleBarcaCaller.Text = callerInfo.CallerMO[2];
            BarcaCallerFormID.Text = callerInfo.CallerMO[1];
            foreach (string key in callerInfo.CallerMO)
            {
                DescriptionBarcaCaller.Text += key + Environment.NewLine;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        public void UpdateLabelText(string newText)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => txtLog.AppendText(newText + Environment.NewLine)));
            }
            else
            {
                //if (txtLog != null)
                //{
                //    txtLog.AppendText(newText + Environment.NewLine);
                //}
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void DescriptionBarcaCaller_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            sipService.CallRedirectAPI(BarcaCallerFormID.Text);
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void IncomingCallForm_Load(object sender, EventArgs e)
        {
            int screenWorkingAreaWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenWorkingAreaHeight = Screen.PrimaryScreen.WorkingArea.Height;

            this.Left = screenWorkingAreaWidth - this.Width;
            this.Top = screenWorkingAreaHeight - this.Height;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            sipService.AnswerCall();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sipService.RejectCall();
            Close();
        }
    }
}
