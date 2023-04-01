using FluentValidation;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Airports
{
    public class AirportUpdateRequestValidation : AbstractValidator<AirportUpdateRequest>
    {
        public AirportUpdateRequestValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_NAME_AIRPORT))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_NAME_AIRPORT)
                .WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_NAME_AIRPORT,
                    Constants.DEFAULT_MAXIMUM_LENGTH_NAME_AIRPORT));

            RuleFor(x => x.IATA)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_IATA_AIRPORT))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_IATA_CODE)
                .WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_IATA_AIRPORT,
                    Constants.DEFAULT_MAXIMUM_LENGTH_IATA_CODE));
        }
    }
}
