using Ozeki.VoIP;
using sipservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPWindowsAgent
{
    public partial class OutgoingCallForm : Form
    {
        private string targetNumber;
        private SIPService sIPService;

        //public OutGoingCallForm()
        //{
        //    InitializeComponent();
        //}

        public OutgoingCallForm(string targetNumber, List<CallerData> callerInfo, SIPService sIPService, string userToken)
        {
            InitializeComponent();
            this.targetNumber = targetNumber;
            this.sIPService = sIPService;
            try
            {
                if (targetNumber != null)
                {
                    //txtLog.AppendText($"Out Going Call To: {targetNumber}" + Environment.NewLine);
                    //TitleBarcaCaller.Text = targetNumber;
                    if (callerInfo != null)
                    {
                        var height = this.Height - ctlCallInfoList.Height;
                        ctlCallInfoList.ShowData(callerInfo, targetNumber, false, userToken, async (s, s1, s2, s3) => await sIPService.CallRedirectAPI(s, s1, s2, s3, true));
                        this.Height = height + ctlCallInfoList.Height;
                        this.Refresh();
                        //txtLog.AppendText($"Incoming Call from: {callerInfo.Items[0].Label}" + Environment.NewLine);
                        //TitleBarcaCaller.Text = callerInfo.Items[0].Label != "Unknown" ? callerInfo.Items[0].Label : CallerID;
                        //BarcaCallerFormID.Text = callerInfo.Items[0].Id;
                        //foreach (CallerDataItem key in callerInfo.Items)
                        //{
                        //    DescriptionBarcaCaller.Text += key.Text + Environment.NewLine;
                        //}
                    }
                    //BarcaCallerFormID.Text = callerInfo.CallerMO[1];
                    //foreach (string key in callerInfo.CallerMO)
                    //{
                    //    DescriptionBarcaCaller.Text += key + Environment.NewLine;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Showing OutGoing Data : {ex}");
            }
        }

        private void OutGoingCallForm_Load(object sender, EventArgs e)
        {
            int screenWorkingAreaWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenWorkingAreaHeight = Screen.PrimaryScreen.WorkingArea.Height;

            this.Left = screenWorkingAreaWidth - this.Width;
            this.Top = screenWorkingAreaHeight - this.Height;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sIPService.RejectCall();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ctlCallInfoList_Load(object sender, EventArgs e)
        {

        }
    }
}
