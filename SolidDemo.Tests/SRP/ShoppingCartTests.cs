using SolidDemo;

namespace SolidDemo.Tests.SRP;

[TestFixture]
public class ShoppingCartTests
{
    [Test]
    public void AddItem_ValidItem_AddsToCart()
    {
        // Arrange
        var cart = new ShoppingCart();
        var item = new LineItem("Product", 1, 10.00m);

        // Act
        cart.AddItem(item);

        // Assert
        Assert.That(cart.Items, Has.Count.EqualTo(1));
        Assert.That(cart.Items[0], Is.EqualTo(item));
    }

    [Test]
    public void AddItem_MultipleItems_AddsAllToCart()
    {
        // Arrange
        var cart = new ShoppingCart();
        var item1 = new LineItem("Product1", 2, 15.00m);
        var item2 = new LineItem("Product2", 1, 25.00m);

        // Act
        cart.AddItem(item1);
        cart.AddItem(item2);

        // Assert
        Assert.That(cart.Items, Has.Count.EqualTo(2));
        Assert.That(cart.Items[0], Is.EqualTo(item1));
        Assert.That(cart.Items[1], Is.EqualTo(item2));
    }

    [Test]
    public void AddItem_ZeroQuantity_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var cart = new ShoppingCart();
        var item = new LineItem("Product", 0, 10.00m);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem(item));
    }

    [Test]
    public void AddItem_NegativeQuantity_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var cart = new ShoppingCart();
        var item = new LineItem("Product", -1, 10.00m);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem(item));
    }

    [Test]
    public void AddItem_NegativePrice_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var cart = new ShoppingCart();
        var item = new LineItem("Product", 1, -5.00m);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem(item));
    }

    [Test]
    public void Subtotal_EmptyCart_ReturnsZero()
    {
        // Arrange
        var cart = new ShoppingCart();

        // Act
        var subtotal = cart.Subtotal();

        // Assert
        Assert.That(subtotal, Is.EqualTo(0m));
    }

    [Test]
    public void Subtotal_SingleItem_ReturnsCorrectTotal()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 2, 10.00m));

        // Act
        var subtotal = cart.Subtotal();

        // Assert
        Assert.That(subtotal, Is.EqualTo(20.00m));
    }

    [Test]
    public void Subtotal_MultipleItems_ReturnsCorrectTotal()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product1", 2, 15.00m));
        cart.AddItem(new LineItem("Product2", 1, 25.00m));
        cart.AddItem(new LineItem("Product3", 3, 10.00m));

        // Act
        var subtotal = cart.Subtotal();

        // Assert
        Assert.That(subtotal, Is.EqualTo(85.00m)); // 30 + 25 + 30
    }

    [Test]
    public void Items_ReturnsReadOnlyList()
    {
        // Arrange
        var cart = new ShoppingCart();
        cart.AddItem(new LineItem("Product", 1, 10.00m));

        // Act
        var items = cart.Items;

        // Assert
        Assert.That(items, Is.InstanceOf<IReadOnlyList<LineItem>>());
    }
}
