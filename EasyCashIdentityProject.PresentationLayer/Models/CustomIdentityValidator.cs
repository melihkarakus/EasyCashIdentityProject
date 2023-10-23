using Microsoft.AspNetCore.Identity;

namespace EasyCashIdentityProject.PresentationLayer.Models
{
	//kimlik doğrulama hatasının açıkça gösterilmesi veya özelleştirilmesi
	public class CustomIdentityValidator : IdentityErrorDescriber
	{
		//override methodun işleyişi bozulmadan bizim istediğimiz tarzda yazılmasını sağlıyoruz
		public override IdentityError PasswordTooShort(int length)//Errors isimleri overrider içerikli tanımlarız
		{
			return new IdentityError()//yeni bir identity Error mesajı oluşturduk
			{
				Code = "PasswordTooShort",//Buraya verilen başlık 
				Description = $"Parolan en az {length} karakter olmalıdır.",//Burası kendi açıklamamız 
			};
		}
		public override IdentityError PasswordRequiresUpper()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresUpper",
				Description = "En az 1 büyük harf giriniz.",
			};
		}
		public override IdentityError PasswordRequiresLower()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresLower",
				Description = "En az 1 küçük harf giriniz."
			};
		}
		public override IdentityError PasswordRequiresDigit()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresDigit",
				Description = "En az 1 rakam giriniz.",
			};
		}
		public override IdentityError PasswordRequiresNonAlphanumeric()
		{
			return new IdentityError()
			{
				Code = "PasswordRequiresNonAlphanumeric",
				Description = "En az 1 sembol giriniz {!,#,/,+}."
			};
		}
	}
}
