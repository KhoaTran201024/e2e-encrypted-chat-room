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
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace main
{
    public partial class Home : Form
    {
        string connectionString = "mongodb+srv://22520039:JaVHhoJmN7iHt9Sr@scopify.9dlayjt.mongodb.net/Users";
        string dbName = "simple_db";
        string collectionName = "people";

        public Home()
        {
            InitializeComponent();
        }

        public class PersonModel
        {
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            public string Name { get; set; }
            public string Password { get; set; }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Home().Close();
            new Signup().Show();
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
                //richTextBox1.Text += text;
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
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(dbName);
            var collection = db.GetCollection<PersonModel>(collectionName);

            string name = textBox1.Text;
            string pwd = textBox2.Text;

            var filter = Builders<PersonModel>.Filter.Eq("Name", name) &
                         Builders<PersonModel>.Filter.Eq("Password", pwd);

            var user = collection.Find(filter).FirstOrDefault();

            if (user != null)
            {
                MessageBox.Show("Login successful.");
                StartListen(sender, e);
                new Chatroom().Show();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}

