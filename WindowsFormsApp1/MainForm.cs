using System;
using System.Windows.Forms;
using System.Xml;
using Ozeki.Media;
using Ozeki.VoIP;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using sipservice;
using Nancy.Hosting.Self;

namespace WindowsFormsApp1
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
        public MainForm()
        {
            InitializeComponent();
            sipService = new SIPService(this);
            singleFormInstance = this;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            if (!nancyStarted)
            {
                StartNancyApi();
                nancyStarted = true;
                //SynchronizationManager.Instance.SetFormShown();

            }
        }
        private void StartNancyApi()
        {
            var port = 5656;
            var uri = new Uri($"http://localhost:{port}");
            _nancyHost = new NancyHost(uri);
            _nancyHost.Start();
            MessageBox.Show($"Nancy is listening on: {uri}");
        }

        private void RegisterAccount(object sender, EventArgs e)
        {
            var userName = txtUsername.Text;
            var displayName = txtUsername.Text;
            var authenticationId = txtUsername.Text;
            var registerPassword = txtPassword.Text;
            var domainHost = "192.168.0.101";
            var domainPort = 5080;
            
            sipService.RegisterAccount(userName, displayName, authenticationId, registerPassword, domainHost, domainPort);
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

