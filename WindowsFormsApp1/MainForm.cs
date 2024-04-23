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
        private OutgoingCallForm outgoingCallForm;
        string selectedSipAccount;
        public MainForm()
        {
            InitializeComponent();

            apiServiceHelper = new ApiServiceHelper();
            sipService = new SIPService(this, apiServiceHelper);
            singleFormInstance = this;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                RegisterAccount();
                LoadingSipAccountsInListBox();

                if (!nancyStarted)
                {
                    StartNancyApi();
                    nancyStarted = true;
                }
            }
            catch (Exception ex)
            {
                HandleInitializationError(ex);
            }
        }
        private void HandleInitializationError(Exception ex)
        {
            MessageBox.Show("An error occurred during form initialization: " + ex.Message);
        }
        internal void LoadingSipAccountsInListBox()
        {
            lstBarcaUsernames.Items.Clear();
            config = SettingsManager.Instance.LoadSettings();

            if (config != null && config.SipSettings != null)
            {
                foreach (var sipAccount in config.SipSettings)
                {
                    if (sipAccount.Value.RegistrableOnClient)
                    {
                        string sipAccountNumber = sipAccount.Key;
                        if (sipAccount.Value.IsItDefault)
                        {
                            sipAccountNumber += "(Default)";
                        }
                        lstBarcaUsernames.Items.Add(sipAccountNumber);
                    }
                }
            }
            //Add an event handler for the SelectedIndexChanged event
            lstBarcaUsernames.SelectedIndexChanged += LstBarcaUsernames_SelectedIndexChanged;

            // If there are items in the ListBox, select the first one to trigger the event
            if (lstBarcaUsernames.Items.Count > 0)
            {
                lstBarcaUsernames.SelectedIndex = 0;
            }
        }
        private void LstBarcaUsernames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBarcaUsernames.SelectedItem != null)
            {
                string selectedUsername = lstBarcaUsernames.SelectedItem.ToString();
                selectedSipAccount = selectedUsername.EndsWith("(Default)")
                    ? selectedUsername.Substring(0, selectedUsername.Length - "(Default)".Length).Trim()
                    : selectedUsername;

                // Find the phone line matching the selected SIP account
                sipService.phoneLine = sipService.phonLines.Find(item => item.SIPAccount.UserName == selectedSipAccount);
            }
            else
            {
                // Handle case where no item is selected
                sipService.phoneLine = null;
            }
        }

        private void StartNancyApi()
        {
            var port = 5656;
            var uri = new Uri($"http://localhost:{port}");

            try
            {
                _nancyHost = new NancyHost(uri);
                _nancyHost.Start();

            }
            catch (Exception ex)
            {
                string errorMessage = $"Failed to start Nancy API: {ex.Message}";
                Console.WriteLine(errorMessage);
                MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RegisterAccount()
        {
            try
            {
                config = SettingsManager.Instance.LoadSettings();

                if (config == null)
                {
                    MessageBox.Show("Failed to load settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CouplePhone = config.CouplePhone;
                IsTransferEnabled = config.IsTransferEnabled;
                BarsaAddress = config.BarsaAddress;
                userToken = config.UserToken;

                foreach (var sipSetting in config.SipSettings.Values)
                {
                    if (int.TryParse(sipSetting.DomainPort, out int domainPort) && sipSetting.RegistrableOnClient)
                    {
                        sipService.RegisterAccount(
                            sipSetting.UserName,
                            sipSetting.DisplayName,
                            sipSetting.AuthenticationId,
                            sipSetting.RegisterPassword,
                            sipSetting.DomainHost,
                            domainPort
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void CreatCallFromApi(string number)
        {
            if (txtCallNumber.InvokeRequired)
            {
                this.Invoke(new Action(() => CreatCallFromApi(number)));
                return;
            }
            string targetNumber=number;

            if (number.StartsWith("$"))
            {
                targetNumber =  number.Substring(1);
            }

            else if (config.SipSettings.ContainsKey(selectedSipAccount))
            {
                string accountPrefix = config.SipSettings[selectedSipAccount].OutGoingCallPrefix;
                targetNumber = accountPrefix + targetNumber;
            }

            txtCallNumber.Text = targetNumber;
            btnCall.PerformClick();
        }
        public void ShowingOutGoingCallForm(string number, SIPService sipService, string token)
        {
            try
            {
                var payload = new
                {
                    CallerId = number
                };

                var callerDataTask = apiServiceHelper.MakeApiCall<List<CallerData>>(BarsaAddress, "GetCallerInfo", payload, userToken);
                callerDataTask.ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        HandleApiCallError(task.Exception);
                        return;
                    }

                    var callerData = task.Result;
                    outgoingCallForm = new OutgoingCallForm(number, callerData, sipService, token);
                    SIPService.ShowNotifyWindow(outgoingCallForm);
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                HandleUnexpectedError(ex);
            }
        }
        private void HandleApiCallError(AggregateException exception)
        {
            // Log or display the error to the user
            Console.WriteLine("Error occurred while making API call: " + exception.InnerException.Message);
            // Optionally, show a message box or log the error
        }
        private void HandleUnexpectedError(Exception ex)
        {
            Console.WriteLine("An unexpected error occurred: " + ex.Message);
            // Optionally, show a message box or log the error
        }
        public void CreateCall(object sender, EventArgs e)
        {
            string phoneNumber = txtCallNumber.Text;

            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                DisplayError("Phone Number Cannot Be Null Or Empty.");
                return;
            }

            if (!IsValidPhoneNumber(phoneNumber))
            {
                DisplayError("Invalid Phone Number Format.");
                return;
            }

            string actualPhoneNumber = NormalizePhoneNumber(phoneNumber);
            ShowingOutGoingCallForm(actualPhoneNumber, sipService, userToken);
            sipService.CreateCall(phoneNumber);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\d+$");
        }
        private string NormalizePhoneNumber(string phoneNumber)
        {
            if (phoneNumber.StartsWith("9"))
            {
                return phoneNumber.Substring(1);
            }
            return phoneNumber;
        }
        private void DisplayError(string errorMessage)
        {
            Console.WriteLine(errorMessage);
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (outgoingCallForm != null && outgoingCallForm.IsHandleCreated)
            {
                // Check if the form's handle has been created
                outgoingCallForm.Invoke(new Action(() =>
                {
                    outgoingCallForm.btnDropCall.Enabled = false;
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

