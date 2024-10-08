using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.G02.BLL;
using MVC.G02.BLL.Interfaces;
using MVC.G02.BLL.Repositories;
using MVC.G02.DAL.Data.Contexts;
using MVC.G02.DAL.Models;
using MVC.G02.PL.Mapping.Employees;
using MVC.G02.PL.Services;

namespace MVC.G02.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<AppDbContext>();// Allow DI for AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration 
                    ["ConnectionStrings:DefaultConnection"]);
            });//Allow DI for AppDbContext
            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();//Allow DI for DepartmentRepository
            builder.Services.AddScoped<IEmployeeRepositry,EmployeeRepositry>();//Allow DI for DepartmentRepository
            builder.Services.AddAutoMapper(typeof(Employeeprofile));//allow DI
            builder.Services.AddScoped<IScopedService,ScopedService>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfwork>();
            builder.Services.AddTransient<ITransientService,TransientService>();
            builder.Services.AddSingleton<ISingeltonService, SingeltonService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie
                (config =>
                {
                    config.LoginPath = "/Account/SignIn";
                    
                 }

                );
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
