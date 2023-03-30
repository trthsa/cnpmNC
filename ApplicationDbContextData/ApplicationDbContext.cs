using BanVeFlightAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BanVeFlightAPI.ApplicationDbContextData
{

    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    var tableName = entityType.GetTableName();
            //    if (tableName.StartsWith("AspNet"))
            //    {
            //        entityType.SetTableName(tableName.Substring(6));
            //    }

            //}
        }

        public DbSet<MaybayModel> Maybays { get; set; }

 


    }
}
