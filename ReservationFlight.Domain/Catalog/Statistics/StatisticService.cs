using ReservationFlight.Data.EF;
using ReservationFlight.Model.Catalog.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Domain.Catalog.Statistics
{
    public class StatisticService : IStatisticService
    {
        private readonly ReservationFlightDbContext _context;
        public StatisticService(ReservationFlightDbContext context)
        {
            _context = context;
        }
        public StatisticViewModel GetStatisticByMonth(int month)
        {
            var numberOfTicketsSold = from r in _context.Reservations
                                      join fs in _context.FlightSchedules on r.IdFlightSchedule equals fs.Id
                                      where fs.Date.Month == month
                                      group r by r.Id into grp                                     
                                      select grp;

            var numberOfCustomersPurchased = _context.Reservations.Select(x => x.IdentityNumber).Distinct().Count();

            var idFLightScheduleWithTheHighestNumberOfPurchases = _context.Reservations
                .GroupBy(x => x.IdFlightSchedule)
                .First()
                .Key;

            var idFlightRoute = _context.FlightSchedules
                .Where(x => 
                    x.Id == idFLightScheduleWithTheHighestNumberOfPurchases)
                .First()
                .FlightRouteId;

            var theRouteWithTheHighestNumberOfPurchases = string.Format(
                "{0}-{1}",
                _context.FlightRoutes.Where(x => x.Id == idFlightRoute).First().DepartureId,
                _context.FlightRoutes.Where(x => x.Id == idFlightRoute).First().ArrivalId);

            var totalRevenue = _context.Reservations.Sum(x => x.Price);

            return new StatisticViewModel
            {
                NumberOfTicketsSold = numberOfTicketsSold.Count(),
                NumberOfCustomersPurchased = numberOfCustomersPurchased,
                TheRouteWithTheHighestNumberOfPurchases = theRouteWithTheHighestNumberOfPurchases,
                TotalRevenue = totalRevenue
            };
        }
    }
}
