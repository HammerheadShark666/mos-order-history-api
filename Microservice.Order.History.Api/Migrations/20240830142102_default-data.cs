using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Microservice.Order.History.Api.Migrations
{
    /// <inheritdoc />
    public partial class defaultdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MSOS_OrderHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressForename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderPlaced = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(19,2)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressLine2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AddressLine3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TownCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    County = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MSOS_OrderHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MSOS_OrderHistory_OrderItem",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(19,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MSOS_OrderHistory_OrderItem", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_MSOS_OrderHistory_OrderItem_MSOS_OrderHistory_OrderId",
                        column: x => x.OrderId,
                        principalTable: "MSOS_OrderHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MSOS_OrderHistory",
                columns: new[] { "Id", "AddressForename", "AddressLine1", "AddressLine2", "AddressLine3", "AddressSurname", "Country", "County", "Created", "CustomerId", "OrderNumber", "OrderPlaced", "OrderStatus", "Postcode", "Total", "TownCity" },
                values: new object[] { new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"), "Test_Forename", "AddressLine1", "AddressLine2", "AddressLine3", "Test_Surname", "England", "West Yorkshire", new DateTime(2024, 8, 30, 15, 21, 2, 212, DateTimeKind.Local).AddTicks(1031), new Guid("6c84d0a3-0c0c-435f-9ae0-4de09247ee15"), "000000001", new DateOnly(2024, 8, 30), "Completed", "HD6 TRF4", 19.98m, "Leeds" });

            migrationBuilder.InsertData(
                table: "MSOS_OrderHistory_OrderItem",
                columns: new[] { "OrderId", "ProductId", "Name", "ProductType", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"), new Guid("29a75938-ce2d-473b-b7fe-2903fe97fd6e"), "Infinity Kings", "Book", 1, 9.99m },
                    { new Guid("24331f31-a2cd-4ff4-8db6-c93d124e4483"), new Guid("37544155-da95-49e8-b7fe-3c937eb1de98"), "Wild Love", "Book", 1, 9.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MSOS_OrderHistory_OrderItem");

            migrationBuilder.DropTable(
                name: "MSOS_OrderHistory");
        }
    }
}
