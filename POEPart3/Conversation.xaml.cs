
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace POEPart3
{
    public partial class Conversation : Window
    {
        private string userName = null;
        private bool askedName = false;

        // Topics chatbot supports
        private readonly HashSet<string> topics = new HashSet<string>
        {
            "password", "phishing", "safe browsing", "privacy", "scam"
        };

        // Topic info mapped via dictionary instead of switch
        private readonly Dictionary<string, string> topicInfo = new Dictionary<string, string>
        {
            { "password", "Use strong, unique passwords and consider using a password manager to keep them safe." },
            { "phishing", "Phishing is a type of scam where attackers try to trick you into giving personal info. Always verify email sources and avoid clicking suspicious links." },
            { "safe browsing", "Safe browsing means avoiding suspicious websites, using HTTPS, and keeping your browser updated." },
            { "privacy", "Protect your privacy by managing app permissions, using strong passwords, and being cautious about sharing personal info online." },
            { "scam", "Scams are fraudulent schemes to steal your money or info. Be skeptical of unsolicited offers and never share sensitive data." }
        };

        // Words to ignore in user input
        private readonly HashSet<string> ignoreWords = new HashSet<string>
        {
            "tell", "what", "me", "about", "to", "how", "can", "do", "please", "i", "am", "my", "the", "a", "is", "are"
        };

        // Sentiment words and calming responses
        private readonly Dictionary<string, string> sentimentResponses = new Dictionary<string, string>
        {
            { "worried", "I understand your concern. Let's take it step by step to address your worries." },
            { "scared", "It's okay to feel scared. I'm here to help you feel more secure." },
            { "anxious", "Feeling anxious is normal. Let's work through your questions together." },
            { "concerned", "I hear your concern. I'll do my best to provide clear answers." }
        };

        public Conversation()
        {
            InitializeComponent();
            AddBotMessage("Hello! Welcome to the Cybersecurity Chatbot. What's your name?");
            askedName = true;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            ProcessUserInput();
        }

        private void UserInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ProcessUserInput();
                e.Handled = true;
            }
        }

        private void ProcessUserInput()
        {
            string input = UserInputBox.Text.Trim();
            if (string.IsNullOrEmpty(input))
                return;

            AddUserMessage(input);
            UserInputBox.Clear();

            if (!askedName)
            {
                AddBotMessage("Sorry, I didn't get your name. What's your name?");
                askedName = true;
                return;
            }

            if (userName == null)
            {
                userName = ExtractName(input);
                if (string.IsNullOrEmpty(userName))
                {
                    AddBotMessage("I didn't catch your name. Could you please tell me your name?");
                    return;
                }
                AddBotMessage($"Hello {userName}! Welcome to the Cybersecurity Chatbot.");
                AddBotMessage("You can ask me about the following topics: password, phishing, safe browsing, privacy, and scam.");
                AddBotMessage("If you want to exit, just type 'exit' or 'quit'. How can I help you today?");
                return;
            }

            if (Regex.IsMatch(input, @"\b(exit|quit|bye|close)\b", RegexOptions.IgnoreCase))
            {
                AddBotMessage("Thank you for chatting! Stay safe and goodbye!");
                UserInputBox.IsEnabled = false;
                return;
            }

            string response = GenerateResponse(input);
            AddBotMessage(response);
            AddBotMessage("Can I help you with anything else?");
        }

        private string ExtractName(string input)
        {
            var words = input.Split(new[] { ' ', ',', '.', '!' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var w in words)
            {
                string lw = w.ToLower();
                if (!ignoreWords.Contains(lw) && Regex.IsMatch(w, @"^[A-Za-z]+$"))
                {
                    return char.ToUpper(w[0]) + w.Substring(1).ToLower();
                }
            }
            return null;
        }

        private string GenerateResponse(string input)
        {
            var words = input.ToLower().Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(w => !ignoreWords.Contains(w))
                .ToList();

            string sentimentResponse = null;
            foreach (var sentimentWord in sentimentResponses.Keys)
            {
                if (words.Contains(sentimentWord))
                {
                    sentimentResponse = sentimentResponses[sentimentWord];
                    break;
                }
            }

            string detectedTopic = null;
            foreach (var topic in topics)
            {
                if (words.Contains(topic) || words.Any(w => w.Contains(topic.Replace(" ", ""))))
                {
                    detectedTopic = topic;
                    break;
                }
            }

            if (detectedTopic == null)
            {
                return "You can only ask things related to cybersecurity topics like password, phishing, safe browsing, privacy, and scam.";
            }

            string topicResponse = GetTopicInfo(detectedTopic);

            if (sentimentResponse != null)
            {
                return $"{sentimentResponse} Here's some information about {detectedTopic}: {topicResponse}";
            }
            else
            {
                return topicResponse;
            }
        }

        private string GetTopicInfo(string topic)
        {
            if (topicInfo.ContainsKey(topic))
            {
                return topicInfo[topic];
            }
            else
            {
                return "Sorry, I don't have information on that topic.";
            }
        }

        private void AddBotMessage(string message)
        {
            AddMessage(message, Brushes.DarkBlue, "Bot");
        }

        private void AddUserMessage(string message)
        {
            AddMessage(message, Brushes.Black, userName ?? "User");
        }

        private void AddMessage(string message, Brush color, string sender)
        {
            var textBlock = new TextBlock
            {
                Text = $"{sender}: {message}",
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                Foreground = color,
                FontWeight = sender == "Bot" ? FontWeights.Bold : FontWeights.Normal
            };

            ChatPanel.Children.Add(textBlock);

            // Scroll to bottom safely
            DependencyObject parent = ChatPanel;
            while (parent != null && parent is not ScrollViewer)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToEnd();
            }
        }
    }
}

