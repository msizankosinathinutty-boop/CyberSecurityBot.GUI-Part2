using CybersecurityAwarenessBot.GUI;

namespace CyberSecurityBot.GUI
{
    public partial class Form1 : Form
    {
        private VoiceGreeting voiceGreeting = new VoiceGreeting();

        private ResponseHandler bot = new ResponseHandler();

        public Form1()
        {
            InitializeComponent();
            voiceGreeting.PlayGreeting();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void btnSend_Click(object sender, EventArgs e)
        {
            string userMessage = txtMessage.Text.Trim();

            // Check for an empty message.
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                MessageBox.Show(
                    "Please type a message first.",
                    "Empty Message",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                txtMessage.Focus();
                return;
            }

            try
            {
                // Display the user's message.
                rtbChat.AppendText(
                    "You: " + userMessage + Environment.NewLine
                );

                // Check sentiment first.

                string botReply = bot.GetSentimentResponse(userMessage);

                if (string.IsNullOrWhiteSpace(botReply))
                {
                    ChatResponseDelegate responseMethod = bot.GetResponse;

                    botReply = responseMethod(userMessage);
                }


                // Display the chatbot's response.
                rtbChat.AppendText(
                    "Bot: " + botReply + Environment.NewLine +
                    Environment.NewLine
                );

                txtMessage.Clear();
                txtMessage.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Something went wrong. Please try again.\n" + ex.Message,
                    "Chatbot Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

    }
}
