namespace SolidDemo
{
    // SRP: LineItem only models a purchasable item.
    internal sealed record LineItem(string Name, int Quantity, decimal UnitPrice)
    {
        public decimal Total => Quantity * UnitPrice;
    }
}
