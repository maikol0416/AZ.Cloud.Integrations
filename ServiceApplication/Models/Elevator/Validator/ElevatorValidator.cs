using Domain.Entities;
using FluentValidation;
using ServiceApplication.Dto;

namespace ServiceApplication.Models.Auth.Validator
{
    public class ElevatorValidator : AbstractValidator<ElevatorDto>
    {
        public ElevatorValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithErrorCode($"NameEmpty")
            .WithMessage(x => x.Code)
                 .WithName(nameof(Elevator.Code));
        }
    }
}
