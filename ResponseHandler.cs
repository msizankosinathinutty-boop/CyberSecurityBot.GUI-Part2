using CybersecurityAwarenessBot.GUI;
using System;
using System.Collections.Generic;

namespace CyberSecurityBot.GUI
{
    public delegate string ChatResponseDelegate(string input);

    public class ResponseHandler
    {
        private Random random = new Random();

        private string currentTopic = "";

        private UserProfile userProfile = new UserProfile();

        private string userName = "";

        private List<string> passwordTips = new List<string>
        {
            "Use a loong, unique password for every account.",
            "Consider using a password manager to store strong passwords.",
            "Never share your passwords with other people."
        };

        private List<string> phishingTips = new List<string>
        {
                "Check the sender's email address before clicking a link.",
                "Be careeful with messages that create urgency or threaten you.",
                "Never provide your password through an unexpected email link."
        };

        private List<string> privacyTips = new List<string>
        {
            "Review the privacy settings oon your social media accounts.",
            "Avoid sharing your location publicly unless necessary.",
            "Think carefully before postinbg personnal information online."
        };


        public string GetResponse(string input)
        {
            string message = input.Trim().ToLower();


            if (message.Contains("my name is "))
            {
                string name = input.Substring(
                    input.ToLower().IndexOf("my name is ") + 11
                ).Trim();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    SetUserName(name);
                    return "Nice to meet you, " + userName +
                           "! I'll remember your name during this conversation.";
                }
            }

            if (message.Contains("what is my name") ||
                message.Contains("what's my name"))
            {
                if (userProfile.Name != "")
                {
                    return "Your name is " + userProfile.Name + ".";
                }

                return "You haven't told me your name yet. Type: My name is [your name].";
            }

            if (message.Contains("i am interested in") ||
                message.Contains("i like"))
            {
                string topic = "";

                if (message.Contains("privacy"))
                    topic = "privacy";
                else if (message.Contains("password"))
                    topic = "password safety";
                else if (message.Contains("phishing") ||
                         message.Contains("scam"))
                    topic = "phishing and scams";

                if (topic != "")
                {
                    userProfile.FavouriteTopic = topic;
                    currentTopic = topic == "password safety"
                        ? "password"
                        : topic == "privacy"
                            ? "privacy"
                            : "phishing";

                    return "Great! I'll remember that you're interested in " +
                           topic + ".";
                }
            }

            if (message.Contains("what is my favourite topic") ||
                message.Contains("what's my favourite topic"))
            {
                if (userProfile.FavouriteTopic != "")
                {
                    return "Your favourite topic is " +
                           userProfile.FavouriteTopic + ".";
                }

                return "You haven't chosen a favourite topic yet. Tell me what interests you.";
            }


            // Handle follow-up questions about the previous topic.
            if (message.Contains("tell me more") ||
                message.Contains("explain more") ||
                message.Contains("another tip") ||
                message.Contains("more information"))
            {
                if (currentTopic == "password")
                {
                    return GetRandomTip(passwordTips);
                }
                else if (currentTopic == "phishing")
                {
                    return GetRandomTip(phishingTips);
                }
                else if (currentTopic == "privacy")
                {
                    return GetRandomTip(privacyTips);
                }
                else
                {
                    return "Which topic would you like to know more about: passwords, phishing or privacy?";
                }
            }

            // Recognise a new topic and remember it.
            if (message.Contains("password"))
            {
                currentTopic = "password";
                return GetRandomTip(passwordTips);
            }
            else if (message.Contains("scam") ||
                     message.Contains("phishing"))
            {
                currentTopic = "phishing";
                return GetRandomTip(phishingTips);
            }
            else if (message.Contains("privacy"))
            {
                currentTopic = "privacy";
                return GetRandomTip(privacyTips);
            }
            else if (message.Contains("hello") ||
                     message.Contains("hi"))
            {
                return "Hello! Ask me about passwords, scams or online privacy.";
            }
            else if (message.Contains("how are you"))
            {
                return "I'm ready to help you stay safe online!";
            }
            else
            {
                return "I'm not sure I understand. Try asking about passwords, scams, phishing or privacy.";
            }
        }


        public string DetectSentiment(string input)
        {
            string message = input.ToLower();

            if (message.Contains("worried") ||
                message.Contains("scared") ||
                message.Contains("afraid") ||
                message.Contains("anxious"))
            {
                return "worried";
            }
            else if (message.Contains("frustrated") ||
                     message.Contains("angry") ||
                     message.Contains("annoyed"))
            {
                return "frustrated";
            }
            else if (message.Contains("curious") ||
                     message.Contains("interested") ||
                     message.Contains("wondering"))
            {
                return "curious";
            }
            else
            {
                return "neutral";
            }
        }


        public string GetSentimentResponse(string input)
        {
            string sentiment = DetectSentiment(input);
            string message = input.ToLower();

            string topic = "";

            if (message.Contains("password"))
                topic = "password";
            else if (message.Contains("scam") ||
                     message.Contains("phishing"))
                topic = "phishing";
            else if (message.Contains("privacy"))
                topic = "privacy";
            else
                topic = currentTopic;

            if (sentiment == "worried")
            {
                return "It's understandable to feel worried. Let's take it one step at a time. " +
                       GetTipForTopic(topic);
            }
            else if (sentiment == "frustrated")
            {
                return "I understand this can be frustrating. Let's keep it simple. " +
                       GetTipForTopic(topic);
            }
            else if (sentiment == "curious")
            {
                return "Great question! Here's something useful to know. " +
                       GetTipForTopic(topic);
            }

            return "";
        }

        private string GetTipForTopic(string topic)
        {
            if (topic == "password")
                return GetRandomTip(passwordTips);
            else if (topic == "phishing")
                return GetRandomTip(phishingTips);
            else if (topic == "privacy")
                return GetRandomTip(privacyTips);
            else
                return "Start by checking the security settings on your important online accounts.";
        }


        public void SetUserName(string name)
        {
            userName = name.Trim();
            userProfile.Name = userName;
        }


        private string GetRandomTip(List<string> tips)
            {
            int index = random.Next(tips.Count);
            return tips[index];
            }
    
        
    }


}
