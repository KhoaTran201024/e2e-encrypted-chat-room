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
        // Key for Vigenere encryption
        private string encryptionKey = "KEY";

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
                
                string message = Encrypt(msgText.Text, encryptionKey);
                byte[] data = Encoding.UTF8.GetBytes(message);
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

        private string Encrypt(string text, string key)
        {
            StringBuilder encryptedText = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                int keyIndex = i % key.Length;
                int shift = key[keyIndex] - 'A';

                if (char.IsLetter(character))
                {
                    char encryptedChar = (char)(('A' + (character - 'A' + shift) % 26));
                    encryptedText.Append(encryptedChar);
                }
                else
                {
                    encryptedText.Append(character);
                }
            }

            return encryptedText.ToString();
        }
    }
}
