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
using System.IO;

namespace Lab3
{
    public partial class chatClient : Form
    {
        public chatClient()
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
                try
                {
                    // Read the marker to determine the type of message
                    byte[] markerBuffer = new byte[5]; // Adjust size according to your markers
                    int markerBytesRead = stream.Read(markerBuffer, 0, markerBuffer.Length);
                    string marker = Encoding.UTF8.GetString(markerBuffer, 0, markerBytesRead);

                    if (marker.StartsWith("TXT:"))
                    {
                        // Receive a text message
                        byte[] buffer = new byte[1024];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);
                        string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        richTextBox1.Invoke((MethodInvoker)delegate {
                            richTextBox1.AppendText(receivedMessage);
                            richTextBox1.ScrollToCaret();
                        });
                    }
                    else if (marker.StartsWith("FILE:"))
                    {
                        // Receive a file
                        // First, read the file name length
                        byte[] fileNameLengthBuffer = new byte[4];
                        stream.Read(fileNameLengthBuffer, 0, fileNameLengthBuffer.Length);
                        int fileNameLength = BitConverter.ToInt32(fileNameLengthBuffer, 0);

                        // Then, read the file name
                        byte[] fileNameBuffer = new byte[fileNameLength];
                        stream.Read(fileNameBuffer, 0, fileNameBuffer.Length);
                        string fileName = Encoding.UTF8.GetString(fileNameBuffer);

                        // Next, read the file content length
                        byte[] fileContentLengthBuffer = new byte[4];
                        stream.Read(fileContentLengthBuffer, 0, fileContentLengthBuffer.Length);
                        int fileContentLength = BitConverter.ToInt32(fileContentLengthBuffer, 0);

                        // Finally, read the file content
                        byte[] fileContentBuffer = new byte[fileContentLength];
                        int totalBytesRead = 0;
                        while (totalBytesRead < fileContentLength)
                        {
                            int bytesRead = stream.Read(fileContentBuffer, totalBytesRead, fileContentLength - totalBytesRead);
                            totalBytesRead += bytesRead;
                        }

                        // Save the file to disk (adjust the path as necessary)
                        string savePath = Path.Combine("ReceivedFiles", fileName);
                        File.WriteAllBytes(savePath, fileContentBuffer);

                        // Update the UI
                        richTextBox1.Invoke((MethodInvoker)delegate {
                            richTextBox1.AppendText($"Received file: {fileName}\n");
                            richTextBox1.ScrollToCaret();
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error receiving message: " + ex.Message);
                    connected = false;
                }
            }
        }


        private void sendBtn_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                MessageBox.Show("Not connected to the server!");
                return;
            }

         //   string ipText = "127.0.0.1";
           // string portText = "8080";
            string text = nameText.Text + ": " + mesText.Text;
            text = text.Trim();
            if (text[text.Length - 1] != '\n')
                text += '\n';
            richTextBox1.AppendText(text);
            byte[] data = Encoding.UTF8.GetBytes(text);
            stream.Write(data, 0, data.Length);
        }

        private void mesText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
