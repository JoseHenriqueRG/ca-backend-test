using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BillingFlowManager.Domain.Entities
{
    public class BillingLines
    {
        [Key]
        public int Id { get; set; }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal? UnitPrice { get; set; }

        [JsonPropertyName("subtotal")]
        public decimal? Subtotal { get; set; }

        public void CalculateSubtotal()
        {
            if (UnitPrice < 0) throw new ArgumentException("UnitPrice cannot be negative.");

            if (Quantity < 0) throw new ArgumentException("Quantity cannot be negative.");

            Subtotal = UnitPrice * Quantity;
        }
    }
}
