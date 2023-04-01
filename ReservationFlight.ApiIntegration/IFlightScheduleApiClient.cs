using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.ApiIntegration
{
    public interface IFlightScheduleApiClient
    {
        Task<List<FlightScheduleViewModel>> GetAll();
        Task<ApiResult<PagedResult<FlightScheduleViewModel>>> GetFlightSchedulesPaging(GetFlightSchedulesPagingRequest request);
        Task<bool> Create(FlightScheduleCreateRequest request);
        Task<FlightScheduleViewModel> GetById(int Id);
        Task<bool> Update(FlightScheduleUpdateRequest request);
        Task<bool> Delete(int Id);
        Task<bool> UpdatePatch(int Id);
        Task<List<FlightViewModel>> GetAllFlightByCondition(FlightScheduleCondition request);

	}
}
