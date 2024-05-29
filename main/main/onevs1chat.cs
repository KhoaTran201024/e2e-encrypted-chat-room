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
                richTextBox1.Text += text;
                // Send message to every client
                byte[] dataToSend = Encoding.UTF8.GetBytes(text);
            }
            clientSocket.Close();
            TCPclientSockets.Remove(clientSocket); // Remove client from the list when disconnected
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            string text = richTextBox1.Text;
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void onevs1chat_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TCPStartListen(sender, e);
        }
    }
}
