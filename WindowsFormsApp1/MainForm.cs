using System;
using System.Windows.Forms;
using System.Xml;
using Ozeki.Media;
using Ozeki.VoIP;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using sipservice;
using Nancy.Hosting.Self;
using utils;

namespace SIPWindowsAgent
{
    public partial class MainForm : Form
    {
        private SIPService sipService;
        private Timer callTimer;
        private int callDurationSeconds;
        private NancyHost _nancyHost;
        private static bool nancyStarted = false;
        ExampleApiModule exampleApiModule;

        private static MainForm singleFormInstance;
        public static MainForm SingleFormInstance { get { return singleFormInstance; } }


        private static int instanceCount = 0;
        public bool TransferPhoneActive;

        public string CouplePhone;
        public string BarsaUser;
        public string BarsaPass;
        public bool IsTransferEnabled;

        public MainForm()
        {
            InitializeComponent();
            sipService = new SIPService(this);
            singleFormInstance = this;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeSettingParameters();
            RegisterAccount();
            if (!nancyStarted)
            {
                StartNancyApi();
                nancyStarted = true;
                //SynchronizationManager.Instance.SetFormShown();

            }
        }

        private void InitializeSettingParameters()
        {
            Properties.Settings.Default.Reload();
            CouplePhone = Properties.Settings.Default.CouplePhone;
            IsTransferEnabled = Properties.Settings.Default.TransferphoneCheckBox;
            BarsaUser = Properties.Settings.Default.BarcaUsername;
            BarsaPass = Properties.Settings.Default.BarcaPass;
        }

        private void StartNancyApi()
        {
            var port = 5656;
            var uri = new Uri($"http://localhost:{port}");
            _nancyHost = new NancyHost(uri);
            _nancyHost.Start();
            // TODO: Handle Error
            //MessageBox.Show($"Nancy is listening on: {uri}");
        }

        private void RegisterAccount()
        {
            var userName = Properties.Settings.Default.Username;
            var displayName = Properties.Settings.Default.Username;
            var authenticationId = Properties.Settings.Default.Username;
            var registerPassword = Properties.Settings.Default.Password;
            var domainHost = Properties.Settings.Default.SIPServerAddressTextBox;
            var domainPort = Properties.Settings.Default.SIPServerPortTextBox;
            if (userName != "" && registerPassword != "" && domainHost != "" && domainPort != 0)
            {
                UpdateLog("Registering ... ");
                sipService.RegisterAccount(userName, displayName, authenticationId, registerPassword, domainHost, domainPort);
            }

        }

        public void CreatCallFromApi(string Number)
        {

            if (txtCallNumber.InvokeRequired || incominNumber.InvokeRequired)
            {
                this.Invoke(new Action(() => CreatCallFromApi(Number)));
            }
            else
            {
                txtCallNumber.Text = Number;
                btnCall.PerformClick();
            }
        }

        public void CreateCall(object sender, EventArgs e)
        {
            sipService.CreateCall(txtCallNumber.Text);
        }
        private void DropCall(object sender, EventArgs e)
        {
            sipService.DropCall();
        }

        private void AnswerCall(object sender, EventArgs e)
        {
            sipService.AnswerCall();
        }

        private void RejectCall(object sender, EventArgs e)
        {
            sipService.RejectCall();

        }

        public void UpdateLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => UpdateLog(message)));
            }
            else
            {
                txtLog.AppendText(message + Environment.NewLine);
            }
        }

        internal void Incomingcall(string callerIDAsCaller)
        {
            incominNumber.Text = callerIDAsCaller;
        }
        public void UpdateIncomingNumber(string number)
        {
            if (incominNumber.InvokeRequired)
            {
                incominNumber.Invoke(new Action(() => UpdateIncomingNumber(number)));
            }
            else
            {
                incominNumber.Text = number;
            }
        }
        private void recivedCallTime_Click(object sender, EventArgs e)
        {

        }

        public void UpdateTextBoxText(Label textBox, string newText)
        {
            lock (textBox)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.Invoke(new Action(() => textBox.Text = newText));
                }
                else
                {
                    textBox.Text = newText;
                }
            }
        }

        private void txtCallNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            sipService.StartOutgoingCalls(couplePhone.Text);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BarcaPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(sipService, this);
            settingsForm.ShowDialog();
        }
    }
}

