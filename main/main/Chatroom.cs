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
using System.IO;
namespace main
{
    public partial class Chatroom : Form
    {
        //private string encryptionKey = "nhomgido";
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
                Task.Run(() => ReceiveData());
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

        private void ReceiveData()
        {
            while (connected)
            {
                try
                {
                    // Read the length of the incoming data (4 bytes for an integer)
                    byte[] lengthBuffer = new byte[4];
                    int bytesRead = stream.Read(lengthBuffer, 0, lengthBuffer.Length);

                    if (bytesRead == 0)
                    {
                        // Connection closed
                        break;
                    }

                    int dataLength = BitConverter.ToInt32(lengthBuffer, 0);

                    if (dataLength > 0)
                    {
                        // Buffer for the incoming data
                        byte[] dataBuffer = new byte[dataLength];
                        int totalBytesRead = 0;

                        while (totalBytesRead < dataLength)
                        {
                            bytesRead = stream.Read(dataBuffer, totalBytesRead, dataLength - totalBytesRead);
                            if (bytesRead == 0)
                            {
                                // Connection closed
                                break;
                            }
                            totalBytesRead += bytesRead;
                        }

                        // Convert data buffer to a string message
                        string message = Encoding.UTF8.GetString(dataBuffer);

                        // Check if the message is a file path (you can have a protocol to distinguish between messages and files)
                        if (IsFileMessage(message))
                        {
                            string filePath = SaveReceivedFile(dataBuffer);
                            richTextBox4.Invoke(new Action(() =>
                            {
                                richTextBox4.AppendText(filePath + "\n");
                            }));
                        }
                        else
                        {
                            richTextBox1.Invoke(new Action(() =>
                            {
                                richTextBox1.AppendText(message + "\n");
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error receiving data: " + ex.Message);
                }
            }
        }

        private bool IsFileMessage(string message)
        {
            // Implement logic to distinguish file messages, e.g., check for a specific prefix
            // This is a simple example, adjust according to your protocol
            return message.StartsWith("FILE:");
        }

        private string SaveReceivedFile(byte[] fileData)
        {
            try
            {
                // Extract file name from the data (depends on your protocol)
                // Here we assume the first 100 bytes contain the file name
                string fileName = Encoding.UTF8.GetString(fileData, 0, 100).Trim('\0');
                string filePath = Path.Combine(Path.GetTempPath(), fileName);

                // Write the remaining data to the file
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(fileData, 100, fileData.Length - 100);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving file: " + ex.Message);
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!connected)
            {
                MessageBox.Show("Not connected to the server!");
                return;
            }

            // Prepare the message text
            string text = richTextBox3.Text + ": " + richTextBox2.Text;
            text = text.Trim();
            if (text[text.Length - 1] != '\n')
                text += '\n';
            richTextBox1.AppendText(text);

            // Send the message text over the network stream
            byte[] data = Encoding.UTF8.GetBytes(text);
            stream.Write(data, 0, data.Length);

            // Send attachments listed in richTextBox4
            string[] filePaths = richTextBox4.Text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string filePath in filePaths)
            {
                string trimmedFilePath = filePath.Trim();

                if (System.IO.File.Exists(trimmedFilePath))
                {
                    try
                    {
                        // Read file bytes
                        byte[] fileData = System.IO.File.ReadAllBytes(trimmedFilePath);

                        // Send file length followed by the file bytes
                        byte[] fileLength = BitConverter.GetBytes(fileData.Length);
                        stream.Write(fileLength, 0, fileLength.Length);
                        stream.Write(fileData, 0, fileData.Length);

                        MessageBox.Show("File sent: " + trimmedFilePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error sending file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("File not found: " + trimmedFilePath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                            richTextBox4.AppendText(filePath + Environment.NewLine);

                        }
                    }
                }
            }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
