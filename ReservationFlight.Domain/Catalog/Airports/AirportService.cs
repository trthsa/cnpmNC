using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using ReservationFlight.Data.EF;
using ReservationFlight.Data.Entities;
using ReservationFlight.Data.Enums;
using ReservationFlight.Model.Catalog.Airports;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;

namespace ReservationFlight.Domain.Catalog.Airports
{
    public class AirportService : IAirportService
    {
        private readonly ReservationFlightDbContext _context;

        public AirportService(
            ReservationFlightDbContext context)
        {
            _context = context;
        }
        public async Task<string> Create(AirportCreateRequest request)
        {
            var airport = new Airport
            {
                Name = request.Name,
                IATA = request.IATA,
                Status = request.Status
            };

            _context.Airports.Add(airport);
            //trả về số lượng bản ghi
            await _context.SaveChangesAsync();
            return airport.IATA;
        }

        public async Task<int> Delete(string IATA)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(x => x.IATA == IATA);
            if (airport == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    IATA));

            _context.Airports.Remove(airport);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<AirportViewModel>> GetAll()
        {
            var query = from c in _context.Airports
                        select new { c };

            return await query.Select(x => new AirportViewModel()
            {
                Name = x.c.Name,
                IATA = x.c.IATA,
                Status = x.c.Status
            }).ToListAsync();
        }

        public async Task<ApiResult<PagedResult<AirportViewModel>>> GetAirportsPaging(GetAirportsPagingRequest request)
        {
            var query = from a in _context.Airports
                        select new { a };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.a.Name.Contains(request.Keyword)
                 || x.a.IATA.ToString().Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new AirportViewModel()
                {
                    Name = x.a.Name,
                    IATA = x.a.IATA,
                    Status = x.a.Status
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<AirportViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<AirportViewModel>>(pagedResult);
        }

        public async Task<AirportViewModel> GetById(string IATA)
        {
            var airport = await _context.Airports.FirstOrDefaultAsync(x => x.IATA == IATA);
            if (airport == null) throw new ReservationFlightException(string.Format(
                    Constants.ERR_NOT_EXIST,
                    IATA));
            else
            {
                var aviationViewModel = new AirportViewModel
                {
                    Name = airport.Name,
                    IATA = airport.IATA,
                    Status = airport.Status
                };
                return aviationViewModel;
            }
        }

        public async Task<int> Update(AirportUpdateRequest request)
        {
            var airport = _context.Airports.Where(x => x.IATA == request.IATA).FirstOrDefault();
            if (airport == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    request.Name));

            airport.Name = request.Name;
            airport.IATA = request.IATA;
            airport.Status = request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePatch(string IATA)
        {
            var airport = _context.Airports.FirstOrDefault(x => x.IATA == IATA);
            if (airport == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    IATA));

            var status = airport.Status;

            switch (status)
            {
                case 0:
                    airport.Status = 1;
                    break;

                case 1:
                    airport.Status = 0;
                    break;                   
            }

            return await _context.SaveChangesAsync();
        }
    }
}
