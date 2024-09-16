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
    public class AppDbContext:DbContext
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
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department> Departments {  get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
