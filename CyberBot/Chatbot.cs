// See https://aka.ms/new-console-template for more information

using System.Diagnostics;

namespace CyberBot
{
    public class NewBaseType
    {
        public void Start()
        {
            PlayGreeting(); // ADD AUDIO
            ShowAsciiArt();
            AskName();
            ChatLoop();
        }

        private void AskName()
        {
            throw new NotImplementedException();
        }

        private void ChatLoop()
        {
            throw new NotImplementedException();
        }

        private void ShowAsciiArt()
        {
            throw new NotImplementedException();
        }

        private void PlayGreeting()
        {
            throw new NotImplementedException();
        }
    }

    public class Chatbot : NewBaseType
    {
        private string userName = "User";

        public void Start()
        {
            ShowAsciiArt();
            AskName();
            ChatLoop();
        }

        private void ShowAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======================================");
            Console.WriteLine("      CYBER SECURITY CHATBOT");
            Console.WriteLine("======================================");
            Console.ResetColor();
        }

        private void AskName()
        {
            Console.Write("Enter your name: ");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                userName = input;

            Console.WriteLine($"Hello {userName}, I am your Cybersecurity Assistant!\n");
        }

        private void ChatLoop()
        {
            while (true)
            {
                Console.Write($"{userName}: ");
                string input = Console.ReadLine()?.ToLower() ?? "";

                if (input.Contains("exit"))
                {
                    Console.WriteLine("Goodbye! Stay safe online.");
                    break;
                }
                else if (input.Contains("how are you"))
                {
                    Console.WriteLine("I'm functioning perfectly and ready to help you!");
                }
                else if (input.Contains("purpose"))
                {
                    Console.WriteLine("My purpose is to help you stay safe online.");
                }
                else if (input.Contains("password"))
                {
                    Console.WriteLine("Use strong passwords with numbers, symbols, and avoid sharing them.");
                }
                else if (input.Contains("phishing"))
                {
                    Console.WriteLine("Be careful of suspicious emails asking for personal information.");
                }
                else if (input.Contains("safe browsing"))
                {
                    Console.WriteLine("Always check URLs and avoid clicking unknown links.");
                }

                else if (input.Contains("virus"))
                {
                    Console.WriteLine("Install antivirus software and keep it updated.");
                }
                else if (input.Contains("vpn"))
                {
                    Console.WriteLine("A vpn helps protect your privacy online");
                }
                else
                {
                    Console.WriteLine("I didn’t understand, please rephrase.");
                }

               
            }
        }
         
                private void PlayGreeting()
                {
                    try{
                        Process.Start("aplay", "sounds/welcome.wav");
                    }
                    catch
                    {
                        Console.WriteLine("(sound could not be played)");
                    }
                }
    }
}