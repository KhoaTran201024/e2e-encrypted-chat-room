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
    public partial class chatForm : Form
    {
        public chatForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new chat().Show(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new chatClient().Show();
        }
    }
}
