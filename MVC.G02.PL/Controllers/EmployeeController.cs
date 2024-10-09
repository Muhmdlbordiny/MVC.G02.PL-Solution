using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC.G02.BLL.Interfaces;
using MVC.G02.DAL.Models;
using MVC.G02.PL.Helper;
using MVC.G02.PL.ViewModels.Employee;

namespace MVC.G02.PL.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    {//
        //private readonly IEmployeeRepositry _empRepositry;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(//IEmployeeRepositry employeeRepositry,
                                  //IDepartmentRepository departmentRepository,
                                  IUnitOfWork unitOfWork,
                                  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            //_empRepositry = employeeRepositry;
            //_departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task< IActionResult> Index(string Inputsearch)
        {
            var employee= Enumerable.Empty<Employee>();// Refer to Empty sequence from Employee
            //IEnumerable<Employee> employee;
            if (Inputsearch.IsNullOrEmpty())
            {
              employee =await _unitOfWork.EmployeeRepositry.GetAllAsync();
            }
            else
            {
               employee = await _unitOfWork.EmployeeRepositry.GetByNameAsync(Inputsearch);
            }
           var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employee);
            return View(result);
        }
        public async Task< IActionResult> Create()
        {
           var departments = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(EmployeeViewModel model)
        {
            try
            {
                //Casting EmployeeViewModel (viewmodel) --->Employee(Model)
                //Mapping :
                //1.ManualMapping
                //Employee employee = new Employee()
                //{
                //    Id=model.Id,
                //    Address=model.Address,
                //    Name =model.Name,
                //    Salary =model.Salary,
                //    Age=model.Age,
                //    HiringDate =model.HiringDate,
                //    IsActive =model.IsActive,
                //    WorkFor =model.WorkFor,
                //    WorkForId =model.WorkForId,
                //    Email = model.Email,
                //    PhoneNumber = model.PhoneNumber
                //};
                //2.Auto Mapping
                if (ModelState.IsValid)
                {
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                 var employee = _mapper.Map<Employee>(model);
                         _unitOfWork.EmployeeRepositry.Add(employee);
                    var count = await _unitOfWork.Complete();
                    if (count > 0)
                    {
                        TempData["Message"] = "Employee is Created!!";
                    }
                    else
                    {
                        TempData["Message"] = "Employee is NoT Created!!";

                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }

            return View(model);
        }
        public async Task<IActionResult> Details(int? id,string viewName)
        {
            if (id is null) return BadRequest();
            var model = await _unitOfWork.EmployeeRepositry.GetAsync(id.Value);
            if (model == null) return NotFound();
            //Mapping :Employee --> EmployeeViewModel
            //Manual Mapping
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel()
            //{
            //    Id = model.Id,
            //    Address = model.Address,
            //    Name = model.Name,
            //    Salary = model.Salary,
            //    Age = model.Age,
            //    HiringDate = model.HiringDate,
            //    IsActive = model.IsActive,
            //    WorkFor = model.WorkFor,
            //    WorkForId = model.WorkForId,
            //    Email = model.Email,
            //    PhoneNumber = model.PhoneNumber

            //};
             var employeeViewModel = _mapper.Map<EmployeeViewModel>(model);
            return View(viewName, employeeViewModel);
        }
        public async Task< IActionResult> Edit(int?id) 
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["departments"] = departments;
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task< IActionResult> Edit([FromRoute]int?id,EmployeeViewModel model)
        {
            try
            {
                
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Address = model.Address,
                    //    Name = model.Name,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    Email = model.Email,
                    //    PhoneNumber = model.PhoneNumber

                    //};
                    if(model.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(model.ImageName, "images");
                    }
                    model.ImageName = DocumentSetting.UploadFile(model.Image, "images");
                    var employee = _mapper.Map<Employee>(model);

                        _unitOfWork.EmployeeRepositry.Update(employee);
                    var count = await _unitOfWork.Complete();
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
        public async Task <IActionResult> Delete(int? id) 
        {
          return await Details(id,"Delete");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task < IActionResult> Delete([FromRoute]int?id,EmployeeViewModel model)
        {
            try
            {
                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    //Employee employee = new Employee()
                    //{
                    //    Id = model.Id,
                    //    Address = model.Address,
                    //    Name = model.Name,
                    //    Salary = model.Salary,
                    //    Age = model.Age,
                    //    HiringDate = model.HiringDate,
                    //    IsActive = model.IsActive,
                    //    WorkFor = model.WorkFor,
                    //    WorkForId = model.WorkForId,
                    //    Email = model.Email,
                    //    PhoneNumber = model.PhoneNumber

                    //};
                    var employee  = _mapper.Map<Employee>(model);


                        _unitOfWork.EmployeeRepositry.Delete(employee);
                    var count =await _unitOfWork.Complete();
                    if (count > 0)
                    {
                        DocumentSetting.DeleteFile(model.ImageName, "images");

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
