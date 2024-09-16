using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.G02.BLL.Interfaces
{
    public interface IGenericRepositry<T>
    {
        IEnumerable<T> GetAll();
        T Get(int? id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
