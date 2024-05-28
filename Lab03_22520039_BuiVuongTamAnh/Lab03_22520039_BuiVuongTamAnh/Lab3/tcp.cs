using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class tcp : Form
    {
        public tcp()
        {
            InitializeComponent();
        }

        private void serverBtn_Click(object sender, EventArgs e)
        {
            new tcpTelnet().Show();
        }

        private void clientBtn_Click(object sender, EventArgs e)
        {
            new tcpClient().Show();
        }
    }
}
