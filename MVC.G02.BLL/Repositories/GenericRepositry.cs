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
    public class GenericRepositry<T> : IGenericRepositry<T>where T : BaseEntity
    {
        private protected readonly AppDbContext _context;
        public GenericRepositry(AppDbContext context)
        {
            _context= context;
        }

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _context.Employees.Include(E => E.WorkFor).ToList();
            }

            return _context.Set<T>().ToList();

            
        }
        public T Get(int? id)
        {
           
                return _context.Set<T>().Find(id);
        }

        
        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
