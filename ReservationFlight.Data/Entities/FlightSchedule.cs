using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Data.Entities
{
    public class FlightSchedule
    {
        public int Id { get; set; }
        public int FlightRouteId { get; set; }
        public string AviationId { get; set; }
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan ScheduledTimeDeparture { get; set; }
        public TimeSpan ScheduledTimeArrival { get; set; }
        public int SeatEconomy { get; set; }
        public int SeatBusiness { get; set; }
        public int Status { get; set; }
        public FlightRoute FlightRoute { get; set; }
        public Aviation Aviation { get; set; }
    }
}
