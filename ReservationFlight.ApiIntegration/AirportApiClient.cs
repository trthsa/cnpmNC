using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReservationFlight.Model.Catalog.Airports;
using ReservationFlight.Model.Catalog.Aviations;
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
    public class AirportApiClient : IAirportApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AirportApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
             IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Create(AirportCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.IATA) ? "" : request.IATA.ToString()), "iata");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PostAsync(
                String.Format(
                    Constants.API_AIRPORT,
                    nameof(Create)), requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string IATA)
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
                    Constants.API_AIRPORT,
                    nameof(Delete)) + IATA);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<ApiResult<PagedResult<AirportViewModel>>> GetAirportsPaging(GetAirportsPagingRequest request)
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
                    Constants.API_AIRPORT,
                    nameof(GetAirportsPaging)));

            var response = await client.GetAsync(
               URL.Append("?")
               .Append("pageIndex=")
               .Append(request.PageIndex)
               .Append("&")
               .Append("pageSize=")
               .Append(request.PageSize)
               .Append("&")
               .Append("keyword=")
               .Append(request.Keyword)
               .ToString());

            var body = await response.Content.ReadAsStringAsync();

            var airports = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<AirportViewModel>>>(body);

            return airports!;
        }

        public async Task<List<AirportViewModel>> GetAll()
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
                    Constants.API_AIRPORT,
                    nameof(GetAll)));

            var body = await response.Content.ReadAsStringAsync();

            var airports = JsonConvert.DeserializeObject<List<AirportViewModel>>(body);

            return airports!;
        }

        public async Task<AirportViewModel> GetById(string IATA)
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
                    Constants.API_AIRPORT,
                    nameof(GetById)) + IATA);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<AirportViewModel>(body);

            return data!;
        }

        public async Task<bool> Update(AirportUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.IATA) ? "" : request.IATA.ToString()), "iata");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Status.ToString()), "status");

            var response = await client.PutAsync(
                string.Format(
                    Constants.API_AIRPORT,
                    nameof(Update)) + request.IATA, 
                requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePatch(string IATA)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var jsonParse = JsonConvert.SerializeObject(IATA);

            var httpContent = new StringContent(jsonParse, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PatchAsync(
                string.Format(
                    Constants.API_AIRPORT,
                    nameof(UpdatePatch)) + IATA, httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
