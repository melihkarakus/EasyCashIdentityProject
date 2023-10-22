using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCashIdentityProject.BusinessLayer.ValidationRules.AppUserValidationRules
{                                         //AbstractValidator sınıfından kalıntı aldık ve bunu dtos tarafında tanımladığımız 
    //RegisterDtos için tanımlancak olan verileri buraya tanımladık ve bunuda burada belirttik.
    public class AppUserRegisterValidator : AbstractValidator<AppUserRegisterDto> 
    {
        // ctor oluşturmamuız lazım yoksa rulefor çıkmaz çünkü abstractValidator tanımı appuserregisterdto içinde rulefor atamış olduk
        public AppUserRegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen AD alanını doldurunuz.");//Kural
            RuleFor(x => x.Name).MinimumLength(20).WithMessage("Lütfen en az 20 karakter girişi yapın.");//minimum kural
            RuleFor(x => x.Name).MaximumLength(20).WithMessage("Lütfen en fazla 20 karakter girişi yapın.");//maximum kural

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Lütfen SOYAD alanını doldurunuz.");
            RuleFor(x => x.Surname).MinimumLength(20).WithMessage("Lütfen en az 20 karakter girişi yapın.");
            RuleFor(x => x.Surname).MaximumLength(20).WithMessage("Lütfen en fazla 20 karakter girişi yapın.");

            RuleFor(x => x.Username).NotEmpty().WithMessage("Lütfen KULLANICI ADI alanını doldurunuz.");
            RuleFor(x => x.Username).MinimumLength(50).WithMessage("Lütfen en az 50 karakter girişi yapın.");
            RuleFor(x => x.Username).MaximumLength(50).WithMessage("Lütfen en fazla 50 karakter girişi yapın.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Lütfen MAİL alanını doldurunuz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Lütfen geçerli mail adresi giriniz.");//sadece email türünde kural

            RuleFor(x => x.Password).NotEmpty().WithMessage("Lütfen ŞİFRE alanını doldurunuz.");

            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Lütfen ŞİFRE TEKRAR alanını doldurunuz.");
            RuleFor(x => x.ConfirmPassword).Equal(y => y.Password).WithMessage("PAROLA eşleşmedi.");//password girilen şifreyi aynı
            //değil mi diye bakan kural
        }

    }
}
