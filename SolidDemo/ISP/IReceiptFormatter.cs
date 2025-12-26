namespace SolidDemo
{
    // ISP: Separate small interfaces for formatting and sending.
    internal interface IReceiptFormatter
    {
        string Format(ShoppingCart cart, decimal total);
    }
}
