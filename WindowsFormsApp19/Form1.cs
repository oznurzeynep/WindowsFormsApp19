using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp19
{
    public partial class WelcomePage : Form
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NewOrder newReceipt = new NewOrder();
            newReceipt.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OldUnpaidOrder rcp = new OldUnpaidOrder();
            rcp.Show();
        }
    }
}
