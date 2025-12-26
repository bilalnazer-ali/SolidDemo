namespace SolidDemo
{
    // LSP: Derived rules can replace base implementations safely.
    internal abstract class CouponRule : IPricingRule
    {
        public abstract decimal ApplyDiscount(decimal subtotal);
    }
}
