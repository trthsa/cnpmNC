using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.ApiIntegration;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Systems.Utilities;
using ReservationFlight.Utility;
using System.Collections;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace ReservationFlight.WebAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class FlightRouteController : Controller
    {
        private readonly IFlightRouteApiClient _flightRouteApiClient;
        private readonly IAirportApiClient _airportApiClient;
        public FlightRouteController(
            IFlightRouteApiClient flightRouteApiClient, 
            IAirportApiClient airportApiClient)
        {
            _flightRouteApiClient = flightRouteApiClient;
            _airportApiClient = airportApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetFlightRoutesPagingRequest
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _flightRouteApiClient.GetFlightRoutesPaging(request);
            ViewBag.Keyword = keyword;
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var airportList = await _airportApiClient.GetAll();
            ViewBag.AirportList = airportList;
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] FlightRouteCreateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var result = await _flightRouteApiClient.Create(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm tuyến bay thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FLightRouteDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _flightRouteApiClient.Delete(request.Id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa tuyến bay thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            return View(new FLightRouteDeleteRequest
            {
                Id = Id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var flightRoute = await _flightRouteApiClient.GetById(Id);

            var detailVm = new FlightRouteViewModel
            {
                Id = flightRoute.Id,
                Departure = flightRoute.Departure,
                Arrival = flightRoute.Arrival,
                Status = flightRoute.Status
            };

            return View(detailVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var flightRoute = await _flightRouteApiClient.GetById(Id);
            var editVm = new FlightRouteUpdateRequest
            {
                DepartureId = flightRoute.Departure,
                ArrivalId = flightRoute.Arrival,
                Status = flightRoute.Status
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
        public async Task<IActionResult> Edit([FromForm] FlightRouteUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _flightRouteApiClient.Update(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", Constants.ERR_FAIL_UPDATE.Substring(0, Constants.ERR_FAIL_UPDATE.LastIndexOf("{"))
                + Constants.ERR_FAIL_UPDATE.Substring(Constants.ERR_FAIL_UPDATE.LastIndexOf("}") + 2));
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _flightRouteApiClient.UpdatePatch(Id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật tình trạng tuyến bay thất bại");
            return View();
        }

        [HttpPost]
        public JsonResult LoadStatus()
        {
            var xmlDoc = XDocument.Load(@"..\ReservationFlight.Model\Xml\Status.xml");
            var xmlElement = xmlDoc.Element("Root").Elements("Item");
            var list = new List<StatusModel>();
            foreach (var item in xmlElement)
            {
                var status = new StatusModel
                {
                    Text = item.Attribute("text").Value,
                    Value = int.Parse(item.Attribute("value").Value)
                };
                list.Add(status);
            }
            return Json(new
            {
                data = list,
                status = true
            });
        }
    }
}
