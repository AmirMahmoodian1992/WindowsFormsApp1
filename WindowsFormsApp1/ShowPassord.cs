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

    public partial class ShowPassord : Form
    {
        private SIPService sIPService;
        public ShowPassord(SIPService SIPServic)
        {
            this.sIPService = SIPServic;
            InitializeComponent();
        }

        private void ShowPassord_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            sIPService.RedirectAfterPass(textBox1.Text);
        }
    }
}
