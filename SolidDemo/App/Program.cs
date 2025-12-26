namespace SolidDemo
{
    internal static class Program
    {
        private static void Main()
        {
            var cart = new ShoppingCart();
            cart.AddItem(new LineItem("Keyboard", 1, 79.99m));
            cart.AddItem(new LineItem("Mouse", 2, 25.00m));

            // OCP: swap in different pricing rules without changing the cart.
            IPricingRule pricingRule = new HolidayDiscountRule();

            // DIP: app code depends on abstractions, not concrete implementations.
            IReceiptFormatter formatter = new PlainTextReceiptFormatter();
            IReceiptSender sender = new EmailReceiptSender(new ConsoleEmailGateway());

            var checkout = new CheckoutService(pricingRule, formatter, sender);
            checkout.Checkout(cart);
        }
    }
}
