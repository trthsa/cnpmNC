using Microsoft.AspNetCore.Mvc;

namespace ReservationFlight.WebAdmin.Controllers
{
    public class StatisticController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
