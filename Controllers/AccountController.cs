using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cosultations.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services;
using Services.Entities;
using Services.Services;

namespace Cosultations.Controllers
{
	
    public class AccountController : Controller
    {
		private readonly UserAccountServices userAccountService;
		private readonly CurrentUser currentUser;
		public AccountController(UserAccountServices userAccountService,CurrentUser currentUser)
		{
			this.userAccountService = userAccountService;
			this.currentUser = currentUser;
		}


		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await LogOut();

			return RedirectToAction("Login", "Account");
		}

		[HttpGet]
		public IActionResult Login()
		{
			if (currentUser.IsAuthenticated)
			{
				return RedirectToAction("Index", "Home", new { id = currentUser.Id });
			}
			var model = new LoginVm();


			return View(model);
		}




		[HttpPost]
		public async Task<IActionResult> Login(LoginVm model, string returnUrl)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = userAccountService.Login(model.Email, model.Password);
			if (user == null)
			{
				return View(model);
			}

			await LogIn(user);
			if (returnUrl != null && Url.IsLocalUrl(returnUrl))
			{
				return Redirect(returnUrl);
			}
			return RedirectToAction("Index", "Consultation");
			
		}
		/*[HttpGet]
		public IActionResult Register()
		{		
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVm model)
		{
			if (ModelState.IsValid && model.ConfirmPassword == model.Password)
			{
				var entity = new User
				{
					Email = model.Email,
					FirstName=model.FirstName,
					LastName=model.LastName,
					Password=model.Password		    
				};
				

				var result = await userAccountService.Register(entity);

				if (!result) return NotFound();

				var user = userAccountService.Login(model.Email, model.Password);

				await LogIn(user);

				return RedirectToAction("Index", "Feed");
			}

			

			return View(model);
		}*/
		public async Task LogIn(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.FirstName),
				new Claim(ClaimTypes.Email, user.Email),

			};
			

			var identity = new ClaimsIdentity(claims, "Cookies");
			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(
					scheme: "Consultation",
					principal: principal);
		}

		private async Task LogOut()
		{
			await HttpContext.SignOutAsync(scheme: "Consultation");
		}
	}
}