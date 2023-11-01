using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class MyLastProcessController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerAccountProcessService _customerAccountProcessService;

        public MyLastProcessController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
        {
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name); // Kullanıcıyı kimlik adına göre bulur.

            var context = new Context(); // Veritabanı bağlamını oluşturur.

            // Kullanıcının kimliği ile ilişkilendirilmiş ve para birimi "Türk Lirası" olan müşteri hesaplarını sorgular ve ilgili ilk hesap ID'sini alır.
            int id = context.CustomerAccounts
                .Where(x => x.AppUserID == user.Id && x.CustomerAccountCurrency == "Türk Lirası")
                .Select(y => y.CustomerAccountID)
                .FirstOrDefault();

            // Hesap ID'si kullanılarak son işlem verilerini alır.
            var values = _customerAccountProcessService.TMyLastProcess(id);

            // Sonuçları bir görünüme aktarır.
            return View(values);
        }
    }
}
