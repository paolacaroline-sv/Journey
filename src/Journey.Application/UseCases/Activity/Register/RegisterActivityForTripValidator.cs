using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Journey.Communication.Requests;

namespace Journey.Application.UseCases.Activity.Register
{
    public class RegisterActivityForTripValidator : AbstractValidator<RequestRegisterActivityJson>
    {
        public RegisterActivityForTripValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The activity name is required.");
        }        
    }
}