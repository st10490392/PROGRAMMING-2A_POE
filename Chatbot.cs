 using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

public class Chatbot 
 {
    private string userName;

    public void start()
    {
        ShowAsciiArt();
        AssemblyKeyNameAttribute();
        ChatLoop();
    }
private void ShowAsciiArt();
{
    console.ForegroundColor = 
    ConsoleColor.Cyan;

    console.WriteLine("====================================");
              console.WriteLine("  CYBER SECURITY CHATBOT");

    console.WriteLine("=====================================");
                         
    console.ResetColor();
}

private void AskName();
{
    Console.Write("Enter your name:  ");
    userName = Console.ReadLine();
    Console.WriteLine($"Hello {userName}, I am your Cybersecurity Assistant!\n");

}
private void ChatLoop()
{
    while (true)
    {
        Console.Write($"{userName}: ");
        string input = 
        Console.ReadLine().ToLower();

        if (input,Contains("exit"))
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

        else if (input.Contains("Safe browsing"))
        {
            Console.WriteLine("Always check URLs and avoid clicking unkown links.");
        }

        else
        {
            Console.WriteLine("I didn't understand, please rephrase.");
        }
    }
}
 }