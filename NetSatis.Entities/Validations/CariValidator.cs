using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using NetSatis.Entities.Tables;

namespace NetSatis.Entities.Validations
{
public    class CariValidator:AbstractValidator<Cari>
    {
        public CariValidator()
        {
            RuleFor(p => p.CariKodu).NotEmpty().WithMessage("Cari Kodu alanı boş geçilemez");
            RuleFor(p => p.CariAdi).NotEmpty().WithMessage("Cari Adı alanı boş geçilemez");
            RuleFor(p => p.YetkiliKisi).NotEmpty().WithMessage("Yetkili Kisi alanı boş geçilemez");
            RuleFor(p => p.FaturaUnvani).NotEmpty().WithMessage("Fatura Unvanı alanı boş geçilemez");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Girdiğiniz e-mail adresi geçersiz");
            RuleFor(p => p.IskontoOrani).GreaterThanOrEqualTo(0).WithMessage("Iskonto Oranı alanı 0'dan Küçük olamaz");
            RuleFor(p => p.RiskLimiti).GreaterThanOrEqualTo(0).WithMessage("Risk Limiti alanı 0'dan Küçük olamaz");
        }
    }
}
