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

            string ipText = "127.0.0.1";
            string portText = "8080";
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

        void ReceiveMessage(Socket clientSocket)
        {
            byte[] dataToSend;
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
                dataToSend = Encoding.UTF8.GetBytes(text);
                foreach (Socket socket in clientSockets)
                {
                    if (socket != clientSocket && socket.Connected)
                    {
                        socket.Send(dataToSend);
                    }
                }
            }

            clientSocket.Close();
            clientSockets.Remove(clientSocket); // Remove client from the list when disconnected
            dataToSend = Encoding.UTF8.GetBytes("Client disconnected\n");
            foreach (Socket socket in clientSockets)
            {
                if (socket != clientSocket && socket.Connected)
                {
                    socket.Send(dataToSend);
                }
            }
        }

        private List<Socket> clientSockets = new List<Socket>();
        private void StartListen(object sender, EventArgs e)
        {
            //Xử lý lỗi InvalidOperationException 
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.Start();
        }
        void StartUnsafeThread()
        {
            byte[] recv = new byte[1];


            Socket listenerSocket = new Socket(
                       AddressFamily.InterNetwork,
                       SocketType.Stream,
                       ProtocolType.Tcp);

            IPEndPoint ipepServer = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);

            listenerSocket.Bind(ipepServer);

            listenerSocket.Listen(-1);

            while (true)
            {
                Socket clientSocket = listenerSocket.Accept();
                clientSockets.Add(clientSocket); // Add new client to the list

             //   richTextBox1.Text += "New client\n";

                Thread clientThread = new Thread(() => ReceiveMessage(clientSocket));
                clientThread.Start();
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            StartListen(sender, e);
        }
    }
}
