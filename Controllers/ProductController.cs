using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Data;
using ProductManagementPanel.Models;

namespace ProductManagementPanel.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // Veritabanı bağlantımızı tutacak değişken
        private readonly ApplicationDbContext _context;

        // Dependency Injection ile veritabanı köprümüzü Controller'a dahil ediyoruz (Constructor)
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. READ (Listeleme): /Product veya /Product/Index adresine girildiğinde çalışır
        public IActionResult Index()
        {
            // Veritabanındaki tüm ürünleri liste halinde çekip View'a (Ekrana) gönderiyoruz.
            var products = _context.Products.ToList();
            return View(products);
        }

        // 2. CREATE (Gösterme): Ürün ekleme formunu ekrana getiren metod (GET isteği)
        [Authorize(Roles = "Admin, Co-Admin")]
        public IActionResult Create()
        {
            return View();
        }
        // 3. CREATE (Kaydetme): Kullanıcı formda 'Kaydet'e basınca çalışacak metod (POST isteği)
        [HttpPost]
        [Authorize(Roles = "Admin, Co-Admin")]
        public IActionResult Create(Product product)
        {
            // Modelimizdeki kurallar (Required, StringLength vb.) ihlal edilmediyse
            if (ModelState.IsValid)
            {
                _context.Products.Add(product); // Ürünü veritabanına ekle
                _context.SaveChanges();         // Değişiklikleri kaydet
                return RedirectToAction("Index"); // Kullanıcıyı tekrar listeleme sayfasına yönlendir
            }
            
            // Eğer kural hatası varsa (örn: isim boş girildiyse) aynı formu hatalarla beraber geri göster
            return View(product);
        }

        // --- GÜNCELLEME (UPDATE) İŞLEMLERİ ---

        // 4. UPDATE (Gösterme): Güncellenecek ürünün bilgilerini forma getiren metod
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id); // Veritabanından ID'ye göre ürünü bul
            if (product == null)
            {
                return NotFound(); // Ürün yoksa hata sayfası döndür
            }
            return View(product); // Ürünü bulduysa bilgileriyle birlikte Edit sayfasına gönder
        }

        // 5. UPDATE (Kaydetme): Formda değişiklik yapıp 'Güncelle'ye basınca çalışır
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product); // EF Core bu nesnenin güncelleneceğini anlar
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // --- SİLME (DELETE) İŞLEMİ ---

        // 6. DELETE (Silme): Listeden sil butonuna basıldığında çalışır
        [Authorize(Roles = "Admin, Co-Admin")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product); // Ürünü veritabanından sil
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}