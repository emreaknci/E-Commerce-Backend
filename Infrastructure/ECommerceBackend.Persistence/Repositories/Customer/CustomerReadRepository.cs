﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceBackend.Application.Repositories;
using ECommerceBackend.Domain.Entities.Concrete;
using ECommerceBackend.Persistence.Contexts;

namespace ECommerceBackend.Persistence.Repositories
{
    public class CustomerReadRepository : ReadRepository<Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ECommerceBackendDbContext context) : base(context)
        {
        }
    }
}
