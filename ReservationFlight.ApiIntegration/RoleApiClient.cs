using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using ReservationFlight.Model.Common;
using ReservationFlight.Model.Systems.Roles;
using ReservationFlight.Utility;

namespace ReservationFlight.ApiIntegration
{
    public class RoleApiClient : IRoleApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        // Dùng để truy cập lấy token
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var response = await client.GetAsync($"/api/roles");

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                List<RoleViewModel> myDeserializeObjList = (List<RoleViewModel>)JsonConvert.DeserializeObject(
                    body, typeof(List<RoleViewModel>))!;
                return new ApiSuccessResult<List<RoleViewModel>>(myDeserializeObjList);
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<List<RoleViewModel>>>(body)!;
        }
    }
}
