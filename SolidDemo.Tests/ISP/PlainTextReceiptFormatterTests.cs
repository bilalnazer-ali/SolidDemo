using SolidDemo;

namespace SolidDemo.Tests.ISP;

[TestFixture]
public class PlainTextReceiptFormatterTests
{
    [Test]
    public void Format_EmptyCart_ReturnsReceiptWithZeroTotal()
    {
        // Arrange
        var formatter = new PlainTextReceiptFormatter();
        var cart = new ShoppingCart();
        var total = 0m;

        // Act
        var receipt = formatter.Format(cart, total);

        // Assert
        Assert.That(receipt, Does.Contain("Receipt"));
        Assert.That(receipt, Does.Contain("-------"));
        Assert.That(receipt, Does.Contain("Total:"));
        Assert.That(receipt, Does.Contain("0.00"));
    }

    [Test]
    public void Format_CartWithSingleItem_IncludesItemInReceipt()
    {
        // Arrange
        var formatter = new PlainTextReceiptFormatter();
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Keyboard", 1, 79.99m));
        var total = 79.99m;

        // Act
        var receipt = formatter.Format(cart, total);

        // Assert
        Assert.That(receipt, Does.Contain("Receipt"));
        Assert.That(receipt, Does.Contain("Keyboard x1:"));
        Assert.That(receipt, Does.Contain("79.99"));
        Assert.That(receipt, Does.Contain("Total:"));
    }

    [Test]
    public void Format_CartWithMultipleItems_IncludesAllItemsInReceipt()
    {
        // Arrange
        var formatter = new PlainTextReceiptFormatter();
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Keyboard", 1, 79.99m));
        cart.AddItem(new LineItem("Mouse", 2, 25.00m));
        var total = 129.99m;

        // Act
        var receipt = formatter.Format(cart, total);

        // Assert
        Assert.That(receipt, Does.Contain("Receipt"));
        Assert.That(receipt, Does.Contain("Keyboard x1:"));
        Assert.That(receipt, Does.Contain("79.99"));
        Assert.That(receipt, Does.Contain("Mouse x2:"));
        Assert.That(receipt, Does.Contain("50.00"));
        Assert.That(receipt, Does.Contain("Total:"));
        Assert.That(receipt, Does.Contain("129.99"));
    }

    [Test]
    public void Format_WithDiscount_ShowsDiscountedTotal()
    {
        // Arrange
        var formatter = new PlainTextReceiptFormatter();
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 100.00m));
        var total = 90.00m; // 10% discount applied

        // Act
        var receipt = formatter.Format(cart, total);

        // Assert
        Assert.That(receipt, Does.Contain("Product x1:"));
        Assert.That(receipt, Does.Contain("100.00"));
        Assert.That(receipt, Does.Contain("Total:"));
        Assert.That(receipt, Does.Contain("90.00"));
    }

    [Test]
    public void Format_ReturnsStringWithNewLines()
    {
        // Arrange
        var formatter = new PlainTextReceiptFormatter();
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 10.00m));
        var total = 10.00m;

        // Act
        var receipt = formatter.Format(cart, total);

        // Assert
        Assert.That(receipt, Does.Contain(Environment.NewLine));
    }

    [Test]
    public void Format_ReturnsNonEmptyString()
    {
        // Arrange
        var formatter = new PlainTextReceiptFormatter();
        var cart = new ShoppingCart();
        var total = 0m;

        // Act
        var receipt = formatter.Format(cart, total);

        // Assert
        Assert.That(receipt, Is.Not.Empty);
    }
}
