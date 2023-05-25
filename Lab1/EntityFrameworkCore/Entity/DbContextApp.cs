using Domain.Entities.ApplicationUser;
using Domain.Entities.Department;
using Domain.Entities.Employee;
using Domain.Entities.Information;
using Domain.Entities.Lab2;
using Domain.Entities.Lab3;
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

        public DbSet<Information> Information { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<ThanNhan> ThanNhan { get; set; }
        public DbSet<PersonCompanies> PersonCompanies { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<StudentInformation> StudentInformation { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DbContextApp).Assembly);

            modelBuilder.Entity<Address>(a =>
            {
                a.ToTable("Address");
                a.HasKey(x => x.Addr_ID);
                a.Property(q => q.Home_addr).IsRequired().HasMaxLength(20);
                a.Property(q => q.Office_addr).HasColumnType("varchar").HasMaxLength(50);
                a.HasOne(x => x.Client).WithOne(x => x.Address).HasForeignKey<Address>(c => c.Addr_ID);
            });
            modelBuilder.Entity<Client>(a =>
            {
                a.ToTable("Client");
                a.HasKey(a => a.Address_ID);
                a.Property(a => a.ClientName).IsRequired().HasMaxLength(50);
                a.Property(a => a.PhoneNO).IsRequired(false).HasMaxLength(10).HasColumnType("varchar");
                a.HasOne(x => x.Address).WithOne(x => x.Client).HasForeignKey<Client>(c => c.Address_ID);
            });
            modelBuilder.Entity<NhanVien>(a =>
            {
                a.ToTable("NhanVien");
                a.HasKey(a => a.MaNV);
                a.Property(a => a.HoNV).HasMaxLength(15).IsRequired(false);
                a.Property(a => a.TenLot).HasMaxLength(15).IsRequired(false);
                a.Property(a => a.TenNV).HasMaxLength(15).IsRequired(false);
                a.Property(a => a.DiaChi).HasMaxLength(30).IsRequired(false);
                a.Property(a => a.Phai).HasMaxLength(3).IsRequired(false);
                a.Property(a => a.NgaySinh).IsRequired(false);
                a.Property(a => a.Luong).HasColumnType("float").IsRequired(false);
                a.Property(a => a.Ma_NQL).HasMaxLength(9).HasColumnType("varchar");
                a.Property(a => a.PHG).IsRequired();
                a.HasMany(a => a.ThanNhan).WithOne(a => a.NhanVien);
            });
            modelBuilder.Entity<ThanNhan>(a =>
            {
                a.ToTable("ThanNhan");
                a.HasKey(a => a.Ma_NVien);
                a.HasKey(a => a.TenNV);
                a.Property(a => a.TenNV).HasMaxLength(15);
                a.Property(a => a.Phai).HasMaxLength(3).IsRequired(false);
                a.Property(a => a.NgaySinh).IsRequired(false);
                a.Property(a => a.QuanHe).HasMaxLength(15).IsRequired(false);
                a.HasOne(x => x.NhanVien).WithMany(x => x.ThanNhan).HasForeignKey(x => x.Ma_NVien);
            });
            modelBuilder.Entity<PersonCompanies>(a =>
            {
                a.ToTable("PersonCompanies");
                a.HasKey(a => a.Id);
                a.Property(a => a.FromYear).IsRequired();
                a.Property(a => a.ToYear).IsRequired(false);
                a.Property(a => a.Current).IsRequired(false);
                a.Property(a => a.Position).HasMaxLength(100).HasColumnType("varchar").IsRequired();
                a.HasOne(a => a.Person).WithMany(a => a.Companies).HasForeignKey(a => a.Person_Id);
                a.HasOne(a => a.Company).WithMany(a => a.PersonCompanies).HasForeignKey(a => a.Company_Id);
            });
            modelBuilder.Entity<Person>(a =>
            {
                a.ToTable("Person");
                a.HasKey(a => a.Id);
                a.Property(a => a.FirstName).HasMaxLength(15).IsRequired();
                a.Property(a => a.LastName).HasMaxLength(15).IsRequired();
                a.HasMany(a => a.Companies).WithOne(a => a.Person);
            });
            modelBuilder.Entity<Company>(a =>
            {
                a.ToTable("Company");
                a.HasKey(a => a.Id);
                a.Property(a => a.Name).HasMaxLength(15).HasColumnType("varchar").IsRequired();
                a.HasMany(a => a.PersonCompanies).WithOne(x => x.Company);
            });
        }
    }
}
