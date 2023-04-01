using Microsoft.AspNetCore.Mvc;
using ReservationFlight.Domain.Catalog.Reservations;
using ReservationFlight.Model.Catalog.Customers;
using ReservationFlight.Model.Catalog.Reservations;

namespace ReservationFlight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReservationsController(
            IReservationService reservationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _reservationService = reservationService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] List<CustomerCreateRequest> request)
        {
            //kiểm tra validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var listCustomer = await _reservationService.CreateInformationCustomer(request);
            if (listCustomer == null)
            {
                return BadRequest();
            }

            return Ok(listCustomer);
        }

        [HttpPost("CreateReservationOneWay")]
        public async Task<IActionResult> CreateReservationOneWay([FromBody] List<ReservationCreateRequest> request)
        {
            //kiểm tra validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var codeReservationOneWay = await _reservationService.CreateReservationOneWay(request);
            if (codeReservationOneWay == null)
            {
                return BadRequest();
            }

            var reservation = await _reservationService.GetReservationByCode(codeReservationOneWay);

            return CreatedAtAction(
                nameof(GetReservationByCode),
                new { reservationCode = codeReservationOneWay },
                reservation);
        }

        [HttpPost("CreateReservationRoundTrip")]
        public async Task<IActionResult> CreateReservationRoundTrip([FromBody] List<ReservationCreateRequest> request)
        {
            //kiểm tra validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var codeReservationRoundTrip = await _reservationService.CreateReservationRoundTrip(request);
            if (codeReservationRoundTrip == null)
            {
                return BadRequest();
            }

            var reservation = await _reservationService.GetReservationByCode(codeReservationRoundTrip);

            return CreatedAtAction(
                nameof(GetReservationByCode),
                new { reservationCode = codeReservationRoundTrip },
                reservation);
        }

        [HttpGet("GetReservationByCode/{reservationCode}")]
        public async Task<IActionResult> GetReservationByCode(string reservationCode)
        {
            var reservation = await _reservationService.GetReservationByCode(reservationCode);
            if (reservation == null)
                return BadRequest();
            return Ok(reservation);
        }

        [HttpGet("GetCustomerById/{customerId}")]
        public async Task<IActionResult> GetCustomerById(string customerId)
        {
            var customer = await _reservationService.GetCustomerById(customerId);
            if (customer == null)
                return BadRequest();
            return Ok(customer);
        }

        [HttpGet("GetReservationsPaging")]
        public async Task<IActionResult> GetAllReservation([FromQuery] GetReservationPagingRequest request)
        {
            var reservations = await _reservationService.GetReservationsPaging(request);
            return Ok(reservations);
        }
    }
}
