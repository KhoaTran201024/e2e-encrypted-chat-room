using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class tcpClient : Form
    {
        public tcpClient()
        {
            InitializeComponent();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            text = text.Trim();
            if (text[text.Length - 1] != '\n')
                text += '\n';
            TcpClient client = new TcpClient();
            IPAddress ip = IPAddress.Parse(ipText.Text);
            IPEndPoint endPoint = new IPEndPoint(ip, int.Parse(portText.Text));
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(text);
            stream.Write(data, 0, data.Length);
            stream.Close();
            client.Close();
        }

        private void portText_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
