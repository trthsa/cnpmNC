using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.ApiIntegration
{
    public class FlightScheduleApiClient : IFlightScheduleApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlightScheduleApiClient(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Create(FlightScheduleCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FlightRouteId.ToString()) ? "" : request.FlightRouteId.ToString()), "flightRouteId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AviationId) ? "" : request.AviationId), "aviationId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FlightNumber) ? "" : request.FlightNumber), "flightNumber");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Price.ToString()) ? "" : request.Price.ToString()), "price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Date.ToString()) ? "" : request.Date.ToString()), "date");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ScheduledTimeDeparture.ToString()) ? "" : request.ScheduledTimeDeparture.ToString()), "scheduledTimeDeparture");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ScheduledTimeArrival.ToString()) ? "" : request.ScheduledTimeArrival.ToString()), "scheduledTimeArrival");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeatEconomy.ToString()) ? "" : request.SeatEconomy.ToString()), "seatEconomy");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeatBusiness.ToString()) ? "" : request.SeatBusiness.ToString()), "seatBusiness");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PostAsync(
                String.Format(
                    Constants.API_FLIGHTSCHEDULE,
                    nameof(Create)), requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int Id)
        {
            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var response = await client.DeleteAsync(
                string.Format(
                    Constants.API_FLIGHTSCHEDULE,
                    nameof(Delete)) + Id);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public Task<List<FlightScheduleViewModel>> GetAll()
        {
			throw new NotImplementedException();
		}

		public async Task<List<FlightViewModel>> GetAllFlightByCondition(FlightScheduleCondition request)
		{			
			var client = _httpClientFactory.CreateClient();
			client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

			var requestContent = new MultipartFormDataContent();
			requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DepartureId.ToString()) ? "" : request.DepartureId), "departureId");
			requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ArrivalId) ? "" : request.ArrivalId), "arrivalId");
			requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Date.ToString()) ? "" : request.Date.ToString()), "date");

			var response = await client.PostAsync(
				string.Format(
					Constants.API_FLIGHTSCHEDULE,
					nameof(GetAllFlightByCondition)), requestContent);

			var body = await response.Content.ReadAsStringAsync();

			var data = JsonConvert.DeserializeObject<List<FlightViewModel>>(body);
			return data!;
		}

		public async Task<FlightScheduleViewModel> GetById(int Id)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var response = await client.GetAsync(
                string.Format(
                    Constants.API_FLIGHTSCHEDULE,
                    nameof(GetById)) + Id);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<FlightScheduleViewModel>(body);

            return data!;
        }

        public async Task<ApiResult<PagedResult<FlightScheduleViewModel>>> GetFlightSchedulesPaging(GetFlightSchedulesPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            //var response = await client.GetAsync($"/api/users/paging?pageIndex=" +
            //    $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");

            StringBuilder URL = new StringBuilder(string.Format(
                    Constants.API_FLIGHTSCHEDULE,
                    nameof(GetFlightSchedulesPaging)));
            URL.Append('?')
               .Append("pageIndex=")
               .Append(request.PageIndex)
               .Append('&')
               .Append("pageSize=")
               .Append(request.PageSize)
               .Append('&')
               .Append("keyword=")
               .Append(request.Keyword);

            var response = await client.GetAsync(URL.ToString());

            var body = await response.Content.ReadAsStringAsync();

            var flightSchedules = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<FlightScheduleViewModel>>>(body);

            return flightSchedules!;
        }

        public async Task<bool> Update(FlightScheduleUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FlightRouteId.ToString()) ? "" : request.FlightRouteId.ToString()), "flightRouteId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AviationId) ? "" : request.AviationId), "aviationId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.FlightNumber) ? "" : request.FlightNumber), "flightNumber");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Price.ToString()) ? "" : request.Price.ToString()), "price");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Date.ToString()) ? "" : request.Date.ToString()), "date");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ScheduledTimeDeparture.ToString()) ? "" : request.ScheduledTimeDeparture.ToString()), "scheduledTimeDeparture");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ScheduledTimeArrival.ToString()) ? "" : request.ScheduledTimeArrival.ToString()), "scheduledTimeArrival");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeatEconomy.ToString()) ? "" : request.SeatEconomy.ToString()), "seatEconomy");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.SeatBusiness.ToString()) ? "" : request.SeatBusiness.ToString()), "seatBusiness");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PutAsync(
                string.Format(
                    Constants.API_FLIGHTSCHEDULE,
                    nameof(Update)) + request.Id,
                requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePatch(int Id)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var jsonParse = JsonConvert.SerializeObject(Id);

            var httpContent = new StringContent(jsonParse, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PatchAsync(
                string.Format(
                    Constants.API_FLIGHTSCHEDULE,
                    nameof(UpdatePatch)) + Id, httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
