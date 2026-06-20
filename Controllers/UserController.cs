using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Data;
using System.Linq;

namespace ProductManagementPanel.Controllers
{
    // Sistemin en kritik noktası olduğu için burayı sadece Admin'lere açıyoruz
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Sistemdeki tüm kullanıcıları listele
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // 2. Tablodan gelen rol değiştirme isteğini veritabanına işle
        [HttpPost]
        public IActionResult ChangeRole(int id, string newRole)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Role = newRole; // Kullanıcının yeni rolünü ata
                _context.SaveChanges(); // Veritabanını güncelle
            }

            // İşlem bitince aynı sayfaya geri dön
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);

            // Güvenlik önlemi: Kendi kendini silmeyi engelle!
            if (user != null && user.Username != User.Identity.Name)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}