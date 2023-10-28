using EasyCashIdentityProject.EntityLayer.Concrete;
using EasyCashIdentityProject.PresentationLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;//signinden bir metod türettik.
        private readonly UserManager<AppUser> _userManager;

        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)//LoginViewModel içindeki verileri buraya taşıdık
        {
            //sonuç eşit ise signınmanager'dan passwordsingin seçip (Loginviewmodel deki username ve password seçip ardından
            //Kullanıcı adı ve şifre hatırlansın mı false çevirdim ve 5 kere hata girince lock olsun mu true yaptım.)
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.password, false, true);
            if (result.Succeeded)//eğer sonuç doğruysa
            {
                //usermanagerdan name şekilde seçicem bunu seçerkende (LoginviewModel içindeki usernameden seçicem)
                var user = await _userManager.FindByNameAsync(loginViewModel.Username);
                if(user.EmailConfirmed == true)//eğer userdan gelen confirmcode doğru ise 
                {
                    //MyProfile Index atmış oluruz.
                    return RedirectToAction("Index", "MyAccounts");
                }
            }
            return View();
        }
    }
}
