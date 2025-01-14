﻿using Ozeki.Media;
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
        private bool isWindowOpen = false;
        GetUsernameAndPassword PassForm;


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
            AppConfig config = SettingsManager.Instance.LoadSettings();
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
            frm.CouplePhone = CouplePhoneTextBox.Text;
            frm.IsTransferEnabled = TransferphoneCheckBox.Checked;
            frm.BarsaAddress = BarsaAddressTextBox.Text;
            AppConfig appConfig = SettingsManager.Instance.LoadSettings();
            appConfig.SipSettings.Clear();
            appConfig.BarsaAddress = BarsaAddressTextBox.Text;
            appConfig.IsTransferEnabled = TransferphoneCheckBox.Checked;
            appConfig.CouplePhone = CouplePhoneTextBox.Text;
            if (int.TryParse(textBoxFormClosingInterval.Text, out int closingFormInterval))
            {
                appConfig.CloseFormInterval = closingFormInterval;
            }
            else
            {
                MessageBox.Show("Please Enter Time Interval In Right Format.");
            }
            var userToken = UserTokenTextBox.Text;
            appConfig.UserToken = userToken;
            frm.userToken = userToken;
            appConfig.BarsaUserName = BarcaUsername.Text;
            if (!string.IsNullOrEmpty(userToken))
            {
                List<SipSettings> sipSettingsList = apiServiceHelper.MakeApiCall<List<SipSettings>>(BarsaAddressTextBox.Text, "GetSipSettings", null, userToken).Result;
                
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
                string username = BarcaUsername.Text;
                if (!appConfig.SipSettings.ContainsKey(username))
                {
                    appConfig.SipSettings[username] = new SipSettings();
                }
                sipService.InitSoftphone();
                sipService.Dispose();
                SettingsManager.Instance.SaveSettings(appConfig);
                foreach (var kvp in appConfig.SipSettings)
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
                frm.LoadingSipAccountsInListBox();
            }
            else
            {
                SettingsManager.Instance.SaveSettings(appConfig);
            }
            Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            if (!NewFlag)
            {
                AppConfig config = SettingsManager.Instance.LoadSettings();
                TransferphoneCheckBox.Checked = config.IsTransferEnabled;
                CouplePhoneTextBox.Text = config.CouplePhone;
                BarsaAddressTextBox.Text = string.IsNullOrEmpty(config.BarsaAddress) ? "https://my.barsasoft.com/" : config.BarsaAddress;

                BarcaUsername.Text = config.BarsaUserName;
                UserTokenTextBox.Text = config.UserToken;
                textBoxFormClosingInterval.Text = config.CloseFormInterval.ToString();
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
            if (!isWindowOpen)
            {
                // Create a new instance of Form2 if it's not already open
                PassForm = new GetUsernameAndPassword(this.sipService, this, this.apiServiceHelper);
                PassForm.FormClosed += (s, args) => isWindowOpen = false; // Update flag when Form2 is closed
                PassForm.Show();
                isWindowOpen = true;
            }
            else
            {
                // Bring the existing instance of Form2 to the front
                PassForm?.BringToFront();
            }
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
