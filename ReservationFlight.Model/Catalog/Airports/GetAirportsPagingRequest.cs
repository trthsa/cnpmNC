using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Airports
{
    public class GetAirportsPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
