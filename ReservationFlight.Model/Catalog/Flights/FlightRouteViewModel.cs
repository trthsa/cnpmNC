using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class FlightRouteViewModel
    {
        public int Id { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_DEPARTURE_ID_FLIGHTROUTE)]
        public string Departure { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_ARRIVAL_ID_FLIGHTROUTE)]
        public string Arrival { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_STATUS_FLIGHTROUTE)]
        public int Status { get; set; }

        public List<FlightScheduleViewModel> FlightSchedules { get; set; }
    }
}
