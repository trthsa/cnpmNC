using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationFlight.ApiIntegration;
using ReservationFlight.Model.Catalog.Aviations;
using ReservationFlight.Utility;
using System.Data;

namespace ReservationFlight.WebAdmin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AviationController : Controller
    {
        private readonly IAviationApiClient _aviationApiClient;
        public AviationController(IAviationApiClient aviationApiClient)
        {
            _aviationApiClient = aviationApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var request = new GetAviationsPagingRequest
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _aviationApiClient.GetAviationsPaging(request);
            ViewBag.Keyword = keyword;
            return View(data.ResultObj);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] AviationCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _aviationApiClient.Create(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm hãng hàng không thất bại");
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AviationDeleteRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _aviationApiClient.Delete(request.AviationCode);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Xóa hãng hàng không thất bại");
            return View(request);
        }

        [HttpGet]
        public IActionResult Delete(string aviationCode)
        {
            return View(new AviationDeleteRequest
            {
                AviationCode = aviationCode
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(string aviationCode)
        {
            var aviation = await _aviationApiClient.GetById(aviationCode);

            var detailVm = new AviationViewModel
            {
                AviationCode = aviation.AviationCode,
                Name = aviation.Name,
                ImageAviation = aviation.ImageAviation
            };

            return View(detailVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string aviationCode)
        {
            var aviation = await _aviationApiClient.GetById(aviationCode);
            var editVm = new AviationUpdateRequest
            {
                AviationCode = aviation.AviationCode,
                Name = aviation.Name
            };

            return View(editVm);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Edit([FromForm] AviationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _aviationApiClient.Update(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", Constants.ERR_FAIL_UPDATE.Substring(0, Constants.ERR_FAIL_UPDATE.LastIndexOf("{"))
                + Constants.ERR_FAIL_UPDATE.Substring(Constants.ERR_FAIL_UPDATE.LastIndexOf("}") + 2));
            return View(request);
        }

        [HttpGet]
        public async Task<IActionResult> EditName(string aviationCode)
        {
            var aviation = await _aviationApiClient.GetById(aviationCode);
            var editVm = new AviationUpdateRequest
            {
                AviationCode = aviation.AviationCode,
                Name = aviation.Name
            };

            return View(editVm);
        }

        [HttpPost]
        public async Task<IActionResult> EditName(AviationUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

           var result = await _aviationApiClient.UpdatePatch(request);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Cập nhật tên hãng hàng không thất bại");
            return View(request);
        }
    }
}
