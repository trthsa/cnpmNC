using FluentValidation;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Model.Catalog.Aviations
{
    public class AviationCreateRequestValidation : AbstractValidator<AviationCreateRequest>
    {
        public AviationCreateRequestValidation()
        {
            RuleFor(x => x.AviationCode)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_AVIATION_CODE))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_AVIATION_CODE)
                .WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_AVIATION_CODE,
                    Constants.DEFAULT_MAXIMUM_LENGTH_AVIATION_CODE));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_NAME_AVIATION))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_NAME_AVIATION).
                WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_NAME_AVIATION,
                    Constants.DEFAULT_MAXIMUM_LENGTH_NAME_AVIATION));
        }
    }
}
