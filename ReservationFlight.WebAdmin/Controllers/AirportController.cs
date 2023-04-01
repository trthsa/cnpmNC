using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.ApiIntegration;
using ReservationFlight.Model.Catalog.Airports;
using ReservationFlight.Model.Systems.Utilities.Enums;
using ReservationFlight.Utility;
using System.Collections;
using System.Data;

namespace ReservationFlight.WebAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AirportController : Controller
    {
        private readonly IAirportApiClient _airportApiClient;
        public AirportController(IAirportApiClient airportApiClient)
        {
            _airportApiClient = airportApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetAirportsPagingRequest
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _airportApiClient.GetAirportsPaging(request);
            ViewBag.Keyword = keyword;
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var statusList = new Hashtable
            {
                { "Đang hoạt động", 1 },
                { "Không hoạt động", 0 }
            };
            ViewBag.StatusList = statusList;
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] AirportCreateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var result = await _airportApiClient.Create(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sân bay thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AirportDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _airportApiClient.Delete(request.IATA);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa sân bay thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(string IATA)
        {
            return View(new AirportDeleteRequest
            {
                IATA = IATA
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string IATA)
        {
            var airport = await _airportApiClient.GetById(IATA);

            var detailVm = new AirportViewModel
            {
                Name = airport.Name,
                IATA = airport.IATA,
                Status = airport.Status
            };

            return View(detailVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string IATA)
        {
            var airport = await _airportApiClient.GetById(IATA);
            var editVm = new AirportUpdateRequest
            {
                IATA = airport.IATA,
                Name = airport.Name,
                Status = airport.Status
            };

            var statusList = new Hashtable
            {
                { "Đang hoạt động", 1 },
                { "Không hoạt động", 0 }
            };
            ViewBag.StatusList = statusList;

            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] AirportUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _airportApiClient.Update(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", Constants.ERR_FAIL_UPDATE.Substring(0, Constants.ERR_FAIL_UPDATE.LastIndexOf("{"))
                + Constants.ERR_FAIL_UPDATE.Substring(Constants.ERR_FAIL_UPDATE.LastIndexOf("}") + 2));
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(string IATA)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _airportApiClient.UpdatePatch(IATA);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật tình trạng sân bay thất bại");
            return View();
        }
    }
}
