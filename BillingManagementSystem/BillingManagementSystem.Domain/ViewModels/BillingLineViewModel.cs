using BillingManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingManagementSystem.Domain.ViewModels
{
    public class BillingLineViewModel
    {
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Subtotal { get; set; }

        public void CalculateSubtotal()
        {
            if (UnitPrice < 0) throw new ArgumentException("UnitPrice cannot be negative.");

            if (Quantity < 0) throw new ArgumentException("Quantity cannot be negative.");

            Subtotal = UnitPrice * Quantity;
        }
    }
}
