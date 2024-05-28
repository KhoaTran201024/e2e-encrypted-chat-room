using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class UDPClient : Form
    {
        public UDPClient()
        {
            InitializeComponent();
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            try { 
                IPAddress ip;
                bool ok = IPAddress.TryParse(ipText.Text, out ip);
                if (!ok)
                {
                    MessageBox.Show("Invalid IP address");
                }
                int port = 0;
                ok = Int32.TryParse(portText.Text, out port);
                if (!ok)
                {
                    MessageBox.Show("Invalid port");
                }
                IPEndPoint IPep = new IPEndPoint(ip, port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                byte[] data = Encoding.UTF8.GetBytes(msgText.Text);
                socket.SendTo(data, IPep);
                Array.Clear(data, 0, data.Length);
                msgText.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi trong lúc gửi");
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            msgText.Text = "";
        }
    }
}
