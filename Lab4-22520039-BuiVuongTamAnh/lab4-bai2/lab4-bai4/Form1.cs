using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace lab4_bai4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true; // Bỏ qua lỗi script tự động
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
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
                    // Sử dụng WebClient để tải nội dung từ URL
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        webBrowser1.Navigate(new Uri(url));
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
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
        private void webBrowser1_ScriptError(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Bỏ qua lỗi script tự động
            webBrowser1.ScriptErrorsSuppressed = true;
        }
        private void SaveHTML()
        {
            string url = textBox1.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        string content = client.DownloadString(url);
                        // Lưu HTML vào tệp tin
                        System.IO.File.WriteAllText("page.html", content);
                    }
                    MessageBox.Show("HTML saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving HTML: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveHTML();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveCompleteWebPage();
        }

        private void SaveCompleteWebPage()
        {
            string url = textBox1.Text;
            if (!string.IsNullOrEmpty(url))
            {
                try
                {
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        // Lấy HTML
                        string htmlContent = client.DownloadString(url);
                        // Lưu HTML vào tệp tin
                        System.IO.File.WriteAllText("page.html", htmlContent);

                        // Lấy các link CSS, Images, JavaScript từ HTML
                        List<string> resources = GetWebResources(htmlContent);

                        // Lưu các tài nguyên vào thư mục
                        foreach (string resource in resources)
                        {
                            string resourceName = System.IO.Path.GetFileName(resource);
                            client.DownloadFile(new Uri(resource), resourceName);
                        }
                    }
                    MessageBox.Show("Complete web page saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving complete web page: " + ex.Message);
                }
            }
        }

        private List<string> GetWebResources(string html)
        {
            List<string> resources = new List<string>();

            // Lấy các đường dẫn tài nguyên từ HTML (CSS, Images, JavaScript)
            // Bạn cần một phương pháp phức tạp hơn để phân tích HTML và tìm các đường dẫn đúng

            // Ví dụ đơn giản: lấy các link href và src
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(href|src)=[\""]([^\""]+)[\""]");
            System.Text.RegularExpressions.MatchCollection matches = regex.Matches(html);
            foreach (System.Text.RegularExpressions.Match match in matches)
            {
                string resource = match.Groups[2].Value;
                if (!string.IsNullOrEmpty(resource))
                    resources.Add(resource);
            }

            return resources;
        }







    }
}