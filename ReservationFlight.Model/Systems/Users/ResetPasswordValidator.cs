using FluentValidation;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_EMAIL_USER));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_PASSWORD_USER))
                .MinimumLength(Constants.DEFAULT_MINIMUM_LENGTH_PASSWORD_USER)
                .WithMessage(string.Format(
                    Constants.ERR_MINIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_PASSWORD_USER))
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
                .WithMessage(Constants.ERR_FORMAT_PASSWORD_USER);
        }
    }
}