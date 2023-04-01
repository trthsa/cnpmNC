using Microsoft.AspNetCore.JsonPatch;
using ReservationFlight.Model.Catalog.Airports;
using ReservationFlight.Model.Catalog.Aviations;
using ReservationFlight.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Domain.Catalog.Aviations
{
    public interface IAviationService
    {
        Task<List<AviationViewModel>> GetAll();
        Task<ApiResult<PagedResult<AviationViewModel>>> GetAviationsPaging(GetAviationsPagingRequest request);
        Task<string> Create(AviationCreateRequest request);
        Task<AviationViewModel> GetById(string aviationCode);
        Task<int> Update(AviationUpdateRequest request);
        Task<int> Delete(string aviationCode);
        Task<int> UpdatePatch(string aviationCode, JsonPatchDocument aviationModel);
    }
}
