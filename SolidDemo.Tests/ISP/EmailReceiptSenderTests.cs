using SolidDemo;

namespace SolidDemo.Tests.ISP;

[TestFixture]
public class EmailReceiptSenderTests
{
    private class TestEmailGateway : IEmailGateway
    {
        public string? LastSubject { get; private set; }
        public string? LastBody { get; private set; }
        public int SendEmailCallCount { get; private set; }

        public void SendEmail(string subject, string body)
        {
            LastSubject = subject;
            LastBody = body;
            SendEmailCallCount++;
        }
    }

    [Test]
    public void Send_CallsEmailGatewayWithCorrectSubject()
    {
        // Arrange
        var gateway = new TestEmailGateway();
        var sender = new EmailReceiptSender(gateway);
        var receipt = "Test Receipt Content";

        // Act
        sender.Send(receipt);

        // Assert
        Assert.That(gateway.LastSubject, Is.EqualTo("Your Receipt"));
    }

    [Test]
    public void Send_CallsEmailGatewayWithCorrectBody()
    {
        // Arrange
        var gateway = new TestEmailGateway();
        var sender = new EmailReceiptSender(gateway);
        var receipt = "Test Receipt Content";

        // Act
        sender.Send(receipt);

        // Assert
        Assert.That(gateway.LastBody, Is.EqualTo(receipt));
    }

    [Test]
    public void Send_CallsEmailGatewayOnce()
    {
        // Arrange
        var gateway = new TestEmailGateway();
        var sender = new EmailReceiptSender(gateway);
        var receipt = "Test Receipt Content";

        // Act
        sender.Send(receipt);

        // Assert
        Assert.That(gateway.SendEmailCallCount, Is.EqualTo(1));
    }

    [Test]
    public void Send_MultipleReceipts_CallsEmailGatewayMultipleTimes()
    {
        // Arrange
        var gateway = new TestEmailGateway();
        var sender = new EmailReceiptSender(gateway);

        // Act
        sender.Send("Receipt 1");
        sender.Send("Receipt 2");
        sender.Send("Receipt 3");

        // Assert
        Assert.That(gateway.SendEmailCallCount, Is.EqualTo(3));
        Assert.That(gateway.LastBody, Is.EqualTo("Receipt 3"));
    }

    [Test]
    public void Constructor_AcceptsIEmailGateway()
    {
        // Arrange
        var gateway = new TestEmailGateway();

        // Act
        var sender = new EmailReceiptSender(gateway);

        // Assert
        Assert.That(sender, Is.Not.Null);
    }
}
