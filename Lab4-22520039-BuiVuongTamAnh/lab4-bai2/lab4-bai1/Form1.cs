using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_bai1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Khi URL thay đổi, tải nội dung từ URL vào richTextBox1
            string url = textBox1.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    // Tạo yêu cầu HTTP
                    System.Net.WebRequest request = System.Net.WebRequest.Create(url);
                    request.Method = "GET";

                    // Nhận phản hồi từ máy chủ
                    using (System.Net.WebResponse response = request.GetResponse())
                    {
                        // Hiển thị các trường header của phản hồi
                        System.Net.WebHeaderCollection headers = response.Headers;
                        richTextBox2.Text = "Response Headers:" + Environment.NewLine;
                        int count = 1;
                        foreach (string header in headers)
                        {
                            richTextBox2.AppendText(count + ". " + header + ": " + headers[header] + Environment.NewLine);
                            count++;
                        }

                        // Đọc nội dung từ phản hồi
                        using (System.IO.Stream dataStream = response.GetResponseStream())
                        {
                            using (System.IO.StreamReader reader = new System.IO.StreamReader(dataStream))
                            {
                                string content = reader.ReadToEnd();
                                richTextBox1.Text = content;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading URL: " + ex.Message);
                }
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


    

