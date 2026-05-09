using Empresa.Application.DTOs;
using FluentValidation;

namespace Empresa.Application.Validators
{
    public class FornecedorDtoValidator : AbstractValidator<FornecedorDto>
    {
        public FornecedorDtoValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("O nome é obrigatório");
            RuleFor(x => x.Nome).MaximumLength(250);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Cnpj).MaximumLength(20);
            RuleFor(x => x.Contato).MaximumLength(50);
            RuleFor(x => x.Endereco).MaximumLength(300);
        }
    }
}
