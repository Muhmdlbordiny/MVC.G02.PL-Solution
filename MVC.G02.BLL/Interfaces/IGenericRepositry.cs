using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.G02.BLL.Interfaces
{
    public interface IGenericRepositry<T>
    {
       Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int? id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
