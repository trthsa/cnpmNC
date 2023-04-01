using ReservationFlight.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Data.Entities
{
    public class Airport
    {
        public string IATA { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public List<FlightRoute> FlightRoutes { get; set; }
    }
}
