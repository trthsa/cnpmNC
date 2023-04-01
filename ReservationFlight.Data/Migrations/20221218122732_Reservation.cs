using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationFlight.Data.Migrations
{
    public partial class Reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    IATA = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.IATA)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "AppConfigs",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfigs", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Aviations",
                columns: table => new
                {
                    AviationCode = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ImageAviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aviations", x => x.AviationCode)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    IdentityNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.IdentityNumber)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdFlightSchedule = table.Column<int>(type: "int", nullable: false),
                    TravelClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "FlightRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    ArrivalId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightRoutes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightRoutes_Airports_ArrivalId",
                        column: x => x.ArrivalId,
                        principalTable: "Airports",
                        principalColumn: "IATA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlightSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightRouteId = table.Column<int>(type: "int", nullable: false),
                    AviationId = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduledTimeDeparture = table.Column<TimeSpan>(type: "time", nullable: false),
                    ScheduledTimeArrival = table.Column<TimeSpan>(type: "time", nullable: false),
                    SeatEconomy = table.Column<int>(type: "int", nullable: false),
                    SeatBusiness = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightSchedules_Aviations_AviationId",
                        column: x => x.AviationId,
                        principalTable: "Aviations",
                        principalColumn: "AviationCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightSchedules_FlightRoutes_FlightRouteId",
                        column: x => x.FlightRouteId,
                        principalTable: "FlightRoutes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "IATA", "Name", "Status" },
                values: new object[,]
                {
                    { "HAN", "Hà Nội (HAN)", 1 },
                    { "HUI", "Huế (HUI)", 1 },
                    { "SGN", "Tp. Hồ Chí Minh (SGN)", 1 }
                });

            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "HomeDescription", "This is description of Reservation Flight" },
                    { "HomeKeyword", "This is keyword of Reservation Flight" },
                    { "HomeTitle", "This is home page of Reservation Flight" }
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "831436e4-ec04-41fd-8ac8-a18ce62e30b9", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), new Guid("69bd714f-9576-45ba-b5b7-f00649be00de") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, "123 Lien Ap 2-6 X.Vinh Loc A H. Binh Chanh", "d23fd84f-fc6d-4944-94d3-b6ee5319b4a3", "lequocanh.qa@gmail.com", true, false, null, "Quoc Anh", "lequocanh.qa@gmail.com", "admin", "AQAAAAEAACcQAAAAEAeYNTs2LlhUC+RsbQRsQWON/iJCgMtJQxHh7s/AS3pq1CB/PF0fvhcE1NR8CWyVQQ==", "0774642207", false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "Aviations",
                columns: new[] { "AviationCode", "ImageAviation", "Name", "Status" },
                values: new object[,]
                {
                    { "BAV", "bambooairways.png", "Bamboo Airways", 1 },
                    { "HVN", "vietnamairlines.png", "Vietnam Airlines", 1 },
                    { "VJC", "vietjetair.jpg", "Vietjet Air", 1 }
                });

            migrationBuilder.InsertData(
                table: "FlightRoutes",
                columns: new[] { "Id", "ArrivalId", "DepartureId", "Status" },
                values: new object[] { 1, "HAN", "SGN", 1 });

            migrationBuilder.InsertData(
                table: "FlightRoutes",
                columns: new[] { "Id", "ArrivalId", "DepartureId", "Status" },
                values: new object[] { 2, "HUI", "SGN", 1 });

            migrationBuilder.InsertData(
                table: "FlightRoutes",
                columns: new[] { "Id", "ArrivalId", "DepartureId", "Status" },
                values: new object[] { 3, "HAN", "HUI", 1 });

            migrationBuilder.InsertData(
                table: "FlightSchedules",
                columns: new[] { "Id", "AviationId", "Date", "FlightNumber", "FlightRouteId", "Price", "ScheduledTimeArrival", "ScheduledTimeDeparture", "SeatBusiness", "SeatEconomy" },
                values: new object[] { 1, "HVN", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "HVN0123", 1, 1300000m, new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 8, 30, 0, 0), 8, 160 });

            migrationBuilder.InsertData(
                table: "FlightSchedules",
                columns: new[] { "Id", "AviationId", "Date", "FlightNumber", "FlightRouteId", "Price", "ScheduledTimeArrival", "ScheduledTimeDeparture", "SeatBusiness", "SeatEconomy" },
                values: new object[] { 2, "VJC", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "VJC0124", 1, 1400000m, new TimeSpan(0, 10, 45, 0, 0), new TimeSpan(0, 8, 45, 0, 0), 8, 160 });

            migrationBuilder.InsertData(
                table: "FlightSchedules",
                columns: new[] { "Id", "AviationId", "Date", "FlightNumber", "FlightRouteId", "Price", "ScheduledTimeArrival", "ScheduledTimeDeparture", "SeatBusiness", "SeatEconomy" },
                values: new object[] { 3, "BAV", new DateTime(2022, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "BAV0125", 1, 1500000m, new TimeSpan(0, 11, 0, 0, 0), new TimeSpan(0, 9, 0, 0, 0), 8, 160 });

            migrationBuilder.CreateIndex(
                name: "IX_FlightRoutes_ArrivalId",
                table: "FlightRoutes",
                column: "ArrivalId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchedules_AviationId",
                table: "FlightSchedules",
                column: "AviationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightSchedules_FlightRouteId",
                table: "FlightSchedules",
                column: "FlightRouteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfigs");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "FlightSchedules");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Aviations");

            migrationBuilder.DropTable(
                name: "FlightRoutes");

            migrationBuilder.DropTable(
                name: "Airports");
        }
    }
}
