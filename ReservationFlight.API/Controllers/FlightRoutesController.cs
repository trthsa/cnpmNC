using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.Domain.Catalog.FlightRoutes;
using ReservationFlight.Model.Catalog.Flights;

namespace ReservationFlight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightRoutesController : ControllerBase
    {
        private readonly IFlightRouteService _flightRouteService;

        public FlightRoutesController(IFlightRouteService flightRouteService)
        {
            _flightRouteService = flightRouteService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllFlightRoutes()
        {
            var allFlightRoutes = await _flightRouteService.GetAll();
            return Ok(allFlightRoutes);
        }

        [HttpGet("GetFlightRoutesPaging")]
        public async Task<IActionResult> GetFlightRoutesPaging([FromQuery] GetFlightRoutesPagingRequest request)
        {
            var flightRoutes = await _flightRouteService.GetFlightRoutesPaging(request);
            return Ok(flightRoutes);
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] FlightRouteCreateRequest request)
        {
            //kiểm tra validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flightRouteId = await _flightRouteService.Create(request);
            if (flightRouteId == 0)
            {
                return BadRequest();
            }

            var flightRoute = await _flightRouteService.GetById(flightRouteId);

            return CreatedAtAction(nameof(GetById), new { Id = flightRouteId }, flightRoute);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var flightRoute = await _flightRouteService.GetById(Id);
            if (flightRoute == null)
                return BadRequest();
            return Ok(flightRoute);
        }


        [HttpDelete("Delete/{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int Id)
        {
            var affectedResult = await _flightRouteService.Delete(Id);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch("UpdatePatch/{Id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePatch(
            [FromRoute] int Id)
        {
            var affectedResult = await _flightRouteService.UpdatePatch(Id);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("Update/{Id}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update(
            [FromRoute] int Id,
            [FromForm] FlightRouteUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = Id;
            var affectedResult = await _flightRouteService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
