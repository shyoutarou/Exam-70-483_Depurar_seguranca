using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Debug_Assert
{
    class Program
    {
        static void Main(string[] args)
        {
            var items = Enumerable.Range(1, 150);
            Debug.Assert(items.ToArray().Length <= 100);
        }

        private void PrintInvoice(string customerName, string customerAddress, List<int> items)
        {
            // Validate inputs.
            // Validate customer name.
            if (string.IsNullOrWhiteSpace(customerName))
                throw new ArgumentNullException("Customer name is missing.");
            // Validate customer address.
            if (string.IsNullOrWhiteSpace(customerAddress))
                throw new ArgumentNullException("Customer address is missing.");
            // Validate item quantities and unit prices.
            foreach (int item in items)
            {
                Debug.Assert(item <= 100,
                "OrderItem " + item + ", quantity is larger than 100.");
                Debug.Assert(item <= 100, "OrderItem " + item +
                ", unit price is larger than $100.00.");
            }
            // Print the invoice.
        }
    }
}
