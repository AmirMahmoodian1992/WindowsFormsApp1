using sipservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        public SettingsForm(SIPService sipService, MainForm frm)
        {
            InitializeComponent();
            this.sipService = sipService;
            this.frm = frm;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            bool InputsIsCorrect = true;
            //TODO do we need these ?
            frm.BarcaUsername.Text = BarcaUsername.Text;
            frm.BarcaPass.Text = BarcaPass.Text;
            frm.txtUsername.Text = txtUsername.Text;
            frm.txtPassword.Text = txtPassword.Text;
            frm.couplePhone.Text = CouplePhone.Text;

            UserName = txtUsername.Text;
            DisplayName = txtUsername.Text;
            AuthenticationId = txtUsername.Text;
            RegisterPassword = txtPassword.Text;
            DomainHost = SIPServerAddressTextBox.Text;

            Properties.Settings.Default.BarcaPass = BarcaPass.Text;
            Properties.Settings.Default.BarcaUsername = BarcaUsername.Text;
            Properties.Settings.Default.TransferphoneCheckBox = TransferphoneCheckBox.Checked;
            Properties.Settings.Default.SIPServerAddressTextBox = SIPServerAddressTextBox.Text;
            Properties.Settings.Default.SIPServerPortTextBox = DomainPort;
            Properties.Settings.Default.CouplePhone= CouplePhone.Text;
            Properties.Settings.Default.Username = txtUsername.Text;
            Properties.Settings.Default.Password = txtPassword.Text;
            if (!int.TryParse(SIPServerPortTextBox.Text, out int domainPort))
            {
                MessageBox.Show("Invalid input for SIP Server Port. Please enter a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                InputsIsCorrect = false;
            }
            else
            {
                Properties.Settings.Default.SIPServerPortTextBox = domainPort;
            }
            
            if (InputsIsCorrect)
            {
                Properties.Settings.Default.Save();
                sipService.RegisterAccount(UserName, DisplayName, AuthenticationId, RegisterPassword, DomainHost, DomainPort);
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
            Properties.Settings.Default.Reload();
            BarcaPass.Text = Properties.Settings.Default.BarcaPass;
            BarcaUsername.Text = Properties.Settings.Default.BarcaUsername;
            TransferphoneCheckBox.Checked = Properties.Settings.Default.TransferphoneCheckBox;
            SIPServerAddressTextBox.Text = Properties.Settings.Default.SIPServerAddressTextBox;
            DomainPort = Properties.Settings.Default.SIPServerPortTextBox;
            CouplePhone.Text = Properties.Settings.Default.CouplePhone;
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;
            SIPServerPortTextBox.Text = Properties.Settings.Default.SIPServerPortTextBox.ToString();

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
            if (TransferphoneCheckBox.Checked)
                frm.TransferPhoneActive = true;
            else
                frm.TransferPhoneActive = false;
        }
    }
}
