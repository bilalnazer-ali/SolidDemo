using SolidDemo;

namespace SolidDemo.Tests.LSP;

[TestFixture]
public class TenOffCouponRuleTests
{
    [Test]
    public void ApplyDiscount_SubtotalGreaterThan10_Reduces10Dollars()
    {
        // Arrange
        var rule = new TenOffCouponRule();
        var subtotal = 50.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(40.00m));
    }

    [Test]
    public void ApplyDiscount_SubtotalEquals10_ReturnsZero()
    {
        // Arrange
        var rule = new TenOffCouponRule();
        var subtotal = 10.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void ApplyDiscount_SubtotalLessThan10_ReturnsZero()
    {
        // Arrange
        var rule = new TenOffCouponRule();
        var subtotal = 5.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void ApplyDiscount_LargeSubtotal_Reduces10Dollars()
    {
        // Arrange
        var rule = new TenOffCouponRule();
        var subtotal = 1000.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(990.00m));
    }

    [Test]
    public void ApplyDiscount_NegativeResult_ReturnsZero()
    {
        // Arrange
        var rule = new TenOffCouponRule();
        var subtotal = 1.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public void TenOffCouponRule_InheritsFromCouponRule()
    {
        // Arrange
        var rule = new TenOffCouponRule();

        // Act & Assert
        Assert.That(rule, Is.InstanceOf<CouponRule>());
    }

    [Test]
    public void TenOffCouponRule_ImplementsIPricingRule()
    {
        // Arrange
        var rule = new TenOffCouponRule();

        // Act & Assert
        Assert.That(rule, Is.InstanceOf<IPricingRule>());
    }

    [Test]
    public void ApplyDiscount_CanBeUsedAsIPricingRule()
    {
        // Arrange
        IPricingRule rule = new TenOffCouponRule();
        var subtotal = 25.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(15.00m));
    }

    [Test]
    public void ApplyDiscount_CanBeUsedAsCouponRule()
    {
        // Arrange
        CouponRule rule = new TenOffCouponRule();
        var subtotal = 30.00m;

        // Act
        var result = rule.ApplyDiscount(subtotal);

        // Assert
        Assert.That(result, Is.EqualTo(20.00m));
    }
}
