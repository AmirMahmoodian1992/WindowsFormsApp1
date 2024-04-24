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

    public partial class TransferCallForm : Form
    {
        private SIPService sipService;
        private SettingsForm settingForm;
        private readonly ApiServiceHelper _apiHelper;
        public TransferCallForm(SIPService SIPServic/*, SettingsForm settingForm, ApiServiceHelper apiHelper*/)
        {
            this.sipService = SIPServic;
            //this.settingForm = settingForm;
            //this._apiHelper = apiHelper;

            InitializeComponent();
        }

        private void ShowPassord_Load(object sender, EventArgs e)
        {
           
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            sipService.TransferCall(txtTransferNumber.Text);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
