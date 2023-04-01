using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Statistics
{
    public class StatisticViewModel
    {
        public int NumberOfTicketsSold { get; set; }
        public int NumberOfCustomersPurchased { get; set; }
        public string TheRouteWithTheHighestNumberOfPurchases { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
