using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori adı kısmı boş bırakılamaz.");
            RuleFor(x=>x.CategoryDescription).NotEmpty().WithMessage("Açıklama kısmı boş bırakılamaz.");
            RuleFor(x => x.CategoryName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girin.");
            RuleFor(x => x.CategoryName).MaximumLength(20).WithMessage("Lütfen 20den fazla karakter girmeyin.");


        }
    }
}
