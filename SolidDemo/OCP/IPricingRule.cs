namespace SolidDemo
{
    // OCP: Add new pricing rules without changing CheckoutService or ShoppingCart.
    internal interface IPricingRule
    {
        decimal ApplyDiscount(decimal subtotal);
    }
}
