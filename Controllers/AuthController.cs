using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Services;
using ProductManagementPanel.ViewModels;
using System.Security.Claims;

namespace ProductManagementPanel.Controllers
{
    /// Kullanıcı kayıt (Register), giriş (Login) ve çıkış (Logout) işlemlerinin HTTP isteklerini yöneten Controller sınıfı.
    /// Veritabanı doğrulamalarını IAuthService üzerinden yapar, başarılı durumlarda şifreli çerez (Cookie) oluşturarak oturumu başlatır.
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

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
                _authService.RegisterUser(model); 
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