using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.Order.History.Api.Migrations
{
    /// <inheritdoc />
    public partial class createtablesdefaultdata : Migration
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
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderPlaced = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(19,2)", nullable: true),
                    OrderHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MSOS_OrderHistory_OrderItem", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_MSOS_OrderHistory_OrderItem_MSOS_OrderHistory_OrderHistoryId",
                        column: x => x.OrderHistoryId,
                        principalTable: "MSOS_OrderHistory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MSOS_OrderHistory_OrderItem_OrderHistoryId",
                table: "MSOS_OrderHistory_OrderItem",
                column: "OrderHistoryId");
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
