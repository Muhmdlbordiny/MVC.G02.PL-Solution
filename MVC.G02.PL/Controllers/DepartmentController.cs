using Microsoft.AspNetCore.Mvc;
using MVC.G02.BLL.Interfaces;
using MVC.G02.BLL.Repositories;
using MVC.G02.DAL.Models;

namespace MVC.G02.PL.Controllers
{
    public class DepartmentController:Controller
    {
        private readonly IDepartmentRepository _deptrepository;//Null
        public DepartmentController(IDepartmentRepository deptrepository)//Ask Clr to create object from DepartmentRepository
        {
            _deptrepository = deptrepository;
        }
        public IActionResult Index()
        {
           var departments= _deptrepository.GetAll();
            return View(departments);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                var count = _deptrepository.Add(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(model);
        }
        public IActionResult Details(int? id,string viewName="Details")
        {
            if (id is null) return BadRequest();//400
           var department = _deptrepository.Get(id.Value);
            if (department == null) return NotFound();//404
            return View(viewName,department);
        }
        public IActionResult Edit(int? id) 
        {
            //if (id is null) return BadRequest();//400

            //var department = _deptrepository.Get(id.Value);
            //if (department == null) return NotFound();//404

            //return View(department);
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                var count = _deptrepository.Update(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
        public IActionResult Delete(int? id) 
        {
            //if (id is null) return BadRequest();//400

            //var department = _deptrepository.Get(id.Value);
            //if (department == null) return NotFound();//404

            //return View(department);
            return Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department model)
        {
            if (ModelState.IsValid)
            {
                var count = _deptrepository.Delete(model);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

    }
}
