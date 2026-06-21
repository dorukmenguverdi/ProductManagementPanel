using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Services;

namespace ProductManagementPanel.Controllers
{
    /// Kullanıcı yönetimi (listeleme, rol değiştirme, silme) işlemlerini yürüten Controller sınıfı.
    /// Güvenlik prensipleri gereği sınıf seviyesinde sınırlandırılmıştır; yalnızca 'Admin' rolüne sahip kullanıcılar erişebilir.
    
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

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
            _userService.DeleteUser(id, User.Identity?.Name);
            return RedirectToAction("Index");
        }
    }
}