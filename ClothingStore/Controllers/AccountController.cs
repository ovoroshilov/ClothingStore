using ClotherStore.Services.Interfaces;
using ClothingStore.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClothingStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid) { 
                var response = await _accountService.Register(registerViewModel);
                if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    return RedirectToAction("GetAllProducts", "Product");
                }
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.LogIn(loginViewModel);
                if (response.StatusCode == Domain.Enums.StatusCode.Ok)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response.Data));
                    return RedirectToAction("GetAllProducts", "Product");
                }
            }
            return View(loginViewModel);
        }

        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GetAllProducts", "Product");
        }
    }
}
