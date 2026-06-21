using ProductManagementPanel.Models;
using ProductManagementPanel.ViewModels;

namespace ProductManagementPanel.Services
{
    // Sistemin kimlik doğrulama (Authentication) ve kayıt işlemlerine ait iş mantığı sözleşmesini tanımlar.
    public interface IAuthService
    {
        void RegisterUser(RegisterViewModel model);
        
        User? ValidateUser(LoginViewModel model);
    }
}