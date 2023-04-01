using Microsoft.AspNetCore.Mvc;
using ReservationFlight.ApiIntegration;
using ReservationFlight.Model.Catalog.Reservations;

namespace ReservationFlight.WebAdmin.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationApiClient _reservationApiClient;
        public ReservationController(IReservationApiClient reservationApiClient)
        {
            _reservationApiClient = reservationApiClient;
        }
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetReservationPagingRequest
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _reservationApiClient.GetReservationsPaging(request);
            ViewBag.Keyword = keyword;
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string reservationCode)
        {
            var data = await _reservationApiClient.GetReservationByCode(reservationCode);
            return View(data);
        }
    }
}
