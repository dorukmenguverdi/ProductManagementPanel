using System.ComponentModel.DataAnnotations;

namespace ProductManagementPanel.ViewModels
{
    // Yeni kullanıcı kayıt (Register) işlemleri sırasında arayüzden gelen verileri taşıyan DTO sınıfı.
    // Güvenlik gereği 'Role' veya 'Id' gibi sistem kritik verileri içermez
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3 ile 50 karakter arasında olmalıdır.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler birbiriyle uyuşmuyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}