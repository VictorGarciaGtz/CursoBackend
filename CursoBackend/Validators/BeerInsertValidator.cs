using CursoBackend.DTOs;
using FluentValidation;

namespace CursoBackend.Validators
{
    public class BeerInsertValidator: AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio.");
            RuleFor(x => x.Name).Length(2, 20).WithMessage("El nombre debe estar entre 2 a 20 caracteres.");
            RuleFor(x => x.BrandID).NotNull().WithMessage("La marca es obligatoria.");
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage("La marca debe ser mayor a 0.");
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage("El {PropertyName} debe ser mayor a 0.");
        }
    }
}
