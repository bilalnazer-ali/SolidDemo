using System.Reflection;
using NetArchTest.Rules;
using NUnit.Framework;
using SolidDemo;

namespace SolidDemo.ArchTests;

public class ArchitectureTests
{
    private static Assembly SolidDemoAssembly => typeof(ShoppingCart).Assembly;

    [Test]
    public void PricingRules_Should_Implement_IPricingRule()
    {
        var ruleTypes = Types.InAssembly(SolidDemoAssembly)
            .That().AreClasses().And().HaveNameEndingWith("Rule")
            .GetTypes()
            .Where(type => !type.IsAbstract);

        Assert.That(ruleTypes, Is.All.Matches<Type>(type => typeof(IPricingRule).IsAssignableFrom(type)));
    }

    [Test]
    public void CheckoutService_Should_Not_Depend_On_Concrete_Implementations()
    {
        var forbidden = new[]
        {
            typeof(HolidayDiscountRule).FullName!,
            typeof(NoDiscountRule).FullName!,
            typeof(TenOffCouponRule).FullName!,
            typeof(PlainTextReceiptFormatter).FullName!,
            typeof(EmailReceiptSender).FullName!,
            typeof(ConsoleEmailGateway).FullName!
        };

        var result = Types.InAssembly(SolidDemoAssembly)
            .That().HaveName(nameof(CheckoutService))
            .Should().NotHaveDependencyOnAny(forbidden)
            .GetResult();

        Assert.That(result.IsSuccessful, Is.True);
    }

    [Test]
    public void EmailReceiptSender_Should_Not_Depend_On_ConsoleGateway()
    {
        var result = Types.InAssembly(SolidDemoAssembly)
            .That().HaveName(nameof(EmailReceiptSender))
            .Should().NotHaveDependencyOnAny(new[] { typeof(ConsoleEmailGateway).FullName! })
            .GetResult();

        Assert.That(result.IsSuccessful, Is.True);
    }

    [Test]
    public void ShoppingCart_Should_Not_Depend_On_Receipt_Or_Delivery()
    {
        var forbidden = new[]
        {
            typeof(IReceiptFormatter).FullName!,
            typeof(IReceiptSender).FullName!,
            typeof(IEmailGateway).FullName!,
            typeof(PlainTextReceiptFormatter).FullName!,
            typeof(EmailReceiptSender).FullName!,
            typeof(ConsoleEmailGateway).FullName!,
            typeof(CheckoutService).FullName!
        };

        var result = Types.InAssembly(SolidDemoAssembly)
            .That().HaveName(nameof(ShoppingCart))
            .Should().NotHaveDependencyOnAny(forbidden)
            .GetResult();

        Assert.That(result.IsSuccessful, Is.True);
    }
}
