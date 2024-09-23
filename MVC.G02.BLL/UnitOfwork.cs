using MVC.G02.BLL.Interfaces;
using MVC.G02.BLL.Repositories;
using MVC.G02.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.G02.BLL
{
    public class UnitOfwork : IUnitOfWork

    {
        public IDepartmentRepository _departmentRepository;
        public IEmployeeRepositry _employeeRepositry;
        private readonly AppDbContext _Context;

        public UnitOfwork(AppDbContext context) 
        {
            _employeeRepositry = new EmployeeRepositry(context);
            _departmentRepository = new DepartmentRepository(context);
            _Context = context;
        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IEmployeeRepositry EmployeeRepositry => _employeeRepositry;
        public async Task<int> Complete()
        {
            return await _Context.SaveChangesAsync();
        }

    }
}
