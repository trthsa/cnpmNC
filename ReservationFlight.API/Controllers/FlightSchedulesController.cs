using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.Domain.Catalog.FlightSchedules;
using ReservationFlight.Model.Catalog.Flights;

namespace ReservationFlight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightSchedulesController : ControllerBase
    {
        private readonly IFlightScheduleService _flightScheduleService;

        public FlightSchedulesController(IFlightScheduleService flightScheduleService)
        {
            _flightScheduleService = flightScheduleService;
        }

        [HttpGet("GetAllFlightSchedule")]
        public async Task<IActionResult> GetAllFlightSchedules()
        {
            var allFlightSchedules = await _flightScheduleService.GetAll();
            return Ok(allFlightSchedules);
        }

		[HttpPost("GetAllFlightByCondition")]
		[Consumes("multipart/form-data")]
		public async Task<IActionResult> GetAllFlightByCondition([FromForm] FlightScheduleCondition request)
		{
			var allFlightSchedules = await _flightScheduleService.GetAllFlightByCondition(request);
			return Ok(allFlightSchedules);
		}

		[HttpGet("GetFlightSchedulesPaging")]
        public async Task<IActionResult> GetFlightSchedulesPaging([FromQuery] GetFlightSchedulesPagingRequest request)
        {
            var flightSchedules = await _flightScheduleService.GetFlightSchedulesPaging(request);
            return Ok(flightSchedules);
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] FlightScheduleCreateRequest request)
        {
            //kiểm tra validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var flightScheduleId = await _flightScheduleService.Create(request);
            if (flightScheduleId == 0)
            {
                return BadRequest();
            }

            var flightSchedule = await _flightScheduleService.GetById(flightScheduleId);

            return CreatedAtAction(nameof(GetById), new { Id = flightScheduleId }, flightSchedule);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var flightSchedule = await _flightScheduleService.GetById(Id);
            if (flightSchedule == null)
                return BadRequest();
            return Ok(flightSchedule);
        }


        [HttpDelete("Delete/{Id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int Id)
        {
            var affectedResult = await _flightScheduleService.Delete(Id);
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
            var affectedResult = await _flightScheduleService.UpdatePatch(Id);
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
            [FromForm] FlightScheduleUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.Id = Id;
            var affectedResult = await _flightScheduleService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
