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
    public partial class OutGoingCallForm : Form
    {
        private string targetNumber;
        private SIPService sIPService;

        public OutGoingCallForm()
        {
            InitializeComponent();
        }

        public OutGoingCallForm(string targetNumber, CallerData callerInfo, SIPService sIPService)
        {
            InitializeComponent();
            this.targetNumber = targetNumber;
            this.sIPService = sIPService;
            try
            {
                if (targetNumber != null)
                {
                    txtLog.AppendText($"Out Going Call To: {targetNumber}" + Environment.NewLine);
                    TitleBarcaCaller.Text = targetNumber;
                    if (callerInfo.Items[0] != null)
                    {
                        ctlCallInfo.ShowData(callerInfo, targetNumber, false, (s) => sIPService.CallRedirectAPI(s));

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
                MessageBox.Show("cannot load user data!!!");
            }
        }

        private void OutGoingCallForm_Load(object sender, EventArgs e)
        {
            int screenWorkingAreaWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenWorkingAreaHeight = Screen.PrimaryScreen.WorkingArea.Height;

            this.Left = screenWorkingAreaWidth - this.Width;
            this.Top = screenWorkingAreaHeight - this.Height;
        }

    }
}
