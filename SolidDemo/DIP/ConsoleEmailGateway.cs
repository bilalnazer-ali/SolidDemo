using System;

namespace SolidDemo
{
    internal sealed class ConsoleEmailGateway : IEmailGateway
    {
        public void SendEmail(string subject, string body)
        {
            Console.WriteLine($"Email Subject: {subject}");
            Console.WriteLine(body);
        }
    }
}
