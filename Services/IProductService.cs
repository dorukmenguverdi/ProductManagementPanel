using ProductManagementPanel.Models;

namespace ProductManagementPanel.Services
{
    // Bu arayüz, ürün servisinde bulunması zorunlu olan yetenekleri tanımlar.
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}