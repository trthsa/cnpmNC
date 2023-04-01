using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.Domain.Catalog.Airports;
using ReservationFlight.Model.Catalog.Airports;
using ReservationFlight.Utility;

namespace ReservationFlight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportsController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAirports()
        {
            var allAirports = await _airportService.GetAll();
            return Ok(allAirports);
        }

        [HttpGet("GetAirportsPaging")]
        public async Task<IActionResult> GetAirportsPaging([FromQuery] GetAirportsPagingRequest request)
        {
            var airports = await _airportService.GetAirportsPaging(request);
            return Ok(airports);
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] AirportCreateRequest request)
        {
            //kiểm tra validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var IATA = await _airportService.Create(request);
            if (string.IsNullOrEmpty(IATA))
            {
                return BadRequest();
            }

            var airport = await _airportService.GetById(IATA);

            return CreatedAtAction(nameof(GetById), new { IATA = IATA }, airport);
        }

        [HttpGet("GetById/{IATA}")]
        public async Task<IActionResult> GetById(string IATA)
        {
            var airport = await _airportService.GetById(IATA);
            if (airport == null)
                return BadRequest();
            return Ok(airport);
        }

        [HttpPut("Update/{IATA}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update(
            [FromRoute] string IATA,
            [FromForm] AirportUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.IATA = IATA;
            var affectedResult = await _airportService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("Delete/{IATA}")]
        [Authorize]
        public async Task<IActionResult> Delete(string IATA)
        {
            var affectedResult = await _airportService.Delete(IATA);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch("UpdatePatch/{IATA}")]
        [Authorize]
        public async Task<IActionResult> UpdateAviationPatch(
            [FromRoute] string IATA)
        {
            var affectedResult = await _airportService.UpdatePatch(IATA);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
