using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Flights
{
	public class FlightScheduleCondition
	{
		public string DepartureId { get; set; }
		public string ArrivalId { get; set; }
		public DateTime Date { get; set; }
		public DateTime DateOneWay { get; set; }
		public DateTime DateRoundTrip { get; set; }
		public string QuantityAdult { get; set; }
		public string QuantityChild { get; set; }
		public int JourneyType { get; set; }
	}
}
