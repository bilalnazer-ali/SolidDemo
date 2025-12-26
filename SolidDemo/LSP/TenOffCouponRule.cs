namespace SolidDemo
{
    internal sealed class TenOffCouponRule : CouponRule
    {
        public override decimal ApplyDiscount(decimal subtotal)
        {
            var discounted = subtotal - 10m;
            return discounted < 0 ? 0 : discounted;
        }
    }
}
