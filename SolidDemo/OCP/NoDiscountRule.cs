namespace SolidDemo
{
    internal sealed class NoDiscountRule : IPricingRule
    {
        public decimal ApplyDiscount(decimal subtotal) => subtotal;
    }
}
