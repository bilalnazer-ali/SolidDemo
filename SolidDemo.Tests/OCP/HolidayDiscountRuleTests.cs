using SolidDemo;

namespace SolidDemo.Tests.OCP;

[TestFixture]
public class HolidayDiscountRuleTests
{
    [Test]
    public void ApplyDiscount_SubtotalLessThan100_ReturnsSubtotalUnchanged()
    {
        // Arrange
        var rule = new HolidayDiscountRule();
        var subtotal = 99.99m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(99.99m));
    }

    [Test]
    public void ApplyDiscount_SubtotalEquals100_Applies10PercentDiscount()
    {
        // Arrange
        var rule = new HolidayDiscountRule();
        var subtotal = 100.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(90.00m));
    }

    [Test]
    public void ApplyDiscount_SubtotalGreaterThan100_Applies10PercentDiscount()
    {
        // Arrange
        var rule = new HolidayDiscountRule();
        var subtotal = 150.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(135.00m));
    }

    [Test]
    public void ApplyDiscount_LargeSubtotal_Applies10PercentDiscount()
    {
        // Arrange
        var rule = new HolidayDiscountRule();
        var subtotal = 1000.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(900.00m));
    }

    [Test]
    public void ApplyDiscount_SmallSubtotal_ReturnsSubtotalUnchanged()
    {
        // Arrange
        var rule = new HolidayDiscountRule();
        var subtotal = 50.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(50.00m));
    }
}
