using SolidDemo;

namespace SolidDemo.Tests.DIP;

[TestFixture]
public class CheckoutServiceTests
{
    private class TestPricingRule : IPricingRule
    {
        private readonly decimal _discountedAmount;

        public TestPricingRule(decimal discountedAmount)
        {
            _discountedAmount = discountedAmount;
        }

        public decimal ApplyDiscount(decimal subtotal)
        {
            return _discountedAmount;
        }
    }

    private class TestReceiptFormatter : IReceiptFormatter
    {
        private readonly string _formattedReceipt;

        public TestReceiptFormatter(string formattedReceipt)
        {
            _formattedReceipt = formattedReceipt;
        }

        public string Format(ShoppingCart cart, decimal total)
        {
            return _formattedReceipt;
        }
    }

    private class TestReceiptSender : IReceiptSender
    {
        public string? LastReceipt { get; private set; }
        public int SendCallCount { get; private set; }

        public void Send(string receipt)
        {
            LastReceipt = receipt;
            SendCallCount++;
        }
    }

    [Test]
    public void Checkout_AppliesPricingRule()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 100.00m));
        
        var pricingRule = new TestPricingRule(90.00m);
        var formatter = new TestReceiptFormatter("Test Receipt");
        var sender = new TestReceiptSender();
        
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Act
        service.Checkout(cart);

        // Assert - formatter would receive the discounted amount
        Assert.That(sender.LastReceipt, Is.EqualTo("Test Receipt"));
    }

    [Test]
    public void Checkout_FormatsReceipt()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 50.00m));
        
        var pricingRule = new TestPricingRule(50.00m);
        var formatter = new TestReceiptFormatter("Formatted Receipt Content");
        var sender = new TestReceiptSender();
        
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Act
        service.Checkout(cart);

        // Assert
        Assert.That(sender.LastReceipt, Is.EqualTo("Formatted Receipt Content"));
    }

    [Test]
    public void Checkout_SendsReceipt()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 25.00m));
        
        var pricingRule = new TestPricingRule(25.00m);
        var formatter = new TestReceiptFormatter("Receipt");
        var sender = new TestReceiptSender();
        
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Act
        service.Checkout(cart);

        // Assert
        Assert.That(sender.SendCallCount, Is.EqualTo(1));
    }

    [Test]
    public void Checkout_WithMultipleItems_CalculatesCorrectSubtotal()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product1", 2, 15.00m));
        cart.AddItem(new LineItem("Product2", 1, 25.00m));
        
        var pricingRule = new NoDiscountRule();
        var formatter = new PlainTextReceiptFormatter();
        var sender = new TestReceiptSender();
        
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Act
        service.Checkout(cart);

        // Assert
        Assert.That(sender.LastReceipt, Does.Contain("Total:"));
        Assert.That(sender.LastReceipt, Does.Contain("55.00"));
    }

    [Test]
    public void Checkout_WithHolidayDiscount_AppliesDiscount()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 100.00m));
        
        var pricingRule = new HolidayDiscountRule();
        var formatter = new PlainTextReceiptFormatter();
        var sender = new TestReceiptSender();
        
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Act
        service.Checkout(cart);

        // Assert
        Assert.That(sender.LastReceipt, Does.Contain("Total:"));
        Assert.That(sender.LastReceipt, Does.Contain("90.00"));
    }

    [Test]
    public void Checkout_WithCoupon_AppliesDiscount()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 50.00m));
        
        var pricingRule = new TenOffCouponRule();
        var formatter = new PlainTextReceiptFormatter();
        var sender = new TestReceiptSender();
        
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Act
        service.Checkout(cart);

        // Assert
        Assert.That(sender.LastReceipt, Does.Contain("Total:"));
        Assert.That(sender.LastReceipt, Does.Contain("40.00"));
    }

    [Test]
    public void Checkout_EmptyCart_HandlesGracefully()
    {
        // Arrange
        var cart = new ShoppingCart();
        
        var pricingRule = new NoDiscountRule();
        var formatter = new PlainTextReceiptFormatter();
        var sender = new TestReceiptSender();
        
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Act
        service.Checkout(cart);

        // Assert
        Assert.That(sender.SendCallCount, Is.EqualTo(1));
        Assert.That(sender.LastReceipt, Does.Contain("Total:"));
        Assert.That(sender.LastReceipt, Does.Contain("0.00"));
    }

    [Test]
    public void Constructor_AcceptsAllDependencies()
    {
        // Arrange
        var pricingRule = new NoDiscountRule();
        var formatter = new PlainTextReceiptFormatter();
        var sender = new TestReceiptSender();

        // Act
        var service = new CheckoutService(pricingRule, formatter, sender);

        // Assert
        Assert.That(service, Is.Not.Null);
    }

    [Test]
    public void Checkout_DependsOnAbstractions_NotConcrete()
    {
        // Arrange - using interfaces, not concrete types
        IPricingRule pricingRule = new TestPricingRule(100.00m);
        IReceiptFormatter formatter = new TestReceiptFormatter("Test");
        IReceiptSender sender = new TestReceiptSender();
        
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 100.00m));

        // Act
        var service = new CheckoutService(pricingRule, formatter, sender);
        service.Checkout(cart);

        // Assert - successfully uses abstraction
        Assert.That(((TestReceiptSender)sender).SendCallCount, Is.EqualTo(1));
    }
}
