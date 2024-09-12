using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.G02.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Code is Required !")]
        public string Code {  get; set; }
        [Required(ErrorMessage ="Must be Enter Name")]
        [DisplayName(" Department Name")]
        public string Name { get; set;}
        public DateTime DateOfCreation {  get; set; }

    }
}
