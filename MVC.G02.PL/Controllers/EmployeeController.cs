using Microsoft.AspNetCore.Mvc;
using MVC.G02.BLL.Interfaces;
using MVC.G02.DAL.Models;

namespace MVC.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepositry _empRepositry;
        public EmployeeController(IEmployeeRepositry employeeRepositry)
        {
            _empRepositry= employeeRepositry;
        }
        public IActionResult Index()
        {
            var employee = _empRepositry.GetAll();
            return View(employee);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee model)
        {
            if(ModelState.IsValid)
            {
                var count = _empRepositry.Add(model);
                if(count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        public IActionResult Details(int? id,string viewName)
        {
            if (id is null) return BadRequest();
            var employee = _empRepositry.Get(id.Value);
            if (employee == null) return NotFound();
            return View(viewName, employee);
        }
        public IActionResult Edit(int?id) 
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int?id,Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count = _empRepositry.Update(model);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }
        public IActionResult Delete(int? id) 
        {
          return Details(id,"Delete");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Delete([FromRoute]int?id,Employee model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var count = _empRepositry.Delete(model);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View(model);
        }
    }
}
