using onvif.services;
using sipservice;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SIPWindowsAgent
{
    public partial class IncomingCallForm : Form
    {
        public event EventHandler AcceptButtonClicked;
        public event EventHandler RejectButtonClicked;
        private SIPService sipService;

        public void Open(string id)
        {

        }

        public IncomingCallForm()
        {
            InitializeComponent();
        }
        public IncomingCallForm(string callerInfo)
        {
            InitializeComponent();
            ctlCallInfo.AddLog($"Incoming Call from: {callerInfo}");
            // Set the caller information in the form
        }
        public IncomingCallForm(String CallerID, CallerData callerInfo, SIPService sipService)
        {

            InitializeComponent();
            this.sipService = sipService;
            // Set the caller information in the form
            try
            {
                if (callerInfo.Items[0] != null)
                {
                    ShowData(callerInfo, CallerID, true, (s) => sipService.CallRedirectAPI(s));

                    //txtLog.AppendText($"Incoming Call from: {callerInfo.Items[0].Label}" + Environment.NewLine);
                    //TitleBarcaCaller.Text = callerInfo.Items[0].Label != "Unknown" ? callerInfo.Items[0].Label : CallerID;
                    //BarcaCallerFormID.Text = callerInfo.Items[0].Id;
                    //foreach (CallerDataItem key in callerInfo.Items)
                    //{
                    //    DescriptionBarcaCaller.Text += key.Text + Environment.NewLine;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("cannot load user data!!!");
            }
        }

        public void ShowData(CallerData data, string callerNumber, bool isInput, Action<string> openMethod)
        {
            ctlCallInfo.ShowData(data, callerNumber, isInput, openMethod);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }
        public void UpdateLabelText(string newText)
        {
            ctlCallInfo.AddLog(newText);
        }



        private void IncomingCallForm_Load(object sender, EventArgs e)
        {
            int screenWorkingAreaWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenWorkingAreaHeight = Screen.PrimaryScreen.WorkingArea.Height;

            this.Left = screenWorkingAreaWidth - this.Width;
            this.Top = screenWorkingAreaHeight - this.Height;
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            sipService.AnswerCall();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sipService != null)
            {
                sipService.RejectCall();
            }
            Close();
        }
    }
}
