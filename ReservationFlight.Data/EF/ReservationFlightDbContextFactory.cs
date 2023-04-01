using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ReservationFlight.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ReservationFlight.Data.EF
{
    public class ReservationFlightDbContextFactory : IDesignTimeDbContextFactory<ReservationFlightDbContext>
    {
        public ReservationFlightDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(Constants.CONNECTION_STRING);

            var optionsBuilder = new DbContextOptionsBuilder<ReservationFlightDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ReservationFlightDbContext(optionsBuilder.Options);
        }
    }
}
