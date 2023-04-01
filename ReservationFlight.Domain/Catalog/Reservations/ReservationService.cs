using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReservationFlight.Data.EF;
using ReservationFlight.Data.Entities;
using ReservationFlight.Model.Catalog.Customers;
using ReservationFlight.Model.Catalog.Reservations;
using ReservationFlight.Model.Common;
using ReservationFlight.Utility;

namespace ReservationFlight.Domain.Catalog.Reservations
{
    public class ReservationService : IReservationService
    {
        private readonly ReservationFlightDbContext _context;
        public ReservationService(
            ReservationFlightDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }

        public async Task<List<string>> CreateInformationCustomer(List<CustomerCreateRequest> request)
        {
            var listCustomer = new List<string>();
            for (var i = 0; i < request.Count; i++)
            {
                var detailCustomer = await _context.Customers.FirstOrDefaultAsync(x => x.IdentityNumber == request[i].IdentityNumber);
                if (detailCustomer != null)
                {
                    listCustomer.Add(detailCustomer.IdentityNumber);
                    continue;
                }

                var customer = new Customer
                {
                    FirstName = request[i].FirstName,
                    LastName = request[i].LastName,
                    BirthDate = request[i].BirthDate,
                    IdentityNumber = request[i].IdentityNumber,
                    Sex = request[i].Sex,
                    Email = request[i].Email,
                    PhoneNumber = request[i].PhoneNumber
                };

                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                listCustomer.Add(customer.IdentityNumber);
            }
            return listCustomer;
        }

        public async Task<string> CreateReservationOneWay(List<ReservationCreateRequest> informationOneWay)
        {
            var codeReservationOneWay = string.Empty;
            foreach (var inform in informationOneWay)
            {
                var reservationOneWay = new Reservation
                {
                    ReservationCode = inform.ReservationCode,
                    IdFlightSchedule = inform.IdFlightSchedule,
                    IdentityNumber = inform.IdentityNumber,
                    Price = inform.Price,
                    Status = inform.Status,
                    TravelClass = "Economy"
                };
                _context.Reservations.Add(reservationOneWay);
                await _context.SaveChangesAsync();
                codeReservationOneWay = reservationOneWay.ReservationCode;
            } 
            return codeReservationOneWay;
        }

        public async Task<string> CreateReservationRoundTrip(List<ReservationCreateRequest> informationRoundTrip)
        {
            var codeReservationRoundTrip = string.Empty;
            foreach (var inform in informationRoundTrip)
            {
                var reservationRoundTrip = new Reservation
                {
                    ReservationCode = inform.ReservationCode,
                    IdFlightSchedule = inform.IdFlightSchedule,
                    IdentityNumber = inform.IdentityNumber,
                    Price = inform.Price,
                    Status = inform.Status,
                    TravelClass = "Economy"
                };
                _context.Reservations.Add(reservationRoundTrip);
                await _context.SaveChangesAsync();
                codeReservationRoundTrip = reservationRoundTrip.ReservationCode;
            }          
            return codeReservationRoundTrip;
        }

        public async Task<ReservationViewModel> GetReservationByCode(string reservationCode)
        {
            var reservation = await _context.Reservations.FirstOrDefaultAsync(x => x.ReservationCode == reservationCode);
            if (reservation == null) throw new ReservationFlightException(string.Format(
                    Constants.ERR_NOT_EXIST,
                    reservationCode));

            var reservationViewModel = new ReservationViewModel
            {
                ReservationCode = reservation.ReservationCode,
                IdFlightSchedule = reservation.IdFlightSchedule,
                TravelClass = reservation.TravelClass,
                IdentityNumber = reservation.IdentityNumber,
                Price = reservation.Price,
                Status = reservation.Status
            };
            return reservationViewModel;
        }

        public async Task<CustomerViewModel> GetCustomerById(string customerId)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.IdentityNumber == customerId);
            if (customer == null) throw new ReservationFlightException(string.Format(
                    Constants.ERR_NOT_EXIST,
                    customerId));
            var customerViewModel = new CustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                BirthDate = customer.BirthDate,
                IdentityNumber = customer.IdentityNumber,
                Sex = customer.Sex,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber
            };
            return customerViewModel;
        }

        public async Task<ApiResult<PagedResult<ReservationViewModel>>> GetReservationsPaging(GetReservationPagingRequest request)
        {
            var query = from r in _context.Reservations
                        select new { r };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.r.ReservationCode.Contains(request.Keyword)
                 || x.r.IdentityNumber.Contains(request.Keyword));
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ReservationViewModel()
                {
                    ReservationCode = x.r.ReservationCode,
                    IdFlightSchedule = x.r.IdFlightSchedule,
                    IdentityNumber = x.r.IdentityNumber,
                    Price = x.r.Price,
                    TravelClass = x.r.TravelClass,
                    Status = x.r.Status,
                }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ReservationViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<ReservationViewModel>>(pagedResult);
        }
    }
}
