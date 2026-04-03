using FluentValidation;
using Journey.Communication.Requests;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
    {
        public RegisterTripValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The trip name is required.");

            RuleFor(x => x.StartDate.Date)
                .NotEmpty().WithMessage("The start date is required.")
                .GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("The start date cannot be in the past.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("The end date is required.")
                .GreaterThan(x => x.StartDate).WithMessage("The end date must be after the start date.");

            RuleFor(x => x)
                .Must(x => x.EndDate.Date >= x.StartDate.Date).WithMessage("The end date must be on or after the start date.");
        }
    }
}