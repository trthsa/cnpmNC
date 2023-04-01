using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;

namespace ReservationFlight.Domain.Catalog.FlightRoutes
{
    public interface IFlightRouteService
    {
        Task<List<FlightRouteViewModel>> GetAll();
        Task<ApiResult<PagedResult<FlightRouteViewModel>>> GetFlightRoutesPaging(GetFlightRoutesPagingRequest request);
        Task<int> Create(FlightRouteCreateRequest request);
        Task<FlightRouteViewModel> GetById(int Id);
        Task<int> Update(FlightRouteUpdateRequest request);
        Task<int> Delete(int Id);
        Task<int> UpdatePatch(int Id);
    }
}
