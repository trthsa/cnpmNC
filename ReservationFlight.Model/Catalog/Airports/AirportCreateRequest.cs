using ReservationFlight.Model.Systems.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Airports
{
    public class AirportCreateRequest
    {
        public string IATA { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
