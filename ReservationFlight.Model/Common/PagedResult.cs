using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Model.Common
{
    public class PagedResult<T> : PagedResultBase
    {
        public List<T> Items { set; get; }
    }
}
