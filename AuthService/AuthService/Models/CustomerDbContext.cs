﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Models
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Customer> Customers { get; set; }
    }
}
