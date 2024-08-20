using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BillingManagementSystem.Domain.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [JsonPropertyName("id")]
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O campo Name não pode ser nulo nem vazio.")]
        [MaxLength(100)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo Email não pode ser nulo nem vazio.")]
        [EmailAddress(ErrorMessage = "O endereço de e-mail fornecido não é válido.")]
        [MaxLength(200)]
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Address não pode ser nulo nem vazio.")]
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }

    public class CustomerComparer : IEqualityComparer<Customer>
    {
        public bool Equals(Customer? x, Customer? y)
        {
            if (ReferenceEquals(x, y)) return true;

            if (x is null || y is null)
                return false;

            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] Customer obj)
        {
            if (obj is null) return 0;

            int hashCustomerName = obj.Name is null ? 0 : obj.Name.GetHashCode();

            int hashCustomerId = obj.Id.GetHashCode();

            return hashCustomerName ^ hashCustomerId;

        }
    }
}
