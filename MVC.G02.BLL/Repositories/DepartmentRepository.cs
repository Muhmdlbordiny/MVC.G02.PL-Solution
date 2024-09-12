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
    //ClR
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;//Null
        public DepartmentRepository(AppDbContext context)//Ask Clr create object from AppDbContext
        {
            _context = context;   
        }
        public int Add(Department entity)
        {
             _context.Departments.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _context.Departments.Remove(entity);
            return _context.SaveChanges();
        }

        public Department Get(int? id)
        {
            //return _context.Departments.FirstOrDefault(x => x.Id == id);
            return _context.Departments.Find(id);

        }

        public IEnumerable<Department> GetAll()
        {
           return _context.Departments.ToList();
        }

        public int Update(Department entity)
        {
            _context.Departments.Update(entity);
            return _context.SaveChanges();
        }
    }
}
