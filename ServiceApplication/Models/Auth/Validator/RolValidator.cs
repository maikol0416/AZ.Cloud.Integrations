using Domain.Entities;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Validator
{
    public class RolValidator : AbstractValidator<RolDto>
    {
        public RolValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithErrorCode($"NameEmpty")
            .WithMessage(x => x.Name)
                 .WithName(nameof(Rol.Name));

            RuleFor(x => x.Description).NotEmpty().WithErrorCode($"DescriptionEmpty")
           .WithMessage(x => x.Description)
                .WithName(nameof(Rol.Description));
        }
    }
}
