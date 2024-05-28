using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
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
            // Update the name variable whenever the text in textBox1 changes
            string name = textBox1.Text.Trim();
            // You can perform any additional processing here, such as validation or formatting
            // For example, if you want to ensure that the name is not empty:
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private async Task<string> PredictGenderAsync(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"https://api.genderize.io?name={name}";
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throw if not success

                string responseBody = await response.Content.ReadAsStringAsync();
                // Parse the JSON response to extract gender prediction
                // For simplicity, assuming the response is in the format: {"name":"Luc","gender":"male","probability":0.99,"count":366}
                // You might want to use a JSON library like Newtonsoft.Json for more robust JSON parsing
                return responseBody.Contains("\"gender\":\"male\"") ? "Male" : "Female";
            }
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Please enter a name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                string gender = await PredictGenderAsync(name);
                MessageBox.Show($"Predicted gender: {gender}", "Prediction Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
