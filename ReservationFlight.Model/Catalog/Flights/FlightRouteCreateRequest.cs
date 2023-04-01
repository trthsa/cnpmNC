using ReservationFlight.Utility;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class FlightRouteCreateRequest
    {
        [Display(Name = Constants.DISPLAY_ATTRIBUTE_DEPARTURE_ID_FLIGHTROUTE)]
        public string DepartureId { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_ARRIVAL_ID_FLIGHTROUTE)]
        public string ArrivalId { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_STATUS_FLIGHTROUTE)]
        public int Status { get; set; }
    }
}
