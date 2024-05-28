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
using System.Threading;

namespace Lab3
{
    public partial class UDPServer : Form
    {
        // Key for Vigenere encryption
        private string encryptionKey = "KEY";

        public UDPServer()
        {
            InitializeComponent();
        }
        delegate void SetTextCallback(string text);
        private void SetText(string text)
        {
            if (this.logBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.logBox.Text += text;
            }
        }
        private void serverThread()
        {
            IPAddress ip = IPAddress.Any;
            int port = 0;
            bool ok = Int32.TryParse(portText.Text, out port);
            IPEndPoint IPep = new IPEndPoint(ip, port);
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.Bind(IPep);
            byte[] data = new byte[1024];
            while (true)
            {
                EndPoint client = new IPEndPoint(IPAddress.Any, 0);
                int length = s.ReceiveFrom(data, ref client);
                string encryptedMessage = Encoding.UTF8.GetString(data, 0, length);
                string message = Decrypt(encryptedMessage, encryptionKey);

                //richTextBox1.Text += "Client " + client.ToString() + ": " + message + "\n";
                SetText("Client " + client.ToString() + ": " + message + "\n");
                Array.Clear(data, 0, data.Length);
            }
        }

        private void listBtn_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread t = new Thread(serverThread);
            t.Start();
        }

        private string Decrypt(string encryptedText, string key)
        {
            StringBuilder decryptedText = new StringBuilder();

            for (int i = 0; i < encryptedText.Length; i++)
            {
                char character = encryptedText[i];
                int keyIndex = i % key.Length;
                int shift = key[keyIndex] - 'A';

                if (char.IsLetter(character))
                {
                    char decryptedChar = (char)(('A' + (character - 'A' - shift + 26) % 26));
                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    decryptedText.Append(character);
                }
            }

            return decryptedText.ToString();
        }
    }
}
