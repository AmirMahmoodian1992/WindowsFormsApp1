using onvif.services;
using sipservice;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static sipservice.SIPService;

namespace SIPWindowsAgent
{
    public partial class CallInfoControl : UserControl
    {
        public CallInfoControl()
        {
            InitializeComponent();
        }

        public static CallerData GetTestData()
        {
            var x = new CallerData
            {
                Label = "شرکت برسا نوین رای",
                Text = "مشتری",
                Id = "0",
                Items = new List<CallerDataItem>
                {
                    new CallerDataItem{Id="11111",Label="آخرین تیکت", Text="تیکت شماره ش/3343432"},
                    new CallerDataItem{Id="2343",Label="قرارداد", Text="قرارداد شماره ش/3343432"},
                    new CallerDataItem{Id="3242343",Label="آخرین تماس", Text="2 روز قبل"},
                }
            };
            return x;
        }

        public void ShowData(CallerData data, string callerNumber, string userToken, OpenMethodDelegate openMethod)
        {
            if (data == null)
            {
                //ShowError(true); // Show error label
                return;
            }
            //if (isInput)
            //    this.Text = "تماس ورودی ...";
            //else
            //    this.Text = "تماس خروجی ...";

            //txtCallerNumber.Text = callerNumber;
            txtType.Text = data.Label;
            linkMain.Text = data.Text;
            linkMain.Click += (s, e) => openMethod(callerNumber, data.Id, data.CustomScript, userToken);
            ctlLayout.RowCount = 0;
            var rowHeight = 30;
            var height = this.Height;
            height = height - ctlLayout.Height + data.Items.Count * rowHeight;
            this.Height = height + 0;

            //foreach (var callerData in data)
            //{
            //    height += callerData.Items.Count * rowHeight;
            //}


            foreach (var item in data.Items)
            {
                ctlLayout.RowCount++;
                ctlLayout.ColumnCount = 2;
                ctlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                ctlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
                ctlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));

                var label1 = new Label() { Text = item.Label, Dock = DockStyle.Fill };
                ctlLayout.Controls.Add(label1, 0, ctlLayout.RowCount - 1);

                var label2 = new LinkLabel() { Text = item.Text, Dock = DockStyle.Fill };
                label2.Click += (s, e) => openMethod(callerNumber, item.Id, item.CustomScript, userToken);
                ctlLayout.Controls.Add(label2, 1, ctlLayout.RowCount - 1);
            }

            //var rowHeight = 30;
            //var height = this.Height;
            //height = height - ctlLayout.Height + data.Items.Count * rowHeight;
            //this.Height = height;
            //for (int i = 0; i < data.Items.Count; i++)
            //{
            //    ctlLayout.RowCount++;
            //    var item = data.Items[i];
            //    ctlLayout.ColumnCount = 2;
            //    ctlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            //    ctlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            //    ctlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
            //    var lable1 = new Label() { Text = item.Label, Dock = DockStyle.Fill };
            //    ctlLayout.Controls.Add(lable1, 0, i);
            //    var lable2 = new LinkLabel() { Text = item.Text, Dock = DockStyle.Fill };
            //    lable2.Click += (s, e) => openMethod(callerNumber, item.Id,item.CustomScript, userToken);
            //    ctlLayout.Controls.Add(lable2, 1, i);
            //}
        }

        //public void AddLog0(string text)
        //{
        //    if (txtLog.IsDisposed || txtLog.Disposing)
        //        return;
        //    txtLog.AppendText(text + Environment.NewLine);
        //}

        //public void AddLog(string text)
        //{
        //    if (txtLog.InvokeRequired)
        //        txtLog.Invoke(new Action(() => AddLog0(text)));
        //    else
        //        AddLog0(text);
        //}

        private void txtCallerNumber_Click(object sender, EventArgs e)
        {

        }

        private void ctlLayout_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}