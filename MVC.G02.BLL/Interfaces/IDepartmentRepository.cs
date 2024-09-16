using Microsoft.Identity.Client;
using MVC.G02.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.G02.BLL.Interfaces
{
    public interface IDepartmentRepository:IGenericRepositry<Department>    
    {
        //IEnumerable<Department> GetAll();
        //Department Get(int?id);
        //int Add(Department entity);
        //int Update(Department entity);
        //int Delete(Department entity);
    }
}
