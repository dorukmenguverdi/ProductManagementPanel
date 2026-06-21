using ProductManagementPanel.Models;

namespace ProductManagementPanel.Services
{
    // Sistemdeki kullanıcıları ve yetki rollerini yönetmek için gereken iş mantığı sözleşmesini (Interface) tanımlar.
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        void ChangeUserRole(int id, string newRole);
        
        // Güvenlik için, silme işlemi yapan kişinin kendi kendini silmesini engellemek adına 
        // işlemi yapan kişinin kullanıcı adını (currentUsername) da parametre olarak alıyoruz.
        void DeleteUser(int id, string? currentUsername); 
    }
}