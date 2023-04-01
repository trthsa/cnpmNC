using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.Domain.Catalog.Aviations;
using ReservationFlight.Model.Catalog.Aviations;
using ReservationFlight.Utility;

namespace ReservationFlight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AviationsController : ControllerBase
    {
        private readonly IAviationService _aviationService;

        public AviationsController(IAviationService aviationService)
        {
            _aviationService = aviationService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAviations()
        {
            var allAviations = await _aviationService.GetAll();
            return Ok(allAviations);
        }

        [HttpGet("GetAviationsPaging")]
        public async Task<IActionResult> GetAviationsPaging([FromQuery] GetAviationsPagingRequest request)
        {
            var aviations = await _aviationService.GetAviationsPaging(request);
            return Ok(aviations);
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] AviationCreateRequest request)
        {
            //kiểm tra validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var aviationCode = await _aviationService.Create(request);
            if (string.IsNullOrEmpty(aviationCode))
            {
                return BadRequest();
            }

            var aviation= await _aviationService.GetById(aviationCode);

            return CreatedAtAction(nameof(GetById), new { aviationCode = aviationCode }, aviation);
        }

        [HttpGet("GetById/{aviationCode}")]
        public async Task<IActionResult> GetById(string aviationCode)
        {
            var aviation = await _aviationService.GetById(aviationCode);
            if (aviation == null)
                return BadRequest(string.Format(
                    Constants.DEFAUT_IMAGE_FILE));
            return Ok(aviation);
        }

        [HttpPut("Update/{aviationCode}")]
        [Consumes("multipart/form-data")]
        [Authorize]
        public async Task<IActionResult> Update(
            [FromRoute] string aviationCode, 
            [FromForm] AviationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            request.AviationCode = aviationCode;
            var affectedResult = await _aviationService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("Delete/{aviationCode}")]
        [Authorize]
        public async Task<IActionResult> Delete(string aviationCode)
        {
            var affectedResult = await _aviationService.Delete(aviationCode);
            if (affectedResult == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch("UpdatePatch/{aviationCode}")]
        [Authorize]
        public async Task<IActionResult> UpdateAviationPatch(
            [FromRoute] string aviationCode, 
            [FromBody] JsonPatchDocument aviationModel)
        {
            var affectedResult = await _aviationService.UpdatePatch(aviationCode, aviationModel);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
