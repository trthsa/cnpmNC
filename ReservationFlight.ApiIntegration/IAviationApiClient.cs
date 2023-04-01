using ReservationFlight.Model.Catalog.Aviations;
using ReservationFlight.Model.Common;

namespace ReservationFlight.ApiIntegration
{
    public interface IAviationApiClient
    {
        Task<List<AviationViewModel>> GetAll();
        Task<ApiResult<PagedResult<AviationViewModel>>> GetAviationsPaging(GetAviationsPagingRequest request);
        Task<bool> Create(AviationCreateRequest request);
        Task<AviationViewModel> GetById(string aviationCode);
        Task<bool> Update(AviationUpdateRequest request);
        Task<bool> Delete(string aviationCode);
        Task<bool> UpdatePatch(AviationUpdateRequest request);
    }
}
