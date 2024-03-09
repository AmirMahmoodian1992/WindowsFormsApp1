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

        public SettingsForm(SIPService sipService, MainForm frm)
        {
            InitializeComponent();
            this.sipService = sipService;
            this.frm = frm; 
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            frm.BarcaUsername.Text =BarcaUsername.Text;
            frm.BarcaPass.Text =BarcaPass.Text; 
            frm.txtUsername.Text = txtUsername.Text;
            frm.txtPassword.Text = txtPassword.Text;    
            frm.couplePhone.Text = couplePhone.Text;

            var userName = txtUsername.Text;
            var displayName = txtUsername.Text;
            var authenticationId = txtUsername.Text;
            var registerPassword = txtPassword.Text;
            var domainHost = "192.168.0.101";
            var domainPort = 5080;

            sipService.RegisterAccount(userName, displayName, authenticationId, registerPassword, domainHost, domainPort);
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
    }
}
