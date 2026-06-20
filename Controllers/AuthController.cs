using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Services;
using ProductManagementPanel.ViewModels;
using System.Security.Claims;

namespace ProductManagementPanel.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        // Artık ApplicationDbContext yok, IAuthService var
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _authService.RegisterUser(model); // Veritabanı işini Servise devrettik
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Doğrulama işini Servise devrettik. Bize ya User döner, ya da null.
                var user = _authService.ValidateUser(model);

                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Index", "Product");
                }

                ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            }
            
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}