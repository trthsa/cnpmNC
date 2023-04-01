using ReservationFlight.Model.Catalog.Airports;
using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.ApiIntegration
{
    public interface IAirportApiClient
    {
        Task<List<AirportViewModel>> GetAll();
        Task<ApiResult<PagedResult<AirportViewModel>>> GetAirportsPaging(GetAirportsPagingRequest request);
        Task<bool> Create(AirportCreateRequest request);
        Task<AirportViewModel> GetById(string IATA);
        Task<bool> Update(AirportUpdateRequest request);
        Task<bool> Delete(string IATA);
        Task<bool> UpdatePatch(string IATA);
    }
}
