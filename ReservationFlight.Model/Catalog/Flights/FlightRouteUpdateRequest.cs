using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class FlightRouteUpdateRequest
    {
        public int Id { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_DEPARTURE_ID_FLIGHTROUTE)]
        public string DepartureId { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_ARRIVAL_ID_FLIGHTROUTE)]
        public string ArrivalId { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_STATUS_FLIGHTROUTE)]
        public int Status { get; set; }
    }
}
