using Microsoft.EntityFrameworkCore;
using MVC.G02.BLL.Interfaces;
using MVC.G02.DAL.Data.Contexts;
using MVC.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.G02.BLL.Repositories
{
    public class EmployeeRepositry :GenericRepositry<Employee>, IEmployeeRepositry
    {
       // private readonly AppDbContext _context;
        public EmployeeRepositry(AppDbContext context):base(context)
        {
           // _context = context;   
        }

        public async Task< IEnumerable<Employee>> GetByNameAsync(string name)
        {
           return await _context.Employees.Where(E=>E.Name.ToLower().Contains(name.ToLower())).Include(E=>E.WorkFor).ToListAsync();
        }
    }
}
