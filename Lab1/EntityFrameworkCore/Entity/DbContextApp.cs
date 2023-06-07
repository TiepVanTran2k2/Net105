using Domain.Entities.ApplicationUser;
using Domain.Entities.Bill;
using Domain.Entities.Lab4;
using Domain.Entities.Product;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Entity
{
    public class DbContextApp : IdentityDbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextApp(DbContextOptions<DbContextApp> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Default"));
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<lab4> Lab4 { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<DetailBill> DetailBill { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextApp).Assembly);            
        }
    }
}
