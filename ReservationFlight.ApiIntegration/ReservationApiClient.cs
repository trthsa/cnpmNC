using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Catalog.Reservations;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;
using System.Net.Http.Headers;
using System.Text;

namespace ReservationFlight.ApiIntegration
{
    public class ReservationApiClient : IReservationApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReservationApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
             IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<string>> CreateCustomer(string request)
        {
            //var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(request, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_RESERVATION,
                    nameof(CreateCustomer)), httpContent);

            var body = await response.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<string>>(body);
            return customers;
        }

        public async Task<bool> CreateReservationOneWay(List<ReservationCreateRequest> request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_RESERVATION,
                    nameof(CreateReservationOneWay)), httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateReservationRoundTrip(List<ReservationCreateRequest> request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_RESERVATION,
                    nameof(CreateReservationRoundTrip)), httpContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<ReservationViewModel> GetReservationByCode(string reservationCode)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

            var response = await client.GetAsync(
                string.Format(
                    Constants.API_RESERVATION,
                    nameof(GetReservationByCode)) + reservationCode);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<ReservationViewModel>(body);
            return data;
        }

        public async Task<ApiResult<PagedResult<ReservationViewModel>>> GetReservationsPaging(GetReservationPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

            //var response = await client.GetAsync($"/api/users/paging?pageIndex=" +
            //    $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");

            var URL = new StringBuilder(string.Format(
                    Constants.API_RESERVATION,
                    nameof(GetReservationsPaging)));
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

            var reservations = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<ReservationViewModel>>>(body);

            return reservations;
        }
    }
}
