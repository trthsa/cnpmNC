using ReservationFlight.Model.Catalog.Customers;
using ReservationFlight.Model.Catalog.Reservations;
using ReservationFlight.Model.Common;

namespace ReservationFlight.Domain.Catalog.Reservations
{
    public interface IReservationService
    {
        Task<ReservationViewModel> GetReservationByCode(string reservationCode);
        Task<CustomerViewModel> GetCustomerById(string customerId);
        Task<List<string>> CreateInformationCustomer(List<CustomerCreateRequest> request);
        Task<string> CreateReservationOneWay(List<ReservationCreateRequest> informationOneWay);
        Task<string> CreateReservationRoundTrip(List<ReservationCreateRequest> informationRoundTrip);
        Task<ApiResult<PagedResult<ReservationViewModel>>> GetReservationsPaging(GetReservationPagingRequest request);
    }
}
