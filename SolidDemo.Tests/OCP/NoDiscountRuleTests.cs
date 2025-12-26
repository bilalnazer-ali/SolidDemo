using SolidDemo;

namespace SolidDemo.Tests.OCP;

[TestFixture]
public class NoDiscountRuleTests
{
    [Test]
    public void ApplyDiscount_ReturnsSubtotalUnchanged()
    {
        // Arrange
        var rule = new NoDiscountRule();
        var subtotal = 100.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(100.00m));
    }

    [Test]
    public void ApplyDiscount_ZeroSubtotal_ReturnsZero()
    {
        // Arrange
        var rule = new NoDiscountRule();
        var subtotal = 0m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void ApplyDiscount_LargeSubtotal_ReturnsSubtotalUnchanged()
    {
        // Arrange
        var rule = new NoDiscountRule();
        var subtotal = 1000.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(1000.00m));
    }
}
