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
using Ozeki.Common;
using Ozeki;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Net.Http;
using System.Collections.Generic;

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
        AppConfig config;

        private static MainForm singleFormInstance;
        public static MainForm SingleFormInstance { get { return singleFormInstance; } }

        public string userToken { get; internal set; }

        private static int instanceCount = 0;
        public bool TransferPhoneActive;

        public string CouplePhone;
        public string BarsaUser;
        public string BarsaPass;
        public bool IsTransferEnabled;
        internal string BarsaAddress;
        List<CallerData> callerData;
        SettingsForm settingsForm;
        private Size previousFormSize;
        ApiServiceHelper apiServiceHelper;
        SettingsManager settingsManager;

        private OutgoingCallForm OutGoingCall;


        string selectedSipAccount;

        public MainForm()
        {
            InitializeComponent();
            apiServiceHelper = new ApiServiceHelper();
            sipService = new SIPService(this, apiServiceHelper);
            singleFormInstance = this;
            settingsManager = new SettingsManager();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterAccount();
            if (!nancyStarted)
            {
                StartNancyApi();
                nancyStarted = true;
            }
            LoadingSipAccountsInListBox();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }
        internal void LoadingSipAccountsInListBox()
        {
            lstBarcaUsernames.Items.Clear();
            //SettingsManager settingsManager = new SettingsManager();
            config = settingsManager.LoadSettings();

            if (config != null)
            {
                foreach (var sipAccount in config.SipSettings)
                {
                    if (sipAccount.Value.RegistrableOnClient)
                    {
                        {
                            string sipAccountNumeber = sipAccount.Key;
                            if (sipAccount.Value.IsItDefault)
                                sipAccountNumeber += "(Default)";
                            lstBarcaUsernames.Items.Add(sipAccountNumeber);
                        }
                    }
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
            string selectedUsername = lstBarcaUsernames.SelectedItem.ToString();
            selectedSipAccount = selectedUsername.EndsWith("(Default)") ? selectedUsername.Substring(0, selectedUsername.Length - "(Default)".Length).Trim() : selectedUsername;
            sipService.phoneLine = sipService.phonLines.Find(item => item.SIPAccount.UserName == selectedSipAccount);
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
            config = settingsManager.LoadSettings();
            CouplePhone = config.CouplePhone;
            IsTransferEnabled = config.IsTransferEnabled;
            BarsaAddress = config.BarsaAddress;
            userToken = config.UserToken;
            foreach (var kvp in config.SipSettings)
            {
                string sipAccount = kvp.Key;
                SipSettings sipSettings = kvp.Value;

                if (int.TryParse(sipSettings.DomainPort, out int domainPort))
                {
                    if (sipSettings.RegistrableOnClient)
                    {
                        sipService.RegisterAccount(
                            sipSettings.UserName,
                            sipSettings.DisplayName,
                            sipSettings.AuthenticationId,
                            sipSettings.RegisterPassword,
                            sipSettings.DomainHost,
                            domainPort 
                        );
                    }
                }
            }


            //foreach (var userSettings in appConfig?.SipSettings?.Values)
            //{
            //    var userName = Properties.Settings.Default.Username;
            //}
            //var userName = Properties.Settings.Default.Username;
            //var displayName = Properties.Settings.Default.Username;
            //var authenticationId = Properties.Settings.Default.Username;
            //var registerPassword = Properties.Settings.Default.Password;
            //var domainHost = Properties.Settings.Default.SIPServerAddressTextBox;
            //var domainPort = Properties.Settings.Default.SIPServerPortTextBox;
            //if (userName != "" && registerPassword != "" && domainHost != "" && domainPort != 0)
            //{
            //    UpdateLog("Registering ... ");
            //    sipService.RegisterAccount(userName, displayName, authenticationId, registerPassword, domainHost, domainPort);
            //}

        }

        public void CreatCallFromApi(string Number)
        {
            string targetNumber;

            if (txtCallNumber.InvokeRequired)
            {
                this.Invoke(new Action(() => CreatCallFromApi(Number)));
            }
            else
            {
                if (Number.StartsWith("$"))
                {
                    targetNumber = Number.Substring(1);
                    txtCallNumber.Text = targetNumber;
                    //ShowingOutGoingCallForm(targetNumber, sipService, userToken);
                }
                else
                {
                    txtCallNumber.Text = Number;
                    string accountPrifix = "";
                    if (config.SipSettings.ContainsKey(selectedSipAccount))
                    {
                        accountPrifix = config.SipSettings[selectedSipAccount].OutGoingCallPrefix;
                    }
                    targetNumber = accountPrifix + Number;

                    txtCallNumber.Text = targetNumber;
                    //ShowingOutGoingCallForm(Number, sipService, userToken);

                }
                btnCall.PerformClick();
            }
        }
        public void ShowingOutGoingCallForm(string number, SIPService sipService, string token)
        {
            var payload = new
            {
                CallerId = number
            };
            callerData = apiServiceHelper.MakeApiCall<List<CallerData>>(BarsaAddress, "GetCallerInfo", payload, userToken).Result;
            OutGoingCall = new OutgoingCallForm(number, callerData, sipService, token);
            SIPService.ShowNotifyWindow(OutGoingCall);
        }

        public async void CreateCall(object sender, EventArgs e)
        {
            string phoneNumber = txtCallNumber.Text;
            string acctualPhoneNumber=phoneNumber;
            if (phoneNumber.StartsWith("9"))
            {
                 acctualPhoneNumber = phoneNumber.Substring(1);
            }
            ShowingOutGoingCallForm(acctualPhoneNumber, sipService, userToken);


            if (string.IsNullOrEmpty(phoneNumber))
            {
                Console.WriteLine("Phone number cannot be null or empty.");
                MessageBox.Show("Phone number cannot be null or empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                string pattern = @"^\d+$";
                if (Regex.IsMatch(phoneNumber, pattern))
                {
                    sipService.CreateCall(phoneNumber);
                }
                else
                {
                    Console.WriteLine("Invalid phone number format.");
                    MessageBox.Show("Invalid phone number format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

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
        internal void CallCompleted()
        {
            if (OutGoingCall != null && OutGoingCall.IsHandleCreated)
            {
                // Check if the form's handle has been created
                OutGoingCall.Invoke(new Action(() =>
                {
                    OutGoingCall.btnDropCall.Enabled = false;
                }));
            }
        }


        public void UpdateLog(string message)
        {
            if (!IsDisposed && txtLog != null && !txtLog.IsDisposed)
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
            SettingsForm settingsForm = new SettingsForm(sipService, this, false, apiServiceHelper);
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

            settingsForm.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        public void getRightSettingForIncominCall(string number)
        {
            settingsForm = new SettingsForm(sipService, this, true, apiServiceHelper);
            settingsForm.getRightSettingForIncominCall(number);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 8;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            txtCallNumber.Text += 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 3;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 4;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 5;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 6;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 7;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 9;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            txtCallNumber.Text += 0;
        }
    }
}

