using BillingManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingManagementSystem.Domain.ViewModels
{
    public class BillingViewModel
    {
        public string InvoiceNumber { get; set; }

        public Customer Customer { get; set; }

        public DateTime DueDate { get; set; }

        [StringLength(3, ErrorMessage = "O campo Currency deve ser uma string com comprimento de 3 caracteres.")]
        public string Currency { get; set; }

        public decimal TotalAmount { get; set; }

        public List<BillingLineViewModel> BillingLines { get; set; }

        public decimal CalculateTotalAmount()
        {
            TotalAmount = BillingLines.Sum(b => b.Subtotal);

            return TotalAmount;
        }
    }
}
