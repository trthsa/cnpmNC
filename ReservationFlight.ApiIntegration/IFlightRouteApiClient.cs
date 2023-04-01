using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;

namespace ReservationFlight.ApiIntegration
{
    public interface IFlightRouteApiClient
    {
        Task<List<FlightRouteViewModel>> GetAll();
        Task<ApiResult<PagedResult<FlightRouteViewModel>>> GetFlightRoutesPaging(GetFlightRoutesPagingRequest request);
        Task<bool> Create(FlightRouteCreateRequest request);
        Task<FlightRouteViewModel> GetById(int Id);
        Task<bool> Update(FlightRouteUpdateRequest request);
        Task<bool> Delete(int Id);
        Task<bool> UpdatePatch(int Id);
    }
}
