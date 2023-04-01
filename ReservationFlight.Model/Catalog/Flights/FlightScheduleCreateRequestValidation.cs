using FluentValidation;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class FlightScheduleCreateRequestValidation : AbstractValidator<FlightScheduleCreateRequest>
    {
        public FlightScheduleCreateRequestValidation()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_PRICE_FLIGHTSCHEDULE))
                .GreaterThan(0)
                .WithMessage(string.Format(
                    Constants.ERR_GREATER_THAN,
                    Constants.DISPLAY_ATTRIBUTE_PRICE_FLIGHTSCHEDULE,
                    0));

            RuleFor(x => x.SeatEconomy)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_SEAT_ECONOMY_FLIGHTSCHEDULE));

            RuleFor(x => x.SeatBusiness)
                .NotEmpty()
                .WithMessage(string.Format(
                    Constants.ERR_EMPTY,
                    Constants.DISPLAY_ATTRIBUTE_SEAT_BUSINESS_FLIGHTSCHEDULE));
        }
    }
}
