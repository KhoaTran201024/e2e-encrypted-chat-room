using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace main
{
    public partial class chatbot : Form
    {
        private static readonly HttpClient Http = new HttpClient();
        private const string ApiKey = "sk-proj-ZqPBBaulCHenQqVlIhtKT3BlbkFJYE8GqPDi5ewdcoWIzRG3";
        public chatbot()
        {
            InitializeComponent();
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void messagesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {




        }

        async private void sendButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
                return;

            var userMessage = richTextBox1.Text;
            messagesListBox.Items.Add($"You: {userMessage}");
            richTextBox1.Clear();

            var responseMessage =  await SendMessageToOpenAI(userMessage);
            messagesListBox.Items.Add($"Bot: {responseMessage}");
        }

        private async Task<string> SendMessageToOpenAI(string message)
        {
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

            var jsonContent = new
            {
                prompt = message,
                model = "text-davinci-003",
                max_tokens = 1000
            };

            var response = await Http.PostAsync("https://api.openai.com/v1/completions", new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
                return "Error: Could not get a response from the server.";

            var responseContent = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(responseContent);

            return data.choices[0].text.ToString();
        }

        private void messagesListBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
