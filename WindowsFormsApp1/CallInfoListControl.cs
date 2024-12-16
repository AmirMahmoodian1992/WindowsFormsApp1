using onvif.services;
using sipservice;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static sipservice.SIPService;

namespace SIPWindowsAgent
{
    public partial class CallInfoListControl : UserControl
    {
        public CallInfoListControl()
        {
            InitializeComponent();
        }

        public List<CallerData> Data { get; private set; }
        public string CallerNumber { get; private set; }
        public string UserToken { get; private set; }
        public SIPService SipService { get; private set; }

        public void ShowData(List<CallerData> data, string callerNumber, bool isInput, string userToken, OpenMethodDelegate openMethod, SIPService sipService, Ozeki.VoIP.IPhoneCall call)
        {
            if (data == null)
            {
                //ShowError(true); // Show error label
                return;
            }
            this.Data = data;
            this.CallerNumber = callerNumber;
            this.UserToken = userToken;
            this.SipService = sipService;

            string text = "";
            if (isInput)
                text = "تماس ورودی " + " برای  " + call.PhoneLine.SIPAccount.UserName;
            else
                text = "تماس خروجی ";
            lblInfo.Text = text;
            txtCallerNumber.Text = callerNumber;
            foreach(var item in  data)
            {
                labelReceiverList.Text += " "+item.Receiver+" ";
            }
            

            var formDiffHeight = this.Height - ctlLayout.Height;
            var currentHeigth = 0;
            //ctlLayout.RowCount = data.Count;
            int i = 0;
            foreach (var callerData in data)
            {
                var control = new CallInfoControl();
                control.ShowData(callerData, callerNumber, userToken, openMethod);
                //control.MinimumSize = control.Size;
                //control.MaximumSize = control.Size;

                //ctlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, h));
                //ctlLayout.Controls.Add(control, 0, i);

                var h = control.Height;
                ctlLayout.Controls.Add(control);
                control.Bounds = new System.Drawing.Rectangle(0, currentHeigth, ctlLayout.Width, h);
                currentHeigth += h;

                ////if (i < ctlLayout.RowStyles.Count)
                ////{
                ////    ctlLayout.RowStyles[i] = new RowStyle(SizeType.Absolute, h);
                ////}
                ////ctlLayout.RowCount++;
                //ctlLayout.ColumnCount = 2;
                //ctlLayout.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 40F);
                //ctlLayout.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 60F);


                //var label1 = new Label() { Text = callerData.Label, Dock = DockStyle.Fill };
                //ctlLayout.Controls.Add(label1, 0, i);

                ////var label2 = new Label() { Text = callerData.Text, Dock = DockStyle.Fill };
                //var label2 = new LinkLabel()
                //{
                //    Text = callerData.Text,
                //    Dock = DockStyle.Fill,
                //    RightToLeft = RightToLeft.Yes // Set RightToLeft property to Yes
                //};
                //label2.Click += (s, e) => openMethod(callerNumber, callerData.Id, callerData.CustomScript, userToken);
                //ctlLayout.Controls.Add(label2, 1, ctlLayout.RowCount - 1);

                ////ctlLayout.SetRow(control, i);
                i++;
            }
            this.Height = formDiffHeight + currentHeigth;

            //ctlLayout.RowCount = 0;
            //var rowHeight = 30;
            //var height = this.Height;
            //height = height - ctlLayout.Height + data[0].Items.Count * rowHeight;
            //this.Height = height;

            //foreach (var callerData in data)
            //{
            //    height += callerData.Items.Count * rowHeight;
            //}
            //this.Height = height;

            //foreach (var callerData in data)
            //{
            //    foreach (var item in callerData.Items)
            //    {
            //        ctlLayout.RowCount++;
            //        ctlLayout.ColumnCount = 2;
            //        ctlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            //        ctlLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            //        ctlLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));

            //        var label1 = new Label() { Text = item.Label, Dock = DockStyle.Fill };
            //        ctlLayout.Controls.Add(label1, 0, ctlLayout.RowCount - 1);

            //        var label2 = new LinkLabel() { Text = item.Text, Dock = DockStyle.Fill };
            //        label2.Click += (s, e) => openMethod(callerNumber, item.Id, item.CustomScript, userToken);
            //        ctlLayout.Controls.Add(label2, 1, ctlLayout.RowCount - 1);
            //    }
            //}

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

        private void lblInfo_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Middle)
            //{
            //    if ((ParentForm is IncomingCallForm ic))
            //    {
            //        var x = new IncomingCallForm(this.CallerNumber, this.Data, this.SipService, this.UserToken);
            //        x.Show();
            //    }
            //    else if ((ParentForm is OutgoingCallForm oc))
            //    {
            //        var x = new OutgoingCallForm(this.CallerNumber, this.Data, this.SipService, this.UserToken);
            //        x.Show();
            //    }
            //    this.ParentForm.Close();
            //}
        }
    }
}