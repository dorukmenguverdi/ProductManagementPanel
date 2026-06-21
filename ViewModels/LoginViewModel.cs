using System.ComponentModel.DataAnnotations;

namespace ProductManagementPanel.ViewModels
{
    // Kullanıcı giriş işlemleri (Login) sırasında arayüzden (View) alınacak verileri taşıyan DTO sınıfı.
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}