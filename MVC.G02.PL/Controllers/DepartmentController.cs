using Microsoft.AspNetCore.Mvc;
using MVC.G02.BLL.Interfaces;
using MVC.G02.BLL.Repositories;
using MVC.G02.DAL.Models;

namespace MVC.G02.PL.Controllers
{
    public class DepartmentController:Controller
    {
        // private readonly IDepartmentRepository _deptrepository;//Null
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentController(IUnitOfWork unitOfWork)//IDepartmentRepository deptrepository)//Ask Clr to create object from DepartmentRepository
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
           var departments= await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department model)
        {
            if (ModelState.IsValid)
            {
                    _unitOfWork.DepartmentRepository.Add(model);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(model);
        }
        public async Task< IActionResult> Details(int? id,string viewName="Details")
        {
            if (id is null) return BadRequest();//400
           var department = await _unitOfWork.DepartmentRepository.GetAsync(id.Value);
            if (department == null) return NotFound();//404
            return View(viewName,department);
        }
        public async Task<IActionResult> Edit(int? id) 
        {
            //if (id is null) return BadRequest();//400

            //var department = _deptrepository.Get(id.Value);
            //if (department == null) return NotFound();//404

            //return View(department);
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                    _unitOfWork.DepartmentRepository.Update(model);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));

                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(int? id) 
        {
            //if (id is null) return BadRequest();//400

            //var department = _deptrepository.Get(id.Value);
            //if (department == null) return NotFound();//404

            //return View(department);
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete(Department model)
        {
            if (ModelState.IsValid)
            {
                    _unitOfWork.DepartmentRepository.Delete(model);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);

        }

    }
}
