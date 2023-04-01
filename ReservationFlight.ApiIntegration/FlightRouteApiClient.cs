using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;
using System.Net.Http.Headers;
using System.Text;

namespace ReservationFlight.ApiIntegration
{
    public class FlightRouteApiClient : IFlightRouteApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FlightRouteApiClient(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Create(FlightRouteCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DepartureId) ? "" : request.DepartureId.ToString()), "departureId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ArrivalId) ? "" : request.ArrivalId.ToString()), "arrivalId");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PostAsync(
                String.Format(
                    Constants.API_FLIGHTROUTE,
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
                    Constants.API_FLIGHTROUTE,
                    nameof(Delete)) + Id);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<FlightRouteViewModel>> GetAll()
        {
            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var response = await client.GetAsync(
                string.Format(
                    Constants.API_FLIGHTROUTE,
                    nameof(GetAll)));

            var body = await response.Content.ReadAsStringAsync();

            var flightRoutes = JsonConvert.DeserializeObject<List<FlightRouteViewModel>>(body);

            return flightRoutes!;
        }

        public async Task<FlightRouteViewModel> GetById(int Id)
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
                    Constants.API_FLIGHTROUTE,
                    nameof(GetById)) + Id);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<FlightRouteViewModel>(body);

            return data!;
        }

        public async Task<ApiResult<PagedResult<FlightRouteViewModel>>> GetFlightRoutesPaging(GetFlightRoutesPagingRequest request)
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
                    Constants.API_FLIGHTROUTE,
                    nameof(GetFlightRoutesPaging)));
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

            var flightRoutes = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<FlightRouteViewModel>>>(body);

            return flightRoutes!;
        }

        public async Task<bool> Update(FlightRouteUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.DepartureId) ? "" : request.DepartureId.ToString()), "departureId");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.ArrivalId) ? "" : request.ArrivalId.ToString()), "arrivalId");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PutAsync(
                string.Format(
                    Constants.API_FLIGHTROUTE,
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
                    Constants.API_FLIGHTROUTE,
                    nameof(UpdatePatch)) + Id, httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
