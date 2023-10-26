using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
	public class ConfirmMailController : Controller
	{
        private readonly UserManager<AppUser> _userManager;

        // ConfirmController sınıfının constructor'ı, UserManager'ı enjekte eder.
        public ConfirmMailController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // HTTP GET isteği karşılayan bir işlem. Onaylama sayfasını görüntüler.
        [HttpGet]
        public IActionResult Index()
        {
            // TempData üzerinden "Mail" verisini alır.
            var value = TempData["Mail"];

            // ViewBag kullanarak view'a bir değer ekler.
            ViewBag.v = value;

            // Onaylama sayfasını görüntüler.
            return View();
        }

        // HTTP POST isteği karşılayan bir işlem. Onaylama işlemi gerçekleştirir.
        [HttpPost]
        public async Task<IActionResult> Index(ConfirmMailViewModel confirmMailViewModel)
        {
            // Kullanıcıyı e-posta adresine göre arar.
            var user = await _userManager.FindByEmailAsync(confirmMailViewModel.Mail);

            // Kullanıcının onaylama kodunu gelen kodla karşılaştırır.
            if (user.ConfirmCode == confirmMailViewModel.ConfirmCode)
            {
                user.EmailConfirmed = true;//userdan gelen emailconfirm true dönmesi gerekli kod 
                await _userManager.UpdateAsync(user);//usermanager atılan kodu update fonksiyonu ile güncellendi ve database kaydedildi.
                // Kullanıcıyı doğru kodla onaylanmışsa "MyProfile" sayfasına yönlendirir.
                return RedirectToAction("Index", "Login");
            }

            // Onaylama kodu eşleşmezse, aynı sayfayı tekrar görüntüler.
            return View();
        }
    }
}
