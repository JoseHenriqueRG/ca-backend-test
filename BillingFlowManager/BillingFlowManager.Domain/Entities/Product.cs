using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BillingFlowManager.Domain.Entities
{
    [Table("Product")]
    public class Product
    {
        [JsonPropertyName("productId")]
        public Guid? ProductId { get; set; }

        [Required(ErrorMessage = "O campo Description não pode ser nulo nem vazio.")]
        [MaxLength(200)]
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product? x, Product? y)
        {
            if (Object.ReferenceEquals(x, y)) return true;

            if (x is null || y is null)
                return false;

            return x.ProductId == y.ProductId && x.Description == y.Description;
        }

        public int GetHashCode([DisallowNull] Product obj)
        {
            if (obj is null) return 0;

            int hashProductDescription = obj.Description is null ? 0 : obj.Description.GetHashCode();

            int hashProductId = obj.ProductId.GetHashCode();

            return hashProductDescription ^ hashProductId;

        }
    }
}
