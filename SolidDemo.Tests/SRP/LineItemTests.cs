using SolidDemo;

namespace SolidDemo.Tests.SRP;

[TestFixture]
public class LineItemTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesLineItem()
    {
        // Arrange & Act
        var item = new LineItem("Product", 2, 10.00m);

        // Assert
        Assert.That(item.Name, Is.EqualTo("Product"));
        Assert.That(item.Quantity, Is.EqualTo(2));
        Assert.That(item.UnitPrice, Is.EqualTo(10.00m));
    }

    [Test]
    public void Total_CalculatesCorrectly()
    {
        // Arrange
        var item = new LineItem("Product", 3, 15.50m);

        // Act
        var total = item.Total;

        // Assert
        Assert.That(total, Is.EqualTo(46.50m));
    }

    [Test]
    public void Total_SingleQuantity_ReturnsUnitPrice()
    {
        // Arrange
        var item = new LineItem("Product", 1, 25.00m);

        // Act
        var total = item.Total;

        // Assert
        Assert.That(total, Is.EqualTo(25.00m));
    }

    [Test]
    public void Total_ZeroPrice_ReturnsZero()
    {
        // Arrange
        var item = new LineItem("Product", 5, 0m);

        // Act
        var total = item.Total;

        // Assert
        Assert.That(total, Is.EqualTo(0m));
    }

    [Test]
    public void Equality_SameValues_AreEqual()
    {
        // Arrange
        var item1 = new LineItem("Product", 2, 10.00m);
        var item2 = new LineItem("Product", 2, 10.00m);

        // Act & Assert
        Assert.That(item1, Is.EqualTo(item2));
    }

    [Test]
    public void Equality_DifferentValues_AreNotEqual()
    {
        // Arrange
        var item1 = new LineItem("Product1", 2, 10.00m);
        var item2 = new LineItem("Product2", 2, 10.00m);

        // Act & Assert
        Assert.That(item1, Is.Not.EqualTo(item2));
    }
}
