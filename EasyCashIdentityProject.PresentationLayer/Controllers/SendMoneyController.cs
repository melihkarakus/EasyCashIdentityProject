using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.DtoLayer.Dtos.CustomerAccountProcessDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Runtime.InteropServices;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class SendMoneyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerAccountProcessService _customerAccountProcessService;

        public SendMoneyController(UserManager<AppUser> userManager, ICustomerAccountProcessService customerAccountProcessService)
        {
            // Kullanıcı yönetimini (UserManager) ve müşteri hesap işlem servisini (_customerAccountProcessService) enjekte eden constructor (kurucu metod).
            _userManager = userManager;
            _customerAccountProcessService = customerAccountProcessService;
        }

        [HttpGet]
        public IActionResult Index(string mycurrency)
        {
            // GET isteği için Index aksiyonu. Müşterinin seçtiği para birimini görüntülemek için 'mycurrency' değerini ViewBag ile görüntüler.
            ViewBag.mycurrency = mycurrency;
            return View(); // 'Index' adlı bir görünümü döndürür.
        }

        [HttpPost]
        public async Task<IActionResult> Index(SendMoneyForCustomerAccountProcessDto sendMoneyForCustomerAccountProcessDto)
        {
            // POST isteği için Index aksiyonu. Para transferi işlemini gerçekleştirir.

            // Yeni bir veritabanı bağlamı oluşturur.
            var context = new Context();

            // Kullanıcıyı oturum açmış kullanıcıdan alır.
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Alıcı hesap numarasına karşılık gelen hesap kimliğini bulur.
            var receiverAccountNumberID = context.CustomerAccounts
                .Where(x => x.CustomerAccounNumber == sendMoneyForCustomerAccountProcessDto.ReceiverAccountNumber)
                .Select(y => y.CustomerAccountID)
                .FirstOrDefault();

            // Gönderen hesap numarasına karşılık gelen hesap kimliğini bulur.
            var senderAccountNumberID = context.CustomerAccounts
                .Where(x => x.AppUserID == user.Id)
                .Where(y => y.CustomerAccountCurrency == "Türk Lirası")
                .Select(z => z.CustomerAccountID)
                .FirstOrDefault();

            // Müşteri hesap işlemi için yeni bir değer nesnesi oluşturur.
            var values = new CustomerAccountProcess();
            values.ProcessDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            values.SenderID = senderAccountNumberID;
            values.ProcessType = "Havale";
            values.ReceiverID = receiverAccountNumberID;
            values.Amount = sendMoneyForCustomerAccountProcessDto.Amount;
            values.Description = sendMoneyForCustomerAccountProcessDto.Description;

            // Müşteri hesap işlem servisi ile bu değerleri veritabanına ekler.
            _customerAccountProcessService.TInsert(values);

            // 'Deneme' denilen başka bir Controller'ın 'Index' aksiyonuna yönlendirir.
            return RedirectToAction("Index", "Deneme");
        }
    }
}
