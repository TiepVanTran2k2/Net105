using Domain.Entities.Department;
using Domain.Entities.Employee;
using Domain.Entities.Information;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Entity
{
    public class DbContextApp : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextApp(DbContextOptions<DbContextApp> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextApp).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("Default"));
        }

        public DbSet<Information> Information { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

    }
}
