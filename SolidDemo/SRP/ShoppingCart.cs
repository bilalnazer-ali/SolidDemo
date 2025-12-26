using System;
using System.Collections.Generic;

namespace SolidDemo
{
    // SRP: ShoppingCart only manages items and totals.
    internal sealed class ShoppingCart
    {
        private readonly List<LineItem> _items = new();

        public IReadOnlyList<LineItem> Items => _items;

        public void AddItem(LineItem item)
        {
            if (item.Quantity <= 0) throw new ArgumentOutOfRangeException(nameof(item.Quantity));
            if (item.UnitPrice < 0) throw new ArgumentOutOfRangeException(nameof(item.UnitPrice));
            _items.Add(item);
        }

        public decimal Subtotal()
        {
            decimal total = 0;
            foreach (var item in _items)
            {
                total += item.Total;
            }
            return total;
        }
    }
}
