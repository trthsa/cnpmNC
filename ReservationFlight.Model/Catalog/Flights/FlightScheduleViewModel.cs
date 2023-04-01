using ReservationFlight.Utility;
using System.ComponentModel.DataAnnotations;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class FlightScheduleViewModel
    {        
        public int Id { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_FLIGHT_ROUTE_ID_FLIGHTSCHEDULE)]
        public string FlightRouteId { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_AVIATION_ID_FLIGHTSCHEDULE)]
        public string AviationId { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_FLIGHT_NUMBER_FLIGHTSCHEDULE)]
        public string FlightNumber { get; set; }
        public string DepartureId { get; set; }
        public string ArrivalId { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_PRICE_FLIGHTSCHEDULE)]
        public decimal Price { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_DATE_FLIGHTSCHEDULE)]
        public DateTime Date { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_SCHEDULED_TIME_DEPARTURE_FLIGHTSCHEDULE)]
        public TimeSpan ScheduledTimeDeparture { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_SCHEDULED_TIME_ARRIVAL_FLIGHTSCHEDULE)]
        public TimeSpan ScheduledTimeArrival { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_SEAT_ECONOMY_FLIGHTSCHEDULE)]
        public int SeatEconomy { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_SEAT_BUSINESS_FLIGHTSCHEDULE)]
        public int SeatBusiness { get; set; }

        [Display(Name = Constants.DISPLAY_ATTRIBUTE_STATUS_FLIGHTSCHEDULE)]
        public int Status { get; set; }
    }
}
