using Microsoft.EntityFrameworkCore;
using ReservationFlight.Data.EF;
using ReservationFlight.Data.Entities;
using ReservationFlight.Data.Enums;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;

namespace ReservationFlight.Domain.Catalog.FlightRoutes
{
    public class FlightRouteService : IFlightRouteService
    {
        private readonly ReservationFlightDbContext _context;
        public FlightRouteService(ReservationFlightDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(FlightRouteCreateRequest request)
        {
            var flightRoute = new FlightRoute
            {
                DepartureId = request.DepartureId,
                ArrivalId = request.ArrivalId,
                Status = request.Status,
            };
            _context.FlightRoutes.Add(flightRoute);
            await _context.SaveChangesAsync();
            return flightRoute.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var flightRoute = await _context.FlightRoutes.FirstOrDefaultAsync(x => x.Id == Id);
            if (flightRoute == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    Id));

            _context.FlightRoutes.Remove(flightRoute);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<FlightRouteViewModel>> GetAll()
        {
            var query = from fr in _context.FlightRoutes
                        join departure in _context.Airports on fr.DepartureId equals departure.IATA
                        join arrival in _context.Airports on fr.ArrivalId equals arrival.IATA
                        select new { fr, departure, arrival };

            return await query.Select(x => new FlightRouteViewModel()
            {
                Id = x.fr.Id,
                Departure = x.departure.Name,
                Arrival = x.arrival.Name,
                Status = x.fr.Status
            }).ToListAsync();
        }

        public async Task<FlightRouteViewModel> GetById(int Id)
        {
            var flightRoute = await _context.FlightRoutes.FirstOrDefaultAsync(x => x.Id == Id);
            if (flightRoute == null) throw new ReservationFlightException(string.Format(
                    Constants.ERR_NOT_EXIST,
                    Id));
            else
            {
                var flightRouteViewModel = new FlightRouteViewModel
                {
                    Id = flightRoute.Id,
                    Departure = flightRoute.DepartureId,
                    Arrival = flightRoute.ArrivalId,
                    Status = flightRoute.Status
                };
                return flightRouteViewModel;
            }
        }

        public async Task<ApiResult<PagedResult<FlightRouteViewModel>>> GetFlightRoutesPaging(GetFlightRoutesPagingRequest request)
        {
            var query = from fr in _context.FlightRoutes
                        join departure in _context.Airports on fr.DepartureId equals departure.IATA
                        join arrival in _context.Airports on fr.ArrivalId equals arrival.IATA
                        select new { fr, departure, arrival };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.fr.DepartureId.Contains(request.Keyword)
                 || x.fr.ArrivalId.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FlightRouteViewModel()
                {
                    Id = x.fr.Id,
                    Departure = x.departure.Name,
                    Arrival = x.arrival.Name,
                    Status = x.fr.Status
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<FlightRouteViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<FlightRouteViewModel>>(pagedResult);
        }

        public async Task<int> Update(FlightRouteUpdateRequest request)
        {
            var flightRoute = _context.FlightRoutes.Where(x => x.Id == request.Id).FirstOrDefault();
            if (flightRoute == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    request.Id));

            flightRoute.DepartureId = request.DepartureId;
            flightRoute.ArrivalId = request.ArrivalId;
            flightRoute.Status = request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePatch(int Id)
        {
            var flightRoute = _context.FlightRoutes.FirstOrDefault(x => x.Id == Id);
            if (flightRoute == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    Id));

            var status = flightRoute.Status;

            switch (status)
            {
                case (int)Status.InActive:
                    flightRoute.Status = 1;
                    break;

                case (int)Status.Active:
                    flightRoute.Status = 0;
                    break;
            }

            return await _context.SaveChangesAsync();
        }
    }
}
