namespace SolidDemo
{
    internal sealed class EmailReceiptSender : IReceiptSender
    {
        private readonly IEmailGateway _emailGateway;

        public EmailReceiptSender(IEmailGateway emailGateway)
        {
            _emailGateway = emailGateway;
        }

        public void Send(string receipt)
        {
            _emailGateway.SendEmail("Your Receipt", receipt);
        }
    }
}
