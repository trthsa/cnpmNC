using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Flights
{
    public class GetFlightSchedulesPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
