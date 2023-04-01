using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ReservationFlight.Model.Common;
using ReservationFlight.Model.Systems.Users;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.ApiIntegration
{
    public class UserApiClient : IUserApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserApiClient(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
             IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            /* Tạo một client có base address là backend api và truyền vào hàm authenticate 
             của backend api một httpcontent vừa tạo ở trên sau đó sẽ trả về response một
            */
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_USER,
                    nameof(Authenticate)), 
                httpContent);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(await response.Content.ReadAsStringAsync())!;
            }

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(await response.Content.ReadAsStringAsync())!;
        }

        public async Task<ApiResult<bool>> Delete(Guid id)
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
                    Constants.API_USER_DELETE,
                    id));

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(body)!;

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(body)!;
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
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
                    Constants.API_USER_GET_BY_ID,
                    id));

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body)!;

            return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body)!;
        }

        public async Task<ApiResult<UserViewModel>> GetByUserName(string userName)
        {
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var response = await client.GetAsync(string.Format(
                    Constants.API_USER_GET_BY_USER_NAME,
                    userName));

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<UserViewModel>>(body)!;

            return JsonConvert.DeserializeObject<ApiErrorResult<UserViewModel>>(body)!;
        }

        public async Task<List<UserViewModel>> GetAll()
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
                    Constants.API_USER,
                    nameof(GetAll)));

            var body = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<List<UserViewModel>>(body);

            return users!;
        }


        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request)
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
                    Constants.API_USER,
                    nameof(GetUsersPaging)));
            var response = await client.GetAsync(
               URL.Append("?")
               .Append("pageIndex=")
               .Append(request.PageIndex)
               .Append("&")
               .Append("pageSize")
               .Append(request.PageSize)
               .Append("&")
               .Append("keyword")
               .Append(request.Keyword)
               .ToString());

            var body = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<UserViewModel>>>(body);

            return users!;
        }

        public async Task<ApiResult<string>> RegisterUser(RegisterRequest registerRequest)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);

            var json = JsonConvert.SerializeObject(registerRequest);

            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_USER,
                    nameof(RegisterUser)),
                httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(result)!;

            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(result)!;
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            // BaseAddress lấy trong appsettings.Development.json bằng Configuratrion
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PutAsync(
                string.Format(
                    Constants.API_USER_ROLE_ASSIGN,
                    id), 
                httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result)!;

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result)!;
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            // BaseAddress lấy trong appsettings.Development.json bằng Configuratrion
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var json = JsonConvert.SerializeObject(request);

            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PutAsync(
                string.Format(
                    Constants.API_USER_UPDATE,
                    id), 
                httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize thành 1 object cùng type với return type BackendApi trả về ở đây là ApiSuccessResult
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result)!;

            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result)!;
        }

        public async Task<ApiResult<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            // BaseAddress lấy trong appsettings.Development.json bằng Configuratrion
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_USER,
                    nameof(ChangePassword)), 
                httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize thành 1 object cùng type với return type BackendApi trả về ở đây là ApiSuccessResult
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result)!;

            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result)!;
        }

        public async Task<ApiResult<bool>> ConfirmEmail(ConfirmEmailViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            // BaseAddress lấy trong appsettings.Development.json bằng Configuratrion
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_USER,
                    nameof(ConfirmEmail)), 
                httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize thành 1 object cùng type với return type BackendApi trả về ở đây là ApiSuccessResult
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result)!;

            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result)!;
        }

        public async Task<ApiResult<string>> ForgotPassword(ForgotPasswordViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            // BaseAddress lấy trong appsettings.Development.json bằng Configuratrion
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_USER,
                    nameof(ForgotPassword)), 
                httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize thành 1 object cùng type với return type BackendApi trả về ở đây là ApiSuccessResult
                return JsonConvert.DeserializeObject<ApiSuccessResult<string>>(result)!;

            }
            return JsonConvert.DeserializeObject<ApiErrorResult<string>>(result)!;
        }

        public async Task<ApiResult<bool>> ResetPassword(ResetPasswordViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            // BaseAddress lấy trong appsettings.Development.json bằng Configuratrion
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);

            var json = JsonConvert.SerializeObject(model);

            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);

            var response = await client.PostAsync(
                string.Format(
                    Constants.API_USER,
                    nameof(ResetPassword)), 
                httpContent);

            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Deserialize thành 1 object cùng type với return type BackendApi trả về ở đây là ApiSuccessResult
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result)!;

            }
            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result)!;
        }

        public async Task<bool> DisableAccount(Guid id)
        {
            var sessions = _httpContextAccessor
                             .HttpContext!
                             .Session
                             .GetString(Constants.TOKEN);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[Constants.BASE_ADDRESS]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Constants.BEARER, sessions);
            var json = JsonConvert.SerializeObject(id);
            var httpContent = new StringContent(json, Encoding.UTF8, Constants.DEFAULT_MEDIA_TYPE);
            var response = await client.PatchAsync(
                string.Format(
                    Constants.API_USER_DISABLE_ACCOUNT,
                    id), 
                httpContent);

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }
    }
}
