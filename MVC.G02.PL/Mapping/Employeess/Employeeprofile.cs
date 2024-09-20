using AutoMapper;
using MVC.G02.PL.ViewModels.Employee;
using MVC.G02.DAL.Models;
namespace MVC.G02.PL.Mapping.Employees
{
    public class Employeeprofile:Profile
    {
        public Employeeprofile() 
        {
            CreateMap<Employee, Employeeprofile>().ReverseMap();
            //CreateMap< Employeeprofile, Employee>();
        }
    }
}
