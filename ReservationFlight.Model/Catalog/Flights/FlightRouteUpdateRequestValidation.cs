using FluentValidation;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class FlightRouteUpdateRequestValidation : AbstractValidator<FlightRouteUpdateRequest>
    {
        public FlightRouteUpdateRequestValidation()
        {
            RuleFor(x => x.DepartureId)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_DEPARTURE_ID_FLIGHTROUTE))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_DEPARTURE_ID_FLIGHTROUTE)
                .WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_DEPARTURE_ID_FLIGHTROUTE,
                    Constants.DEFAULT_MAXIMUM_LENGTH_DEPARTURE_ID_FLIGHTROUTE));

            RuleFor(x => x.ArrivalId)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_ARRIVAL_ID_FLIGHTROUTE))
                .MaximumLength(Constants.DEFAULT_MAXIMUM_LENGTH_ARRIVAL_ID_FLIGHTROUTE).
                WithMessage(string.Format(
                    Constants.ERR_MAXIMUM_LENGTH,
                    Constants.DISPLAY_ATTRIBUTE_ARRIVAL_ID_FLIGHTROUTE,
                    Constants.DEFAULT_MAXIMUM_LENGTH_ARRIVAL_ID_FLIGHTROUTE));
        }
    }
}
