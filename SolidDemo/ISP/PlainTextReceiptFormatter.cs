using System;
using System.Collections.Generic;

namespace SolidDemo
{
    internal sealed class PlainTextReceiptFormatter : IReceiptFormatter
    {
        public string Format(ShoppingCart cart, decimal total)
        {
            var lines = new List<string>
            {
                "Receipt",
                "-------"
            };

            foreach (var item in cart.Items)
            {
                lines.Add($"{item.Name} x{item.Quantity}: {item.Total:C}");
            }

            lines.Add($"Total: {total:C}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
