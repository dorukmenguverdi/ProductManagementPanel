using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Models;
using ProductManagementPanel.Services; 

namespace ProductManagementPanel.Controllers
{
    /// Ürünlerle ilgili HTTP isteklerini (GET/POST) karşılayan, yetkilendirme kontrollerini yapan ve 
    /// iş mantığı için IProductService'e yönlendiren Controller sınıfı.
    /// Tüm sınıf bazında, sisteme giriş yapmamış kullanıcıların erişimi engellenmiştir.

    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            // İş mantığı tamamen serviste, controller sadece sonucu alıp View'a iletiyor.
            var products = _productService.GetAllProducts();
            return View(products);
        }

        [Authorize(Roles = "Admin, Co-Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Co-Admin")]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product); 
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin, Co-Admin")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id); 
            return RedirectToAction("Index");
        }
    }
}