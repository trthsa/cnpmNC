using FluentValidation;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordViewModel>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_EMAIL_USER));
        }
    }
}
