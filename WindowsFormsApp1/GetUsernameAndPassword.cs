using Ozeki.Media;
using sipservice;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIPWindowsAgent
{

    public partial class GetUsernameAndPassword : Form
    {
        private SIPService sIPService;
        private SettingsForm settingForm;
        private readonly ApiServiceHelper _apiHelper;
        public GetUsernameAndPassword(SIPService SIPServic, SettingsForm settingForm, ApiServiceHelper apiHelper)
        {
            this.sIPService = SIPServic;
            this.settingForm = settingForm;
            this._apiHelper = apiHelper;

            InitializeComponent();
        }

        private void ShowPassord_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var payload = new
            {
                barsaUserName = txtUserName.Text,
                barsaPassword = txtPassword.Text,
            };

            TokenResponse tokenResponse = _apiHelper.MakeApiCall<TokenResponse>(settingForm.BarsaAddressTextBox.Text, "GetToken", payload, null).Result;
            //sIPService.GetToken(txtUserName.Text, txtPassword.Text, settingForm.BarsaAddressTextBox.Text).Result;
            if (tokenResponse.Success)
            {
                settingForm.BarcaUsername.Text = txtUserName.Text;
                settingForm.UserTokenTextBox.Text = tokenResponse.Token;
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
