using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;//usermanager sınıfından bir appuser bağlı türetim yaptık.

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
			if (ModelState.IsValid)
			{
				// ModelState, sayfanın sunum katmanında doğrulama kurallarının geçip geçmediğini kontrol eder.

				AppUser appUser = new AppUser()
				{
					UserName = appUserRegisterDto.Username,
					Name = appUserRegisterDto.Name,
					Surname = appUserRegisterDto.Surname,
					Email = appUserRegisterDto.Email,
					City = "Veri Gelecek",
					District = "Veri Gelecek",
					ImageUrl = "Veri Gelecek"
				};

				// Yeni bir AppUser nesnesi oluşturulur ve kullanıcı tarafından sağlanan verilerle doldurulur.

				var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);

				// _userManager, kullanıcı hesaplarını yönetmek için bir kimlik doğrulama servisini temsil eder ve 
				// CreateAsync yöntemi ile yeni bir kullanıcı hesabı oluşturulur.

				if (result.Succeeded)
				{
					// Kullanıcı hesabının başarıyla oluşturulup oluşturulmadığı kontrol edilir ve başarılıysa aşağıdaki işlem yapılır:
					// Yönlendirme yapılır ve kullanıcıyı "ConfirmMail" adlı controller'ın "Index" adlı action'ına yönlendirir.

					return RedirectToAction("Index", "ConfirmMail");
				}
				else
				{
					foreach (var item in result.Errors) //ekrana password ile alakalı hata çıkartmak istiyorsak bu kodu çalıştırabiliriz
					{
						ModelState.AddModelError("", item.Description); //key değeri boşluk bırakıp item.description yani açıklaması
					}
				}
			}

			// Eğer ModelState doğrulamayı geçemezse veya kullanıcı hesabı oluşturulamazsa, kullanıcıyı aynı sayfada tutar ve View() yöntemiyle
			// aynı sayfanın görünümünü geri döndürür.
			return View();
		}
    }
}
