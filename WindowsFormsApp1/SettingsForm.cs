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
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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

        public SettingsForm(SIPService sipService, MainForm frm, bool newFlag)
        {
            InitializeComponent();

            this.sipService = sipService;
            this.frm = frm;
            NewFlag = newFlag;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            bool InputsIsCorrect = true;
            //TODO do we need these ?
            frm.BarsaUser = BarcaUsername.Text;
            frm.BarsaPass = BarcaPass.Text;
            //frm.txtUsername.Text = txtUsername.Text;
            //frm.txtPassword.Text = txtPassword.Text;
            frm.CouplePhone = CouplePhoneTextBox.Text;
            frm.IsTransferEnabled = TransferphoneCheckBox.Checked;
            frm.BarsaAddress = BarsaAddressTextBox.Text;

            UserName = txtUsername.Text;
            DisplayName = txtUsername.Text;
            AuthenticationId = txtUsername.Text;
            RegisterPassword = txtPassword.Text;
            DomainHost = SIPServerAddressTextBox.Text;

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
            string sanitizedBarcaUsername = new string(BarcaUsername.Text.Where(c => char.IsLetterOrDigit(c)).ToArray());

            SipSettings sipSettings = new SipSettings();

            SettingsManager settingsManager = new SettingsManager();
            AppConfig config = settingsManager.LoadSettings();

            string username = BarcaUsername.Text;

            if (!config.SipSettings.ContainsKey(username))
            {
                config.SipSettings[username] = new SipSettings();
                
            }

            config.SipSettings[username].BarcaPass = BarcaPass.Text;
            config.SipSettings[username].BarsaUserName = BarcaUsername.Text;
            config.SipSettings[username].DomainHost = SIPServerAddressTextBox.Text;
            config.SipSettings[username].DomainPort = SIPServerPortTextBox.Text;
            config.SipSettings[username].IsTransferEnabled = TransferphoneCheckBox.Checked;
            config.SipSettings[username].CouplePhone = CouplePhoneTextBox.Text;
            config.SipSettings[username].UserName = txtUsername.Text;
            config.SipSettings[username].RegisterPassword = txtPassword.Text;
            config.SipSettings[username].BarsaAddress = BarsaAddressTextBox.Text;   

            // Save settings


            //Properties.Settings.Default["BarcaPass_" + sanitizedBarcaUsername] = BarcaPass.Text;

            //Properties.Settings.Default["BarcaUsername_" + sanitizedBarcaUsername] = BarcaUsername.Text;
            //Properties.Settings.Default["TransferphoneCheckBox_" + sanitizedBarcaUsername] = TransferphoneCheckBox.Checked;
            //Properties.Settings.Default["SIPServerAddressTextBox_" + sanitizedBarcaUsername] = SIPServerAddressTextBox.Text;
            //Properties.Settings.Default["DomainPort_" + sanitizedBarcaUsername] = DomainPort;
            //Properties.Settings.Default["CouplePhoneTextBox_" + sanitizedBarcaUsername] = CouplePhoneTextBox.Text;
            //Properties.Settings.Default["txtUsername_" + sanitizedBarcaUsername] = txtUsername.Text;
            //Properties.Settings.Default["txtPassword_" + sanitizedBarcaUsername] = txtPassword.Text;
            //Properties.Settings.Default["BarsaAddressTextBox_" + sanitizedBarcaUsername] = BarsaAddressTextBox.Text;


            // Save all settings
            //Properties.Settings.Default.Save();



            //if (!int.TryParse(SIPServerPortTextBox.Text, out int domainPort))
            //{
            //    MessageBox.Show("Invalid input for SIP Server Port. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    InputsIsCorrect = false;
            //}
            //else
            //{
            //    sipSettings.domainPort = domainPort;
            //}

            if (InputsIsCorrect)
            {
                //Properties.Settings.Default.Save();
                settingsManager.SaveSettings(config);
                sipService.RegisterAccount(UserName, DisplayName, AuthenticationId, RegisterPassword, DomainHost, DomainPort);
                frm.lstBarcaUsernames.Refresh();
                Close();
            }
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

                // Load settings
                SettingsManager settingsManager = new SettingsManager();
                AppConfig config = settingsManager.LoadSettings();

                // Modify settings for a specific user
                string username = frm.lstBarcaUsernames.Text;

                if (!config.SipSettings.ContainsKey(username))
                {
                    MessageBox.Show("doesnt fined account!!");
                }
                else
                {
                    // Save settings
                    settingsManager.SaveSettings(config);

                    Properties.Settings.Default.Reload();

                    // Modify these lines to retrieve settings based on the current BarcaUsername
                    BarcaPass.Text = config.SipSettings[username].BarcaPass;
                    BarcaUsername.Text = config.SipSettings[username].BarsaUserName;
                    TransferphoneCheckBox.Checked = config.SipSettings[username].IsTransferEnabled;
                    SIPServerAddressTextBox.Text = config.SipSettings[username].DomainHost;
                    if (!int.TryParse(config.SipSettings[username].DomainPort, out int domainPort))
                    {
                        MessageBox.Show("Invalid input for SIP Server Port. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DomainPort = domainPort;
                    }
                    CouplePhoneTextBox.Text = config.SipSettings[username].CouplePhone;
                    txtUsername.Text = config.SipSettings[username].UserName;
                    txtPassword.Text = config.SipSettings[username].RegisterPassword;
                    SIPServerPortTextBox.Text = config.SipSettings[username].DomainPort;
                    BarsaAddressTextBox.Text = config.SipSettings[username].BarsaAddress;
                }
            }

        }

        private void SIPServerPortTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(SIPServerPortTextBox.Text, out int domainPort))
                MessageBox.Show("Invalid input for SIP Server Port. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                DomainPort = domainPort;
        }

        private void TransferphoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            frm.TransferPhoneActive = (TransferphoneCheckBox.CheckState == CheckState.Checked);
            CouplePhoneTextBox.Enabled = (TransferphoneCheckBox.CheckState == CheckState.Checked);

        }

        private void ShowPass_Click(object sender, EventArgs e)
        {
            BarcaPass.PasswordChar = txtPassword.PasswordChar == '*' ? '\0' : '*'; // Show characters
        }

        private void ShowPassSip_Click(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = txtPassword.PasswordChar == '*' ? '\0' : '*'; // Show characters
        }
    }
}
