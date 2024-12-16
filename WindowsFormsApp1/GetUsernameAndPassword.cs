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

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var payload = new
                {
                    barsaUserName = txtUserName.Text,
                    barsaPassword = txtPassword.Text,
                };

                TokenResponse tokenResponse = await _apiHelper.MakeApiCall<TokenResponse>(settingForm.BarsaAddressTextBox.Text, "GetToken", payload, null);

                if (tokenResponse!=null )
                {
                    if (tokenResponse.Success)
                    {
                        settingForm.BarcaUsername.Text = txtUserName.Text;
                        settingForm.UserTokenTextBox.Text = tokenResponse.Token;
                        Close(); // Close the login form
                    }
                    else
                    {
                        MessageBox.Show(tokenResponse.ErrorMessage ?? "An error occurred during login.");
                    }
                }
                else
                {
                    MessageBox.Show("Token Response Is Empty.");

                }
            }
            catch (Exception ex)
            {
                // Handle unexpected errors
                MessageBox.Show("An unexpected error occurred: " + ex.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
