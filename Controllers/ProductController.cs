using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Models;
using ProductManagementPanel.Services; // Servisleri dahil ettik

namespace ProductManagementPanel.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // Artık ApplicationDbContext yerine servisimizi çağırıyoruz
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
                _productService.AddProduct(product); // Veritabanı işlemi servise devredildi
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
                _productService.UpdateProduct(product); // Veritabanı işlemi servise devredildi
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin, Co-Admin")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id); // Veritabanı işlemi servise devredildi
            return RedirectToAction("Index");
        }
    }
}