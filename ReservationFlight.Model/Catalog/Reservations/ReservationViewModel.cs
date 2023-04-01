using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Reservations
{
    public class ReservationViewModel
    {
        public string ReservationCode { get; set; }
        public int IdFlightSchedule { get; set; }
        public string TravelClass { get; set; }
        public string IdentityNumber { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
    }
}
