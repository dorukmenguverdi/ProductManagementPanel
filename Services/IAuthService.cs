using ProductManagementPanel.Models;
using ProductManagementPanel.ViewModels;

namespace ProductManagementPanel.Services
{
    public interface IAuthService
    {
        // Kayıt işlemini veritabanına işleyen yetenek
        void RegisterUser(RegisterViewModel model);
        
        // Kullanıcı adı ve şifreyi kontrol edip, doğruysa User nesnesini döndüren yetenek
        User? ValidateUser(LoginViewModel model);
    }
}