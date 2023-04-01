using FluentValidation;
using ReservationFlight.Utility;

namespace ReservationFlight.Model.Systems.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            // Đây là một phương thức của abstract validator
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

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_PHONE_NUMBER_USER))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_PHONE_NUMBER_USER)
                .WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_PHONE_NUMBER_USER,
                    Constants.DEFAULT_MAXIMUM_LENGTH_PHONE_NUMBER_USER))
                .MinimumLength(Constants.DEFAULT_MINIMUM_LENGTH_PHONE_NUMBER_USER)
                .WithMessage(string.Format(
                    Constants.ERR_MINIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_PHONE_NUMBER_USER,
                    Constants.DEFAULT_MINIMUM_LENGTH_PHONE_NUMBER_USER));

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_USER_NAME_USER));

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_ADDRESS_USER))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_ADDRESS_USER)
                .WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_ADDRESS_USER,
                    Constants.DEFAULT_MAXIMUM_LENGTH_ADDRESS_USER));

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_PASSWORD_USER))
                .MinimumLength(Constants.DEFAULT_MINIMUM_LENGTH_PASSWORD_USER)
                .WithMessage(string.Format(
                    Constants.ERR_MINIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_PASSWORD_USER,
                    Constants.DEFAULT_MINIMUM_LENGTH_PASSWORD_USER))
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")
                .WithMessage(Constants.ERR_FORMAT_PASSWORD_USER);

            // Khi ta viết => {} thì sẽ tự động hiểu request là của Register và context là của CustomContext
            //RuleFor(x => x).Custom((request, context) =>
            //  {
            //      if (request.Password != request.ConfirmPassword)
            //      {
            //          context.AddFailure("Mật khẩu xác nhận không khớp với mật khẩu");
            //      }
            //  });
        }
    }
}