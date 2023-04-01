using ReservationFlight.Model.Catalog.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.ApiIntegration
{
    public interface IStatisticApiClient
    {
        Task<StatisticViewModel> GetStatisticByMonth(int month);
    }
}
