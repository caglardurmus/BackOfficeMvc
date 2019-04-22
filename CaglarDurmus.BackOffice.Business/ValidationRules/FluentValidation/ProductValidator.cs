using FluentValidation;
using CaglarDurmus.BackOffice.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglarDurmus.BackOffice.Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün ismi boş olamaz!");
            RuleFor(p => p.CategoryID).NotEmpty();
            RuleFor(p => p.QuantityPerUnit).NotEmpty();
            RuleFor(p => p.UnitsInStock).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();

            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitsInStock).GreaterThanOrEqualTo((short)0);
            //RuleFor(p => p.UnitPrice).GreaterThan(10).When(p => p.CategoryId == 2).WithMessage("Kategori 2'de Fiyat 10 dan büyük olmalı!");

            //RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürün adı A ile başlamalı!");           //ProductName A ile başlasın diye örnek metot
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
