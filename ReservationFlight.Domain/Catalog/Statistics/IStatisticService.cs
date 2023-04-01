using ReservationFlight.Model.Catalog.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Domain.Catalog.Statistics
{
    public interface IStatisticService
    {
        StatisticViewModel GetStatisticByMonth(int month);
    }
}
