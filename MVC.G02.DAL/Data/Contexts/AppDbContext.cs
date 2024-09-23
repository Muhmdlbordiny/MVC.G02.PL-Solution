using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC.G02.DAL.Data.Configurations;
using MVC.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVC.G02.DAL.Data.Contexts
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) //chain on open connection with database direct
        {
            
        }
       // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       // {
       //     optionsBuilder.UseSqlServer("Server=.;Database=CompanyMVC;Trusted_Connection=True;TrustServerCertificate=True");
       // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        }
        public DbSet<Department> Departments {  get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApplicationUser>ApplicationUsers { get; set; }
    }
}
