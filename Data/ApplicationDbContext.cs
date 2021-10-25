using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaMielApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LaMielApp.Models.Contactanos> DataContactanos { get; set; }

        public DbSet<LaMielApp.Models.Product> DataProduct {get; set;}

        public DbSet<LaMielApp.Models.Proforma> DataProforma {get; set;}
    }
}
