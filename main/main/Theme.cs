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
    public partial class Theme : Form
    {

        public Theme()
        {       
            InitializeComponent();
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

                richTextBox1.Text += "New client\n";

                Thread clientThread = new Thread(() => ReceiveMessage(clientSocket));
                clientThread.Start();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            StartListen(sender, e);
            new Chatroom().Show();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }








        // handle 1vs1 chat
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

                richTextBox2.Text += "New client\n";

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
                richTextBox2.Text += text;
                // Send message to every client
                byte[] dataToSend = Encoding.UTF8.GetBytes(text);
            }
            clientSocket.Close();
            TCPclientSockets.Remove(clientSocket); // Remove client from the list when disconnected
        }


        private void button1_Click(object sender, EventArgs e)
        {
        
            new onevs1chat().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new chatbot().Show();
        }
    }
}
