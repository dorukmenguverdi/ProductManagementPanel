using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Services; // Servisi dahil ettik

namespace ProductManagementPanel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        // Artık ApplicationDbContext yerine IUserService kullanıyoruz
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            var users = _userService.GetAllUsers();
            return View(users);
        }

        [HttpPost]
        public IActionResult ChangeRole(int id, string newRole)
        {
            _userService.ChangeUserRole(id, newRole);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            // İşi servise devrediyoruz, işlemi yapan kişinin adını (User.Identity?.Name) parametre olarak gönderiyoruz
            _userService.DeleteUser(id, User.Identity?.Name);
            return RedirectToAction("Index");
        }
    }
}