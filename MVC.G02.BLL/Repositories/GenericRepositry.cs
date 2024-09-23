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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _context.Employees.Include(E => E.WorkFor).AsNoTracking().ToListAsync();
            }

            return await _context.Set<T>().AsNoTracking().ToListAsync();

            
        }
        public async Task<T> GetAsync(int? id)
        {
           
                return await _context.Set<T>().FindAsync(id);
        }

        
        public  void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
