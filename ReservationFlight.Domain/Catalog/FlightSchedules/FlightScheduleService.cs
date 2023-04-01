using Microsoft.EntityFrameworkCore;
using ReservationFlight.Data.EF;
using ReservationFlight.Data.Entities;
using ReservationFlight.Data.Enums;
using ReservationFlight.Model.Catalog.Flights;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationFlight.Domain.Catalog.FlightSchedules
{
    public class FlightScheduleService : IFlightScheduleService
    {
        private readonly ReservationFlightDbContext _context;
        public FlightScheduleService(ReservationFlightDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(FlightScheduleCreateRequest request)
        {
            var flightSchedule = new FlightSchedule
            {
                FlightRouteId = request.FlightRouteId,
                AviationId = request.AviationId,
                FlightNumber = request.FlightNumber,
                Price = request.Price,
                Date = request.Date,
                ScheduledTimeDeparture = request.ScheduledTimeDeparture,
                ScheduledTimeArrival = request.ScheduledTimeArrival,
                SeatEconomy = request.SeatEconomy,
                SeatBusiness= request.SeatBusiness,
                Status = request.Status,
            };
            _context.FlightSchedules.Add(flightSchedule);
            await _context.SaveChangesAsync();
            return flightSchedule.Id;
        }

        public async Task<int> Delete(int Id)
        {
            var flightSchedule = await _context.FlightSchedules.FirstOrDefaultAsync(x => x.Id == Id);
            if (flightSchedule == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    Id));

            _context.FlightSchedules.Remove(flightSchedule);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<FlightScheduleViewModel>> GetAll()
        {
            var query = from fs in _context.FlightSchedules
                        join av in _context.Aviations on fs.AviationId equals av.AviationCode
                        join fr in _context.FlightRoutes on fs.FlightRouteId equals fr.Id
                        select new { fs, av, fr };

            return await query.Select(x => new FlightScheduleViewModel()
            {
                Id = x.fs.Id,
                FlightRouteId = x.fs.FlightRouteId.ToString(),
                AviationId = x.av.Name,
                FlightNumber = x.fs.FlightNumber,
                Price = x.fs.Price,
                Date = x.fs.Date,
                ScheduledTimeDeparture = x.fs.ScheduledTimeDeparture,
                ScheduledTimeArrival = x.fs.ScheduledTimeArrival,
                SeatEconomy = x.fs.SeatEconomy,
                SeatBusiness = x.fs.SeatBusiness,
                Status = x.fs.Status,
            }).ToListAsync();
        }

		public async Task<List<FlightViewModel>> GetAllFlightByCondition(FlightScheduleCondition request)
		{
			var query = from fs in _context.FlightSchedules
						join av in _context.Aviations on fs.AviationId equals av.AviationCode
						join fr in _context.FlightRoutes on fs.FlightRouteId equals fr.Id
                        where fr.DepartureId.Equals(request.DepartureId)
                        && fr.ArrivalId.Equals(request.ArrivalId)
                        && fs.Date.Equals(request.Date)
						select new { fs, av, fr };

			return await query.Select(x => new FlightViewModel()
			{
				Id = x.fs.Id,
				DepartureId = x.fr.DepartureId, 
                ArrivalId = x.fr.ArrivalId,
				AviationId = x.av.Name,
				FlightNumber = x.fs.FlightNumber,
				Price = x.fs.Price,
				Date = x.fs.Date,
				ScheduledTimeDeparture = x.fs.ScheduledTimeDeparture,
				ScheduledTimeArrival = x.fs.ScheduledTimeArrival,
				SeatEconomy = x.fs.SeatEconomy,
				SeatBusiness = x.fs.SeatBusiness,
				Status = x.fs.Status,
			}).ToListAsync();
		}

		public async Task<FlightScheduleViewModel> GetById(int Id)
        {
            var flightSchedule = await _context.FlightSchedules.FirstOrDefaultAsync(x => x.Id == Id);
            var flightRoute = await _context.FlightRoutes.FirstOrDefaultAsync(x => x.Id == flightSchedule.FlightRouteId);
            if (flightSchedule == null) throw new ReservationFlightException(string.Format(
                    Constants.ERR_NOT_EXIST,
                    Id));
            else
            {
                var flightRouteViewModel = new FlightScheduleViewModel
                {
                    Id = flightSchedule.Id,
                    FlightRouteId = flightSchedule.FlightRouteId.ToString(),
                    AviationId = flightSchedule.AviationId,
                    FlightNumber = flightSchedule.FlightNumber,
                    DepartureId = flightRoute.DepartureId,
                    ArrivalId = flightRoute.ArrivalId,
                    Price = flightSchedule.Price,
                    Date = flightSchedule.Date,
                    ScheduledTimeDeparture = flightSchedule.ScheduledTimeDeparture,
                    ScheduledTimeArrival = flightSchedule.ScheduledTimeArrival,
                    SeatEconomy = flightSchedule.SeatEconomy,
                    SeatBusiness = flightSchedule.SeatBusiness,
                    Status = flightSchedule.Status,
                };
                return flightRouteViewModel;
            }
        }

        public async Task<ApiResult<PagedResult<FlightScheduleViewModel>>> GetFlightSchedulesPaging(GetFlightSchedulesPagingRequest request)
        {
            var query = from fs in _context.FlightSchedules
                        join av in _context.Aviations on fs.AviationId equals av.AviationCode
                        join fr in _context.FlightRoutes on fs.FlightRouteId equals fr.Id
                        select new { fs, av, fr };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.fr.DepartureId.Contains(request.Keyword)
                 || x.fr.ArrivalId.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new FlightScheduleViewModel()
                {
                    Id = x.fs.Id,
                    FlightRouteId = string.Concat(x.fr.DepartureId, '-', x.fr.ArrivalId),
                    AviationId = x.av.Name,
                    FlightNumber = x.fs.FlightNumber,
                    Price = x.fs.Price,
                    Date = x.fs.Date,
                    ScheduledTimeDeparture = x.fs.ScheduledTimeDeparture,
                    ScheduledTimeArrival = x.fs.ScheduledTimeArrival,
                    SeatEconomy = x.fs.SeatEconomy,
                    SeatBusiness = x.fs.SeatBusiness,
                    Status = x.fs.Status,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<FlightScheduleViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<FlightScheduleViewModel>>(pagedResult);
        }

        public async Task<int> Update(FlightScheduleUpdateRequest request)
        {
            var flightSchedule = _context.FlightSchedules.Where(x => x.Id == request.Id).FirstOrDefault();
            if (flightSchedule == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    request.Id));

            flightSchedule.FlightRouteId = request.FlightRouteId;
            flightSchedule.AviationId = request.AviationId;
            flightSchedule.FlightNumber = request.FlightNumber;
            flightSchedule.Price = request.Price;
            flightSchedule.Date = request.Date;
            flightSchedule.ScheduledTimeDeparture = request.ScheduledTimeDeparture;
            flightSchedule.ScheduledTimeArrival = request.ScheduledTimeArrival;
            flightSchedule.SeatEconomy = request.SeatEconomy;
            flightSchedule.SeatBusiness = request.SeatBusiness;
            flightSchedule.Status = request.Status;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdatePatch(int Id)
        {
            var flightSchedule = _context.FlightSchedules.FirstOrDefault(x => x.Id == Id);
            if (flightSchedule == null) throw new ReservationFlightException(
                string.Format(
                    Constants.ERR_NOT_EXIST,
                    Id));

            var status = flightSchedule.Status;

            switch (status)
            {
                case (int)Status.InActive:
                    flightSchedule.Status = 1;
                    break;

                case (int)Status.Active:
                    flightSchedule.Status = 0;
                    break;
            }

            return await _context.SaveChangesAsync();
        }
    }
}
