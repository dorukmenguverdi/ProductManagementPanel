using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProductManagementPanel.Data;

var builder = WebApplication.CreateBuilder(args);

// ==============================================================================
// 1. SERVİS KAYITLARI (DEPENDENCY INJECTION CONTAINER)
// Uygulamanın ihtiyaç duyduğu tüm araçlar ve servisler burada sisteme tanıtılır.
// ==============================================================================

builder.Services.AddControllersWithViews();

// --- Güvenlik: Çerez (Cookie) Bazlı Kimlik Doğrulama Ayarları ---
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Yetkisiz veya anonim bir erişim denemesinde yönlendirilecek rotalar:
        options.LoginPath = "/Auth/Login"; 
        options.AccessDeniedPath = "/Auth/AccessDenied"; 
    });

// --- Veritabanı (ORM) Kaydı ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

// --- İş Mantığı Servisleri (Business Logic Layer) Kayıtları ---
// Mimarideki bağımlılıkları çözmek için Interface'ler somut sınıflara (Implementation) bağlanır.
// AddScoped: Her HTTP isteği için nesneyi bir kez oluşturur, işlem bitince bellekten atar.
builder.Services.AddScoped<ProductManagementPanel.Services.IProductService, ProductManagementPanel.Services.ProductService>();

builder.Services.AddScoped<ProductManagementPanel.Services.IAuthService, ProductManagementPanel.Services.AuthService>();

builder.Services.AddScoped<ProductManagementPanel.Services.IUserService, ProductManagementPanel.Services.UserService>();

// Builder süreci biter, uygulama ayağa kalkar.
var app = builder.Build();


// ==============================================================================
// 2. HTTP İSTEK BORU HATTI (MIDDLEWARE PIPELINE)
// Gelen web isteklerinin (Request) sırasıyla geçeceği güvenlik ve işlem durakları.
// (DİKKAT: Buradaki sıralama mimari açıdan kritiktir)
// ==============================================================================

// --- Hata Yönetimi ---
if (!app.Environment.IsDevelopment())
{
    // Canlı ortamda (Production) detaylı teknik hataları gizle, şık hata sayfasını göster.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection(); // HTTP isteklerini güvenli HTTPS protokolüne zorla.
app.UseStaticFiles();      // CSS, JS, Resim gibi statik dosyaların dışarıya sunulmasına izin ver.

app.UseRouting();          // URL rotalarını çözümle.

// --- Güvenlik Turnikeleri ---
app.UseAuthentication();  // 1. Adım: Kullanıcının kimliğini doğrula (Sen kimsin?)

app.UseAuthorization();   // 2. Adım: Kullanıcının yetkisini kontrol et (Buraya girmeye yetkin var mı?)

// --- Controller Yönlendirmesi ---
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Varsayılan URL şeması

app.Run();