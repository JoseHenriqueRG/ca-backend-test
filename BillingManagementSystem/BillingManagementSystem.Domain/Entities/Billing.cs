using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BillingManagementSystem.Domain.Entities
{
    [Table("Billing")]
    public class Billing
    {
        [Key]
        public int Id { get; set; }

        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("due_date")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal? TotalAmount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("lines")]
        public List<BillingLines> BillingLines { get; set; }

    }
}
