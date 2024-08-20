using BillingManagementSystem.Domain.Entities;
using BillingManagementSystem.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingManagementSystem.Domain.Interfaces
{
    public interface IBillingApplication
    {
        Task<ActionResult<BillingViewModel>> Insert(BillingViewModel billing);
        Task<ActionResult<IList<BillingViewModel>>> GetAll();
    }
}
