using BillingManagementSystem.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingManagementSystem.Domain.Interfaces
{
    public interface ICrudApplication<T> where T : class
    {
        Task<ActionResult<T>> Get(string id);
        Task<ActionResult<IList<T>>> GetAll();
        Task<ActionResult<T>> Insert(T entity);
        Task<ActionResult<T>> Update(T entity);
        Task<ActionResult<T>> Delete(T entity);
    }
}
