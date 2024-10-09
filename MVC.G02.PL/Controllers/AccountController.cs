using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MVC.G02.DAL.Models;
using MVC.G02.PL.Helper;
using MVC.G02.PL.ViewModels;
using NuGet.Common;

namespace MVC.G02.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController
			(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager
			) 
        {
			_userManager = userManager;
            _signInManager = signInManager;
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
                        ModelState.AddModelError(key: string.Empty, "Email is Already Exisit");

                    }
                ModelState.AddModelError(key: string.Empty, "User Name is Already Exisit");
			}

			//code Registration
			return View(model);
		}
		#region SignIn
		public IActionResult SignIn()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> SignIn(SignInViewModel model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						//Check Password
						var Flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (Flag)
						{
							//sigin
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.Rememberme, false);
							if (result.Succeeded)
							{

								return RedirectToAction("Index", "Home");
							}

						}
					}
					ModelState.AddModelError(string.Empty, "Invaild Login !!");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}

			}
			return View(model);

		}
		#endregion
		#region SignOut
		public new async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(SignIn));
		} 
		#endregion
		public IActionResult ForgetPassword()
		{
			return View();
		}
		[HttpPost]
		public async Task< IActionResult> sendResetePasswordUrl( ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					//create token
					var token = await	_userManager.GeneratePasswordResetTokenAsync(user);
					//Create ResetPassword Url
					var url=	Url.Action("ResetePassword", "Account", new {email = model.Email,token = token},Request.Scheme);
				//https://localhost:44393/Account/ResetePassword?email=ahmed@gmail.com&token
					  //Create Email

					var email = new Email()
					{
						To = model.Email,
						Subject = "Resete Password",
						Body = url
					};
					//sendMail
					EmailSetting.Sendemail(email);
					return RedirectToAction(nameof(CheckyourInbox));


				}
				ModelState.AddModelError(key: string.Empty, "Invaild Operation plz try again !!");
			}
			return View(model);
		}
		[HttpGet]
		public IActionResult CheckyourInbox()
		{
			return View();
		}
		[HttpGet]
		public IActionResult ResetePassword(string email, string token)
		{
			TempData["email"] = email;
			TempData["token"] =token;

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetePassword(ResetePasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var email = TempData["email"] as string;
				var token = TempData["token"] as string;
				var user = await _userManager.FindByEmailAsync(email);
				if (user is not null)
				{
					var result =
						await	_userManager.ResetPasswordAsync(user, token, model.Password);
					if (result.Succeeded) 
					{
						return RedirectToAction(nameof(SignIn));
					}
				}
			}
			ModelState.AddModelError(key: string.Empty, "Invaild Operation plz try again !!");

			return View(model);
		}
		public IActionResult AccessDenied()
		{
			return View();
		}

    }
}
