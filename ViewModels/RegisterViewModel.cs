using System.ComponentModel.DataAnnotations;

namespace ProductManagementPanel.ViewModels
{
    // Bu sınıf sadece arayüzden (View) kayıt verilerini güvenli bir şekilde almak için kullanılır.
    // Veritabanına doğrudan gitmez, "Role" veya "Id" gibi kritik verileri içermez.
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Kullanıcı adı 3 ile 50 karakter arasında olmalıdır.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        // Gerçek dünyada aranan o profesyonel detay: Şifre Doğrulama alanı
        [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler birbiriyle uyuşmuyor.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}