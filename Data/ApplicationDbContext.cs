using Microsoft.EntityFrameworkCore;
using ProductManagementPanel.Models;

namespace ProductManagementPanel.Data
{
    // DbContext sınıfından miras alarak veritabanı yönetimini bu sınıfa devrediyoruz.
    public class ApplicationDbContext : DbContext
    {
        // Bu constructor, veritabanı ayarlarının (hangi veritabanı türü, hangi bağlantı adresi vb.) dışarıdan (Program.cs) enjekte edilmesini sağlar.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}