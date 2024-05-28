using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4_bai2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // Thêm các thiết bị vào comboBox1
            // Thêm các lựa chọn User Agent vào comboBox1
            comboBox1.Items.Add("Desktop - Chrome");
            comboBox1.Items.Add("Desktop - Firefox");
            comboBox1.Items.Add("Desktop - Safari");
            comboBox1.Items.Add("Mobile - iPhone");
            comboBox1.Items.Add("Mobile - Android");
            // Chọn thiết bị mặc định là Desktop
            comboBox1.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Đăng ký sự kiện ScriptError của WebBrowser2
            webBrowser2.ScriptErrorsSuppressed = true; // Bỏ qua lỗi script tự động
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Khi URL thay đổi, tải nội dung từ URL vào richTextBox1
            string url = textBox1.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    // Sử dụng WebClient để tải nội dung từ URL
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        string content = client.DownloadString(url);
                        richTextBox1.Text = content;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading URL: " + ex.Message);
                }
            }
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userAgent = "";

            // Trích xuất User Agent dựa trên lựa chọn của người dùng
            string selectedDevice = comboBox1.SelectedItem.ToString();
            if (selectedDevice.Contains("Desktop"))
            {
                if (selectedDevice.Contains("Chrome"))
                    userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
                else if (selectedDevice.Contains("Firefox"))
                    userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:82.0) Gecko/20100101 Firefox/82.0";
                else if (selectedDevice.Contains("Safari"))
                    userAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0.1 Safari/605.1.15";
            }
            else if (selectedDevice.Contains("Mobile"))
            {
                if (selectedDevice.Contains("iPhone"))
                    userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 14_0 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.0 Mobile/15E148 Safari/604.1";
                else if (selectedDevice.Contains("Android"))
                    userAgent = "Mozilla/5.0 (Linux; Android 10; SM-G960U) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.66 Mobile Safari/537.36";
            }

            // Sử dụng User Agent để hiển thị nội dung từ richTextBox1 trong webBrowser2
            webBrowser2.Navigate("about:blank");
            webBrowser2.Navigate(textBox1.Text, "", null, "User-Agent: " + userAgent);
        }

        private void webBrowser2_ScriptError(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Bỏ qua lỗi script tự động
            webBrowser2.ScriptErrorsSuppressed = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
                string url = textBox1.Text;
                string userAgent = comboBox1.Text;
               
                webBrowser2.Navigate(url, null, null, "User-Agent:" + userAgent);
        }
    }
}
