using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class FlightViewModel
    {
        public int Id { get; set; }
        public string DepartureId { get; set; }
        public string ArrivalId { get; set; }
        public string AviationId { get; set; }
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan ScheduledTimeDeparture { get; set; }
        public TimeSpan ScheduledTimeArrival { get; set; }
        public int SeatEconomy { get; set; }
        public int SeatBusiness { get; set; }
        public int Status { get; set; }
    }
}
