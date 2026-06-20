using System.ComponentModel.DataAnnotations;

namespace ProductManagementPanel.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre zorunludur.")]
        public string Password { get; set; } = string.Empty;

        // Kullanıcının sistemdeki rolünü tutacak alan. 
        // Güvenlik gereği sisteme yeni kayıt olan herkesi varsayılan olarak en düşük yetki olan "Personel" yapıyoruz.
        public string Role { get; set; } = "Personel"; 
    }
}