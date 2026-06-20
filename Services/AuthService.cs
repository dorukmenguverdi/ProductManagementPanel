using ProductManagementPanel.Data;
using ProductManagementPanel.Models;
using ProductManagementPanel.ViewModels;

namespace ProductManagementPanel.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RegisterUser(RegisterViewModel model)
        {
            var user = new User
            {
                Username = model.Username,
                Password = model.Password,
                Role = "Personel" // Varsayılan rol
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User? ValidateUser(LoginViewModel model)
        {
            // Kullanıcı adı ve şifre veritabanında eşleşiyor mu diye kontrol et
            return _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
        }
    }
}