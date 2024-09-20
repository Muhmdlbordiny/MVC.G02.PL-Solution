using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVC.G02.BLL.Interfaces;
using MVC.G02.DAL.Models;
using MVC.G02.PL.ViewModels.Employee;

namespace MVC.G02.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepositry _empRepositry;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepositry employeeRepositry, IDepartmentRepository departmentRepository,IMapper mapper)
        {
            _empRepositry = employeeRepositry;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public IActionResult Index(string Inputsearch)
        {
            var employee= Enumerable.Empty<Employee>();// Refer to Empty sequence from Employee
            //IEnumerable<Employee> employee;
            if (Inputsearch.IsNullOrEmpty())
            {
              employee = _empRepositry.GetAll();

            }
            else
            {
               employee = _empRepositry.GetByName(Inputsearch);
            }
           var result = _mapper.Map<IEnumerable<EmployeeViewModel>>(employee);
            return View(result);
        }
        public IActionResult Create()
        {
           var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
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
                 var employee = _mapper.Map<Employee>(model);
                    var count = _empRepositry.Add(employee);
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
        public IActionResult Details(int? id,string viewName)
        {
            if (id is null) return BadRequest();
            var model = _empRepositry.Get(id.Value);
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
        public IActionResult Edit(int?id) 
        {
            var departments = _departmentRepository.GetAll();
            ViewData["departments"] = departments;
            return Details(id, "Edit");
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int?id,EmployeeViewModel model)
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
                    var employee = _mapper.Map<Employee>(model);

                    var count = _empRepositry.Update(employee);
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
        public IActionResult Delete([FromRoute]int?id,EmployeeViewModel model)
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


                    var count = _empRepositry.Delete(employee);
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
