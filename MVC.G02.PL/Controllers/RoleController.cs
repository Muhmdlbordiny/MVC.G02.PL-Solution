using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC.G02.DAL.Models;
using MVC.G02.PL.ViewModels;
using MVC.G02.PL.ViewModels.Role;

namespace MVC.G02.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _rolemanager = roleManager;
           _userManager = userManager;
        }
        public async Task<IActionResult> Index(string Inputsearch)
        {
            var Roles = Enumerable.Empty<RoleViewModel>();// Refer to Empty sequence from user
            if (Inputsearch.IsNullOrEmpty())
            {
                Roles = await _rolemanager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToListAsync();
            }
            else
            {
                Roles = await _rolemanager.Roles.Where(R =>
                        R.Name.ToLower()

                      .Contains(Inputsearch.ToLower())).
                      Select(R => new RoleViewModel()
                      {
                          Id = R.Id,
                          RoleName = R.Name

                      }).ToListAsync();
            }
            return View(Roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(RoleViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var role = new IdentityRole() //Mapping
                {
                    Name = model.RoleName

                };
                var result = await _rolemanager.CreateAsync(role);
                if(result.Succeeded)
                    return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName)
        {
            if (id is null) return BadRequest();
            var rolefromDb = await _rolemanager.FindByIdAsync(id);
            if (rolefromDb is null)
            {
                return NotFound();
            }
            var role = new RoleViewModel() //Mapping Manual
            {
                Id = rolefromDb.Id,
                RoleName = rolefromDb.Name
               
            };
            return View(viewName, role);
        }

        public async Task<IActionResult> Edit(string? id)
        {

            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, RoleViewModel model)
        {
            try
            {

                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var rolefromDb = await _rolemanager.FindByIdAsync(id);
                    if (rolefromDb is null)
                    {
                        return NotFound();
                    }

                    rolefromDb.Name = model.RoleName;
                    var result = await _rolemanager.UpdateAsync(rolefromDb);
                    if (result.Succeeded)
                    {

                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);

        }
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {
            try
            {

                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var rolefromDb = await _rolemanager.FindByIdAsync(id);
                    if (rolefromDb is null)
                    {
                        return NotFound();
                    }

                    rolefromDb.Name = model.RoleName;
                    var result = await _rolemanager.DeleteAsync(rolefromDb);
                    if (result.Succeeded)
                    {

                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);


        }
        [HttpGet]
        public async  Task<IActionResult> AddOrRemove(string roleid)
        {
           var role = await _rolemanager.FindByIdAsync(roleid);
            if (role is null) 
                return NotFound();
            ViewData["Roleid"] = roleid;
            var usersinrolevm = new List<UserinRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                var userinrole = new UserinRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if(await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userinrole.IsSelected = true;
                }
                else
                {
                    userinrole.IsSelected = false;

                }
                usersinrolevm.Add(userinrole);
            }
            return View(usersinrolevm);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemove(string roleid,List<UserinRoleViewModel> users)
        {
            var role = await _rolemanager.FindByIdAsync(roleid);
            if(role is null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach(var user in users)
                {
                    var appuser =await _userManager.FindByIdAsync(user.UserId);
                    if (appuser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appuser,role.Name))
                        {
                           await _userManager.AddToRoleAsync(appuser, role.Name);
                         }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appuser,role.Name))
                        {
                           await _userManager.RemoveFromRoleAsync(appuser,role.Name);

                        } 
                    }
                }
                return RedirectToAction(nameof(Edit), new { id = roleid });

            }
            return View(users);
        }
    }
}
