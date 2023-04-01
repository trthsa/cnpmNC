using Microsoft.AspNetCore.JsonPatch;
using ReservationFlight.Model.Catalog.Airports;
using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Domain.Catalog.Airports
{
    public interface IAirportService
    {
        Task<List<AirportViewModel>> GetAll();
        Task<ApiResult<PagedResult<AirportViewModel>>> GetAirportsPaging(GetAirportsPagingRequest request);
        Task<string> Create(AirportCreateRequest request);
        Task<AirportViewModel> GetById(string IATA);
        Task<int> Update(AirportUpdateRequest request);
        Task<int> Delete(string IATA);
        Task<int> UpdatePatch(string IATA);
    }
}
