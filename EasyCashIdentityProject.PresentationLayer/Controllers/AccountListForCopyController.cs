using EasyCashIdentityProject.BusinessLayer.Abstract;
using EasyCashIdentityProject.DataAccessLayer.Concrete;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class AccountListForCopyController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerAccountService _customerAccountService;

        public AccountListForCopyController(UserManager<AppUser> userManager, ICustomerAccountService customerAccountService)
        {
            _userManager = userManager;
            _customerAccountService = customerAccountService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);//Otantike olan kullanıcı seçtik
            var context = new Context();//Database Bağlantısı sağladık
            //CustomerAccounts içindeki verileri bir listeleme şeklinde bizim otantike olan kullanıcının id ile listeleme seçiyoruz.
            var values = _customerAccountService.TGetCustomerAccountList(user.Id);
            return View(values);
        }
    }
}
