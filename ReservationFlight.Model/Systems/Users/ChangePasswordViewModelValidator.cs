using FluentValidation;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Model.Systems.Users
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_NEW_PASSWORD_USER))
                 .MinimumLength(Constants.DEFAULT_MINIMUM_LENGTH_NEW_PASSWORD_USER)
                 .WithMessage(string.Format(
                     Constants.ERR_MINIMUM_LENGTH,
                     Constants.DISPLAY_ATTRIBUTE_NEW_PASSWORD_USER,
                     Constants.DEFAULT_MINIMUM_LENGTH_NEW_PASSWORD_USER))
               .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
               .WithMessage(Constants.ERR_FORMAT_PASSWORD_USER);
        }
    }
}