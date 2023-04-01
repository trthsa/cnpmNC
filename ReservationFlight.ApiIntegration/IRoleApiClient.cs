using ReservationFlight.Model.Common;
using ReservationFlight.Model.Systems.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationFlight.ApiIntegration
{
    public interface IRoleApiClient
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
