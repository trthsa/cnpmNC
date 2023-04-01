using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReservationFlight.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReservationFlight.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
               new AppConfig() { Key = "HomeTitle", Value = "This is home page of Reservation Flight" },
               new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of Reservation Flight" },
               new AppConfig() { Key = "HomeDescription", Value = "This is description of Reservation Flight" }
               );

            // any guid
            var roleId = new Guid("8D04DCE2-969A-435D-BBA4-DF3F325983DC");
            var adminId = new Guid("69BD714F-9576-45BA-B5B7-F00649BE00DE");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "lequocanh.qa@gmail.com",
                NormalizedEmail = "lequocanh.qa@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abcd1234"),
                SecurityStamp = string.Empty,
                PhoneNumber = "0774642207",
                Address = "123 Lien Ap 2-6 X.Vinh Loc A H. Binh Chanh",
                Name = "Quoc Anh",
            });

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });

            #region Seed Aviation
            modelBuilder.Entity<Aviation>().HasData(
                new Aviation()
                {
                    AviationCode = "HVN",
                    Name = "Vietnam Airlines",
                    ImageAviation = "vietnamairlines.png",
                    Status = (int)Enums.Status.Active
                },

                new Aviation()
                {
                    AviationCode = "VJC",
                    Name = "Vietjet Air",
                    ImageAviation = "vietjetair.jpg",
                    Status = (int)Enums.Status.Active
                },

                new Aviation()
                {
                    AviationCode = "BAV",
                    Name = "Bamboo Airways",
                    ImageAviation = "bambooairways.png",
                    Status = (int)Enums.Status.Active
                }
              );
            #endregion

            #region Seed Airport
            modelBuilder.Entity<Airport>().HasData(
                new Airport
                {
                    IATA = "SGN",
                    Name = "Tp. Hồ Chí Minh (SGN)",
                    Status = (int)Enums.Status.Active
                },
                new Airport
                {
                    IATA = "HAN",
                    Name = "Hà Nội (HAN)",
                    Status = (int)Enums.Status.Active
                },
                new Airport
                {
                    IATA = "HUI",
                    Name = "Huế (HUI)",
                    Status = (int)Enums.Status.Active
                });
            #endregion

            #region Seed FlightRoute
            modelBuilder.Entity<FlightRoute>().HasData(
                new FlightRoute
                {
                    Id = 1,                    
                    DepartureId = "SGN",
                    ArrivalId = "HAN",
                    Status = (int)Enums.Status.Active
                },
                new FlightRoute
                {
                    Id = 2,
                    DepartureId = "SGN",
                    ArrivalId = "HUI",
                    Status = (int)Enums.Status.Active
                },
                new FlightRoute
                {
                    Id = 3,
                    DepartureId = "HUI",
                    ArrivalId = "HAN",
                    Status = (int)Enums.Status.Active
                });
            #endregion

            #region Seed FlightSchedule
            modelBuilder.Entity<FlightSchedule>().HasData(
                new FlightSchedule
                {
                    Id = 1,
                    FlightRouteId = 1,
                    AviationId = "HVN",
                    FlightNumber = "HVN0123",
                    Price = 1300000,
                    Date = new DateTime(2022, 11, 23),
                    ScheduledTimeDeparture = new TimeSpan(8, 30, 00),
                    ScheduledTimeArrival = new TimeSpan(10, 30, 00),
                    SeatBusiness = 8,
                    SeatEconomy = 160
                },
                new FlightSchedule
                {
                    Id = 2,
                    FlightRouteId = 1,
                    AviationId = "VJC",
                    FlightNumber = "VJC0124",
                    Price = 1400000,
                    Date = new DateTime(2022, 11, 23),
                    ScheduledTimeDeparture = new TimeSpan(8, 45, 00),
                    ScheduledTimeArrival = new TimeSpan(10, 45, 00),
                    SeatBusiness = 8,
                    SeatEconomy = 160
                },
                new FlightSchedule
                {
                    Id = 3,
                    FlightRouteId = 1,
                    AviationId = "BAV",
                    FlightNumber = "BAV0125",
                    Price = 1500000,
                    Date = new DateTime(2022, 11, 23),
                    ScheduledTimeDeparture = new TimeSpan(9, 00, 00),
                    ScheduledTimeArrival = new TimeSpan(11, 00, 00),
                    SeatBusiness = 8,
                    SeatEconomy = 160
                });
            #endregion
        }
    }
}
