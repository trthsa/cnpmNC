using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using ReservationFlight.Model.Systems.Users;
using ReservationFlight.Utility;
using ReservationFlight.ApiIntegration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservationFlight.WebAdmin.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserApiClient _userApiClient;

        // Dùng config để lấy key và issuer trong appsettings.json
        private readonly IConfiguration _configuration;

        public LoginController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string cookie = Request.Cookies["userToken"]!;
            if (cookie != null)
            {
                var userPrincipal = ValidateToken(cookie);

                // tập properties của cookie
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1),
                    IsPersistent = true,
                    IssuedUtc = DateTimeOffset.UtcNow.AddMonths(1),
                };

                // Set key token trong session bằng token nhận được khi authenticate
                HttpContext.Session.SetString(Constants.TOKEN, cookie);


                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal,
                    authProperties);

                return RedirectToAction(Constants.DEFAULT_INDEX_PAGE, Constants.DEFAULT_HOME_CONTROLLER);
            }
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            /* Khi đăng nhập thành công thì chúng ta sẽ giả mã token này ra có những claim gì */

            // Nhận 1 token được mã hóa
            var result = await _userApiClient.Authenticate(request);

            if (result.ResultObj == null)
            {
                // Hiển thị thông báo Tài khoản không tồn tại
                ModelState.AddModelError("", result.Message);
                return View();
            }

            // Giải mã token đã mã hóa và lấy token, lấy cả các claim đã định nghĩa trong UserService
            // khi debug sẽ thấy nhận được gì ( có nhận được cả issuer )
            var userPrincipal = this.ValidateToken(result.ResultObj);

            if (userPrincipal.IsInRole(Constants.DEFAULT_VALUE_ADMIN) == false)
            {
                ModelState.AddModelError(nameof(request.UserName), Constants.ERR_NOT_PERMIT_TO_ACCESS_PAGE);
                return View(request);
            }

            if (request.RememberMe == true)
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Append("userToken", result.ResultObj, option);
            }            

            // tập properties của cookie
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMonths(1),
                IsPersistent = request.RememberMe,
                IssuedUtc = DateTimeOffset.UtcNow.AddMonths(1),
            };

            // Set key token trong session bằng token nhận được khi authenticate
            HttpContext.Session.SetString(Constants.TOKEN, result.ResultObj);


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                userPrincipal,
                authProperties);

            return RedirectToAction(Constants.DEFAULT_INDEX_PAGE, Constants.DEFAULT_HOME_CONTROLLER);
        }

        // Hàm giải mã token ( chứa thông tin về đăng nhập )
        private ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));


            // Giải mã thông tin claim mà ta đã gắn cho token ấy ( định nghĩa claim trong UserService.cs )
            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            // trả về một principal có token đã giải mã
            return principal;
        }
    }
}
