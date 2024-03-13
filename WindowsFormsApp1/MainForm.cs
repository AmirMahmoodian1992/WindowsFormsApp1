using System;
using System.Windows.Forms;
using System.Xml;
using Ozeki.Media;
using Ozeki.VoIP;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using sipservice;
using Nancy.Hosting.Self;
using utils;
using System.Configuration;
using Ozeki.Network;
using System.Threading.Tasks;

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
        internal string BarsaAddress;
        CallerData callerData;

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

            SettingsManager settingsManager = new SettingsManager();
            AppConfig config = settingsManager.LoadSettings();

            // Populate the ListBox with BarsaUsernames
            //foreach (SipSettings property in config.SipSettings[username])
            //{
            //    if (property.Name.StartsWith("BarcaUsername_"))
            //    {
            //        string username = property.Name.Replace("BarcaUsername_", "");
            //        lstBarcaUsernames.Items.Add(username);
            //    }
            //}
            if (config!= null)
            {
                foreach (var userEntry in config.SipSettings)
                {
                    string usernameBarsa = userEntry.Key;

                    // Assuming BarcaUsername is a property of UserSettings
                    string barcaUsername = userEntry.Value.BarsaUserName;

                    lstBarcaUsernames.Items.Add(barcaUsername);
                }
            }

            // Add an event handler for the SelectedIndexChanged event
            lstBarcaUsernames.SelectedIndexChanged += LstBarcaUsernames_SelectedIndexChanged;

            // If there are items in the ListBox, select the first one to trigger the event
            if (lstBarcaUsernames.Items.Count > 0)
            {
                lstBarcaUsernames.SelectedIndex = 0;
            }
        }

        private void LstBarcaUsernames_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update the form with the selected BarsaUsername's settings
            string selectedUsername = lstBarcaUsernames.SelectedItem.ToString();

        }


        private void InitializeSettingParameters()
        {
            Properties.Settings.Default.Reload();
            CouplePhone = Properties.Settings.Default.CouplePhone;
            IsTransferEnabled = Properties.Settings.Default.TransferphoneCheckBox;
            BarsaUser = Properties.Settings.Default.BarcaUsername;
            BarsaPass = Properties.Settings.Default.BarcaPass;
            BarsaAddress = Properties.Settings.Default?.BarsaAddress;
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

        public async void CreatCallFromApi(string Number)
        {

            if (txtCallNumber.InvokeRequired)
            {
                this.Invoke(new Action(() => CreatCallFromApi(Number)));
            }
            else
            {
                string Token= await sipService.CallTokenAPI();
                callerData= await sipService.CallGetCallerInfoApi(Token, Number);
                txtCallNumber.Text = callerData.Number;
                btnCall.PerformClick();
            }
        }

        public void CreateCall(object sender, EventArgs e)
        {
            sipService.CreateCall(txtCallNumber.Text, callerData);
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

        //internal void Incomingcall(string callerIDAsCaller)
        //{
        //    incominNumber.Text = callerIDAsCaller;
        //}
        //public void UpdateIncomingNumber(string number)
        //{
        //    if (incominNumber.InvokeRequired)
        //    {
        //        incominNumber.Invoke(new Action(() => UpdateIncomingNumber(number)));
        //    }
        //    else
        //    {
        //        incominNumber.Text = number;
        //    }
        //}
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
            sipService.StartOutgoingCalls(CouplePhone);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BarcaPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void SettingButton_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(sipService, this, false);
            settingsForm.ShowDialog();
        }

        private void MuteButton_Click(object sender, EventArgs e)
        {
            sipService.PutCallOnHold();
        }

        private void UnmuteButton_Click(object sender, EventArgs e)
        {
            sipService.UnHoldCall();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(sipService, this, true);
            settingsForm.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}

