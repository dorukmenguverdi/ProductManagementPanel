using System.ComponentModel.DataAnnotations;

namespace ProductManagementPanel.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı en fazla 100 karakter olabilir.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Fiyat alanı zorunludur.")]
        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}