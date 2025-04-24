using Empresa.Application.DTOs;
using FluentValidation;

namespace Empresa.Application.Validators
{
    public class UsuarioDtoValidator : AbstractValidator<UsuarioDto>
    {
        public UsuarioDtoValidator()
        {
            RuleFor(x => x.UserNome).NotEmpty();
            RuleFor(x => x.UserLogin).NotEmpty();
            RuleFor(x => x.UserPass).NotEmpty().MinimumLength(6);
        }
    }
}
