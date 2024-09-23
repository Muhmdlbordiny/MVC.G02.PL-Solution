using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.G02.DAL.Models;
using MVC.G02.PL.ViewModels;

namespace MVC.G02.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager) 
        {
			_userManager = userManager;
		}
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
		public async Task <IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid) 
			{
				var user = await _userManager.FindByNameAsync(model.UserName);
				if (user is null)
				{
					user = await _userManager.FindByEmailAsync(model.Email);
					//Mapping:signupviewModel to Application User
					if (user is null)
					{
						user = new ApplicationUser()
						{
							UserName = model.UserName,
							Email = model.Email,
							FirstName = model.FirstName,
							LastName = model.LastName,
							IsAgree = model.IsAgree,
						};
						var result = await _userManager.CreateAsync(user, model.Password);
						if (result.Succeeded)
						{
							return RedirectToAction("SignIn");
						}
						foreach (var error in result.Errors)
							ModelState.AddModelError(string.Empty, error.Description);
					}
				}
				ModelState.AddModelError(string.Empty, "User Name is Already Exisit");
			}
			
            //code Registration
			return View(model);
		}
	}
}
