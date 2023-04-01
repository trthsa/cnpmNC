using Newtonsoft.Json;
using ReservationFlight.Model.Catalog.Aviations;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ReservationFlight.Utility;
using ReservationFlight.Model.Common;
using Microsoft.AspNetCore.JsonPatch;

namespace ReservationFlight.ApiIntegration
{
    public class AviationApiClient : IAviationApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AviationApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
             IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> Create(AviationCreateRequest request)
        {
            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ImageAviation != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageAviation.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageAviation.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "imageAviation", request.ImageAviation.FileName);
            }
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AviationCode) ? " " : request.AviationCode.ToString()), "aviationCode");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? " " : request.Name.ToString()), "name");

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_AVIATION,
                    nameof(Create)), requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(string aviationCode)
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
                    Constants.API_AVIATION,
                    nameof(Delete)) + aviationCode);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<List<AviationViewModel>> GetAll()
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
                    Constants.API_AVIATION,
                    nameof(GetAll)));

            var body = await response.Content.ReadAsStringAsync();

            var aviations = JsonConvert.DeserializeObject<List<AviationViewModel>>(body);

            return aviations!;
        }

        public async Task<ApiResult<PagedResult<AviationViewModel>>> GetAviationsPaging(GetAviationsPagingRequest request)
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
                    Constants.API_AVIATION,
                    nameof(GetAviationsPaging)));
            URL.Append('?');
            URL.Append("pageIndex=");
            URL.Append(request.PageIndex);
            URL.Append('&');
            URL.Append("pageSize=");
            URL.Append(request.PageSize);
            URL.Append('&');
            URL.Append("keyword=");
            URL.Append(request.Keyword);

            var response = await client.GetAsync(URL.ToString());
              
            var body = await response.Content.ReadAsStringAsync();

            var aviations = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<AviationViewModel>>>(body);

            return aviations!;
        }

        public async Task<AviationViewModel> GetById(string aviationCode)
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
                    Constants.API_AVIATION,
                    nameof(GetById)) + aviationCode);

            var body = await response.Content.ReadAsStringAsync();

            var data = JsonConvert.DeserializeObject<AviationViewModel>(body);

            return data!;
        }

        public async Task<bool> Update(AviationUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestContent = new MultipartFormDataContent();

            if (request.ImageAviation != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ImageAviation.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ImageAviation.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "imageAviation", request.ImageAviation.FileName);
            }
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.AviationCode) ? "" : request.AviationCode.ToString()), "aviationCode");
            requestContent.Add(new StringContent(string.IsNullOrEmpty(request.Name) ? "" : request.Name.ToString()), "name");

            var response = await client.PutAsync(
                string.Format(
                    Constants.API_AVIATION,
                    nameof(Update)) + request.AviationCode,
                requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePatch(AviationUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();

            var sessions = _httpContextAccessor
                .HttpContext!
                .Session
                .GetString(Constants.TOKEN);

            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var requestPath = new JsonPatchDocument<AviationUpdateRequest>();
            requestPath.Replace(e => e.Name, request.Name);
            var jsonParse = JsonConvert.SerializeObject(requestPath);
            var httpContent = new StringContent(jsonParse, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PatchAsync(
                string.Format(
                    Constants.API_AVIATION,
                    nameof(UpdatePatch)) + request.AviationCode, httpContent);
            return response.IsSuccessStatusCode;
        }
    }
}
