using ProductManagementPanel.Data;
using ProductManagementPanel.Models;

namespace ProductManagementPanel.Services
{
    // IUserService sözleşmesini uygulayan, kullanıcı yönetimi ve yetkilendirme iş mantığını (Business Logic) barındıran servis sınıfı.
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void ChangeUserRole(int id, string newRole)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Role = newRole;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id, string? currentUsername)
        {
            var user = _context.Users.Find(id);
            
            // Kullanıcı varsa ve silinmek istenen kişi "kendisi" değilse sil
            if (user != null && user.Username != currentUsername)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}