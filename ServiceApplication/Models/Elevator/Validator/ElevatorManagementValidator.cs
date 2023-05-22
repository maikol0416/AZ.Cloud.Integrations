using Domain.Entities;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Validator
{
    public class ElevatorManagementValidator : AbstractValidator<ElevatorMovementDto>
    {
        public ElevatorManagementValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithErrorCode($"NameEmpty")
            .WithMessage(x => x.Code)
                 .WithName(nameof(ElevatorMovementDto.Code));
        }
    }
}
