using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    [Authorize]//login işlemi zorunlu
    public class MyAccountsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public MyAccountsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // HTTP GET isteği karşılayan bu metot, kullanıcının hesap bilgilerini görüntülemek için kullanılır.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Kullanıcının kimlik bilgileri (örneğin, kullanıcı adı) ile giriş yapmış olmasını bekliyoruz
            var values = await _userManager.FindByNameAsync(User.Identity.Name);

            // Kullanıcı verilerini düzenlemek için bir DTO (Data Transfer Object) oluşturuyoruz
            AppUserEditDto appUserEditDto = new AppUserEditDto();
            appUserEditDto.Name = values.Name; //Valuesden gelen veriyi appuseregisterdto eşitliyoruz 
            appUserEditDto.Surname = values.Surname;
            appUserEditDto.PhoneNumber = values.PhoneNumber;
            appUserEditDto.Email = values.Email;
            appUserEditDto.City = values.City;
            appUserEditDto.District = values.District;
            appUserEditDto.ImageUrl = values.ImageUrl;

            // DTO'yu görüntüleyen bir görünüme dönüş yapılır
            return View(appUserEditDto);
        }

        // HTTP POST isteği karşılayan bu metot, kullanıcının hesap bilgilerini güncellemek için kullanılır.
        [HttpPost]
        public async Task<IActionResult> Index(AppUserEditDto appUserEditDto)
        {
            // Kullanıcının belirttiği yeni şifre ve teyit şifresini karşılaştırırız
            if (appUserEditDto.Password == appUserEditDto.ConfirmPassword)
            {
                // Kullanıcının kimlik bilgileri (örneğin, kullanıcı adı) ile giriş yapmış olmasını bekliyoruz
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                // Kullanıcının girdiği yeni bilgilerle kullanıcı nesnesini güncelliyoruz
                user.PhoneNumber = appUserEditDto.PhoneNumber;
                user.Surname = appUserEditDto.Surname;
                user.Name = appUserEditDto.Name;
                user.City = appUserEditDto.City;
                user.District = appUserEditDto.District;
                user.ImageUrl = "Veri Gelecek"; // Kullanıcı resmi güncellenmeli
                user.Email = appUserEditDto.Email;

                // Kullanıcının şifresini güncellemek ve güncellenmiş kullanıcıyı kaydetmek için UserManager'ı kullanırız
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, appUserEditDto.Password);
                var result = await _userManager.UpdateAsync(user);

                // Kullanıcının bilgileri başarıyla güncellendiyse, oturumu kapatmadan önce giriş sayfasına yönlendiririz
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            // Eğer şifreler uyuşmuyorsa veya başka bir hata olursa, aynı sayfaya geri dönülür
            return View();
        }
    }
}
