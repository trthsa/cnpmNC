using FluentValidation;
using ReservationFlight.Utility;

namespace ReservationFlight.Model.Systems.Users
{
    public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_NAME_USER))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_NAME_USER)
                .WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_NAME_USER,
                    Constants.DEFAULT_MAXIMUM_LENGTH_NAME_USER));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_EMAIL_USER))
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage(Constants.ERR_FORMAT_EMAIL_USER);

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_USER_NAME_USER));

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_ADDRESS_USER));

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_PHONE_NUMBER_USER));
        }
    }
}