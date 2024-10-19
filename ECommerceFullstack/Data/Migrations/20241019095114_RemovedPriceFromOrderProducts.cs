using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceFullstack.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPriceFromOrderProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderProducts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
