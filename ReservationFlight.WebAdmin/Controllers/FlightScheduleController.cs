using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.ApiIntegration;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Systems.Utilities;
using ReservationFlight.Utility;
using System.Collections;
using System.Data;
using System.Xml.Linq;

namespace ReservationFlight.WebAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class FlightScheduleController : Controller
    {
        private readonly IFlightScheduleApiClient _flightScheduleApiClient;
        private readonly IFlightRouteApiClient _flightRouteApiClient;
        private readonly IAviationApiClient _aviationApiClient;

        public FlightScheduleController(
            IFlightScheduleApiClient flightScheduleApiClient,
            IAviationApiClient aviationApiClient,
            IFlightRouteApiClient flightRouteApiClient)
        {
            _flightScheduleApiClient = flightScheduleApiClient;
            _aviationApiClient = aviationApiClient;
            _flightRouteApiClient = flightRouteApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetFlightSchedulesPagingRequest
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _flightScheduleApiClient.GetFlightSchedulesPaging(request);
            ViewBag.Keyword = keyword;
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var aviationList = await _aviationApiClient.GetAll();
            var flightRouteList = await _flightRouteApiClient.GetAll();
            ViewBag.AviationList = aviationList;
            ViewBag.FlightRouteList = flightRouteList;
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] FlightScheduleCreateRequest request)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest();
            }

            var result = await _flightScheduleApiClient.Create(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm chuyến bay thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FlightScheduleDeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _flightScheduleApiClient.Delete(request.Id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa chuyến bay thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            return View(new FlightScheduleDeleteRequest
            {
                Id = Id
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int Id)
        {
            var flightSchedule = await _flightScheduleApiClient.GetById(Id);
            var flightRoute = await _flightRouteApiClient.GetById(int.Parse(flightSchedule.FlightRouteId));

            var detailVm = new FlightScheduleViewModel
            {
                Id = flightSchedule.Id,
                FlightRouteId = string.Concat(flightRoute.Departure, " - ", flightRoute.Arrival),
                FlightNumber = flightSchedule.FlightNumber,
                AviationId = flightSchedule.AviationId,
                Price = flightSchedule.Price,
                Date = flightSchedule.Date,
                ScheduledTimeDeparture = flightSchedule.ScheduledTimeDeparture,
                ScheduledTimeArrival = flightSchedule.ScheduledTimeArrival,
                SeatEconomy = flightSchedule.SeatEconomy,
                SeatBusiness = flightSchedule.SeatBusiness,
                Status = flightSchedule.Status
            };
            return View(detailVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var flightSchedule = await _flightScheduleApiClient.GetById(Id);
            var editVm = new FlightScheduleUpdateRequest
            {
                Id = flightSchedule.Id,
                FlightRouteId = int.Parse(flightSchedule.FlightRouteId),
                FlightNumber = flightSchedule.FlightNumber,
                AviationId = flightSchedule.AviationId,
                Price = flightSchedule.Price,
                Date = flightSchedule.Date,
                ScheduledTimeDeparture = flightSchedule.ScheduledTimeDeparture,
                ScheduledTimeArrival = flightSchedule.ScheduledTimeArrival,
                SeatEconomy = flightSchedule.SeatEconomy,
                SeatBusiness = flightSchedule.SeatBusiness,
                Status = flightSchedule.Status
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
        public async Task<IActionResult> Edit([FromForm] FlightScheduleUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _flightScheduleApiClient.Update(request);
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

            var result = await _flightScheduleApiClient.UpdatePatch(Id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật tình trạng chuyến bay thất bại");
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
