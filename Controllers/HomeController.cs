using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductManagementPanel.Models;

namespace ProductManagementPanel.Controllers;

public class HomeController : Controller
{
    /// Uygulamanın ana karşılama (Lobi) işlemlerini, gizlilik politikasını ve genel hata (Exception) sayfalarını yöneten Controller.
    private readonly ILogger<HomeController> _logger;

    // Dependency Injection ile .NET'in yerleşik loglama servisini içeri alır.
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Uygulamanın kök dizinine gelindiğinde çalışan ana karşılama sayfasını (Dashboard/Lobi) döndürür.
    public IActionResult Index()
    {
        return View();
    }

    // Uygulamanın gizlilik politikası ve yasal uyarılar sayfasını döndürür.
    public IActionResult Privacy()
    {
        return View();
    }

    // Uygulama genelinde yakalanmayan bir hata (Exception) oluştuğunda kullanıcıya gösterilecek olan güvenli hata sayfasını döndürür.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
