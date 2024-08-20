using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingFlowManager.Domain.Interfaces
{
    public interface IRepository <T> where T : class
    {
        Task<T> Get(string id);
        Task<IList<T>> GetAll();
        Task<bool> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Exist(string id);
    }
}
