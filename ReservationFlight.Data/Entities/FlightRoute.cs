using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Data.Entities
{
    public class FlightRoute
    {
        public int Id { get; set; }
        public string DepartureId { get; set; }
        public string ArrivalId { get; set; }
        public int Status { get; set; }
        public Airport Airport { get; set; }
        public List<FlightSchedule> FlightSchedules { get; set; }
    }
}
