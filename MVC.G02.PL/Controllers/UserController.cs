using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC.G02.DAL.Models;
using MVC.G02.PL.Helper;
using MVC.G02.PL.ViewModels;
using MVC.G02.PL.ViewModels.Employee;

namespace MVC.G02.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string Inputsearch)
        {
            var users = Enumerable.Empty<UserViewModel>();// Refer to Empty sequence from user
            if (Inputsearch.IsNullOrEmpty())
            {
                users = await _userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FirstName = U.FirstName,
                    LastName = U.LastName,
                    Email = U.Email,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(U => 
                        U.Email.ToLower()

                      .Contains(Inputsearch.ToLower())).
                      Select(U => new UserViewModel()
                      {
                          Id = U.Id,
                          FirstName = U.FirstName,
                          LastName = U.LastName,
                          Email = U.Email,
                          Roles = _userManager.GetRolesAsync(U).Result

                      }).ToListAsync();
            }
            return View(users);
        }
        public async Task<IActionResult> Details(string? id, string viewName)
        {
            if (id is null) return BadRequest();
            var userfromDb = await _userManager.FindByIdAsync(id);
            if (userfromDb is null)
            {
                return NotFound();
            }
            var user = new UserViewModel()
            {
                Id = userfromDb.Id,
                FirstName = userfromDb.FirstName,
                LastName = userfromDb.LastName,
                Email = userfromDb.Email,
                Roles = _userManager.GetRolesAsync(userfromDb).Result
            };
            return View(viewName, user);
        }

        public async Task<IActionResult> Edit(string? id)
        {

            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string? id, UserViewModel model)
        {
            try
            {

                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var userfromdb = await _userManager.FindByIdAsync(id);

                    if (userfromdb is null) return NotFound(); //404
                    userfromdb.FirstName = model.FirstName;
                    userfromdb.LastName = model.LastName;
                    userfromdb.Email = model.Email;
                    var result = await _userManager.UpdateAsync(userfromdb);
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
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            try
            {

                if (id != model.Id) return BadRequest();
                if (ModelState.IsValid)
                {
                    var userfromdb = await _userManager.FindByIdAsync(id);

                    if (userfromdb is null) return NotFound(); //404

                    var result = await _userManager.DeleteAsync(userfromdb);
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
    }
}