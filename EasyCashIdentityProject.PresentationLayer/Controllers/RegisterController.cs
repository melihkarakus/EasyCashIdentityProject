using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

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
				Random random = new Random(); //Random sayı için oluşturuldu
				int code;
				code = random.Next(100000, 1000000);//random içine belirtilen sayılar ilave edildi random oluşacaktır.
				// ModelState, sayfanın sunum katmanında doğrulama kurallarının geçip geçmediğini kontrol eder.

				AppUser appUser = new AppUser()
				{
					UserName = appUserRegisterDto.Username,
					Name = appUserRegisterDto.Name,
					Surname = appUserRegisterDto.Surname,
					Email = appUserRegisterDto.Email,
					City = "Veri Gelecek",
					District = "Veri Gelecek",
					ImageUrl = "Veri Gelecek",
					ConfirmCode = code
				};

				// Yeni bir AppUser nesnesi oluşturulur ve kullanıcı tarafından sağlanan verilerle doldurulur.

				var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);

				// _userManager, kullanıcı hesaplarını yönetmek için bir kimlik doğrulama servisini temsil eder ve 
				// CreateAsync yöntemi ile yeni bir kullanıcı hesabı oluşturulur.

				if (result.Succeeded)
				{
					// Kullanıcı hesabının başarıyla oluşturulup oluşturulmadığı kontrol edilir ve başarılıysa aşağıdaki işlem yapılır:
					// Yönlendirme yapılır ve kullanıcıyı "ConfirmMail" adlı controller'ın "Index" adlı action'ına yönlendirir.
					// MimeMessage nesnesi oluşturuluyor, bu nesne e-posta ile ilgili temel bilgileri ve içeriği temsil eder.
					MimeMessage mimeMessage = new MimeMessage();

					// Gönderen e-posta adresi oluşturuluyor.
					MailboxAddress mailboxAddressFrom = new MailboxAddress("Easy Cash Admin", "pproje1818@gmail.com");

					// Alıcı e-posta adresi oluşturuluyor. 'appUser.Email' alıcı e-posta adresini içeriyor.
					MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);

					// Gönderen ve alıcı adresleri MimeMessage nesnesine ekleniyor.
					mimeMessage.From.Add(mailboxAddressFrom);
					mimeMessage.To.Add(mailboxAddressTo);

					// E-posta gövdesini oluşturan BodyBuilder nesnesi oluşturuluyor ve metin gövde içeriği ayarlanıyor.
					var bodyBuilder = new BodyBuilder();
					bodyBuilder.TextBody = "Kayıt işlemi gerçekleştirmek için onay kodunuz: " + code;

					// BodyBuilder nesnesinin içeriği MimeMessage nesnesine ekleniyor.
					mimeMessage.Body = bodyBuilder.ToMessageBody();

					// E-postanın konusu belirleniyor.
					mimeMessage.Subject = "Easy Cash Onay Kodu";

					// SmtpClient nesnesi oluşturuluyor ve Gmail SMTP sunucusuna bağlanılıyor.
					SmtpClient client = new SmtpClient();
					client.Connect("smtp.gmail.com", 587, false);

					// Gmail hesabınıza oturum açılıyor. Kullanıcı adı ve şifre ile oturum açılıyor, ancak uygulama özel şifre kullanmanız daha güvenli olabilir.
					client.Authenticate("pproje1818@gmail.com", "nqvyqdcpddamvarl");

					// E-posta gönderme işlemi gerççekleştiriliyor.
					client.Send(mimeMessage);

					// SmtpClient bağlantısı kapatılıyor.
					client.Disconnect(true);

                    TempData["Mail"] = appUserRegisterDto.Email; //appuserda mail tempdata eşitlendi.

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
