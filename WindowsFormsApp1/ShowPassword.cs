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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SIPWindowsAgent
{

    public partial class ShowPassword : Form
    {
        private SIPService sIPService;
        SettingsManager settingsManager;
        public ShowPassword(SIPService SIPServic)
        {
            this.sIPService = SIPServic;
            settingsManager = new SettingsManager();
            InitializeComponent();
        }

        private void ShowPassord_Load(object sender, EventArgs e)
        {
            AppConfig config = settingsManager.LoadSettings();
            txtUserName.Text = config.BarsaUserName;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            sIPService.RedirectAfterPass(txtPassword.Text);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
