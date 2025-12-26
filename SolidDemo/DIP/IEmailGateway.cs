namespace SolidDemo
{
    internal interface IEmailGateway
    {
        void SendEmail(string subject, string body);
    }
}
