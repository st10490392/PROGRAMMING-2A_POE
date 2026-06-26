// See https://aka.ms/new-console-template for more information

using System;
using CyberBot;

internal class NewBaseType
{
    static void Main(string[] args)
    {
        Chatbot bot = new Chatbot();
        bot.Start();
    }
}

class Program : NewBaseType
{
} 
