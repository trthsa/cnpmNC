using ReservationFlight.Model.Common;
using ReservationFlight.Model.Systems.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.ApiIntegration
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<List<UserViewModel>> GetAll();
        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<string>> RegisterUser(RegisterRequest registerRequest);

        Task<ApiResult<bool>> UpdateUser(Guid id, UserUpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);
        Task<ApiResult<UserViewModel>> GetByUserName(string userName);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

        Task<ApiResult<bool>> ChangePassword(ChangePasswordViewModel model);

        Task<ApiResult<bool>> ConfirmEmail(ConfirmEmailViewModel model);

        Task<ApiResult<string>> ForgotPassword(ForgotPasswordViewModel model);

        Task<ApiResult<bool>> ResetPassword(ResetPasswordViewModel model);

        Task<bool> DisableAccount(Guid id);
    }
}
