namespace SolidDemo
{
    internal sealed class CheckoutService
    {
        private readonly IPricingRule _pricingRule;
        private readonly IReceiptFormatter _formatter;
        private readonly IReceiptSender _sender;

        public CheckoutService(
            IPricingRule pricingRule,
            IReceiptFormatter formatter,
            IReceiptSender sender)
        {
            _pricingRule = pricingRule;
            _formatter = formatter;
            _sender = sender;
        }

        public void Checkout(ShoppingCart cart)
        {
            var subtotal = cart.Subtotal();
            var total = _pricingRule.ApplyDiscount(subtotal);
            var receipt = _formatter.Format(cart, total);
            _sender.Send(receipt);
        }
    }
}
