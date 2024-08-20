﻿using BillingFlowManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingFlowManager.Domain.Interfaces
{
    public interface IRepositoryBilling
    {
        Task<bool> Insert(Billing billing);
        Task<IList<Billing>> GetAll();
    }
}
