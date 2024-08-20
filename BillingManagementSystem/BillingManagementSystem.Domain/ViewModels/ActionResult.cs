using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingManagementSystem.Domain.ViewModels
{
    public class ActionResult<T> where T : class
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Value { get; set; }
    }
}
