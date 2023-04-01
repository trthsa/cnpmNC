using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Domain.Catalog.FlightSchedules
{
    public interface IFlightScheduleService
    {
        Task<List<FlightScheduleViewModel>> GetAll();
        Task<ApiResult<PagedResult<FlightScheduleViewModel>>> GetFlightSchedulesPaging(GetFlightSchedulesPagingRequest request);
        Task<int> Create(FlightScheduleCreateRequest request);
        Task<FlightScheduleViewModel> GetById(int Id);
        Task<int> Update(FlightScheduleUpdateRequest request);
        Task<int> Delete(int Id);
        Task<int> UpdatePatch(int Id);
        Task<List<FlightViewModel>> GetAllFlightByCondition(FlightScheduleCondition request);
    }
}
