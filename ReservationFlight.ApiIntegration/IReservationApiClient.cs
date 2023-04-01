using ReservationFlight.Model.Catalog.Reservations;
using ReservationFlight.Model.Common;

namespace ReservationFlight.ApiIntegration
{
    public interface IReservationApiClient
    {
        Task<List<string>> CreateCustomer(string request);
        Task<bool> CreateReservationOneWay(List<ReservationCreateRequest> request);
        Task<bool> CreateReservationRoundTrip(List<ReservationCreateRequest> request);
        Task<ApiResult<PagedResult<ReservationViewModel>>> GetReservationsPaging(GetReservationPagingRequest request);
        Task<ReservationViewModel> GetReservationByCode(string reservationCode);
    }
}
