using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.Domain.Catalog.Statistics;

namespace ReservationFlight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("GetStatisticByMonth/{month}")]
        public IActionResult GetStatisticByMonth(int month)
        {
            var statistic = _statisticService.GetStatisticByMonth(month);
            return Ok(statistic);
        }
    }
}
