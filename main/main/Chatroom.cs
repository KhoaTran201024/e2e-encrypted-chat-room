using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace main
{
    public partial class Chatroom : Form
    {
        private string encryptionKey = "nhomgido";
        public Chatroom()
        {
            InitializeComponent();
            string ipText = "127.0.0.1";
            string portText = "8080";

            client = new TcpClient();
            try
            {
                client.Connect(ipText, int.Parse(portText));
                stream = client.GetStream();
                connected = true;
                MessageBox.Show("Connected to the server!");

                // Start receiving messages in a separate thread
                Task.Run(() => ReceiveMessages());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to the server: " + ex.Message);
            }
        }

        private TcpClient client;
        private NetworkStream stream;
        private bool connected = false;

        private string Encrypt(string text, string key)
        {
            StringBuilder encryptedText = new StringBuilder();

            for (int i = 0; i < text.Length; i++)
            {
                char character = text[i];
                int keyIndex = i % key.Length;
                int shift = key[keyIndex] - 'a';

                if (char.IsLetter(character))
                {
                    char encryptedChar = (char)(('a' + (character - 'a' + shift) % 26));
                    encryptedText.Append(encryptedChar);
                }
                else
                {
                    encryptedText.Append(character);
                }
            }

            return encryptedText.ToString();
        }
        private string Decrypt(string encryptedText, string key)
        {
            StringBuilder decryptedText = new StringBuilder();

            for (int i = 0; i < encryptedText.Length; i++)
            {
                char character = encryptedText[i];
                int keyIndex = i % key.Length;
                int shift = key[keyIndex] - 'a';

                if (char.IsLetter(character))
                {
                    char decryptedChar = (char)(('a' + (character - 'a' - shift + 26) % 26));
                    decryptedText.Append(decryptedChar);
                }
                else
                {
                    decryptedText.Append(character);
                }
            }

            return decryptedText.ToString();
        }

        private void ReceiveMessages()
        {
            while (connected)
            {
                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                richTextBox1.Invoke((MethodInvoker)delegate {
                    richTextBox1.AppendText(receivedMessage);
                    richTextBox1.ScrollToCaret();
                });
            }
        }

        private void Chatroom_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                MessageBox.Show("Not connected to the server!");
                return;
            }
            string text = richTextBox3.Text + ": " + richTextBox2.Text;
            text = text.Trim();
            if (text[text.Length - 1] != '\n')
                text += '\n';
            richTextBox1.AppendText(text);
            byte[] data = Encoding.UTF8.GetBytes(text);
            stream.Write(data, 0, data.Length);
        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Chatroom_Load_1(object sender, EventArgs e)
        {

        }
    }

}
