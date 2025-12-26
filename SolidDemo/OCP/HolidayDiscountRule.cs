namespace SolidDemo
{
    internal sealed class HolidayDiscountRule : IPricingRule
    {
        public decimal ApplyDiscount(decimal subtotal)
        {
            if (subtotal >= 100m) return subtotal * 0.90m;
            return subtotal;
        }
    }
}
