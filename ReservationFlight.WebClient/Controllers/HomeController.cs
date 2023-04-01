using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservationFlight.ApiIntegration;
using ReservationFlight.Model.Catalog.Customers;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Catalog.Reservations;
using ReservationFlight.Utility;
using ReservationFlight.WebClient.Models;
using System.Diagnostics;

namespace ReservationFlight.WebClient.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
		private readonly IAirportApiClient _airportApiClient;
        private readonly IFlightScheduleApiClient _flightScheduleApiClient;
        private readonly IReservationApiClient _reservationApiClient;
		public HomeController(
            ILogger<HomeController> logger,
			IAirportApiClient airportApiClient,
            IFlightScheduleApiClient flightScheduleApiClient,
            IReservationApiClient reservationApiClient)
        {
            _logger = logger;
            _airportApiClient = airportApiClient;
            _flightScheduleApiClient = flightScheduleApiClient;
            _reservationApiClient = reservationApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.Clear();
            ViewBag.AirportList = await _airportApiClient.GetAll();
            return View();
        }

        [HttpPost]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> Search(FlightScheduleCondition request)
        {

            
            // Bởi vì mặc định tìm kiếm lúc nào cũng là OneWay nên sẽ gán request.Date = request.DateOneWay
            request.Date = request.DateOneWay;
            var result = await _flightScheduleApiClient.GetAllFlightByCondition(request);

            HttpContext.Session.SetObject("OneWay", request);

            HttpContext.Session.SetString("QuantityAdult", request.QuantityAdult);

            HttpContext.Session.SetString("QuantityChild", request.QuantityChild);

            HttpContext.Session.SetString(nameof(request.JourneyType), request.JourneyType.ToString());

            return View(result);
        }

        public async Task<IActionResult> SelectedOneWayFlightSchedule(int Id)
        {
            // Id này là IdFlightSchedule
            var detailOneWay = await _flightScheduleApiClient.GetById(Id);

            // Tạo session lưu model của detailOneWay và xuất giá trị đã chọn ra view 
            HttpContext.Session.SetObject("DetailOneWay", detailOneWay);

            /* Đọc giá trị model request OneWay đã thực hiện phía Search ban đầu, 
             * từ đó lấy ra giá trị Departure và Arrival rồi hoán đổi giá trị giữa
             * hai trường này để tiếp tục tra cứu RoundTrip
            */
            var request = HttpContext.Session.GetObject<FlightScheduleCondition>("OneWay");       

            /*
             * Bởi vì đây là tìm kiếm RoundTrip nên giá trị Departure từ OneWay sẽ là giá trị
             * Arrival và Arrival từ OneWay sẽ là giá trị Departure
            */
            var departureId = request.ArrivalId;
            var arrivalId = request.DepartureId;

            request.Date = request.DateRoundTrip;
            request.DepartureId = departureId;
            request.ArrivalId = arrivalId;

            var result = await _flightScheduleApiClient.GetAllFlightByCondition(request);
            HttpContext.Session.Remove(nameof(request.JourneyType));

            return View("Search", result);
        }

        public async Task<IActionResult> InputInformation(int Id)
        {
            var journeyType = 0;
            var checkJourneyType = int.TryParse(HttpContext.Session.GetString("JourneyType"), out journeyType);
            if (checkJourneyType)
            {
                if (journeyType == 1)
                {
                    HttpContext.Session.Remove("JourneyType"); 
                    // Id này là IdFlightSchedule
                    var detailOneWay = await _flightScheduleApiClient.GetById(Id);

                    // Tạo session lưu model của detailOneWay và xuất giá trị đã chọn ra view 
                    HttpContext.Session.SetObject("DetailOneWay", detailOneWay);
                    return View();
                }
                
            }
            HttpContext.Session.Remove("JourneyType");
            var detailRoundTrip = await _flightScheduleApiClient.GetById(Id);
            HttpContext.Session.SetObject("DetailRoundTrip", detailRoundTrip);
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ReservationFlight (string formDataCustomer)
        {
            //var result = JsonNode.Parse(test);
            //var result = JsonConvert.DeserializeObject<List<CustomerCreateRequest>>(formDataCustomer);
            var listCustomerResult = await _reservationApiClient.CreateCustomer(formDataCustomer);
            var detailOneWay = HttpContext.Session.GetObject<FlightScheduleViewModel>("DetailOneWay");
            var detailRoundTrip = HttpContext.Session.GetObject<FlightScheduleViewModel>("DetailRoundTrip");
            var randomIdReservation = new RandomGenerator();
            var codeReservationOneWay = randomIdReservation.Generate();
            var listReservationOneWay = new List<ReservationCreateRequest>();
            foreach (var item in listCustomerResult)
            {
                var reservationOneWay = new ReservationCreateRequest
                {
                    ReservationCode = codeReservationOneWay,
                    IdentityNumber = item,
                    IdFlightSchedule = detailOneWay.Id,
                    Price = detailOneWay.Price,
                    TravelClass = "Economy",
                    Status = 0,
                };
                listReservationOneWay.Add(reservationOneWay);
            }

            var createReservationOneWayResult = await _reservationApiClient.CreateReservationOneWay(listReservationOneWay);
            if (detailRoundTrip != null)
            {
                var codeReservationRoundTrip = randomIdReservation.Generate();
                var listReservationRoundTrip = new List<ReservationCreateRequest>();
                foreach (var item in listCustomerResult)
                {
                    var reservationRoundTrip = new ReservationCreateRequest
                    {
                        ReservationCode = codeReservationRoundTrip,
                        IdentityNumber = item,
                        IdFlightSchedule = detailRoundTrip.Id,
                        Price = detailRoundTrip.Price,
                        TravelClass = "Economy",
                        Status = 0,
                    };
                    listReservationRoundTrip.Add(reservationRoundTrip);
                }
                var createReservationRoundTripResult = await _reservationApiClient.CreateReservationRoundTrip(listReservationRoundTrip);
                if (createReservationOneWayResult && createReservationRoundTripResult)
                {
                    return Content("Thanh cong");
                }
            }

            if (createReservationOneWayResult)
            {
                return Content("Thanh cong");
            }
            else
            {
                return Content("That Bai");
            }           
        }

        public ActionResult Successful()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}