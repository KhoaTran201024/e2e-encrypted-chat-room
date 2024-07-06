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
    public partial class onevs1chat : Form
    {
        // Key for Vigenere encryption
        private string encryptionKey = "nhomgido";
        public onevs1chat()
        {
            InitializeComponent();
        }
        private void TCPStartListen(object sender, EventArgs e)
        {
            //Xử lý lỗi InvalidOperationException 
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(TCPStartUnsafeThread));
            serverThread.Start();
        }

        private List<Socket> TCPclientSockets = new List<Socket>();
        void TCPStartUnsafeThread()
        {
            byte[] recv = new byte[1];


            Socket listenerSocket = new Socket(
                       AddressFamily.InterNetwork,
                       SocketType.Stream,
                       ProtocolType.Tcp);

            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);

            listenerSocket.Bind(ipepServer);

            listenerSocket.Listen(-1);

            while (true)
            {
                Socket clientSocket = listenerSocket.Accept();
                TCPclientSockets.Add(clientSocket); // Add new client to the list

                //richTextBox2.Text += "New client\n";

                Thread clientThread = new Thread(() => TCPReceiveMessages(clientSocket));
                clientThread.Start();
            }

        }
        void TCPReceiveMessages(Socket clientSocket)
        {
            while (clientSocket.Connected)
            {

                string text = "";
                string st = "";
                byte[] recv = new byte[1024];
                do
                {
                    int bytesReceived = clientSocket.Receive(recv);
                    text += Encoding.UTF8.GetString(recv, 0, bytesReceived);
                    if (text == "")
                        break;
                } while (text[text.Length - 1] != '\n');
                if (text == "")
                    break;
                st += text;
                richTextBox1.Text += Decrypt(st, encryptionKey);
                



                // Send message to every client
                byte[] dataToSend = Encoding.UTF8.GetBytes(text);
                



            }
            clientSocket.Close();
            TCPclientSockets.Remove(clientSocket); // Remove client from the list when disconnected
        }

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

        private void button1_Click(object sender, EventArgs e)
        {
            
            string text = Encrypt(richTextBox1.Text, encryptionKey);
            text = text.Trim();
            if (text[text.Length - 1] != '\n')
                text += '\n';
            TcpClient client = new TcpClient();
            IPAddress ip = IPAddress.Parse(textBox1.Text);
            IPEndPoint endPoint = new IPEndPoint(ip, int.Parse(textBox2.Text));
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(text);
            stream.Write(data, 0, data.Length);
            stream.Close();
            client.Close();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            TCPStartListen(sender, e);
            MessageBox.Show("Server start listening.");
        }

        private void button3_Click(object sender, EventArgs e)
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Multiselect = true; // Allow multiple file selection
                    openFileDialog.Filter = "All Files|*.*"; // You can customize the filter if needed

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {


                        // Display selected file paths in richTextBox2
                        foreach (string filePath in openFileDialog.FileNames)
                        {
                            richTextBox1.AppendText(filePath + Environment.NewLine);

                        }
                    }
                }
            }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
