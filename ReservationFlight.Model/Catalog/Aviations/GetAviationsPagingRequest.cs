using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Model.Catalog.Aviations
{
    public class GetAviationsPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
