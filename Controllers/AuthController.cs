using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Data;
using ProductManagementPanel.Models;
using System.Security.Claims;

namespace ProductManagementPanel.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // --- KAYIT OLMA EKRANI ---
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Mülakat Notu: Gerçek projelerde şifreler "Hash"lenerek (kriptolanarak) veritabanına kaydedilir. 
                // Dar zamanlı bir task olduğu için şimdilik düz metin olarak kaydediyoruz.
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // --- GİRİŞ YAPMA EKRANI ---
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Veritabanında bu kullanıcı adı ve şifreye sahip biri var mı diye bakıyoruz
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // 1. Kullanıcının dijital yaka kartını (Claims) hazırlıyoruz
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role) // En önemli kısım: Rol bilgisini çereze gömüyoruz!
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // 2. Çerezi (Cookie) tarayıcıya verip sistemi açıyoruz
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                // Başarılı girişte doğrudan Ürün listesine yönlendir
                return RedirectToAction("Index", "Product");
            }

            // Eğer eşleşme yoksa hata mesajıyla aynı sayfayı geri döndür
            ViewBag.Error = "Kullanıcı adı veya şifre hatalı!";
            return View();
        }

        // --- ÇIKIŞ YAPMA ---
        public IActionResult Logout()
        {
            // Çerezi silip güvenli çıkış yap
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}