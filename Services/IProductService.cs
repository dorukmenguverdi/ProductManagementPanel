using ProductManagementPanel.Models;

namespace ProductManagementPanel.Services
{
    /// Ürün yönetimi iş mantığını (Business Logic) tanımlayan arayüz sözleşmesidir.
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product? GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}