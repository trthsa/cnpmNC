using FluentValidation;
using ReservationFlight.Utility;
using System;

namespace ReservationFlight.Model.Systems.Users
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            // Đây là một phương thức của abstract validator
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_USER_NAME_USER));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_PASSWORD_USER));
        }
    }
}