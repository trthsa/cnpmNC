using ReservationFlight.Model.Common;

namespace ReservationFlight.Model.Systems.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
