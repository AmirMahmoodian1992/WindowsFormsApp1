using Ozeki.Media;
using Ozeki.VoIP;
using sipservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SIPWindowsAgent
{
    public partial class SettingsForm : Form
    {
        public event EventHandler AcceptButtonClicked;
        public event EventHandler RejectButtonClicked;
        private SIPService sipService;
        MainForm frm;
        int DomainPort;
        string UserName;
        string DisplayName;
        string AuthenticationId;
        string RegisterPassword;
        string DomainHost;
        private bool NewFlag = true;
        internal ApiServiceHelper apiServiceHelper;

        public SettingsForm(SIPService sipService, MainForm frm, bool newFlag, ApiServiceHelper apiServiceHelper)
        {
            InitializeComponent();

            this.sipService = sipService;
            this.frm = frm;
            NewFlag = newFlag;
            this.apiServiceHelper = apiServiceHelper;
        }
        public void getRightSettingForIncominCall(string username)
        {
            SettingsManager settingsManager = new SettingsManager();
            AppConfig config = settingsManager.LoadSettings();
            foreach (var kvp in config.SipSettings)
            {
                if (kvp.Value.UserName == username)
                {
                    //frm.BarsaUser = config.BarsaUserName;
                    //frm.BarsaPass = config.BarcaPass;
                    frm.CouplePhone = config.CouplePhone;
                    frm.IsTransferEnabled = config.IsTransferEnabled;
                    frm.BarsaAddress = config.BarsaAddress;
                }
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            //TODO do we need these ?
            frm.BarsaUser = BarcaUsername.Text;
            frm.BarsaPass = UserTokenTextBox.Text;
            //frm.txtUsername.Text = txtUsername.Text;
            //frm.txtPassword.Text = txtPassword.Text;
            frm.CouplePhone = CouplePhoneTextBox.Text;
            frm.IsTransferEnabled = TransferphoneCheckBox.Checked;
            frm.BarsaAddress = BarsaAddressTextBox.Text;


            //UserName = txtUsername.Text;
            //DisplayName = txtUsername.Text;
            //AuthenticationId = txtUsername.Text;
            //RegisterPassword = txtPassword.Text;
            //DomainHost = SIPServerAddressTextBox.Text;

            //Properties.Settings.Default.BarcaPass = BarcaPass.Text;
            //Properties.Settings.Default.BarcaUsername = BarcaUsername.Text;
            //Properties.Settings.Default.TransferphoneCheckBox = TransferphoneCheckBox.Checked;
            //Properties.Settings.Default.SIPServerAddressTextBox = SIPServerAddressTextBox.Text;
            //Properties.Settings.Default.SIPServerPortTextBox = DomainPort;
            //Properties.Settings.Default.CouplePhone = CouplePhoneTextBox.Text;
            //Properties.Settings.Default.Username = txtUsername.Text;
            //Properties.Settings.Default.Password = txtPassword.Text;
            //Properties.Settings.Default.BarsaAddress = BarsaAddressTextBox.Text;

            // Sanitize BarcaUsername to create a valid setting key


            SettingsManager settingsManager = new SettingsManager();
            AppConfig appConfig = new AppConfig();
            //appConfig.BarcaPass = BarcaPass.Text;
            //appConfig.BarsaUserName = BarcaUsername.Text;
            appConfig.BarsaAddress = BarsaAddressTextBox.Text;
            appConfig.IsTransferEnabled = TransferphoneCheckBox.Checked;
            appConfig.CouplePhone = CouplePhoneTextBox.Text;
            var userToken = UserTokenTextBox.Text;
            if (userToken != null)
            {
                List<SipSettings> sipSettingsList = apiServiceHelper.MakeApiCall<List<SipSettings>>(BarsaAddressTextBox.Text, "GetSipSettings", null, userToken).Result;
                //await sipService.GetSipSettingAsync(userToken, appConfig.BarsaAddress);

                appConfig.UserToken = userToken;
                frm.userToken = userToken;
                appConfig.BarsaUserName = BarcaUsername.Text;

                if (sipSettingsList != null)
                {

                    foreach (var sipSettingsItems in sipSettingsList)
                    {
                        if (!appConfig.SipSettings.ContainsKey(sipSettingsItems.UserName))
                        {
                            appConfig.SipSettings.Add(sipSettingsItems.UserName, sipSettingsItems);
                        }
                        else
                        {
                            // Decide what to do when the key already exists, e.g., overwrite or ignore
                            // Here, we'll overwrite the existing value with the new one
                            appConfig.SipSettings[sipSettingsItems.UserName] = sipSettingsItems;
                        }
                    }
                }



                AppConfig config = settingsManager.LoadSettings();

                string username = BarcaUsername.Text;

                if (!config.SipSettings.ContainsKey(username))
                {
                    config.SipSettings[username] = new SipSettings();

                }

                settingsManager.SaveSettings(appConfig);
                foreach (var kvp in appConfig.SipSettings)
                {
                    string sipAccount = kvp.Key;
                    SipSettings sipSettings = kvp.Value;

                    // Access properties of sipSettings and pass them to sipService.RegisterAccount method
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
                            domainPort // Use the converted domainPort
                        );
                        }

                    }
                }
                frm.LoadingSipAccountsInListBox();
            }
            Close();

        }

        private void couplePhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void BarsaPasslabel_Click(object sender, EventArgs e)
        {

        }

        private void BarsaUsernamelabel_Click(object sender, EventArgs e)
        {

        }

        private void BarcaPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void BarcaUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;

            //Properties.Settings.Default.Reload();
            //BarcaPass.Text = Properties.Settings.Default.BarcaPass;
            //BarcaUsername.Text = Properties.Settings.Default.BarcaUsername;
            //TransferphoneCheckBox.Checked = Properties.Settings.Default.TransferphoneCheckBox;
            //SIPServerAddressTextBox.Text = Properties.Settings.Default.SIPServerAddressTextBox;
            //DomainPort = Properties.Settings.Default.SIPServerPortTextBox;
            //CouplePhoneTextBox.Text = Properties.Settings.Default.CouplePhone;
            //txtUsername.Text = Properties.Settings.Default.Username;
            //txtPassword.Text = Properties.Settings.Default.Password;
            //SIPServerPortTextBox.Text = Properties.Settings.Default.SIPServerPortTextBox.ToString();
            //BarsaAddressTextBox.Text = Properties.Settings.Default.BarsaAddress;
            if (!NewFlag)
            {

                SettingsManager settingsManager = new SettingsManager();
                AppConfig config = settingsManager.LoadSettings();
                //BarcaPass.Text = config.BarcaPass;
                //BarcaUsername.Text = config.BarsaUserName;
                TransferphoneCheckBox.Checked = config.IsTransferEnabled;
                CouplePhoneTextBox.Text = config.CouplePhone;
                BarsaAddressTextBox.Text = config.BarsaAddress;
                BarcaUsername.Text = config.BarsaUserName;
                UserTokenTextBox.Text = config.UserToken;

            }
            if (TransferphoneCheckBox.Checked)
            {
                CouplePhoneTextBox.Enabled = true;
            }
            else
            {
                CouplePhoneTextBox.Enabled = false;
            }

        }

        private void SIPServerPortTextBox_TextChanged(object sender, EventArgs e)
        {
            //if (!int.TryParse(SIPServerPortTextBox.Text, out int domainPort))
            //    MessageBox.Show("Invalid input for SIP Server Port. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else
            //    DomainPort = domainPort;
        }

        private void TransferphoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            frm.TransferPhoneActive = (TransferphoneCheckBox.CheckState == CheckState.Checked);
            CouplePhoneTextBox.Enabled = (TransferphoneCheckBox.CheckState == CheckState.Checked);
            if (TransferphoneCheckBox.Checked)
            {
                CouplePhoneTextBox.Enabled = true;
            }
            else
            {
                CouplePhoneTextBox.Enabled = false;
            }

        }

        private void ShowPass_Click(object sender, EventArgs e)
        {
            UserTokenTextBox.PasswordChar = UserTokenTextBox.PasswordChar == '*' ? '\0' : '*'; // Show characters
        }

        private void ShowPassSip_Click(object sender, EventArgs e)
        {
            //txtPassword.PasswordChar = txtPassword.PasswordChar == '*' ? '\0' : '*'; // Show characters
        }

        private void BarsaAddressTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetUsernameAndPassword PassForm = new GetUsernameAndPassword(this.sipService, this, this.apiServiceHelper);
            PassForm.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BarsaAddressTextBox_Validating(object sender, CancelEventArgs e)
        {
            string pattern = @"^(https?://)?((localhost)|([\w-]+(\.[\w-]+)+))(:\d+)?(/[\w- ./?%&=]*)?$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(BarsaAddressTextBox.Text))
            {
                MessageBox.Show("Please enter a valid HTTP or HTTPS address.", "Invalid Address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
    }
}
